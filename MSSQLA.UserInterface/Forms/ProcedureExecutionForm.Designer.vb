<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProcedureExecutionForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.mainPanel = New System.Windows.Forms.Panel()
        Me.paramsTable = New System.Windows.Forms.TableLayoutPanel()
        Me.bottomPanelBorder = New System.Windows.Forms.Panel()
        Me.bottomPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnExecute = New FontAwesome.Sharp.IconButton()
        Me.btnCancel = New FontAwesome.Sharp.IconButton()
        Me.lblNoParams = New System.Windows.Forms.Label()
        Me.lblProcedureName = New System.Windows.Forms.Label()
        Me.mainPanel.SuspendLayout()
        Me.bottomPanelBorder.SuspendLayout()
        Me.bottomPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'mainPanel
        '
        Me.mainPanel.AutoSize = True
        Me.mainPanel.Controls.Add(Me.paramsTable)
        Me.mainPanel.Controls.Add(Me.bottomPanelBorder)
        Me.mainPanel.Controls.Add(Me.lblProcedureName)
        Me.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainPanel.Location = New System.Drawing.Point(1, 1)
        Me.mainPanel.Name = "mainPanel"
        Me.mainPanel.Size = New System.Drawing.Size(605, 88)
        Me.mainPanel.TabIndex = 0
        '
        'paramsTable
        '
        Me.paramsTable.AutoSize = True
        Me.paramsTable.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.paramsTable.ColumnCount = 2
        Me.paramsTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.7718!))
        Me.paramsTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.2282!))
        Me.paramsTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.paramsTable.Location = New System.Drawing.Point(0, 38)
        Me.paramsTable.Name = "paramsTable"
        Me.paramsTable.Padding = New System.Windows.Forms.Padding(10)
        Me.paramsTable.RowCount = 1
        Me.paramsTable.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.paramsTable.Size = New System.Drawing.Size(605, 0)
        Me.paramsTable.TabIndex = 17
        '
        'bottomPanelBorder
        '
        Me.bottomPanelBorder.AutoSize = True
        Me.bottomPanelBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.bottomPanelBorder.Controls.Add(Me.bottomPanel)
        Me.bottomPanelBorder.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.bottomPanelBorder.Location = New System.Drawing.Point(0, 36)
        Me.bottomPanelBorder.Name = "bottomPanelBorder"
        Me.bottomPanelBorder.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.bottomPanelBorder.Size = New System.Drawing.Size(605, 52)
        Me.bottomPanelBorder.TabIndex = 19
        '
        'bottomPanel
        '
        Me.bottomPanel.AutoSize = True
        Me.bottomPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.bottomPanel.Controls.Add(Me.btnExecute)
        Me.bottomPanel.Controls.Add(Me.btnCancel)
        Me.bottomPanel.Controls.Add(Me.lblNoParams)
        Me.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.bottomPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.bottomPanel.Location = New System.Drawing.Point(0, 2)
        Me.bottomPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.bottomPanel.Name = "bottomPanel"
        Me.bottomPanel.Padding = New System.Windows.Forms.Padding(0, 10, 10, 10)
        Me.bottomPanel.Size = New System.Drawing.Size(605, 50)
        Me.bottomPanel.TabIndex = 15
        '
        'btnExecute
        '
        Me.btnExecute.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(194, Byte), Integer))
        Me.btnExecute.FlatAppearance.BorderSize = 0
        Me.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExecute.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExecute.ForeColor = System.Drawing.Color.White
        Me.btnExecute.IconChar = FontAwesome.Sharp.IconChar.Play
        Me.btnExecute.IconColor = System.Drawing.Color.White
        Me.btnExecute.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnExecute.IconSize = 18
        Me.btnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExecute.Location = New System.Drawing.Point(499, 10)
        Me.btnExecute.Margin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(96, 30)
        Me.btnExecute.TabIndex = 3
        Me.btnExecute.Text = "EXECUTE"
        Me.btnExecute.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExecute.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(194, Byte), Integer))
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.IconChar = FontAwesome.Sharp.IconChar.DoorOpen
        Me.btnCancel.IconColor = System.Drawing.Color.White
        Me.btnCancel.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnCancel.IconSize = 18
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(398, 10)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 30)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblNoParams
        '
        Me.lblNoParams.BackColor = System.Drawing.Color.Transparent
        Me.bottomPanel.SetFlowBreak(Me.lblNoParams, True)
        Me.lblNoParams.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoParams.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblNoParams.Location = New System.Drawing.Point(10, 10)
        Me.lblNoParams.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblNoParams.Name = "lblNoParams"
        Me.lblNoParams.Size = New System.Drawing.Size(380, 30)
        Me.lblNoParams.TabIndex = 16
        Me.lblNoParams.Text = "No parameters found"
        Me.lblNoParams.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblNoParams.Visible = False
        '
        'lblProcedureName
        '
        Me.lblProcedureName.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblProcedureName.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblProcedureName.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblProcedureName.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblProcedureName.Location = New System.Drawing.Point(0, 0)
        Me.lblProcedureName.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lblProcedureName.Name = "lblProcedureName"
        Me.lblProcedureName.Size = New System.Drawing.Size(605, 38)
        Me.lblProcedureName.TabIndex = 16
        Me.lblProcedureName.Text = "ProcedureName"
        Me.lblProcedureName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ProcedureExecutionForm
        '
        Me.AcceptButton = Me.btnExecute
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.DodgerBlue
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(607, 90)
        Me.Controls.Add(Me.mainPanel)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ProcedureExecutionForm"
        Me.Padding = New System.Windows.Forms.Padding(1)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ProcedureExecutionForm"
        Me.mainPanel.ResumeLayout(False)
        Me.mainPanel.PerformLayout()
        Me.bottomPanelBorder.ResumeLayout(False)
        Me.bottomPanelBorder.PerformLayout()
        Me.bottomPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents mainPanel As Panel
    Friend WithEvents paramsTable As TableLayoutPanel
    Friend WithEvents lblProcedureName As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents bottomPanelBorder As Panel
    Friend WithEvents bottomPanel As FlowLayoutPanel
    Friend WithEvents btnExecute As FontAwesome.Sharp.IconButton
    Friend WithEvents btnCancel As FontAwesome.Sharp.IconButton
    Friend WithEvents lblNoParams As Label
End Class
