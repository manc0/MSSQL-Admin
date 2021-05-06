Imports System.IO
Imports System.Xml
Imports MSSQLA.UserInterface.ClassUtils
Imports MSSQLA.BusinessLogicLayer
Imports FontAwesome.Sharp

Public Class MainForm
    Private ReadOnly Property DatabaseLogic As New DatabaseLogic
    Private ReadOnly Property FileLogic As New FileLogic
    Private Property GlobalTableCounter As Integer
    Private Property GlobalNewEditorCounter As Integer = 1

    ' Flags that avoid triggering events recursively
    Private _ignoreObjectExplorerAfterSelected As Boolean = False
    Private _ignoreTabControlSelectedEvent As Boolean = False

#Region "Form Initialization"

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

        TablesTabControl.ItemSize = New Size(0, 25)
        TablesTabControl.SizeMode = TabSizeMode.Normal
        TablesTabControl.DrawMode = DrawMode.OwnerDrawFixed

        EditorsTabControl.ItemSize = New Size(0, 25)
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
        tvObjectExplorer.SelectedNode = Nothing
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
    Private Sub AddNewEditor()
        Dim tab As New TabPage() With {
            .Text = "New " & GlobalNewEditorCounter,
            .Name = "New " & GlobalNewEditorCounter
        }
        Dim userEditor As New UserEditor(String.Empty)
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
            Dim userTable As UserTable = CType(tab.Controls(0), UserTable)

            If userTable.CanBeUpdated Then
                userTable.UpdateTable()
            End If

            If TablesTabControl.SelectedTab Is tab Then
                SetCurrentTable(userTable)
                btnSubmit.Enabled = userTable.CanBeUpdated
            End If
        Next
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
        DatabaseLogic.SetConnection(server, user, pass, timeout)

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
            lblConnStatus.ForeColor = Color.PaleGreen

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

            Dim rootNode As New TreeNode(databaseName)
            Dim tablesNode As New TreeNode("Tables")
            Dim proceduresNode As New TreeNode("Procedures")
            Dim functionsNode As New TreeNode("Functions")
            Dim viewsNode As New TreeNode("Views")
            Dim treeNodeArray() = New TreeNode() {tablesNode, viewsNode, proceduresNode, functionsNode}


            For Each table In tablesList
                If Not tvObjectExplorer.Nodes.ContainsKey(table) Then
                    tablesNode.Nodes.Add(table)
                End If
            Next

            For Each procedure In proceduresList
                If Not tvObjectExplorer.Nodes.ContainsKey(procedure) Then
                    proceduresNode.Nodes.Add(procedure)
                End If
            Next

            For Each func In functionsList
                If Not tvObjectExplorer.Nodes.ContainsKey(func) Then
                    functionsNode.Nodes.Add(func)
                End If
            Next

            For Each view In viewsList
                If Not tvObjectExplorer.Nodes.ContainsKey(view) Then
                    viewsNode.Nodes.Add(view)
                End If
            Next

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

            If CurrentEditor.GetText.IndexOf("USE ", 0, StringComparison.CurrentCultureIgnoreCase) = -1 And Not IsNothing(databaseName) Then
                sqlCode = "USE " & databaseName & vbNewLine & sqlCode
            End If

            Try
                Dim codeExecution = DatabaseLogic.ExecuteSqlCode(sqlCode)
                Dim dtList As List(Of DataTable) = codeExecution(0)
                Dim recordsAffected = codeExecution(1)

                For Each dt As DataTable In dtList
                    GlobalTableCounter += 1
                    AddNewTable(dt, "Table " & GlobalTableCounter, databaseName, False)
                    tvObjectExplorer.SelectedNode = Nothing
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

    Private Sub tvObjectExplorer_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvObjectExplorer.AfterSelect
        If Not _ignoreObjectExplorerAfterSelected And tvObjectExplorer.SelectedNode IsNot Nothing Then
            _ignoreTabControlSelectedEvent = True
            SetSelectedTable(e.Node)
            _ignoreTabControlSelectedEvent = False
        End If
    End Sub

    Private Sub TablesTabControl_Selected(sender As Object, e As TabControlEventArgs) Handles TablesTabControl.Selected
        Dim index As Integer = TablesTabControl.SelectedIndex
        _ignoreObjectExplorerAfterSelected = True

        If index <> -1 And Not _ignoreTabControlSelectedEvent Then
            Dim userTable As UserTable = CType(TablesTabControl.TabPages(index).Controls(0), UserTable)

            SetCurrentTable(userTable)
            btnSubmit.Enabled = userTable.CanBeUpdated
            userTable.ColumnsMode = CurrentColumnsMode

            tvObjectExplorer.SelectedNode = Nothing
        End If

        _ignoreObjectExplorerAfterSelected = False
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
            tvObjectExplorer.SelectedNode = Nothing

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
        EditorsTabControl.SetTabEditMode(EditorsTabControl.SelectedTab, True)

        If CurrentEditor.IsSaved() Then
            EditorsTabControl.SetTabEditMode(EditorsTabControl.SelectedTab, False)
        Else
            EditorsTabControl.SetTabEditMode(EditorsTabControl.SelectedTab, True)
        End If
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

    Private Sub BtnFullscreen_Click(sender As Object, e As EventArgs) Handles btnFullscreen.Click
        If btnFullscreen.Checked Then
            FormBorderStyle = FormBorderStyle.None
            WindowState = FormWindowState.Maximized
        Else
            FormBorderStyle = FormBorderStyle.Sizable
            WindowState = FormWindowState.Normal
        End If
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

End Class
