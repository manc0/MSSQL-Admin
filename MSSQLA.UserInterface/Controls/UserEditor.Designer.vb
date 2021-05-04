Imports ScintillaNET_FindReplaceDialog

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UserEditor
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserEditor))
        Me.Scintilla = New ScintillaNET.Scintilla()
        Me.MyAutocompleteMenu = New AutocompleteMenuNS.AutocompleteMenu()
        Me.MyImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'Scintilla
        '
        Me.Scintilla.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Scintilla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Scintilla.Location = New System.Drawing.Point(0, 20)
        Me.Scintilla.Name = "Scintilla"
        Me.Scintilla.Size = New System.Drawing.Size(363, 89)
        Me.Scintilla.TabIndex = 15
        Me.Scintilla.WrapMode = ScintillaNET.WrapMode.Word
        '
        'MyAutocompleteMenu
        '
        Me.MyAutocompleteMenu.AllowsTabKey = True
        Me.MyAutocompleteMenu.AppearInterval = 100
        Me.MyAutocompleteMenu.Colors = CType(resources.GetObject("MyAutocompleteMenu.Colors"), AutocompleteMenuNS.Colors)
        Me.MyAutocompleteMenu.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyAutocompleteMenu.ImageList = Me.MyImageList
        Me.MyAutocompleteMenu.Items = New String(-1) {}
        Me.MyAutocompleteMenu.MinFragmentLength = 1
        Me.MyAutocompleteMenu.TargetControlWrapper = Nothing
        '
        'MyImageList
        '
        Me.MyImageList.ImageStream = CType(resources.GetObject("MyImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.MyImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.MyImageList.Images.SetKeyName(0, "keyword.png")
        Me.MyImageList.Images.SetKeyName(1, "operator.png")
        Me.MyImageList.Images.SetKeyName(2, "function.png")
        Me.MyImageList.Images.SetKeyName(3, "object.png")
        '
        'UserEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Scintilla)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "UserEditor"
        Me.Padding = New System.Windows.Forms.Padding(0, 20, 0, 0)
        Me.Size = New System.Drawing.Size(363, 109)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Scintilla As ScintillaNET.Scintilla
    Friend WithEvents FindReplace As FindReplace
    Friend WithEvents MyAutocompleteMenu As AutocompleteMenuNS.AutocompleteMenu
    Friend WithEvents MyImageList As ImageList
End Class
