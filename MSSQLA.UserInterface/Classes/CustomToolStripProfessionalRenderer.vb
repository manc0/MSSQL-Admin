Public Class CustomToolStripProfessionalRenderer
    Inherits ToolStripProfessionalRenderer

    Public Sub New(professionalColorTable As ProfessionalColorTable)
        MyBase.New(professionalColorTable)
    End Sub

    Protected Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
        If e.Item.Text <> String.Empty Then
            MyBase.OnRenderMenuItemBackground(e)
        End If
    End Sub

End Class
