Public Class CustomToolStripSystemRenderer
    Inherits ToolStripSystemRenderer

    Protected Overrides Sub OnRenderToolStripBorder(e As ToolStripRenderEventArgs)

    End Sub

    Protected Overrides Sub OnRenderButtonBackground(e As ToolStripItemRenderEventArgs)
        Dim btn = TryCast(e.Item, ToolStripButton)
        Dim rectangle As New Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1)
        Dim selectedBrush As New SolidBrush(Color.FromArgb(44, 49, 59))
        Dim checkedBrush As New SolidBrush(Color.FromArgb(53, 59, 67))
        Dim pen As New Pen(Color.FromArgb(44, 49, 59))

        If Not e.Item.Selected Then
            MyBase.OnRenderButtonBackground(e)
        Else
            e.Graphics.FillRectangle(selectedBrush, rectangle)
            e.Graphics.DrawRectangle(pen, rectangle)
        End If

        If btn IsNot Nothing AndAlso btn.CheckOnClick AndAlso btn.Checked Then
            e.Graphics.FillRectangle(checkedBrush, rectangle)
            e.Graphics.DrawRectangle(pen, rectangle)
        End If

    End Sub

End Class