Imports System.IO
Imports System.Xml
Imports AutocompleteMenuNS
Imports ScintillaNET
Imports ScintillaNET_FindReplaceDialog
Imports MSSQLA.UserInterface.ClassUtils
Imports MSSQLA.BusinessLogicLayer

Public Class MainForm
    Private ReadOnly Property DatabaseLogic As New DatabaseLogic
    Private ReadOnly Property FileLogic As New FileLogic
    Private Property GlobalTableCounter As Integer

    ' Flags that avoid triggering events recursively
    Private _ignoreTableListIndexChangedEvent As Boolean = False
    Private _ignoreTabControlSelectedEvent As Boolean = False

#Region "Form Initialization"

    Protected Overrides Sub OnLoad(e As EventArgs)
        InitializeScintilla()
        InitializeAutoCompleteMenu()
        LoadConnectionSettings()

        MyMenuStrip.Renderer = New ToolStripProfessionalRenderer(New MenuColorTable())

        MyFindReplace = New FindReplace(MyScintilla) With {
            .Scintilla = MyScintilla
        }

        MyToolStrip.RenderMode = ToolStripRenderMode.System
        MyToolStrip.Renderer = New CustomToolStripRenderer()

        Log("Welcome " & Environment.UserName & ".")
        MyBase.OnLoad(e)
    End Sub

    ''' <summary>
    ''' Loads Scintilla settings.
    ''' </summary>
    Private Sub InitializeScintilla()
        Dim backColor As Color = Color.FromArgb(40, 44, 52)
        Dim selectionColor As Color = Color.FromArgb(255, 38, 79, 120)

        ' Reset the styles.
        MyScintilla.StyleResetDefault()
        MyScintilla.Styles(Style.[Default]).Font = "Consolas"
        MyScintilla.Styles(Style.[Default]).Size = 12
        MyScintilla.StyleClearAll()

        ' Lexer
        MyScintilla.Lexer = Lexer.Sql

        ' Margin
        MyScintilla.Margins(0).Width = 50

        ' Disable whitespaces visibility.
        MyScintilla.ViewWhitespace = WhitespaceMode.Invisible

        ' Set the common editor colors.
        MyScintilla.CaretForeColor = Color.FromArgb(1, 77, 122, 200)
        MyScintilla.CaretWidth = 2
        MyScintilla.SetSelectionBackColor(True, selectionColor)

        ' Syntax highlight
        With MyScintilla
            .Styles(Style.Default).BackColor = backColor

            .Styles(Style.LineNumber).ForeColor = Color.FromArgb(1, 149, 159, 161)
            .Styles(Style.LineNumber).BackColor = backColor

            .Styles(Style.Sql.Default).ForeColor = Color.LightGray
            .Styles(Style.Sql.Default).BackColor = backColor
            .Styles(Style.Sql.Default).Bold = False
            .Styles(Style.Sql.Default).Italic = False
            .Styles(Style.Sql.Default).Underline = False

            .Styles(Style.Sql.Comment).ForeColor = Color.FromArgb(255, 87, 159, 56)
            .Styles(Style.Sql.Comment).BackColor = backColor
            .Styles(Style.Sql.Comment).Bold = False
            .Styles(Style.Sql.Comment).Italic = True
            .Styles(Style.Sql.Comment).Underline = False

            .Styles(Style.Sql.CommentLine).ForeColor = Color.FromArgb(255, 87, 159, 56)
            .Styles(Style.Sql.CommentLine).BackColor = backColor
            .Styles(Style.Sql.CommentLine).Bold = False
            .Styles(Style.Sql.CommentLine).Italic = True
            .Styles(Style.Sql.CommentLine).Underline = False

            .Styles(Style.Sql.CommentLineDoc).ForeColor = Color.FromArgb(255, 87, 159, 56)
            .Styles(Style.Sql.CommentLineDoc).BackColor = backColor
            .Styles(Style.Sql.CommentLineDoc).Bold = False
            .Styles(Style.Sql.CommentLineDoc).Italic = True
            .Styles(Style.Sql.CommentLineDoc).Underline = False

            .Styles(Style.Sql.Word).ForeColor = Color.FromArgb(255, 100, 150, 215)
            .Styles(Style.Sql.Word).BackColor = backColor
            .Styles(Style.Sql.Word).Bold = False
            .Styles(Style.Sql.Word).Italic = False
            .Styles(Style.Sql.Word).Underline = False

            .Styles(Style.Sql.Word2).ForeColor = Color.HotPink
            .Styles(Style.Sql.Word2).BackColor = backColor
            .Styles(Style.Sql.Word2).Bold = False
            .Styles(Style.Sql.Word2).Italic = False
            .Styles(Style.Sql.Word2).Underline = False

            .Styles(Style.Sql.User1).ForeColor = Color.FromArgb(255, 62, 201, 174)
            .Styles(Style.Sql.User1).BackColor = backColor
            .Styles(Style.Sql.User1).Bold = False
            .Styles(Style.Sql.User1).Italic = False
            .Styles(Style.Sql.User1).Underline = False

            .Styles(Style.Sql.User2).ForeColor = Color.IndianRed
            .Styles(Style.Sql.User2).BackColor = backColor
            .Styles(Style.Sql.User2).Bold = False
            .Styles(Style.Sql.User2).Italic = False
            .Styles(Style.Sql.User2).Underline = False

            .Styles(Style.Sql.Identifier).BackColor = backColor
            .Styles(Style.Sql.Identifier).ForeColor = Color.LightGray
            .Styles(Style.Sql.Identifier).Bold = False
            .Styles(Style.Sql.Identifier).Italic = False
            .Styles(Style.Sql.Identifier).Underline = False

            .Styles(Style.Sql.Number).BackColor = backColor
            .Styles(Style.Sql.Number).ForeColor = Color.FromArgb(255, 181, 206, 168)
            .Styles(Style.Sql.Number).Bold = False
            .Styles(Style.Sql.Number).Italic = False
            .Styles(Style.Sql.Number).Underline = False

            .Styles(Style.Sql.Operator).BackColor = backColor
            .Styles(Style.Sql.Operator).ForeColor = Color.LightSlateGray
            .Styles(Style.Sql.Operator).Bold = True
            .Styles(Style.Sql.Operator).Italic = False
            .Styles(Style.Sql.Operator).Underline = False

            .Styles(Style.Sql.String).BackColor = backColor
            .Styles(Style.Sql.String).ForeColor = Color.FromArgb(255, 214, 157, 133)
            .Styles(Style.Sql.String).Bold = False
            .Styles(Style.Sql.String).Italic = False
            .Styles(Style.Sql.String).Underline = False

            .Styles(Style.Sql.Character).ForeColor = Color.FromArgb(255, 214, 157, 133)
            .Styles(Style.Sql.Character).BackColor = backColor
            .Styles(Style.Sql.Character).Bold = False
            .Styles(Style.Sql.Character).Italic = False
            .Styles(Style.Sql.Character).Underline = False
        End With

        ' Keywords
        MyScintilla.SetKeywords(0, SQL_KEYWORDS)
        MyScintilla.SetKeywords(1, SQL_FUNCTIONS)
        MyScintilla.SetKeywords(4, SQL_OPERATORS)
        MyScintilla.SetKeywords(5, SQL_OBJECTS)

        ' Instruct the lexer to calculate folding
        MyScintilla.SetProperty("fold", "1")
        MyScintilla.SetProperty("fold.compact", "1")

        ' Configure a margin to display folding symbols
        MyScintilla.Margins(2).Type = MarginType.Symbol
        MyScintilla.Margins(2).Mask = Marker.MaskFolders
        MyScintilla.Margins(2).Sensitive = True
        MyScintilla.Margins(2).Width = 13

        ' Configure folding markers with respective symbols
        MyScintilla.Markers(Marker.Folder).Symbol = MarkerSymbol.Arrow
        MyScintilla.Markers(Marker.FolderOpen).Symbol = MarkerSymbol.ArrowDown
        MyScintilla.Markers(Marker.FolderEnd).Symbol = MarkerSymbol.Arrow

        MyScintilla.Markers(Marker.FolderMidTail).Symbol = MarkerSymbol.VLine
        MyScintilla.Markers(Marker.FolderOpenMid).Symbol = MarkerSymbol.ArrowDown

        MyScintilla.Markers(Marker.FolderSub).Symbol = MarkerSymbol.VLine
        MyScintilla.Markers(Marker.FolderTail).Symbol = MarkerSymbol.VLine

        ' Set colors for all folding markers
        For i As Integer = Marker.FolderEnd To Marker.FolderOpen
            MyScintilla.Markers(i).SetForeColor(backColor)
            MyScintilla.Markers(i).SetBackColor(Color.FromArgb(1, 149, 159, 161))
        Next i

        ' Enable automatic folding
        MyScintilla.AutomaticFold = (AutomaticFold.Show Or AutomaticFold.Click Or AutomaticFold.Change)

        MyScintilla.SetFoldMarginColor(True, backColor)
        MyScintilla.SetFoldMarginHighlightColor(True, backColor)

        MyScintilla.SetFoldMarginColor(True, backColor)
        MyScintilla.SetFoldMarginHighlightColor(True, backColor)
        MyScintilla.IndentationGuides = IndentView.LookBoth
        MyScintilla.Select()
    End Sub

    ''' <summary>
    ''' Fills the autocompletemenu.
    ''' </summary>
    Private Sub InitializeAutoCompleteMenu()
        MyAutocompleteMenu.TargetControlWrapper = New ScintillaWrapper(MyScintilla)

        Array.ForEach(SQL_KEYWORDS.Split(" "), Function(item)
                                                   MyAutocompleteMenu.AddItem(New SnippetAutocompleteItem(item) With {.ImageIndex = 0, .ToolTipTitle = "SQL Keywords"})
                                                   Return True
                                               End Function)

        Array.ForEach(SQL_OPERATORS.Split(" "), Function(item)
                                                    MyAutocompleteMenu.AddItem(New SnippetAutocompleteItem(item) With {.ImageIndex = 1, .ToolTipTitle = "SQL Logical and Relational Operators"})
                                                    Return True
                                                End Function)

        Array.ForEach(SQL_FUNCTIONS.Split(" "), Function(item)
                                                    MyAutocompleteMenu.AddItem(New SnippetAutocompleteItem(item) With {.ImageIndex = 2, .ToolTipTitle = "SQL Functions"})
                                                    Return True
                                                End Function)

        Array.ForEach(SQL_OBJECTS.Split(" "), Function(item)
                                                  MyAutocompleteMenu.AddItem(New SnippetAutocompleteItem(item) With {.ImageIndex = 3, .ToolTipTitle = "SQL Objects"})
                                                  Return True
                                              End Function)
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
        CurrentTable = Nothing
        CurrentDGV = Nothing
        TabControl.TabPages.Clear()
        TabControl.Visible = False
        GlobalTableCounter = 0
    End Sub

    ''' <summary>
    ''' Adds a new tab to the TabControl.
    ''' </summary>
    ''' <param name="dt">DataTable to be added in that tab.</param>
    ''' <param name="tableName">Name of the table.</param>
    ''' <param name="databaseName">Name of the database which the table belongs to.</param>
    ''' <param name="canBeUpdated">Used to indicate wether the table can be updated from the DGV or not.</param>
    Private Sub AddNewTable(dt As DataTable, tableName As String, databaseName As String, canBeUpdated As Boolean)
        Dim tabNames = From tabs In TabControl.TabPages.Cast(Of TabPage)
                       Select tabs.Name
        Dim tabSymbol As String = IIf(canBeUpdated, "✎", "👁") & " "

        _ignoreTabControlSelectedEvent = True
        If Not tabNames.Contains(tableName) Then
            Dim tab As New TabPage() With {
                .Text = tabSymbol & tableName,
                .Name = tableName,
                .Margin = New Padding(30)
            }
            AddUserTableControl(tab, New UserTable(dt, canBeUpdated, tableName, databaseName))

            TabControl.TabPages.Add(tab)
            TabControl.SelectTab(tab)
        Else
            TabControl.SelectTab(tableName)
            Dim tab = TabControl.SelectedTab
            AddUserTableControl(tab, New UserTable(dt, canBeUpdated, tableName, databaseName))
        End If

        _ignoreTabControlSelectedEvent = False
        TabControl.Visible = True
    End Sub

    ''' <summary>
    ''' Updates all tabs if their tables got any changes made by the user after a manual insert, update or delete.
    ''' </summary>
    Private Sub UpdateTabs()
        For Each tab As TabPage In TabControl.TabPages
            Dim userTable As UserTable = CType(tab.Controls(0), UserTable)

            If userTable.CanBeUpdated Then
                userTable.UpdateTable()
            End If

            If TabControl.SelectedTab Is tab Then
                SetCurrentUserTable(userTable)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Closes the current sesion and forces the user to connect again to the server.
    ''' </summary>
    Private Sub CloseSession()
        cbDatabases.Items.Clear()
        lbTableList.Items.Clear()
        btnDisconnect.Enabled = False
        btnConnect.Enabled = True
        btnSubmit.Enabled = False
        btnExecute.Enabled = False
        cbDatabases.Enabled = False
        lblConnStatus.Text = "Disconnected"
        lblConnStatus.ForeColor = Color.IndianRed
        ClearDgv()

        Log("Connection closed.")
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

        cbDatabases.Items.Clear()
        lbTableList.Items.Clear()
        btnSubmit.Enabled = False
        ClearDgv()

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
        Dim tableName As String = CType(TabControl.SelectedTab.Controls(0), UserTable).TableName
        Dim databaseName As String = CType(TabControl.SelectedTab.Controls(0), UserTable).DatabaseName
        If IsNothing(databaseName) Or IsNothing(tableName) Then Return

        If CurrentTable.GetChanges() IsNot Nothing Then
            Dim dr As DialogResult = MessageBox.Show("Are you sure to submit changes?", "Submit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

            If dr = DialogResult.Yes Then
                CurrentDGV.EndEdit()

                Try
                    DatabaseLogic.UpdateDataTable(tableName, databaseName, CurrentTable)
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
        If Not String.IsNullOrEmpty(MyScintilla.Text) Then
            Dim sqlCode As String = IIf(MyScintilla.SelectedText.Length > 0, MyScintilla.SelectedText, MyScintilla.Text)
            Dim databaseName As String = cbDatabases.SelectedItem?.ToString()

            If MyScintilla.Text.IndexOf("USE", 0, StringComparison.CurrentCultureIgnoreCase) = -1 And Not IsNothing(databaseName) Then
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
                UpdateTabs()
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
        If CurrentDGV IsNot Nothing AndAlso CurrentDGV.RowCount > 0 Then
            Dim sfd As New SaveFileDialog With {
                    .Filter = "CSV File(*.csv)|*.csv|XML File(*.xml)|*.xml",
                    .Title = "Export Table As...",
                    .FileName = "My Table"
            }

            If sfd.ShowDialog() = DialogResult.OK Then
                Select Case sfd.FilterIndex
                    Case 1 ' CSV
                        FileLogic.ToCsv(CurrentDGV, sfd.FileName)
                    Case 2 ' XML
                        FileLogic.ToXml(CurrentTable, sfd.FileName)
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
            FileLogic.ToSql(MyScintilla.Text, sfd.FileName)
        End If
    End Sub

#End Region

#Region "XPath"

    ''' <summary>
    ''' Executes the XPath expression written by the user.
    ''' </summary>
    Private Sub ExecuteXpath()
        If CurrentDGV IsNot Nothing AndAlso CurrentDGV.RowCount > 0 Then
            Try
                Dim sw = New StringWriter()
                CurrentTable.TableName = "Item"
                CurrentTable.WriteXml(sw, XmlWriteMode.IgnoreSchema)
                CurrentTable.TableName = ""

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

    Private Sub CbDatabases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDatabases.SelectedIndexChanged
        btnSubmit.Enabled = False
        lbTableList.Items.Clear()
        ClearDgv()
        FillTablesListBox()
    End Sub

    Private Sub TableList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbTableList.SelectedIndexChanged
        If Not _ignoreTableListIndexChangedEvent Then
            _ignoreTabControlSelectedEvent = True
            SetSelectedTable()
            _ignoreTabControlSelectedEvent = False
        End If
    End Sub

    Private Sub TabControl_Selected(sender As Object, e As TabControlEventArgs) Handles TabControl.Selected
        Dim index As Integer = TabControl.SelectedIndex
        _ignoreTableListIndexChangedEvent = True

        If index <> -1 And Not _ignoreTabControlSelectedEvent Then
            Dim userTable As UserTable = CType(TabControl.TabPages(index).Controls(0), UserTable)

            SetCurrentUserTable(userTable)
            btnSubmit.Enabled = userTable.CanBeUpdated

            If userTable.CanBeUpdated Then
                lbTableList.SelectedItem = userTable.TableName
            Else
                lbTableList.SelectedIndex = -1
            End If
        End If

        _ignoreTableListIndexChangedEvent = False
    End Sub

    Private Sub BtnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        SubmitChanges()
    End Sub

    Private Sub BtnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        ExecuteCode()
    End Sub

    Private Sub Scintilla_CharAdded(sender As Object, e As CharAddedEventArgs) Handles MyScintilla.CharAdded
        Dim currentPos = MyScintilla.CurrentPosition

        Select Case Chr(e.Char)
            Case "("
                MyScintilla.InsertText(currentPos, ")")
            Case "{"
                MyScintilla.InsertText(currentPos, "}")
            Case "["
                MyScintilla.InsertText(currentPos, "]")
            Case Chr(34).ToString
                MyScintilla.InsertText(currentPos, Chr(34).ToString)
            Case "'"
                MyScintilla.InsertText(currentPos, "'")
        End Select
    End Sub

    Private Sub Scintilla_ZoomChanged(sender As Object, e As EventArgs) Handles MyScintilla.ZoomChanged
        Dim zoom As Integer = MyScintilla.Zoom * 2
        MyScintilla.Margins(0).Width = 50 + zoom
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
        MyScintilla.Undo()
    End Sub

    Private Sub BtnRedo_Click(sender As Object, e As EventArgs) Handles btnRedo.Click
        MyScintilla.Redo()
    End Sub

    Private Sub BtnCut_Click(sender As Object, e As EventArgs) Handles btnCut.Click
        MyScintilla.Cut()
    End Sub

    Private Sub BtnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        MyScintilla.Copy()
    End Sub

    Private Sub BtnPaste_Click(sender As Object, e As EventArgs) Handles btnPaste.Click
        MyScintilla.Paste()
    End Sub

    Private Sub BtnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        MyScintilla.SelectAll()
    End Sub

    Private Sub BtnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        MyFindReplace.ShowFind()
    End Sub

    Private Sub BtnReplace_Click(sender As Object, e As EventArgs) Handles btnReplace.Click
        MyFindReplace.ShowReplace()
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Export()
    End Sub

    Private Sub BtnSaveSql_Click(sender As Object, e As EventArgs) Handles btnSaveSql.Click
        SaveQuery()
    End Sub

    Private Sub BtnExecuteXpath_Click(sender As Object, e As EventArgs) Handles btnExecuteXpath.Click
        ExecuteXpath()
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

#End Region

End Class
