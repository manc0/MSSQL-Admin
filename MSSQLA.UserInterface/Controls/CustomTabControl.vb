Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class CustomTabControl
    Inherits TabControl

    Private Const TabTextOffset As String = "      "

    Private Const TabTextOffsetWithAsterik As String = "*     "

#Region "Constructor"

    Public Sub New()
        MyBase.New()

        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.DoubleBuffer Or
                 ControlStyles.ResizeRedraw Or
                 ControlStyles.UserPaint, True)
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Occurs when the add button is clicked.
    ''' </summary>
    Public Event OnAddButtonClick()

    ''' <summary>
    ''' Occurs when a tab is closed.
    ''' </summary>
    Public Event OnTabClose As EventHandler

    ''' <summary>
    ''' Occurs when a new tab is added.
    ''' </summary>
    Public Event OnNewTab()

    ''' <summary>
    ''' Event Args used during the OnTabClose event.
    ''' Index will return -1 if the tab collection was cleared.
    ''' </summary>
    Public Class CloseTabEventArgs
        Inherits EventArgs

        Public Property Cancel As Boolean
        Public ReadOnly Property Index As Integer

        Public Sub New(index As Integer)
            Me.Index = index
        End Sub

    End Class

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Adds a new tab and raises the OnNewTab event.
    ''' </summary>
    ''' <param name="tab">Tab to close.</param>
    ''' <param name="isAddButton">Check this if the given tab is the add button.</param>
    Public Sub AddTab(tab As TabPage, Optional isAddButton As Boolean = False)
        If Not isAddButton Then
            tab.Text &= TabTextOffset
            RaiseEvent OnNewTab()
        End If

        If HasAddButton And Not isAddButton Then
            TabPages.Insert(LastIndex, tab)
        Else
            TabPages.Add(tab)
        End If
    End Sub

    ''' <summary>
    ''' Closes the specified tab and selects the next one. Raises the OnTabClose event.
    ''' </summary>
    ''' <param name="index">Index of the tab to clsoe.</param>
    Public Sub CloseTabAt(index As Integer)
        Dim closeTabEventArs As New CloseTabEventArgs(index)
        RaiseEvent OnTabClose(Me, closeTabEventArs)

        If Not closeTabEventArs.Cancel Then
            If TabCount > 0 Then
                Try
                    If HasAddButton And index = LastIndex - 1 Then
                        SelectTab(index - 1)
                    Else
                        SelectTab(index + 1)
                    End If
                Catch
                    Try
                        SelectTab(index - 1)
                    Catch
                    End Try
                Finally
                    TabPages.RemoveAt(index)
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Clears the tab collection and raises the event OnTabClose.
    ''' </summary>
    Public Sub Clear()
        TabPages.Clear()
        If HasAddButton Then
            CreateAddButtonTab()
        End If

        RaiseEvent OnTabClose(Nothing, New CloseTabEventArgs(-1))
    End Sub

    ''' <summary>
    ''' Modifies the name and text of the given tab.
    ''' </summary>
    ''' <param name="tab">Tab to modify.</param>
    ''' <param name="name">New name and text.</param>
    ''' <param name="edited">Indicates wether the tab has been edited or not.</param>
    Public Sub EditTabNameAndText(tab As TabPage, name As String, edited As Boolean)
        tab.Name = name
        tab.Text = name & IIf(edited, TabTextOffsetWithAsterik, TabTextOffset)
    End Sub

    ''' <summary>
    ''' Adds or removes an asterisk at the end of the given tab's text to indicate if it has been modified in some way.
    ''' </summary>
    ''' <param name="tab">The tab to modify.</param>
    ''' <param name="edited">Indicates wether the tab has been edited or not.</param>
    Public Sub SetTabEditMode(tab As TabPage, edited As Boolean)
        If (HasAddButton And TabPages.IndexOf(tab) < LastIndex) Or Not HasAddButton Then
            If edited And Not tab.Text.Contains(TabTextOffsetWithAsterik) Then
                tab.Text = tab.Text.Replace(TabTextOffset, TabTextOffsetWithAsterik)
            ElseIf Not edited And tab.Text.Contains(TabTextOffsetWithAsterik) Then
                Dim asteriskIndex As Integer = tab.Text.LastIndexOf(TabTextOffsetWithAsterik)
                tab.Text = tab.Text.Replace(TabTextOffsetWithAsterik, TabTextOffset)
            End If
        End If
    End Sub

#End Region

#Region "Properties"

    Private _backcolor As Color = Color.Empty
    Private _hotTabIndex As Integer = -1
    Public _createAddButton As Boolean
    Private _selectedTabColor As Color = Color.Empty
    Private _hotTabColor As Color = Color.Empty
    Private _defaultTabColor As Color = Color.Empty
    Private _tabForeColor As Color = Color.White

    <Browsable(True),
    Description("The background color used to display text and graphics in a control.")>
    Public Overrides Property BackColor() As Color
        Get
            If _backcolor.Equals(Color.Empty) Then
                If Parent Is Nothing Then
                    Return Control.DefaultBackColor
                Else
                    Return Parent.BackColor
                End If
            End If
            Return _backcolor
        End Get
        Set(Value As Color)
            If _backcolor.Equals(Value) Then Return
            _backcolor = Value
            Invalidate()
            'Let the Tabpages know that the backcolor has changed.
            MyBase.OnBackColorChanged(EventArgs.Empty)
        End Set
    End Property

    <Browsable(True),
    Description("The background color of the selected tab.")>
    Public Property SelectedTabColor() As Color
        Get
            Return _selectedTabColor
        End Get
        Set(Value As Color)
            _selectedTabColor = Value
        End Set
    End Property

    <Browsable(True),
    Description("The background color of the hot tab.")>
    Public Property HotTabColor() As Color
        Get
            Return _hotTabColor
        End Get
        Set(Value As Color)
            _hotTabColor = Value
        End Set
    End Property

    <Browsable(True),
    Description("The default background color of tabs.")>
    Public Property DefaultTabColor() As Color
        Get
            Return _defaultTabColor
        End Get
        Set(Value As Color)
            _defaultTabColor = Value
        End Set
    End Property

    <Browsable(True),
    Description("The color to draw the title of the tab.")>
    Public Property TabForeColor() As Color
        Get
            Return _tabForeColor
        End Get
        Set(Value As Color)
            _tabForeColor = Value
        End Set
    End Property

    <Browsable(True),
    Description("Creates a button to add new tabs at the end of this TabControl.")>
    Public Property HasAddButton() As Boolean
        Get
            Return _createAddButton
        End Get
        Set(value As Boolean)
            _createAddButton = value

            If value And Not Me.DesignMode Then
                CreateAddButtonTab()
            End If
        End Set
    End Property

    Private ReadOnly Property CloseButtonHeight As Integer
        Get
            Return FontHeight
        End Get
    End Property

    Private ReadOnly Property LastIndex As Integer
        Get
            Return TabPages.Count - 1
        End Get
    End Property

    Private Property HotTabIndex As Integer
        Get
            Return _hotTabIndex
        End Get
        Set(value As Int32)
            If _hotTabIndex <> value Then
                _hotTabIndex = value
                Me.Invalidate()
            End If
        End Set
    End Property

#End Region

#Region "Overridden Methods"

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        If TabCount <= 0 Then Return
        e.Graphics.Clear(BackColor)

        Dim sf As New StringFormat With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }
        Dim brush = New SolidBrush(Color.Empty)

        'Draw the Tabs
        For index As Integer = 0 To TabCount - 1
            Dim r As Rectangle = GetTabRect(index)
            Dim tab As TabPage = TabPages(index)
            Dim bs As ButtonBorderStyle = ButtonBorderStyle.Solid

            If index = SelectedIndex Then
                brush = New SolidBrush(SelectedTabColor)
            ElseIf index = HotTabIndex Then
                brush = New SolidBrush(HotTabColor)
            Else
                brush = New SolidBrush(DefaultTabColor)
            End If

            'Draw a border around TabPage
            Dim topRect = TabPages(index).Bounds
            Dim borderBrush = New SolidBrush(SelectedTabColor)
            topRect.Inflate(2, 2)

            e.Graphics.FillRectangle(borderBrush, topRect)
            ControlPaint.DrawBorder(e.Graphics, topRect, borderBrush.Color, bs)

            e.Graphics.FillRectangle(brush, r)
            ControlPaint.DrawBorder(e.Graphics, r,
                Color.Empty, 0, ButtonBorderStyle.None,
                Color.Empty, 0, ButtonBorderStyle.None,
                Color.Empty, 0, ButtonBorderStyle.None,
                borderBrush.Color, 4, ButtonBorderStyle.Solid)

            'Draw the text
            Dim rText As Rectangle = r
            If index = LastIndex And HasAddButton Then
                rText.Offset(0, -5)
                DrawText(tab, rText, e.Graphics, brush)
            Else
                rText.Offset(0, -2)
                DrawText(tab, rText, e.Graphics, brush)
            End If

            If (HasAddButton And index < TabPages.Count - 1) Or Not HasAddButton Then
                DrawCloseButton(r, e.Graphics, index)
            End If

            e.Graphics.ResetTransform()
        Next

        brush.Dispose()
        MyBase.OnPaint(e)
    End Sub

    Private Structure RECT
        Public Left, Top, Right, Bottom As Integer
    End Structure

    Protected Overrides Sub WndProc(ByRef m As Message)
        ' Remove borders
        If m.Msg = &H1300 + 40 Then
            Dim rc As RECT = m.GetLParam(GetType(RECT))
            rc.Left -= 4
            rc.Right += 4
            rc.Top -= 4
            rc.Bottom += 4
            Marshal.StructureToPtr(rc, m.LParam, True)
        End If

        If m.Msg = TCM_SETPADDING Then
            m.LParam = MakeLParam(Me.Padding.X + CloseButtonHeight \ 2, Me.Padding.Y)
        End If
        If m.Msg = WM_MOUSEDOWN AndAlso Not Me.DesignMode Then
            If (HasAddButton And HotTabIndex < TabPages.Count - 1) Or Not HasAddButton Then
                Dim pt As Point = Me.PointToClient(Cursor.Position)
                Dim closeRect As Rectangle = GetCloseButtonRect(HotTabIndex)
                If closeRect.Contains(pt) Then
                    CloseTabAt(HotTabIndex)
                    m.Msg = WM_NULL
                End If
            End If
        End If

        MyBase.WndProc(m)
    End Sub

    Public Overrides Sub ResetBackColor()
        _backcolor = Color.Empty
        Invalidate()
    End Sub

    Protected Overrides Sub OnParentBackColorChanged(e As EventArgs)
        MyBase.OnParentBackColorChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnSelectedIndexChanged(e As EventArgs)
        MyBase.OnSelectedIndexChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)

        If GetTabRect(LastIndex).Contains(e.Location) And HasAddButton Then
            RaiseEvent OnAddButtonClick()
        End If
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        Dim HTI As New TCHITTESTINFO(e.X, e.Y)
        HotTabIndex = SendMessage(Me.Handle, TCM_HITTEST, IntPtr.Zero, HTI)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        HotTabIndex = -1
    End Sub

    Protected Overrides Sub OnSelecting(e As TabControlCancelEventArgs)
        If e.TabPageIndex = LastIndex And HasAddButton And TabPages.Count > 1 Then
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "Private Methods"

    Private Sub CreateAddButtonTab()
        Dim tab As New TabPage() With {
            .Text = "+",
            .Name = "+Tab"
        }
        tab.Font = New Font(Font.FontFamily, 16, FontStyle.Bold)

        AddTab(tab, True)
    End Sub

    Private Sub DrawText(tab As TabPage, tabRect As Rectangle, graphics As Graphics, brush As SolidBrush)
        Dim DrawFont As New Font(tab.Font.FontFamily, tab.Font.Size, FontStyle.Regular)
        brush.Color = IIf(tab.Enabled, Color.White, Color.DarkGray)
        TextRenderer.DrawText(graphics, tab.Text, DrawFont, tabRect, TabForeColor, Color.Transparent, TextFormatFlags.HorizontalCenter Or TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine Or TextFormatFlags.VerticalCenter)
    End Sub

    Private Sub DrawCloseButton(tabRect As Rectangle, graphics As Graphics, index As Integer)
        Dim selectedOrHot As Boolean = index = SelectedIndex OrElse index = HotTabIndex
        Dim contentRect As Rectangle = New Rectangle(Point.Empty, tabRect.Size)

        Using bm As Bitmap = New Bitmap(contentRect.Width, contentRect.Height)
            Using bmGraphics As Graphics = Graphics.FromImage(bm)
                If selectedOrHot Then
                    Dim closeRect As Rectangle = New Rectangle(contentRect.Right - CloseButtonHeight, 0, CloseButtonHeight, CloseButtonHeight)
                    closeRect.Offset(-2, (contentRect.Height - closeRect.Height) \ 2)

                    Dim brush As SolidBrush = New SolidBrush(IIf(index = SelectedIndex, SelectedTabColor, HotTabColor))
                    Dim closeFont As Font = New Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular)

                    bmGraphics.FillRectangle(brush, closeRect)
                    TextRenderer.DrawText(bmGraphics, "✖", closeFont, closeRect, TabForeColor, brush.Color, TextFormatFlags.HorizontalCenter Or TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine Or TextFormatFlags.VerticalCenter)
                End If
            End Using

            graphics.DrawImage(bm, tabRect)
        End Using
    End Sub

    Private Function GetCloseButtonRect(index As Integer) As Rectangle
        Dim tabRect As Rectangle = GetTabRect(index)
        Dim closeRect As Rectangle = New Rectangle(tabRect.Left, tabRect.Top, CloseButtonHeight, CloseButtonHeight)

        Select Case Alignment
            Case TabAlignment.Left
                closeRect.Offset((tabRect.Width - closeRect.Width) \ 2, 0)
            Case TabAlignment.Right
                closeRect.Offset((tabRect.Width - closeRect.Width) \ 2, tabRect.Height - closeRect.Height)
            Case Else
                closeRect.Offset(tabRect.Width - closeRect.Width, (tabRect.Height - closeRect.Height) \ 2)
        End Select

        Return closeRect
    End Function

    Private Function MakeLParam(lo As Integer, hi As Integer) As IntPtr
        Return New IntPtr((hi << 16) Or (lo And &HFFFF))
    End Function

#End Region

#Region "Interop"

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Int32, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hwnd As IntPtr, msg As Integer, wParam As IntPtr, ByRef lParam As TCHITTESTINFO) As Integer
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Private Structure TCHITTESTINFO
        Public pt As Point
        Public flags As TCHITTESTFLAGS
        Public Sub New(x As Integer, y As Integer)
            pt = New Point(x, y)
        End Sub
    End Structure

    <Flags()>
    Private Enum TCHITTESTFLAGS
        TCHT_NOWHERE = 1
        TCHT_ONITEMICON = 2
        TCHT_ONITEMLABEL = 4
        TCHT_ONITEM = TCHT_ONITEMICON Or TCHT_ONITEMLABEL
    End Enum

    Private Const WM_NULL As Integer = &H0
    Private Const WM_MOUSEDOWN As Integer = &H201

    Private Const TCM_FIRST = &H1300
    Private Const TCM_HITTEST = TCM_FIRST + 13
    Private Const TCM_SETPADDING = TCM_FIRST + 43

#End Region

End Class