Imports System.IO
Imports System.Xml
Imports MSSQLA.UserInterface.ClassUtils
Imports MSSQLA.BusinessLogicLayer
Imports FontAwesome.Sharp
Imports System.Runtime.InteropServices

Public Class MainForm
    Private ReadOnly Property DatabaseLogic As New DatabaseLogic
    Private ReadOnly Property FileLogic As New FileLogic
    Private Property GlobalTableCounter As Integer
    Private Property GlobalNewEditorCounter As Integer = 1

    ' Flags that avoid triggering events recursively
    Private _ignoreTabControlSelectedEvent As Boolean = False

#Region "Form Initialization"

    Public Sub New()
        InitializeComponent()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
    End Sub

    Private Const HTLEFT As Integer = 10, HTRIGHT As Integer = 11, HTTOP As Integer = 12, HTTOPLEFT As Integer = 13, HTTOPRIGHT As Integer = 14,
        HTBOTTOM As Integer = 15, HTBOTTOMLEFT As Integer = 16, HTBOTTOMRIGHT As Integer = 17, CS_DROPSHADOW As Integer = 131072

    Const RESIZE_OFFSET As Integer = 10

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    Private Overloads ReadOnly Property Top As Rectangle
        Get
            Return New Rectangle(0, 0, Me.ClientSize.Width, RESIZE_OFFSET)
        End Get
    End Property

    Private Overloads ReadOnly Property Left As Rectangle
        Get
            Return New Rectangle(0, 0, RESIZE_OFFSET, Me.ClientSize.Height)
        End Get
    End Property

    Private Overloads ReadOnly Property Bottom As Rectangle
        Get
            Return New Rectangle(0, Me.ClientSize.Height - RESIZE_OFFSET, Me.ClientSize.Width, RESIZE_OFFSET)
        End Get
    End Property

    Private Overloads ReadOnly Property Right As Rectangle
        Get
            Return New Rectangle(Me.ClientSize.Width - RESIZE_OFFSET, 0, RESIZE_OFFSET, Me.ClientSize.Height)
        End Get
    End Property

    Private ReadOnly Property TopLeft As Rectangle
        Get
            Return New Rectangle(0, 0, RESIZE_OFFSET, RESIZE_OFFSET)
        End Get
    End Property

    Private ReadOnly Property TopRight As Rectangle
        Get
            Return New Rectangle(Me.ClientSize.Width - RESIZE_OFFSET, 0, RESIZE_OFFSET, RESIZE_OFFSET)
        End Get
    End Property

    Private ReadOnly Property BottomLeft As Rectangle
        Get
            Return New Rectangle(0, Me.ClientSize.Height - RESIZE_OFFSET, RESIZE_OFFSET, RESIZE_OFFSET)
        End Get
    End Property

    Private ReadOnly Property BottomRight As Rectangle
        Get
            Return New Rectangle(Me.ClientSize.Width - RESIZE_OFFSET, Me.ClientSize.Height - RESIZE_OFFSET, RESIZE_OFFSET, RESIZE_OFFSET)
        End Get
    End Property

    Protected Overrides Sub WndProc(ByRef message As Message)
        MyBase.WndProc(message)

        If message.Msg = &H84 Then
            Dim cursorPosition = Me.PointToClient(Cursor.Position)

            If TopLeft.Contains(cursorPosition) Then
                message.Result = CType(HTTOPLEFT, IntPtr)
            ElseIf TopRight.Contains(cursorPosition) Then
                message.Result = CType(HTTOPRIGHT, IntPtr)
            ElseIf BottomLeft.Contains(cursorPosition) Then
                message.Result = CType(HTBOTTOMLEFT, IntPtr)
            ElseIf BottomRight.Contains(cursorPosition) Then
                message.Result = CType(HTBOTTOMRIGHT, IntPtr)
            ElseIf Top.Contains(cursorPosition) Then
                message.Result = CType(HTTOP, IntPtr)
            ElseIf Left.Contains(cursorPosition) Then
                message.Result = CType(HTLEFT, IntPtr)
            ElseIf Right.Contains(cursorPosition) Then
                message.Result = CType(HTRIGHT, IntPtr)
            ElseIf Bottom.Contains(cursorPosition) Then
                message.Result = CType(HTBOTTOM, IntPtr)
            End If
        End If
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        LoadConnectionSettings()

        For Each control In leftPanel.Controls
            If TypeOf control Is IconButton Then
                SwitchIconButtonColor(control)
            End If
        Next

        MyMenuStrip.Renderer = New CustomToolStripProfessionalRenderer(New MenuColorTable())
        MyToolStrip.RenderMode = ToolStripRenderMode.System
        MyToolStrip.Renderer = New CustomToolStripSystemRenderer()
        DatabaseMenuStrip.Renderer = New CustomToolStripProfessionalRenderer(New MenuColorTable())
        TablesAndViewsMenuStrip.Renderer = New CustomToolStripProfessionalRenderer(New MenuColorTable())
        ProceduresMenuStrip.Renderer = New CustomToolStripProfessionalRenderer(New MenuColorTable())

        TablesTabControl.ItemSize = New Size(0, 32)
        TablesTabControl.SizeMode = TabSizeMode.Normal
        TablesTabControl.DrawMode = DrawMode.OwnerDrawFixed

        EditorsTabControl.ItemSize = New Size(0, 32)
        EditorsTabControl.SizeMode = TabSizeMode.Normal
        EditorsTabControl.DrawMode = DrawMode.OwnerDrawFixed

        AddHandler UserEditor.TextModified, AddressOf CurrentEditor_TextModified

        Log("Welcome " & Environment.UserName & ".")

        MyBase.OnLoad(e)
    End Sub

    ''' <summary>
    ''' Loads the last credentials used.
    ''' </summary>
    Private Sub LoadConnectionSettings()
        tbServer.Text = My.Settings.Server
        tbUser.Text = My.Settings.User
        tbPass.Text = My.Settings.Pass
        tbTimeout.Text = My.Settings.Timeout
    End Sub

#End Region

#Region "Form Functionality"

    ''' <summary>
    ''' Writes on the console.
    ''' </summary>
    ''' <param name="str">Message to log.</param>
    Private Sub Log(str As String)
        outputArea.AppendText("> " & DateTime.Now.TimeOfDay.ToString("hh\:mm\:ss") & " " & str & vbNewLine)
    End Sub

    ''' <summary>
    ''' Removes all tables and sets the current one to null.
    ''' </summary>
    Private Sub ClearTables()
        GlobalTableCounter = 0
        CurrentTable = Nothing
        TablesTabControl.Clear()

        btnSubmit.Enabled = False
    End Sub

    ''' <summary>
    ''' Removes all editors and sets the current one to null.
    ''' </summary>
    Private Sub ClearEditors()
        GlobalNewEditorCounter = 1
        CurrentEditor = Nothing
        EditorsTabControl.Clear()
    End Sub

    ''' <summary>
    ''' Adds a new tab to the TabControl.
    ''' </summary>
    ''' <param name="dt">DataTable to be added in that tab.</param>
    ''' <param name="tableName">Name of the table.</param>
    ''' <param name="databaseName">Name of the database which the table belongs to.</param>
    ''' <param name="canBeUpdated">Used to indicate wether the table can be updated from the DGV or not.</param>
    Private Sub AddNewTable(dt As DataTable, tableName As String, databaseName As String, canBeUpdated As Boolean)
        Dim tabNames = From tabs In TablesTabControl.TabPages.Cast(Of TabPage)
                       Select tabs.Name
        Dim tabSymbol As String = IIf(canBeUpdated, "✎", "👁") & " "

        _ignoreTabControlSelectedEvent = True
        If Not tabNames.Contains(tableName) Then
            Dim tab As New TabPage() With {
                .Text = tabSymbol & tableName,
                .Name = tableName
            }
            AddControlToTab(tab, New UserTable(dt, canBeUpdated, tableName, databaseName))

            TablesTabControl.AddTab(tab)
            TablesTabControl.SelectTab(tab)
        Else
            TablesTabControl.SelectTab(tableName)
            Dim tab = TablesTabControl.SelectedTab
            AddControlToTab(tab, New UserTable(dt, canBeUpdated, tableName, databaseName))
        End If

        _ignoreTabControlSelectedEvent = False
    End Sub

    ''' <summary>
    ''' Adds a new editor without path or filename or text.
    ''' </summary>
    Private Sub AddNewEditor(Optional text As String = "")
        Dim tab As New TabPage() With {
            .Text = "New " & GlobalNewEditorCounter,
            .Name = "New " & GlobalNewEditorCounter
        }
        Dim userEditor As New UserEditor(text)
        AddControlToTab(tab, userEditor)

        EditorsTabControl.AddTab(tab)
        EditorsTabControl.SelectTab(tab)

        userEditor.Scintilla.Select()
    End Sub

    ''' <summary>
    ''' Adds a new editor with path, filename and text.
    ''' </summary>
    ''' <param name="text">Initial text of the editor.</param>
    ''' <param name="filePath">Absolute path to the file is being edited.</param>
    Private Sub AddNewEditor(text As String, filePath As String)
        Dim fileName = Path.GetFileName(filePath)
        Dim tab As New TabPage() With {
            .Text = fileName,
            .Name = fileName
        }

        Dim userEditor As New UserEditor(text, filePath, fileName)
        AddControlToTab(tab, userEditor)

        EditorsTabControl.AddTab(tab)
        EditorsTabControl.SelectTab(tab)

        userEditor.Scintilla.Select()
    End Sub

    ''' <summary>
    ''' Updates all tabs if their tables got any changes made by the user after a manual insert, update or delete.
    ''' </summary>
    Private Sub UpdateAllTables()
        For Each tab As TabPage In TablesTabControl.TabPages
            Try
                Dim userTable As UserTable = CType(tab.Controls(0), UserTable)

                If userTable.CanBeUpdated Then
                    userTable.UpdateTable()
                End If

                If TablesTabControl.SelectedTab Is tab Then
                    SetCurrentTable(userTable)
                    btnSubmit.Enabled = userTable.CanBeUpdated
                End If
            Catch ex As Exception
                Continue For
            End Try
        Next
    End Sub

    Private Sub OpenDesignMode(alter As Boolean, Optional tableName As String = Nothing)
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        If IsNothing(databaseName) Then Return

        If Not String.IsNullOrEmpty(tableName) Then
            Using form As New TableDesignForm(tableName, databaseName, alter)
                Dim result = form.ShowDialog()

                If result = DialogResult.OK Then
                    UpdateAllTables()
                End If
            End Using
        Else
            Using form As New TableDesignForm(databaseName, alter)
                Dim result = form.ShowDialog()

                If result = DialogResult.OK Then
                    FillObjectExplorer()
                End If
            End Using
        End If
    End Sub

    Public Sub OpenBackupForm()
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        If IsNothing(databaseName) Then Return

        Using form As New DatabaseBackupForm(databaseName)
            Dim result = form.ShowDialog()

            If result = DialogResult.OK Then
                MessageBox.Show(databaseName & " backed up on " & form.Path, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf result = DialogResult.Abort Then
                MessageBox.Show(form.ErrorMessage, "MSSQL Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Closes the current sesion and forces the user to connect again to the server.
    ''' </summary>
    Private Sub CloseSession()
        ClearTables()
        cbDatabases.Items.Clear()
        tvObjectExplorer.Nodes.Clear()
        btnDisconnect.Enabled = False
        btnConnect.Enabled = True
        btnExecute.Enabled = False
        btnReload.Enabled = False
        cbDatabases.Enabled = False
        lblConnStatus.Text = "Disconnected"
        lblConnStatus.ForeColor = Color.IndianRed

        Log("Connection closed.")
    End Sub

    ''' <summary>
    ''' Switches Icon's color depending if the button is enabled or not.
    ''' <param name="button">Button to check.</param>
    ''' <param name="isExecuteButton">Optional parameter to check if the given button is the Execute one, which has other color.</param>
    ''' </summary>
    Private Sub SwitchIconButtonColor(button As IconButton, Optional isExecuteButton As Boolean = False)
        Dim isEnabled As Boolean = button.Enabled

        If isEnabled Then
            If isExecuteButton Then
                button.IconColor = Color.Lime
            Else
                button.IconColor = Color.White
            End If
            button.ForeColor = Color.White
        Else
            button.IconColor = Color.Gray
        End If
    End Sub

#End Region

#Region "Database Logic"

    ''' <summary>
    ''' Tests the connection for the first time.
    ''' </summary>
    Private Async Sub Connect()
        Dim server As String = tbServer.Text
        Dim user As String = tbUser.Text
        Dim pass As String = tbPass.Text
        Dim timeout As Integer = tbTimeout.Text

        If chbLoginMode.Checked Then
            DatabaseLogic.SetWindowsLogin(server, timeout)
        Else
            DatabaseLogic.SetSqlServerLogin(server, user, pass, timeout)
        End If

        Try
            lblConnStatus.Text = "Connecting..."
            lblConnStatus.ForeColor = Color.Orange
            btnConnect.Enabled = False
            CurrentStatus = ConnectionStatus.Connecting
            Await DatabaseLogic.TestConnection(timeout)

            My.Settings.Server = server
            My.Settings.User = user
            My.Settings.Pass = pass
            My.Settings.Timeout = timeout
            My.Settings.Save()

            btnExecute.Enabled = True
            btnDisconnect.Enabled = True
            btnReload.Enabled = True
            cbDatabases.Enabled = True
            lblConnStatus.Text = "Connected"
            lblConnStatus.ForeColor = Color.SpringGreen

            CurrentStatus = ConnectionStatus.Connected
            Log("Connection established.")
            FillDatabasesComboBox()
        Catch ex As Exception
            btnConnect.Enabled = True
            btnExecute.Enabled = False
            btnDisconnect.Enabled = False
            btnReload.Enabled = False
            cbDatabases.Enabled = False
            lblConnStatus.Text = "Disconnected"
            lblConnStatus.ForeColor = Color.IndianRed

            CurrentStatus = ConnectionStatus.Disconnected
            Log(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Fills the ComboBox containing the databases.
    ''' </summary>
    Private Sub FillDatabasesComboBox()
        Try
            Dim databaseList As List(Of String) = DatabaseLogic.GetDatabasesList()

            For Each db In databaseList
                If Not cbDatabases.Items.Contains(db) Then
                    cbDatabases.Items.Add(db)
                End If
            Next

            Try
                For Each db In cbDatabases.Items
                    If Not databaseList.Contains(db) Then
                        cbDatabases.Items.Remove(db)
                    End If
                Next
            Catch
            End Try
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Fills the object explorer containing the database items.
    ''' </summary>
    Private Sub FillObjectExplorer()
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        If IsNothing(databaseName) Then Return

        Try
            Dim tablesList As List(Of String) = DatabaseLogic.GetTablesListFromDatabase(databaseName)
            Dim proceduresList As List(Of String) = DatabaseLogic.GetProceduresListFromDatabase(databaseName)
            Dim functionsList As List(Of String) = DatabaseLogic.GetFunctionsListFromDatabase(databaseName)
            Dim viewsList As List(Of String) = DatabaseLogic.GetViewsListFromDatabase(databaseName)

            Dim rootNode As New TreeNode(databaseName, 0, 0)
            Dim tablesNode As New TreeNode("Tables", 5, 5)
            Dim viewsNode As New TreeNode("Views", 5, 5)
            Dim proceduresNode As New TreeNode("Procedures", 5, 5)
            Dim functionsNode As New TreeNode("Functions", 5, 5)
            Dim treeNodeArray() = New TreeNode() {tablesNode, viewsNode, proceduresNode, functionsNode}

            For Each table In tablesList
                If Not tvObjectExplorer.Nodes.ContainsKey(table) Then
                    tablesNode.Nodes.Add(table, table, 1, 1)
                End If
            Next

            For Each view In viewsList
                If Not tvObjectExplorer.Nodes.ContainsKey(view) Then
                    viewsNode.Nodes.Add(view, view, 2, 2)
                End If
            Next

            For Each procedure In proceduresList
                If Not tvObjectExplorer.Nodes.ContainsKey(procedure) Then
                    proceduresNode.Nodes.Add(procedure, procedure, 3, 3)
                End If
            Next

            For Each func In functionsList
                If Not tvObjectExplorer.Nodes.ContainsKey(func) Then
                    functionsNode.Nodes.Add(func, func, 4, 4)
                End If
            Next

            If tablesNode.Nodes.Count = 0 Then tablesNode.ForeColor = Color.Gray
            If viewsNode.Nodes.Count = 0 Then viewsNode.ForeColor = Color.Gray
            If proceduresNode.Nodes.Count = 0 Then proceduresNode.ForeColor = Color.Gray
            If functionsNode.Nodes.Count = 0 Then functionsNode.ForeColor = Color.Gray

            tvObjectExplorer.Nodes.Clear()
            tvObjectExplorer.Nodes.Add(rootNode)
            rootNode.Nodes.AddRange(treeNodeArray)

            tvObjectExplorer.ExpandAll()
        Catch ex As Exception
            Log(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Sets the current table to be shown on the DGV.
    ''' </summary>
    Private Sub SetSelectedTable(node As TreeNode)
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        If IsNothing(databaseName) Or IsNothing(node.Parent) Then Return

        If node.Parent.Text = "Tables" Or node.Parent.Text = "Views" Then
            Try
                Dim dt As DataTable = DatabaseLogic.GetTableFromDataBase(node.Text, databaseName)
                AddNewTable(dt, node.Text, databaseName, True)

                btnSubmit.Enabled = True
            Catch ex As Exception
                Log(ex.Message)
            End Try
        End If

    End Sub

    ''' <summary>
    ''' Submits all changes on the DGV to the server.
    ''' </summary>
    Private Sub SubmitChanges()
        Dim tableName As String = CType(TablesTabControl.SelectedTab.Controls(0), UserTable).TableName
        Dim databaseName As String = CType(TablesTabControl.SelectedTab.Controls(0), UserTable).DatabaseName
        If IsNothing(databaseName) Or IsNothing(tableName) Then Return

        If CurrentTable.Changes IsNot Nothing Then
            Dim dr As DialogResult = MessageBox.Show("Are you sure to submit changes?", "MSSQL Admin", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

            If dr = DialogResult.Yes Then
                CurrentTable.EndEdit()

                Try
                    DatabaseLogic.UpdateDataTable(tableName, databaseName, CurrentTable.DataTable)

                    Log("Changes submitted to server.")
                    UpdateAllTables()
                Catch ex As Exception
                    Log(ex.Message)
                End Try
            End If
        Else
            Log("No changes were found.")
        End If
    End Sub

    ''' <summary>
    ''' Executes the code written by user on the server.
    ''' </summary>
    Private Sub ExecuteCode()
        If CurrentEditor IsNot Nothing AndAlso Not String.IsNullOrEmpty(CurrentEditor.GetText) Then
            Dim sqlCode As String = IIf(CurrentEditor.GetSelectedText.Length > 0, CurrentEditor.GetSelectedText, CurrentEditor.GetText)
            Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
            Dim useDatabase As Boolean = False

            If CurrentEditor.GetText.IndexOf("USE ", 0, StringComparison.CurrentCultureIgnoreCase) = -1 AndAlso Not IsNothing(databaseName) Then
                useDatabase = True
            End If

            Try
                Dim codeExecution = If(useDatabase, DatabaseLogic.ExecuteSqlCode(sqlCode, databaseName), DatabaseLogic.ExecuteSqlCode(sqlCode, Nothing))
                Dim dtList As List(Of DataTable) = codeExecution(0)
                Dim recordsAffected = codeExecution(1)

                For Each dt As DataTable In dtList
                    GlobalTableCounter += 1
                    AddNewTable(dt, "Table " & GlobalTableCounter, databaseName, False)
                Next

                If recordsAffected = -1 Then recordsAffected = 0

                Log("Execution succesful. " & recordsAffected & " record(s) affected.")
                FillDatabasesComboBox()
                FillObjectExplorer()
                UpdateAllTables()
            Catch ex As Exception
                Log(ex.Message)
            End Try
        Else
            Log("Nothing to execute.")
        End If
    End Sub

    ''' <summary>
    ''' Opens a form to execute the given procedure.
    ''' </summary>
    Private Sub ExecuteProcedure(procedureName As String)
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        If IsNothing(databaseName) Or IsNothing(procedureName) Then Return

        Using form As New ProcedureExecutionForm(procedureName, databaseName)
            Dim result = form.ShowDialog()

            If result = DialogResult.OK Then
                Try
                    Dim params = form.ParamsConfig
                    Dim dtList As List(Of DataTable) = DatabaseLogic.ExecuteProcedure(procedureName, databaseName, params)

                    For Each dt As DataTable In dtList
                        GlobalTableCounter += 1
                        AddNewTable(dt, "Table " & GlobalTableCounter, databaseName, False)
                    Next

                    Log("Execution of " & procedureName & " was succesful.")
                    FillDatabasesComboBox()
                    FillObjectExplorer()
                    UpdateAllTables()
                Catch ex As Exception
                    Log(ex.Message)
                End Try
            End If
        End Using
    End Sub

#End Region

#Region "File Logic"

    ''' <summary>
    ''' Exports the current table that's being displayed in the DGV.
    ''' </summary>
    Private Sub Export()
        If CurrentTable IsNot Nothing AndAlso CurrentTable.RowCount > 0 Then
            Dim sfd As New SaveFileDialog With {
                    .Filter = "CSV File(*.csv)|*.csv|XML File(*.xml)|*.xml",
                    .Title = "Export Table As...",
                    .FileName = "My Table"
            }

            If sfd.ShowDialog() = DialogResult.OK Then
                Select Case sfd.FilterIndex
                    Case 1 ' CSV
                        FileLogic.ToCsv(CurrentTable.DataGridView, sfd.FileName)
                    Case 2 ' XML
                        FileLogic.ToXml(CurrentTable.DataTable, sfd.FileName)
                End Select
            End If
        Else
            Log("Nothing to export.")
        End If
    End Sub

    ''' <summary>
    ''' Saves the current query.
    ''' </summary>
    Private Sub SaveQuery(tab As TabPage)
        If tab.Controls.Count = 0 Then Return

        Dim userEditor As UserEditor = CType(tab.Controls(0), UserEditor)

        If userEditor IsNot Nothing Then
            If String.IsNullOrEmpty(userEditor.Path) Or String.IsNullOrEmpty(userEditor.FileName) Then
                SaveQueryAs(tab)
            Else
                Dim filePath As String = userEditor.Path
                Dim fileName As String = Path.GetFileName(filePath)

                FileLogic.ToSql(userEditor.GetText(), filePath)
                EditorsTabControl.SetTabEditMode(tab, False)
                userEditor.LastSavedText = userEditor.GetText()
                Log(fileName & " saved at " & Path.GetDirectoryName(filePath) & ".")
            End If
        Else
            Log("Nothing to save.")
        End If
    End Sub

    ''' <summary>
    ''' Saves the current editor in a new file.
    ''' </summary>
    Private Sub SaveQueryAs(tab As TabPage)
        If tab.Controls.Count = 0 Then Return

        Dim userEditor As UserEditor = CType(tab.Controls(0), UserEditor)

        If userEditor IsNot Nothing Then
            Dim sfd As New SaveFileDialog With {
               .Filter = "SQL File(*.sql)|*.sql",
               .Title = "Save File",
               .FileName = tab.Name
            }

            If sfd.ShowDialog() = DialogResult.OK Then
                Dim filePath As String = sfd.FileName
                Dim fileName As String = Path.GetFileName(filePath)

                FileLogic.ToSql(userEditor.GetText, filePath)

                EditorsTabControl.EditTabNameAndText(tab, fileName, False)
                EditorsTabControl.SetTabEditMode(tab, False)

                userEditor.Path = filePath
                userEditor.FileName = fileName
                userEditor.LastSavedText = userEditor.GetText()

                Log(fileName & " saved at " & Path.GetDirectoryName(filePath) & ".")
            End If
        Else
            Log("Nothing to save.")
        End If
    End Sub

    ''' <summary>
    ''' Opens a new file.
    ''' </summary>
    Private Sub OpenFile()
        Dim ofd As New OpenFileDialog With {
            .Filter = "SQL File(*.sql)|*.sql",
            .Title = "Open SQL file"
        }

        If ofd.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = ofd.FileName
            Dim fileName As String = Path.GetFileName(filePath)
            Dim sr As New StreamReader(filePath)

            If (From tab In EditorsTabControl.TabPages.Cast(Of TabPage)
                Select tab.Name).ToList().Contains(fileName) Then
                EditorsTabControl.SelectTab(fileName)
            Else
                AddNewEditor(sr.ReadToEnd(), filePath)
            End If

            sr.Close()
            Log("Loaded " & filePath & ".")
        End If
    End Sub

#End Region

#Region "XPath"

    ''' <summary>
    ''' Executes the XPath expression written by the user.
    ''' </summary>
    Private Sub ExecuteXpath()
        If CurrentTable IsNot Nothing AndAlso CurrentTable.RowCount > 0 Then
            Try
                Dim sw = New StringWriter()
                Dim dataTable = CurrentTable.DataTable
                dataTable.TableName = "Item"
                dataTable.WriteXml(sw, XmlWriteMode.IgnoreSchema)
                dataTable.TableName = ""

                Dim xmlDoc As New XmlDocument
                xmlDoc.LoadXml(sw.ToString)

                Dim nodeList As XmlNodeList = xmlDoc.DocumentElement.SelectNodes(xpathExpression.Text)
                xpathArea.Text = ""

                If nodeList.Count > 0 Then
                    For Each node As XmlNode In nodeList
                        xpathArea.AppendText(node.OuterXml & vbNewLine)
                    Next
                Else
                    xpathArea.AppendText("No matches found." & vbNewLine)
                End If
            Catch ex As Exception
                xpathArea.Text = ""
                xpathArea.AppendText("Wrong XPath expression." & vbNewLine)
            End Try
        Else
            xpathArea.Text = ""
            xpathArea.AppendText("Table is empty." & vbNewLine)
        End If
    End Sub

#End Region

#Region "Events"

    Private Sub CbDatabases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDatabases.SelectedIndexChanged
        tvObjectExplorer.Nodes.Clear()
        ClearTables()
        FillObjectExplorer()
    End Sub

    Private Sub TvObjectExplorer_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvObjectExplorer.NodeMouseDoubleClick
        Dim myNode As TreeNode = tvObjectExplorer.SelectedNode

        If myNode IsNot Nothing AndAlso myNode.Parent IsNot Nothing Then
            If myNode.Parent.Text = "Tables" Or myNode.Parent.Text = "Views" Then
                _ignoreTabControlSelectedEvent = True
                SetSelectedTable(e.Node)
                _ignoreTabControlSelectedEvent = False
            ElseIf myNode.Parent.Text = "Procedures" Then
                btnExecuteProcedure.PerformClick()
            End If
        End If
    End Sub

    Private Sub TvObjectExplorer_MouseDown(sender As Object, e As MouseEventArgs) Handles tvObjectExplorer.MouseDown
        If e.Button = MouseButtons.Right Then
            tvObjectExplorer.SelectedNode = tvObjectExplorer.GetNodeAt(e.X, e.Y)
            Dim myNode As TreeNode = tvObjectExplorer.SelectedNode

            If myNode IsNot Nothing AndAlso myNode.Parent IsNot Nothing Then
                If myNode.Parent.Text = "Tables" Or myNode.Parent.Text = "Views" Then
                    btnDesign.Visible = Not myNode.Parent.Text = "Views"
                    TablesAndViewsMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y)
                ElseIf myNode.Parent.Text = "Procedures" Then
                    ProceduresMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y)
                End If
            ElseIf myNode IsNot Nothing AndAlso myNode.Parent Is Nothing Then
                DatabaseMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y)
            End If
        End If
    End Sub

    Private Sub TvObjectExplorer_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles tvObjectExplorer.BeforeSelect
        If e.Node.ForeColor = Color.Gray Then
            e.Cancel = True
        End If
    End Sub

    Private Sub TvObjectExplorer_AfterExpand(sender As Object, e As TreeViewEventArgs) Handles tvObjectExplorer.AfterExpand
        If e.Node.ImageIndex = 5 Then
            e.Node.SelectedImageIndex = 6
            e.Node.ImageIndex = 6
        End If
    End Sub

    Private Sub TvObjectExplorer_AfterCollapse(sender As Object, e As TreeViewEventArgs) Handles tvObjectExplorer.AfterCollapse
        If e.Node.ImageIndex = 6 Then
            e.Node.SelectedImageIndex = 5
            e.Node.ImageIndex = 5
        End If
    End Sub

    Private Sub TablesTabControl_Selected(sender As Object, e As TabControlEventArgs) Handles TablesTabControl.Selected
        Dim index As Integer = TablesTabControl.SelectedIndex

        If index <> -1 And Not _ignoreTabControlSelectedEvent Then
            Dim userTable As UserTable = CType(TablesTabControl.TabPages(index).Controls(0), UserTable)

            SetCurrentTable(userTable)
            btnSubmit.Enabled = userTable.CanBeUpdated
            userTable.ColumnsMode = CurrentColumnsMode
        End If
    End Sub

    Private Sub EditorsTabControl_Selected(sender As Object, e As TabControlEventArgs) Handles EditorsTabControl.Selected
        Dim index As Integer = EditorsTabControl.SelectedIndex
        Dim lastIndex As Integer = EditorsTabControl.TabPages.Count - 1

        If index <> -1 And index <> lastIndex Then
            Dim userEditor As UserEditor = CType(EditorsTabControl.TabPages(index).Controls(0), UserEditor)
            userEditor.Zoom = CurrentZoom
            userEditor.Scintilla.Select()

            SetCurrentEditor(userEditor)
        End If
    End Sub

    Private Sub EditorsTabControl_OnTabClose(sender As Object, e As CustomTabControl.CloseTabEventArgs) Handles EditorsTabControl.OnTabClose
        Dim index As Integer = e.Index

        If index <> -1 Then
            Dim tabToClose As TabPage = EditorsTabControl.TabPages(index)
            Dim editor As UserEditor = CType(tabToClose.Controls(0), UserEditor)

            If Not editor.IsSaved Then
                Dim dr As DialogResult = MessageBox.Show("Do you want to save the changes you made to " & tabToClose.Name & "?", "MSSQL Admin", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

                Select Case dr
                    Case DialogResult.Yes
                        e.Cancel = False
                        SaveQuery(tabToClose)
                    Case DialogResult.No
                        e.Cancel = False
                    Case DialogResult.Cancel
                        e.Cancel = True
                        Return
                End Select
            End If
        End If

        If EditorsTabControl.TabPages.Count <= 2 Then
            EditorsTabControl.Visible = False
            splitter2.Visible = False
            GlobalNewEditorCounter = 1
            CurrentEditor = Nothing

            If Not TablesTabControl.Visible Then
                EditorsTabControl.Dock = DockStyle.Top
                TablesTabControl.Dock = DockStyle.Fill

                EditorsTabControl.SendToBack()
                TablesTabControl.BringToFront()
            End If
        End If
    End Sub

    Private Sub TablesTabControl_OnTabClose(sender As Object, e As CustomTabControl.CloseTabEventArgs) Handles TablesTabControl.OnTabClose
        If TablesTabControl.TabPages.Count <= 1 Then
            TablesTabControl.Visible = False
            splitter2.Visible = False
            GlobalTableCounter = 0
            CurrentTable = Nothing
            btnSubmit.Enabled = False

            If EditorsTabControl.Visible Then
                EditorsTabControl.Dock = DockStyle.Fill
                TablesTabControl.Dock = DockStyle.Bottom

                EditorsTabControl.BringToFront()
                TablesTabControl.SendToBack()
            End If
        End If
    End Sub

    Private Sub EditorsTabControl_OnNewTab() Handles EditorsTabControl.OnNewTab
        If EditorsTabControl.TabPages.Count = 1 Then
            EditorsTabControl.Visible = True
            splitter2.Visible = TablesTabControl.Visible

            If Not TablesTabControl.Visible Then
                EditorsTabControl.Dock = DockStyle.Fill
                TablesTabControl.Dock = DockStyle.Bottom

                EditorsTabControl.BringToFront()
                TablesTabControl.SendToBack()
            End If
        End If

        GlobalNewEditorCounter += 1
    End Sub

    Private Sub TablesTabControl_OnNewTab() Handles TablesTabControl.OnNewTab
        If TablesTabControl.TabPages.Count = 0 Then
            TablesTabControl.Visible = True
            splitter2.Visible = EditorsTabControl.Visible

            If EditorsTabControl.Visible Then
                EditorsTabControl.Dock = DockStyle.Top
                TablesTabControl.Dock = DockStyle.Fill

                EditorsTabControl.SendToBack()
                TablesTabControl.BringToFront()
            End If
        End If
    End Sub

    Private Sub BtnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click
        If btnOutput.Checked Then
            btnOutput.Checked = True
            btnXpath.Checked = False

            outputArea.Visible = True
            xpathPanel.Visible = False

            splitter1.Visible = True
            bottomPanel.Visible = True
        Else
            splitter1.Visible = False
            bottomPanel.Visible = False
        End If
    End Sub

    Private Sub BtnXpath_Click(sender As Object, e As EventArgs) Handles btnXpath.Click
        If btnXpath.Checked Then
            btnOutput.Checked = False
            btnXpath.Checked = True

            outputArea.Visible = False
            xpathPanel.Visible = True

            splitter1.Visible = True
            bottomPanel.Visible = True
        Else
            splitter1.Visible = False
            bottomPanel.Visible = False
        End If
    End Sub

    Private Sub XpathExpression_TextChanged(sender As Object, e As EventArgs) Handles xpathExpression.TextChanged
        Dim selectionIndex As Integer = xpathExpression.SelectionStart

        If Not xpathExpression.Text.StartsWith("//") Then
            xpathExpression.Text = "//"
            If selectionIndex >= 1 Then
                xpathExpression.SelectionStart = 2
            Else
                xpathExpression.SelectionStart = selectionIndex + 1
            End If
        End If
    End Sub

    Private Sub CurrentEditor_TextModified(sender As Object, e As EventArgs)
        EditorsTabControl.SetTabEditMode(EditorsTabControl.SelectedTab, Not CurrentEditor.IsSaved())
    End Sub

    Private Sub MainForm_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        For Each filePath In files
            Dim fileExtension As String = Path.GetExtension(filePath).ToLower()

            If fileExtension Like ".sql" Then
                Dim fileName As String = Path.GetFileName(filePath)

                Dim sr As New StreamReader(filePath)

                If Not (From tab In EditorsTabControl.TabPages.Cast(Of TabPage)
                        Select tab.Name).ToList().Contains(fileName) Then
                    AddNewEditor(sr.ReadToEnd(), filePath)
                    Log("Loaded " & filePath & ".")
                End If

                sr.Close()
            End If
        Next
    End Sub

    Private Sub MainForm_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub


    Private Sub MyMenuStrip_MouseDown(sender As Object, e As MouseEventArgs) Handles MyMenuStrip.MouseDown
        If (e.Button = MouseButtons.Left) Then
            ReleaseCapture()
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
            btnMaximizeRestoreWindow.IconChar = IconChar.WindowMaximize
        End If
    End Sub

    Private Sub BtnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Connect()
    End Sub

    Private Sub BtnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
        CloseSession()
    End Sub

    Private Sub BtnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        FillDatabasesComboBox()
    End Sub

    Private Sub BtnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        SubmitChanges()
    End Sub

    Private Sub BtnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        ExecuteCode()
    End Sub

    Private Sub EditorsTabControl_OnAddButtonClick() Handles EditorsTabControl.OnAddButtonClick
        AddNewEditor()
    End Sub

    Private Sub BtnCloseAllEditors_Click(sender As Object, e As EventArgs) Handles btnCloseAllEditors.Click
        EditorsTabControl.Visible = False 'Added here to prevent a color flash
        EditorsTabControl.TabPages.Cast(Of TabPage).ToList() _
            .FindAll(Function(tab) tab.Controls.Count > 0 AndAlso Not CType(tab.Controls(0), UserEditor).IsSaved) _
            .ForEach(Sub(tab) SaveQuery(tab))

        ClearEditors()
    End Sub

    Private Sub BtnCloseAllTables_Click(sender As Object, e As EventArgs) Handles btnCloseAllTables.Click
        TablesTabControl.Visible = False 'Added here to prevent a color flash
        ClearTables()
    End Sub

    Private Sub BtnClearOutput_Click(sender As Object, e As EventArgs) Handles btnClearOutput.Click
        outputArea.Text = ""
    End Sub

    Private Sub BtnClearXpath_Click(sender As Object, e As EventArgs) Handles btnClearXpath.Click
        xpathExpression.Text = ""
        xpathArea.Text = ""
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        CloseSession()
        Application.Exit()
    End Sub

    Private Sub BtnCloseWindow_Click(sender As Object, e As EventArgs) Handles btnCloseWindow.Click
        CloseSession()
        Application.Exit()
    End Sub

    Private Sub BtnMaximizeRestoreWindow_Click(sender As Object, e As EventArgs) Handles btnMaximizeRestoreWindow.Click
        If WindowState = FormWindowState.Normal Then
            WindowState = FormWindowState.Maximized
            btnMaximizeRestoreWindow.IconChar = IconChar.WindowRestore
        Else
            WindowState = FormWindowState.Normal
            btnMaximizeRestoreWindow.IconChar = IconChar.WindowMaximize
        End If
    End Sub

    Private Sub BtnMinimizeWindow_Click(sender As Object, e As EventArgs) Handles btnMinimizeWindow.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub BtnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click
        CurrentEditor?.Undo()
    End Sub

    Private Sub BtnRedo_Click(sender As Object, e As EventArgs) Handles btnRedo.Click
        CurrentEditor?.Redo()
    End Sub

    Private Sub BtnCut_Click(sender As Object, e As EventArgs) Handles btnCut.Click
        CurrentEditor?.Cut()
    End Sub

    Private Sub BtnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        CurrentEditor?.Copy()
    End Sub

    Private Sub BtnPaste_Click(sender As Object, e As EventArgs) Handles btnPaste.Click
        CurrentEditor?.Paste()
    End Sub

    Private Sub BtnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        CurrentEditor?.SelectAll()
    End Sub

    Private Sub BtnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        CurrentEditor?.ShowFind()
    End Sub

    Private Sub BtnReplace_Click(sender As Object, e As EventArgs) Handles btnReplace.Click
        CurrentEditor?.ShowReplace()
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Export()
    End Sub

    Private Sub BtnSaveSql_Click(sender As Object, e As EventArgs) Handles btnSaveSql.Click
        SaveQuery(EditorsTabControl.SelectedTab)
    End Sub

    Private Sub BtnSaveAs_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click
        SaveQueryAs(EditorsTabControl.SelectedTab)
    End Sub

    Private Sub BtnSaveAll_Click(sender As Object, e As EventArgs) Handles btnSaveAll.Click
        EditorsTabControl.TabPages.Cast(Of TabPage).ToList() _
            .FindAll(Function(tab) tab.Controls.Count > 0) _
            .ForEach(Sub(tab) SaveQuery(tab))
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNewEditor()
    End Sub

    Private Sub BtnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        OpenFile()
    End Sub

    Private Sub BtnExecuteXpath_Click(sender As Object, e As EventArgs) Handles btnExecuteXpath.Click
        ExecuteXpath()
    End Sub

    Private Sub BtnExecute_EnabledChanged(sender As Object, e As EventArgs) Handles btnExecute.EnabledChanged
        SwitchIconButtonColor(sender, True)
    End Sub

    Private Sub BtnSubmit_EnabledChanged(sender As Object, e As EventArgs) Handles btnSubmit.EnabledChanged
        SwitchIconButtonColor(sender)
    End Sub

    Private Sub BtnConnect_EnabledChanged(sender As Object, e As EventArgs) Handles btnConnect.EnabledChanged
        SwitchIconButtonColor(sender)
    End Sub

    Private Sub BtnDisconnect_EnabledChanged(sender As Object, e As EventArgs) Handles btnDisconnect.EnabledChanged
        SwitchIconButtonColor(sender)
    End Sub

    Private Sub BtnReload_EnabledChanged(sender As Object, e As EventArgs) Handles btnReload.EnabledChanged
        SwitchIconButtonColor(sender)
    End Sub

    Private Sub BtnZoomIn_Click(sender As Object, e As EventArgs) Handles btnZoomIn.Click
        If CurrentEditor IsNot Nothing Then CurrentEditor.Zoom += 1
    End Sub

    Private Sub BtnZoomOut_Click(sender As Object, e As EventArgs) Handles btnZoomOut.Click
        If CurrentEditor IsNot Nothing Then CurrentEditor.Zoom -= 1
    End Sub

    Private Sub BtnResetZoom_Click(sender As Object, e As EventArgs) Handles btnResetZoom.Click
        If CurrentEditor IsNot Nothing Then CurrentEditor.Zoom = 0
    End Sub

    Private Sub BtnEditTable_Click(sender As Object, e As EventArgs) Handles btnEditTable.Click
        Dim myNode = tvObjectExplorer.SelectedNode

        If myNode IsNot Nothing Then
            SetSelectedTable(myNode)
        End If
    End Sub

    Private Sub BtnDesign_Click(sender As Object, e As EventArgs) Handles btnDesign.Click
        Dim myNode = tvObjectExplorer.SelectedNode

        If myNode IsNot Nothing Then
            OpenDesignMode(True, myNode.Text)
        End If
    End Sub

    Private Sub BtnCreateTable_Click(sender As Object, e As EventArgs) Handles btnCreateTable.Click
        If cbDatabases.SelectedIndex <> -1 Then
            OpenDesignMode(False)
        End If
    End Sub

    Private Sub BtnDropDatabase_Click(sender As Object, e As EventArgs) Handles btnDropDatabase.Click
        Dim myNode As TreeNode = tvObjectExplorer.SelectedNode
        Dim databaseName As String = myNode?.Text
        If IsNothing(databaseName) Then Return

        Try
            Dim dr As DialogResult = MessageBox.Show("Are you really sure to drop " & databaseName & "?", "MSSQL Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If dr = DialogResult.Yes Then
                DatabaseLogic.Drop(databaseName, "DATABASE", "MASTER")

                cbDatabases.SelectedIndex = -1
                tvObjectExplorer.Nodes.Clear()
                ClearTables()
                FillDatabasesComboBox()
                FillObjectExplorer()

                Log(databaseName & " was successfuly dropped.")
            End If
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    Private Sub BtnTruncateTable_Click(sender As Object, e As EventArgs) Handles btnTruncateTable.Click
        Dim myNode As TreeNode = tvObjectExplorer.SelectedNode
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        Dim tableName As String = myNode?.Text
        If IsNothing(databaseName) Or IsNothing(tableName) Then Return

        Try
            Dim dr As DialogResult = MessageBox.Show("Are you sure to truncate " & tableName & "?", "MSSQL Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If dr = DialogResult.Yes Then
                DatabaseLogic.Truncate(tableName, If(myNode.Parent.Text = "Tables", "TABLE", "VIEW"), databaseName)
                FillObjectExplorer()

                Log(tableName & " was successfuly truncated.")
            End If
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    Private Sub BtnDropTable_Click(sender As Object, e As EventArgs) Handles btnDropTable.Click
        Dim myNode As TreeNode = tvObjectExplorer.SelectedNode
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        Dim tableName As String = myNode?.Text
        If IsNothing(databaseName) Or IsNothing(tableName) Then Return

        Try
            Dim dr As DialogResult = MessageBox.Show("Are you sure to drop " & tableName & "?", "MSSQL Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If dr = DialogResult.Yes Then
                DatabaseLogic.Drop(tableName, If(myNode.Parent.Text = "Tables", "TABLE", "VIEW"), databaseName)
                FillObjectExplorer()

                Log(tableName & " was successfuly dropped.")
            End If
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    Private Sub BtnExecuteProcedure_Click(sender As Object, e As EventArgs) Handles btnExecuteProcedure.Click
        Dim myNode = tvObjectExplorer.SelectedNode

        If myNode IsNot Nothing Then
            ExecuteProcedure(myNode.Text)
        End If
    End Sub

    Private Sub BtnShowProcedureDefinition_Click(sender As Object, e As EventArgs) Handles btnShowProcedureDefinition.Click
        Dim myNode As TreeNode = tvObjectExplorer.SelectedNode
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        Dim procedureName As String = myNode?.Text
        If IsNothing(databaseName) Or IsNothing(procedureName) Then Return

        Try
            Dim definition As String = DatabaseLogic.GetScalar("SELECT OBJECT_DEFINITION (OBJECT_ID('" & procedureName & "'))", databaseName).ToString()
            AddNewEditor(definition)
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    Private Sub BtnDropProcedure_Click(sender As Object, e As EventArgs) Handles btnDropProcedure.Click
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        Dim procedureName As String = tvObjectExplorer.SelectedNode?.Text
        If IsNothing(databaseName) Or IsNothing(procedureName) Then Return

        Try
            Dim dr As DialogResult = MessageBox.Show("Are you sure to drop " & procedureName & "?", "MSSQL Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If dr = DialogResult.Yes Then
                DatabaseLogic.Drop(procedureName, "PROCEDURE", databaseName)
                FillObjectExplorer()

                Log(procedureName & " was successfuly dropped.")
            End If
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    Private Sub BtnBackupDatabase_Click(sender As Object, e As EventArgs) Handles btnBackupDatabase.Click
        OpenBackupForm()
    End Sub

    Private Sub BtnBackupDatabase2_Click(sender As Object, e As EventArgs) Handles btnBackupDatabase2.Click
        OpenBackupForm()
    End Sub

    Private Sub BtnTableMode_Click(sender As Object, e As EventArgs) Handles btnTableMode.Click
        If btnTableMode.Checked Then
            CurrentColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            btnTableMode.IconChar = IconChar.Check
        Else
            CurrentColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            btnTableMode.IconChar = IconChar.None
        End If

        If CurrentTable IsNot Nothing Then CurrentTable.ColumnsMode = CurrentColumnsMode
    End Sub

    Private Sub TbServer_Enter(sender As Object, e As EventArgs) Handles tbServer.Enter
        If tbServer.Text = "Server" Then
            tbServer.Text = String.Empty
        End If
    End Sub

    Private Sub TbServer_Leave(sender As Object, e As EventArgs) Handles tbServer.Leave
        If String.IsNullOrEmpty(tbServer.Text) Then
            tbServer.Text = "Server"
        End If
    End Sub

    Private Sub TbServer_TextChanged(sender As Object, e As EventArgs) Handles tbServer.TextChanged
        If tbServer.Text = "Server" Then
            tbServer.ForeColor = Color.Gray
        Else
            tbServer.ForeColor = Color.LightGray
        End If
    End Sub

    Private Sub TbTimeout_Enter(sender As Object, e As EventArgs) Handles tbTimeout.Enter
        If tbTimeout.Text = "Timeout" Then
            tbTimeout.Text = String.Empty
        End If
    End Sub

    Private Sub TbTimeout_Leave(sender As Object, e As EventArgs) Handles tbTimeout.Leave
        If String.IsNullOrEmpty(tbTimeout.Text) Then
            tbTimeout.Text = "Timeout"
        End If
    End Sub

    Private Sub TbTimeout_TextChanged(sender As Object, e As EventArgs) Handles tbTimeout.TextChanged
        If tbTimeout.Text = "Timeout" Then
            tbTimeout.ForeColor = Color.Gray
        Else
            tbTimeout.ForeColor = Color.LightGray
        End If
    End Sub

    Private Sub TbUser_Enter(sender As Object, e As EventArgs) Handles tbUser.Enter
        If tbUser.Text = "User" Then
            tbUser.Text = String.Empty
        End If
    End Sub

    Private Sub TbUser_Leave(sender As Object, e As EventArgs) Handles tbUser.Leave
        If String.IsNullOrEmpty(tbUser.Text) Then
            tbUser.Text = "User"
        End If
    End Sub

    Private Sub MainForm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        BackColor = Color.DodgerBlue
    End Sub

    Private Sub MainForm_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        BackColor = Color.OrangeRed
    End Sub

    Private Sub ChbLoginMode_CheckedChanged(sender As Object, e As EventArgs) Handles chbLoginMode.CheckedChanged
        tbUser.Enabled = Not chbLoginMode.Checked
        tbPass.Enabled = Not chbLoginMode.Checked
    End Sub

    Private Sub TbUser_TextChanged(sender As Object, e As EventArgs) Handles tbUser.TextChanged
        If tbUser.Text = "User" Then
            tbUser.ForeColor = Color.Gray
        Else
            tbUser.ForeColor = Color.LightGray
        End If
    End Sub

    Private Sub TbPass_Enter(sender As Object, e As EventArgs) Handles tbPass.Enter
        If tbPass.Text = "Password" Then
            tbPass.Text = String.Empty
        End If
    End Sub

    Private Sub TbPass_Leave(sender As Object, e As EventArgs) Handles tbPass.Leave
        If String.IsNullOrEmpty(tbPass.Text) Then
            tbPass.Text = "Password"
        End If
    End Sub

    Private Sub TbPass_TextChanged(sender As Object, e As EventArgs) Handles tbPass.TextChanged
        If tbPass.Text = "Password" Then
            tbPass.ForeColor = Color.Gray
            tbPass.UseSystemPasswordChar = False
        Else
            tbPass.ForeColor = Color.LightGray
            tbPass.UseSystemPasswordChar = True
        End If
    End Sub

#End Region

#Region "Interop"

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Public Const HT_CAPTION As Integer = &H2

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Int32, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function
    <DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function

#End Region

End Class
