Imports ScintillaNET
Imports MSSQLA.UserInterface.ClassUtils
Imports ScintillaNET_FindReplaceDialog
Imports AutocompleteMenuNS

Public Class UserEditor

    Private ReadOnly _ignoreTextChangedEvent As Boolean

    Friend Property LastSavedText As String

    Friend Property Path As String

    Friend Property FileName As String

    Friend Property Zoom As Integer
        Get
            Return Scintilla.Zoom
        End Get
        Set(value As Integer)
            Scintilla.Zoom = value
        End Set
    End Property

    Friend ReadOnly Property IsSaved As Boolean
        Get
            Return LastSavedText = Scintilla.Text
        End Get
    End Property

#Region "Constructors"

    Friend Sub New(content As String)
        InitializeComponent()
        InitializeScintilla()
        InitializeAutoCompleteMenu()

        FindReplace = New FindReplace(Scintilla) With {
            .Scintilla = Scintilla
        }

        _ignoreTextChangedEvent = True
        Scintilla.Text = content
        LastSavedText = content
        _ignoreTextChangedEvent = False

        Scintilla.EmptyUndoBuffer()
    End Sub

    Friend Sub New(content As String, path As String, fileName As String)
        Me.New(content)

        Me.Path = path
        Me.FileName = fileName
    End Sub

#End Region

#Region "Private Methods"

    Private Sub InitializeScintilla()
        Dim backColor As Color = Color.FromArgb(40, 44, 52)
        Dim selectionColor As Color = Color.FromArgb(255, 38, 79, 120)

        ' Reset the styles.
        Scintilla.StyleResetDefault()
        Scintilla.Styles(Style.[Default]).Font = "Consolas"
        Scintilla.Styles(Style.[Default]).Size = 12
        Scintilla.StyleClearAll()

        ' Lexer
        Scintilla.Lexer = Lexer.Sql

        ' Margin
        Scintilla.Margins(0).Width = 50

        ' Zoom
        Scintilla.Zoom = CurrentZoom

        ' Disable whitespaces visibility.
        Scintilla.ViewWhitespace = WhitespaceMode.Invisible

        ' Set the common editor colors.
        Scintilla.CaretForeColor = Color.FromArgb(1, 77, 122, 200)
        Scintilla.CaretWidth = 2
        Scintilla.SetSelectionBackColor(True, selectionColor)

        ' Syntax highlight
        With Scintilla
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

            .Styles(Style.Sql.CommentDoc).ForeColor = Color.FromArgb(255, 87, 159, 56)
            .Styles(Style.Sql.CommentDoc).BackColor = backColor
            .Styles(Style.Sql.CommentDoc).Bold = False
            .Styles(Style.Sql.CommentDoc).Italic = True
            .Styles(Style.Sql.CommentDoc).Underline = False

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

            .Styles(Style.IndentGuide).ForeColor = Color.FromArgb(1, 149, 159, 161)
            .Styles(Style.IndentGuide).BackColor = Color.FromArgb(1, 149, 159, 161)
        End With

        ' Keywords
        Scintilla.SetKeywords(0, SQL_KEYWORDS)
        Scintilla.SetKeywords(1, SQL_FUNCTIONS)
        Scintilla.SetKeywords(4, SQL_OPERATORS)
        Scintilla.SetKeywords(5, SQL_OBJECTS)

        ' Instruct the lexer to calculate folding
        Scintilla.SetProperty("fold", "1")
        Scintilla.SetProperty("fold.compact", "1")

        ' Configure a margin to display folding symbols
        Scintilla.Margins(2).Type = MarginType.Symbol
        Scintilla.Margins(2).Mask = Marker.MaskFolders
        Scintilla.Margins(2).Sensitive = True
        Scintilla.Margins(2).Width = 13

        ' Configure folding markers with respective symbols
        Scintilla.Markers(Marker.Folder).Symbol = MarkerSymbol.BoxPlus
        Scintilla.Markers(Marker.FolderOpen).Symbol = MarkerSymbol.BoxMinus
        Scintilla.Markers(Marker.FolderEnd).Symbol = MarkerSymbol.BoxPlusConnected

        Scintilla.Markers(Marker.FolderMidTail).Symbol = MarkerSymbol.TCorner
        Scintilla.Markers(Marker.FolderOpenMid).Symbol = MarkerSymbol.BoxMinusConnected

        Scintilla.Markers(Marker.FolderSub).Symbol = MarkerSymbol.VLine
        Scintilla.Markers(Marker.FolderTail).Symbol = MarkerSymbol.LCorner

        ' Set colors for all folding markers
        For i As Integer = Marker.FolderEnd To Marker.FolderOpen
            Scintilla.Markers(i).SetForeColor(backColor)
            Scintilla.Markers(i).SetBackColor(Color.FromArgb(1, 149, 159, 161))
        Next i

        ' Enable automatic folding
        Scintilla.AutomaticFold = (AutomaticFold.Show Or AutomaticFold.Click Or AutomaticFold.Change)

        Scintilla.SetFoldMarginColor(True, backColor)
        Scintilla.SetFoldMarginHighlightColor(True, backColor)

        Scintilla.SetFoldMarginColor(True, backColor)
        Scintilla.SetFoldMarginHighlightColor(True, backColor)
        Scintilla.IndentationGuides = IndentView.LookBoth
        Scintilla.Select()
    End Sub

    Private Sub InitializeAutoCompleteMenu()
        MyAutocompleteMenu.TargetControlWrapper = New ScintillaWrapper(Scintilla)

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

#End Region

#Region "Friend Methods"

    Friend Function GetText() As String
        Return Scintilla.Text
    End Function

    Friend Function GetSelectedText() As String
        Return Scintilla.SelectedText
    End Function

    Friend Sub Undo()
        Scintilla.Undo()
    End Sub

    Friend Sub Redo()
        Scintilla.Redo()
    End Sub

    Friend Sub Cut()
        Scintilla.Cut()
    End Sub

    Friend Sub Copy()
        Scintilla.Copy()
    End Sub

    Friend Sub Paste()
        Scintilla.Paste()
    End Sub

    Friend Sub SelectAll()
        Scintilla.SelectAll()
    End Sub

    Friend Sub ShowFind()
        FindReplace.ShowFind()
    End Sub

    Friend Sub ShowReplace()
        FindReplace.ShowReplace()
    End Sub

#End Region

#Region "Events"

    Private Sub Scintilla_CharAdded(sender As Object, e As CharAddedEventArgs) Handles Scintilla.CharAdded
        Dim currentPos = Scintilla.CurrentPosition

        Select Case Chr(e.Char)
            Case "("
                Scintilla.InsertText(currentPos, ")")
            Case "{"
                Scintilla.InsertText(currentPos, "}")
            Case "["
                Scintilla.InsertText(currentPos, "]")
            Case Chr(34).ToString
                Scintilla.InsertText(currentPos, Chr(34).ToString)
            Case "'"
                Scintilla.InsertText(currentPos, "'")
        End Select
    End Sub

    Private Sub Scintilla_ZoomChanged(sender As Object, e As EventArgs) Handles Scintilla.ZoomChanged
        Dim zoom As Integer = Scintilla.Zoom * 2
        Scintilla.Margins(0).Width = 50 + zoom

        CurrentZoom = Scintilla.Zoom
    End Sub

    Public Shared Event TextModified As EventHandler

    Private Sub Scintilla_TextChanged(sender As Object, e As EventArgs) Handles Scintilla.TextChanged
        If Not _ignoreTextChangedEvent Then
            RaiseEvent TextModified(sender, e)
        End If
    End Sub

#End Region

End Class
