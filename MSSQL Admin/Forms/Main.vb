Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Xml
Imports AutocompleteMenuNS
Imports ScintillaNET
Imports ScintillaNET_FindReplaceDialog

Public Class MainForm
    Private Const SQL_KEYWORDS As String = "add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go "
    Private Const SQL_OPERATORS As String = "all and any between cross exists in inner is join left like not null or outer pivot right some unpivot "
    Private Const SQL_FUNCTIONS As String = "ascii char charindex concat concat_ws datalength difference format left len lower ltrim nchar patindex quotename replace replicate reverse right rtrim soundex space str stuff substring translate trim unicode upper abs acos asin atan atn2 avg ceiling count cos cot degrees exp floor log log10 max min pi power radians rand round sign sin sqrt square sum tan current_timestamp dateadd datediff datefromparts datename datepart day getdate getutcdate isdate month sysdatetime year cast coalesce convert current_user iif isnull isnumeric nullif session_user sessionproperty system_user user_name "
    Private Const SQL_OBJECTS As String = "sys objects sysobjects "

    Private ReadOnly Property MyConnection As Connection = Connection.Instance
    Private Property ConnectionString As String = "Connection Timeout=5;"
    Private Property DataAdapter As SqlDataAdapter
    Private Property DataTable As DataTable

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeScintilla()
        LoadConnectionSettings()
        LoadAutoCompleteMenu()

        MyMenuStrip.Renderer = New ToolStripProfessionalRenderer(New MenuColorTable())

        MyFindReplace = New FindReplace(MyScintilla) With {
            .Scintilla = MyScintilla
        }

        MyToolStrip.RenderMode = ToolStripRenderMode.System
        MyToolStrip.Renderer = New ToolStripOverride()

        Log("Welcome " & Environment.UserName & ".")
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

        With MyScintilla
            .Styles(Style.Default).BackColor = backColor

            .Styles(Style.LineNumber).ForeColor = Color.FromArgb(1, 149, 159, 161)
            .Styles(Style.LineNumber).BackColor = backColor

            .Styles(Style.Sql.Default).ForeColor = Color.Gainsboro
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

            ' scintilla.SetKeywords(0,
            .Styles(Style.Sql.Word).ForeColor = Color.FromArgb(255, 100, 150, 215)
            .Styles(Style.Sql.Word).BackColor = backColor
            .Styles(Style.Sql.Word).Bold = False
            .Styles(Style.Sql.Word).Italic = False
            .Styles(Style.Sql.Word).Underline = False

            ' scintilla.SetKeywords(1,
            .Styles(Style.Sql.Word2).ForeColor = Color.HotPink
            .Styles(Style.Sql.Word2).BackColor = backColor
            .Styles(Style.Sql.Word2).Bold = False
            .Styles(Style.Sql.Word2).Italic = False
            .Styles(Style.Sql.Word2).Underline = False

            ' scintilla.SetKeywords(4
            .Styles(Style.Sql.User1).ForeColor = Color.FromArgb(255, 62, 201, 174)
            .Styles(Style.Sql.User1).BackColor = backColor
            .Styles(Style.Sql.User1).Bold = False
            .Styles(Style.Sql.User1).Italic = False
            .Styles(Style.Sql.User1).Underline = False

            ' scintilla.SetKeywords(5,
            .Styles(Style.Sql.User2).ForeColor = Color.IndianRed
            .Styles(Style.Sql.User2).BackColor = backColor
            .Styles(Style.Sql.User2).Bold = False
            .Styles(Style.Sql.User2).Italic = False
            .Styles(Style.Sql.User2).Underline = False

            .Styles(Style.Sql.Identifier).BackColor = backColor
            .Styles(Style.Sql.Identifier).ForeColor = Color.Gainsboro
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
    ''' Loads the last credentials used.
    ''' </summary>
    Private Sub LoadConnectionSettings()
        tbServer.Text = My.Settings.Server
        tbUser.Text = My.Settings.User
        tbPass.Text = My.Settings.Pass
    End Sub

    ''' <summary>
    ''' Fills the autocompletemenu.
    ''' </summary>
    Private Sub LoadAutoCompleteMenu()
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
                                                  MyAutocompleteMenu.AddItem(New SnippetAutocompleteItem(item) With {.ImageIndex = 3, .ToolTipTitle = "Objects"})
                                                  Return True
                                              End Function)
    End Sub

    ''' <summary>
    ''' Connects to the server.
    ''' </summary>
    Private Sub ConnectToServer()
        Dim server As String = tbServer.Text
        Dim user As String = tbUser.Text
        Dim pass As String = tbPass.Text

        ConnectionString &= "Server=" & server & ";"
        ConnectionString &= "User Id=" & user & ";"
        ConnectionString &= "Password=" & pass & ";"

        cbDatabases.Items.Clear()
        lbTableList.Items.Clear()
        btnSubmit.Enabled = False
        ClearDgv()

        Try
            MyConnection.Open(ConnectionString)

            My.Settings.Server = server
            My.Settings.User = user
            My.Settings.Pass = pass
            My.Settings.Save()

            btnExecute.Enabled = True
            btnDisconnect.Enabled = True
            cbDatabases.Enabled = True
            lblConnStatus.Text = "Connected"
            lblConnStatus.ForeColor = Color.PaleGreen
            Log("Connection established.")
            RetrieveDatabases()
        Catch ex As Exception
            btnExecute.Enabled = False
            btnDisconnect.Enabled = False
            cbDatabases.Enabled = False
            lblConnStatus.Text = "Disconnected"
            lblConnStatus.ForeColor = Color.IndianRed
            Log(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Closes the current connection.
    ''' </summary>
    Private Sub DisconnectFromServer()
        Try
            MyConnection?.Close()
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
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Gets all databases in the server.
    ''' </summary>
    Private Sub RetrieveDatabases()
        Try
            Dim databaseList As New List(Of String)()

            Using cmd As SqlCommand = New SqlCommand("SELECT name FROM master.sys.databases", MyConnection.SqlConn)
                Using reader As SqlDataReader = cmd.ExecuteReader()

                    While (reader.Read())
                        databaseList.Add(reader.GetString(0))
                    End While
                    reader.Close()

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
                End Using
            End Using
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Gets all tables in the current selected database.
    ''' </summary>
    Private Sub RetrieveTables()
        Dim databaseName As String = cbDatabases.SelectedItem?.ToString
        If IsNothing(databaseName) Then Return

        Try
            Dim tableList As New List(Of String)()

            Using cmd As SqlCommand = New SqlCommand("SELECT TABLE_NAME FROM " & databaseName & ".INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME <> 'sysdiagrams'", MyConnection.SqlConn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While (reader.Read())
                        tableList.Add(reader.GetString(0))
                    End While
                    reader.Close()

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
                End Using
            End Using

            Using cmd As New SqlCommand("USE " & databaseName, MyConnection.SqlConn)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Log(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Sets the current table to be shown on the DGV.
    ''' </summary>
    Private Sub SetSelectedTable()
        Dim tableName As String = lbTableList.SelectedItem?.ToString
        If IsNothing(tableName) Then Return

        Try
            Using cmd As New SqlCommand("SELECT * FROM " & tableName, MyConnection.SqlConn)
                Dim ds As New DataSet
                DataAdapter = New SqlDataAdapter(cmd)
                DataTable = New DataTable

                DataAdapter.Fill(ds, tableName)
                DataAdapter.Fill(DataTable)
                dgv.DataSource = DataTable
                dgv.Refresh()
                btnSubmit.Enabled = True
            End Using
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Submits all changes on the DGV to the server.
    ''' </summary>
    Private Sub SubmitFromDgv()
        If DataTable IsNot Nothing And lbTableList.SelectedIndex <> -1 Then
            Dim dr As DialogResult = MessageBox.Show("Are you sure to submit changes?", "Submit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

            If dr = DialogResult.Yes Then
                dgv.EndEdit()

                Try
                    Dim cmdBuilder As New SqlCommandBuilder(DataAdapter)
                    Dim changes = DataTable.GetChanges()

                    If changes IsNot Nothing Then
                        DataAdapter.Update(changes)
                    End If
                    Log("Changes submitted to server.")
                Catch ex As Exception
                    Log(ex.Message)
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Executes the code written by user on the server.
    ''' </summary>
    Private Sub ExecuteSqlCode()
        If Not MyConnection.IsOpen Then Return

        If Not String.IsNullOrEmpty(MyScintilla.Text) Then
            Using cmd As New SqlCommand(MyScintilla.Text, MyConnection.SqlConn)
                Try
                    Dim reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        DataTable = New DataTable
                        DataTable.Load(reader)
                        dgv.DataSource = DataTable
                        dgv.Refresh()

                        btnSubmit.Enabled = False
                        lbTableList.SelectedIndex = -1
                    End If

                    Dim recordsAffected = reader.RecordsAffected
                    If recordsAffected = -1 Then recordsAffected = 0
                    Log("Execution succesful. " & recordsAffected & " record(s) affected.")
                    reader.Close()

                    RetrieveDatabases()
                    RetrieveTables()
                    SetSelectedTable()
                Catch ex As Exception
                    Log(ex.Message)
                End Try
            End Using
        Else
            Log("Nothing to execute.")
        End If
    End Sub

    ''' <summary>
    ''' Exports the current table shown in the DGV.
    ''' </summary>
    Private Sub ExportTo()
        If dgv.RowCount > 0 Then
            Dim sfd As New SaveFileDialog With {
                    .Filter = "CSV File(*.csv)|*.csv|XML File(*.xml)|*.xml",
                    .Title = "Export Table As...",
                    .FileName = "My Table"
            }

            If sfd.ShowDialog() = DialogResult.OK Then
                Select Case sfd.FilterIndex
                    Case 1 ' CSV
                        Dim columnCount As Integer = dgv.Columns.Count - 1
                        Dim rowCount As Integer = dgv.Rows.Count - 1
                        Dim columnNames As String = ""
                        Dim outputCsv As New List(Of String)

                        For c = 0 To columnCount
                            columnNames += dgv.Columns(c).HeaderText.ToString() + ","
                        Next
                        outputCsv.Add(columnNames)

                        For i = 0 To rowCount - 1
                            Dim rowItems As String = ""

                            For j = 0 To columnCount
                                rowItems += dgv.Rows(i).Cells(j).Value.ToString() + ","
                            Next

                            outputCsv.Add(rowItems)
                        Next

                        File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8)
                    Case 2 ' XML
                        Dim sw = New StringWriter()
                        DataTable.TableName = "Item"
                        DataTable.WriteXml(sw, XmlWriteMode.IgnoreSchema)
                        DataTable.TableName = ""

                        File.WriteAllText(sfd.FileName, sw.ToString())
                End Select
            End If
        Else
            Log("Nothing to export.")
        End If
    End Sub

    ''' <summary>
    ''' Saves the written query as SQL.
    ''' </summary>
    Private Sub SaveQuery()
        Dim sfd As New SaveFileDialog With {
                .Filter = "SQL File(*.sql)|*.sql",
                .Title = "Save query",
                .FileName = "query.sql"
        }

        If sfd.ShowDialog() = DialogResult.OK Then
            File.WriteAllText(sfd.FileName, MyScintilla.Text, Encoding.UTF8)
        End If
    End Sub

    ''' <summary>
    ''' Executes the XPath expression written by the user.
    ''' </summary>
    Private Sub ExecuteXpath()
        If dgv.RowCount > 0 Then
            Try
                Dim sw = New StringWriter()
                DataTable.TableName = "Item"
                DataTable.WriteXml(sw, XmlWriteMode.IgnoreSchema)
                DataTable.TableName = ""

                Dim xmlDoc As New XmlDocument
                xmlDoc.LoadXml(sw.ToString)

                Dim nodeList As XmlNodeList = xmlDoc.DocumentElement.SelectNodes(xpathExpression.Text)
                xpathTextBox.Text = ""

                If nodeList.Count > 0 Then
                    For Each node As XmlNode In nodeList
                        xpathTextBox.AppendText(node.OuterXml & vbNewLine)
                    Next
                Else
                    xpathTextBox.AppendText("No matches found." & vbNewLine)
                End If
            Catch ex As Exception
                xpathTextBox.Text = ""
                xpathTextBox.AppendText("Wrong XPath expression." & vbNewLine)
            End Try
        Else
            Log("Table is empty.")
        End If
    End Sub

    ''' <summary>
    ''' Writes on the console.
    ''' </summary>
    ''' <param name="str">Message to log.</param>
    Private Sub Log(str As String)
        outputTextBox.AppendText("> " & DateTime.Now.TimeOfDay.ToString("hh\:mm\:ss") & " " & str & vbNewLine)
    End Sub

    ''' <summary>
    ''' Empties the DGV.
    ''' </summary>
    Private Sub ClearDgv()
        DataTable = Nothing
        dgv.DataSource = DataTable
        dgv.Refresh()
    End Sub

    Private Sub BtnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        ConnectToServer()
    End Sub

    Private Sub BtnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
        DisconnectFromServer()
    End Sub

    Private Sub CbDatabases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDatabases.SelectedIndexChanged
        btnSubmit.Enabled = False
        lbTableList.Items.Clear()
        ClearDgv()
        RetrieveTables()
    End Sub

    Private Sub TableList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbTableList.SelectedIndexChanged
        SetSelectedTable()
    End Sub

    Private Sub Dgv_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgv.DataError
        Log(e.Exception.Message)
    End Sub

    Private Sub BtnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        SubmitFromDgv()
    End Sub

    Private Sub BtnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        ExecuteSqlCode()
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

            outputTextBox.Visible = True
            xpathEvaluatorPanel.Visible = False
            xpathTextBox.Visible = False
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

            outputTextBox.Visible = False
            xpathEvaluatorPanel.Visible = True
            xpathEvaluatorPanel.Select()
            xpathTextBox.Visible = True
            splitter1.Visible = True
            bottomPanel.Visible = True
        Else
            splitter1.Visible = False
            bottomPanel.Visible = False
        End If
    End Sub

    Private Sub BtnClearOutput_Click(sender As Object, e As EventArgs) Handles btnClearOutput.Click
        outputTextBox.Text = ""
    End Sub

    Private Sub btnClearXpath_Click(sender As Object, e As EventArgs) Handles btnClearXpath.Click
        xpathExpression.Text = ""
        xpathTextBox.Text = ""
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Try
            MyConnection?.Close()
        Catch
        Finally
            End
        End Try
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
        ExportTo()
    End Sub

    Private Sub BtnSaveSql_Click(sender As Object, e As EventArgs) Handles btnSaveSql.Click
        SaveQuery()
    End Sub

    Public Class ToolStripOverride
        Inherits ToolStripSystemRenderer

        Protected Overrides Sub OnRenderToolStripBorder(e As ToolStripRenderEventArgs)

        End Sub

    End Class

    Class MenuColorTable
        Inherits ProfessionalColorTable

        Public Sub New()
            MyBase.UseSystemColors = False
        End Sub

        Public Overrides ReadOnly Property MenuBorder As Color
            Get
                Return Color.Empty
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemBorder As Color
            Get
                Return Color.FromArgb(44, 49, 59)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
            Get
                Return Color.FromArgb(44, 49, 59)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
            Get
                Return Color.FromArgb(44, 49, 59)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemPressedGradientBegin As Color
            Get
                Return Color.FromArgb(44, 49, 59)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemPressedGradientMiddle As Color
            Get
                Return Color.FromArgb(44, 49, 59)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemPressedGradientEnd As Color
            Get
                Return Color.FromArgb(44, 49, 59)
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
            Get
                Return Color.FromArgb(53, 59, 67)
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color
            Get
                Return Color.FromArgb(53, 59, 67)
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientEnd As Color
            Get
                Return Color.FromArgb(53, 59, 67)
            End Get
        End Property

        Public Overrides ReadOnly Property ToolStripDropDownBackground As Color
            Get
                Return Color.FromArgb(53, 59, 67)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelected As Color
            Get
                Return Color.FromArgb(44, 49, 59)
            End Get
        End Property

    End Class

    Private Sub btnExecuteXpath_Click(sender As Object, e As EventArgs) Handles btnExecuteXpath.Click
        ExecuteXpath()
    End Sub

End Class
