Imports System.IO
Imports System.Xml
Imports MSSQLA.UserInterface.ClassUtils
Imports MSSQLA.BusinessLogicLayer
Imports FontAwesome.Sharp

Public Class MainForm
    Private ReadOnly Property DatabaseLogic As New DatabaseLogic
    Private ReadOnly Property FileLogic As New FileLogic
    Private Property GlobalTableCounter As Integer
    Private Property GlobalNewEditorCounter As Integer

    ' Flags that avoid triggering events recursively
    Private _ignoreTableListIndexChangedEvent As Boolean = False
    Private _ignoreTabControlSelectedEvent As Boolean = False

#Region "Form Initialization"

    Protected Overrides Sub OnLoad(e As EventArgs)
        LoadConnectionSettings()

        For Each control In leftPanel.Controls
            If TypeOf control Is IconButton Then
                SwitchIconButtonColor(control)
            End If
        Next

        MyMenuStrip.Renderer = New ToolStripProfessionalRenderer(New MenuColorTable())
        MyToolStrip.RenderMode = ToolStripRenderMode.System
        MyToolStrip.Renderer = New CustomToolStripRenderer()

        TablesTabControl.ItemSize = New Size(0, 25)
        TablesTabControl.SizeMode = TabSizeMode.Normal
        TablesTabControl.DrawMode = DrawMode.OwnerDrawFixed

        EditorsTabControl.ItemSize = New Size(0, 25)
        EditorsTabControl.SizeMode = TabSizeMode.Normal
        EditorsTabControl.DrawMode = DrawMode.OwnerDrawFixed
        EditorsTabControl.Visible = False

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
    ''' Empties the DGV.
    ''' </summary>
    Private Sub ClearDgv()
        GlobalTableCounter = 0
        CurrentTable = Nothing
        TablesTabControl.TabPages.Clear()
        btnSubmit.Enabled = False
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
                .Text = tabSymbol & tableName & TabTextOffset,
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

    Private Sub AddNewEditor()
        If EditorsTabControl.TabPages.Count = 1 Then
            EditorsTabControl.Visible = True
        End If

        GlobalNewEditorCounter += 1

        Dim tab As New TabPage() With {
            .Text = "New " & GlobalNewEditorCounter & TabTextOffset,
            .Name = "New SQL File"
        }
        Dim userEditor As New UserEditor(GlobalNewEditorCounter)
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
        ClearDgv()
        cbDatabases.Items.Clear()
        lbTableList.Items.Clear()
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
        DatabaseLogic.SetConnection(server, user, pass, ConnectionTimeout)

        ClearDgv()
        cbDatabases.Items.Clear()
        lbTableList.Items.Clear()

        Try
            lblConnStatus.Text = "Connecting..."
            lblConnStatus.ForeColor = Color.Orange
            btnConnect.Enabled = False
            CurrentStatus = ConnectionStatus.Connecting
            Await DatabaseLogic.TestConnection(ConnectionTimeout)

            My.Settings.Server = server
            My.Settings.User = user
            My.Settings.Pass = pass
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
    ''' Fills the ListBox Gets containing the tables from the selected database.
    ''' </summary>
    Private Sub FillTablesListBox()
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        If IsNothing(databaseName) Then Return

        Try
            Dim tableList As List(Of String) = DatabaseLogic.GetTablesListFromDatabase(databaseName)

            For Each table In tableList
                If Not lbTableList.Items.Contains(table) Then
                    lbTableList.Items.Add(table)
                End If
            Next

            Try
                For Each db In lbTableList.Items
                    If Not tableList.Contains(db) Then
                        If lbTableList.SelectedItem IsNot Nothing Then
                            If lbTableList.SelectedItem.ToString.Equals(db) Then
                                ClearDgv()
                            End If
                        End If

                        lbTableList.Items.Remove(db)
                    End If
                Next
            Catch
            End Try
        Catch ex As Exception
            Log(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Sets the current table to be shown on the DGV.
    ''' </summary>
    Private Sub SetSelectedTable()
        Dim tableName As String = lbTableList.SelectedItem?.ToString
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString()
        If IsNothing(databaseName) Or IsNothing(tableName) Then Return

        Try
            Dim dt As DataTable = DatabaseLogic.GetTableFromDataBase(tableName, databaseName)
            AddNewTable(dt, tableName, databaseName, True)

            btnSubmit.Enabled = True
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Submits all changes on the DGV to the server.
    ''' </summary>
    Private Sub SubmitChanges()
        Dim tableName As String = CType(TablesTabControl.SelectedTab.Controls(0), UserTable).TableName
        Dim databaseName As String = CType(TablesTabControl.SelectedTab.Controls(0), UserTable).DatabaseName
        If IsNothing(databaseName) Or IsNothing(tableName) Then Return

        If CurrentTable.Changes IsNot Nothing Then
            Dim dr As DialogResult = MessageBox.Show("Are you sure to submit changes?", "Submit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

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
        If Not String.IsNullOrEmpty(CurrentEditor.GetText) Then
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
                    lbTableList.SelectedIndex = -1
                Next

                If recordsAffected = -1 Then recordsAffected = 0

                Log("Execution succesful. " & recordsAffected & " record(s) affected.")
                FillDatabasesComboBox()
                FillTablesListBox()
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
    ''' Saves the query as SQL.
    ''' </summary>
    Private Sub SaveQuery()
        Dim sfd As New SaveFileDialog With {
                .Filter = "SQL File(*.sql)|*.sql",
                .Title = "Save query",
                .FileName = "query.sql"
        }

        If sfd.ShowDialog() = DialogResult.OK Then
            FileLogic.ToSql(CurrentEditor.GetText, sfd.FileName)
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

    Private Sub CbDatabases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDatabases.SelectedIndexChanged
        lbTableList.Items.Clear()
        ClearDgv()
        FillTablesListBox()
    End Sub

    Private Sub TableList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbTableList.SelectedIndexChanged
        If Not _ignoreTableListIndexChangedEvent And lbTableList.SelectedIndex <> -1 Then
            _ignoreTabControlSelectedEvent = True
            SetSelectedTable()
            _ignoreTabControlSelectedEvent = False
        End If
    End Sub

    Private Sub TablesTabControl_Selected(sender As Object, e As TabControlEventArgs) Handles TablesTabControl.Selected
        Dim index As Integer = TablesTabControl.SelectedIndex
        _ignoreTableListIndexChangedEvent = True

        If index <> -1 And Not _ignoreTabControlSelectedEvent Then
            Dim userTable As UserTable = CType(TablesTabControl.TabPages(index).Controls(0), UserTable)

            SetCurrentTable(userTable)
            btnSubmit.Enabled = userTable.CanBeUpdated

            If userTable.CanBeUpdated Then
                lbTableList.SelectedItem = userTable.TableName
            Else
                lbTableList.SelectedIndex = -1
            End If
        ElseIf index = -1 Then
            btnSubmit.Enabled = False
            lbTableList.SelectedIndex = -1
        End If

        _ignoreTableListIndexChangedEvent = False
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

    Private Sub EditorsTabControl_OnAddButtonClick() Handles EditorsTabControl.OnAddButtonClick
        AddNewEditor()
    End Sub

    Private Sub EditorsTabControl_OnTabClose() Handles EditorsTabControl.OnTabClose
        If EditorsTabControl.TabPages.Count = 1 Then
            EditorsTabControl.Visible = False
            GlobalNewEditorCounter = 0
        End If
    End Sub

    Private Sub BtnCloseTab_Click(sender As Object, e As EventArgs) Handles btnCloseTab.Click
        TablesTabControl.CloseTabAt(TablesTabControl.SelectedIndex)
    End Sub

    Private Sub BtnCloseAllTabs_Click(sender As Object, e As EventArgs) Handles btnCloseAllTabs.Click
        ClearDgv()
        lbTableList.SelectedIndex = -1
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

    Private Sub BtnClearOutput_Click(sender As Object, e As EventArgs) Handles btnClearOutput.Click
        outputArea.Text = ""
    End Sub

    Private Sub BtnClearXpath_Click(sender As Object, e As EventArgs) Handles btnClearXpath.Click
        xpathExpression.Text = ""
        xpathArea.Text = ""
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        CloseSession()
    End Sub

    Private Sub BtnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click
        CurrentEditor.Undo()
    End Sub

    Private Sub BtnRedo_Click(sender As Object, e As EventArgs) Handles btnRedo.Click
        CurrentEditor.Redo()
    End Sub

    Private Sub BtnCut_Click(sender As Object, e As EventArgs) Handles btnCut.Click
        CurrentEditor.Cut()
    End Sub

    Private Sub BtnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        CurrentEditor.Copy()
    End Sub

    Private Sub BtnPaste_Click(sender As Object, e As EventArgs) Handles btnPaste.Click
        CurrentEditor.Paste()
    End Sub

    Private Sub BtnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        CurrentEditor.SelectAll()
    End Sub

    Private Sub BtnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        CurrentEditor.ShowFind()
    End Sub

    Private Sub BtnReplace_Click(sender As Object, e As EventArgs) Handles btnReplace.Click
        CurrentEditor.ShowReplace()
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Export()
    End Sub

    Private Sub BtnSaveSql_Click(sender As Object, e As EventArgs) Handles btnSaveSql.Click
        SaveQuery()
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNewEditor()
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

#End Region

End Class
