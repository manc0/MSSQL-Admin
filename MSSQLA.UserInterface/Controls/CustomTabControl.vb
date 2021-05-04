Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class CustomTabControl
    Inherits TabControl

    Private SelectedTabColor As Color = Color.FromArgb(77, 122, 200)
    Private HotTabColor As Color = Color.FromArgb(77, 144, 200)
    Private DefaultColor As Color = Color.FromArgb(40, 44, 52)

    Public Event OnAddButtonClick()
    Public Event OnTabClose()

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.DoubleBuffer Or
                 ControlStyles.ResizeRedraw Or
                 ControlStyles.UserPaint, True)
    End Sub

    'UserControl1 overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

#Region "Properties"

    Private _backcolor As Color = Color.Empty
    Private _hotTabIndex As Integer = -1
    Public _createAddButton As Boolean

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
    Description("Add new tab button")>
    Public Property HasAddButton() As Boolean
        Get
            Return _createAddButton
        End Get
        Set(value As Boolean)
            _createAddButton = value

            If value Then
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

    Private Property HotTabIndex As Int32
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
            m.LParam = MAKELPARAM(Me.Padding.X + CloseButtonHeight \ 2, Me.Padding.Y)
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
                brush = New SolidBrush(DefaultColor)
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

#Region "Public Methods"

    Public Sub AddTab(tab As TabPage, Optional isAddButton As Boolean = False)
        If HasAddButton And Not isAddButton Then
            TabPages.Insert(LastIndex, tab)
        Else
            TabPages.Add(tab)
        End If
    End Sub

    ''' <summary>
    ''' Closes the specified tab and selects the next one.
    ''' </summary>
    ''' <param name="index"></param>
    Public Sub CloseTabAt(index As Integer)
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

        RaiseEvent OnTabClose()
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
        TextRenderer.DrawText(graphics, tab.Text, DrawFont, tabRect, Color.White, Color.Transparent, TextFormatFlags.HorizontalCenter Or TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine Or TextFormatFlags.VerticalCenter)
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
                    TextRenderer.DrawText(bmGraphics, "✖", closeFont, closeRect, Color.White, brush.Color, TextFormatFlags.HorizontalCenter Or TextFormatFlags.NoPadding Or TextFormatFlags.SingleLine Or TextFormatFlags.VerticalCenter)
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

    Private Function MAKELPARAM(lo As Integer, hi As Integer) As IntPtr
        Return New IntPtr((hi << 16) Or (lo And &HFFFF))
    End Function

#End Region

#Region "Interop"

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Int32, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hwnd As IntPtr, msg As Int32, wParam As IntPtr, ByRef lParam As TCHITTESTINFO) As Int32
    End Function

    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)>
    Private Structure TCHITTESTINFO
        Public pt As Point
        Public flags As TCHITTESTFLAGS
        Public Sub New(x As Int32, y As Int32)
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

    Private Const WM_NULL As Int32 = &H0
    Private Const WM_SETFONT = &H30
    Private Const WM_FONTCHANGE = &H1D
    Private Const WM_MOUSEDOWN As Int32 = &H201

    Private Const TCM_FIRST = &H1300
    Private Const TCM_HITTEST = TCM_FIRST + 13
    Private Const TCM_SETPADDING = TCM_FIRST + 43

#End Region

End Class

Friend Structure RECT
    Public Left, Top, Right, Bottom As Integer
End Structure