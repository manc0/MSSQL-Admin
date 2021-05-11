<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatabaseBackupForm
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.mainPanel = New System.Windows.Forms.Panel()
        Me.bottomPanelBorder = New System.Windows.Forms.Panel()
        Me.bottomPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnCancel = New FontAwesome.Sharp.IconButton()
        Me.btnBackup = New FontAwesome.Sharp.IconButton()
        Me.topPanel = New System.Windows.Forms.Panel()
        Me.lblDatabase = New System.Windows.Forms.Label()
        Me.tbDatabase = New System.Windows.Forms.TextBox()
        Me.chbDifferentialMode = New System.Windows.Forms.CheckBox()
        Me.lblPath = New System.Windows.Forms.Label()
        Me.tbPath = New System.Windows.Forms.TextBox()
        Me.btnChoosePath = New FontAwesome.Sharp.IconButton()
        Me.mainPanel.SuspendLayout()
        Me.bottomPanelBorder.SuspendLayout()
        Me.bottomPanel.SuspendLayout()
        Me.topPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(398, 38)
        Me.lblTitle.TabIndex = 16
        Me.lblTitle.Text = "Database Backup"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'mainPanel
        '
        Me.mainPanel.AutoSize = True
        Me.mainPanel.Controls.Add(Me.topPanel)
        Me.mainPanel.Controls.Add(Me.lblTitle)
        Me.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainPanel.Location = New System.Drawing.Point(1, 1)
        Me.mainPanel.Name = "mainPanel"
        Me.mainPanel.Size = New System.Drawing.Size(398, 239)
        Me.mainPanel.TabIndex = 1
        '
        'bottomPanelBorder
        '
        Me.bottomPanelBorder.AutoSize = True
        Me.bottomPanelBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.bottomPanelBorder.Controls.Add(Me.bottomPanel)
        Me.bottomPanelBorder.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.bottomPanelBorder.Location = New System.Drawing.Point(0, 149)
        Me.bottomPanelBorder.Name = "bottomPanelBorder"
        Me.bottomPanelBorder.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.bottomPanelBorder.Size = New System.Drawing.Size(398, 52)
        Me.bottomPanelBorder.TabIndex = 18
        '
        'bottomPanel
        '
        Me.bottomPanel.AutoSize = True
        Me.bottomPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.bottomPanel.Controls.Add(Me.btnBackup)
        Me.bottomPanel.Controls.Add(Me.btnCancel)
        Me.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.bottomPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.bottomPanel.Location = New System.Drawing.Point(0, 2)
        Me.bottomPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.bottomPanel.Name = "bottomPanel"
        Me.bottomPanel.Padding = New System.Windows.Forms.Padding(0, 10, 10, 10)
        Me.bottomPanel.Size = New System.Drawing.Size(398, 50)
        Me.bottomPanel.TabIndex = 15
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
        Me.btnCancel.Location = New System.Drawing.Point(191, 10)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 30)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnBackup
        '
        Me.btnBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(194, Byte), Integer))
        Me.btnBackup.Enabled = False
        Me.btnBackup.FlatAppearance.BorderSize = 0
        Me.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackup.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackup.ForeColor = System.Drawing.Color.White
        Me.btnBackup.IconChar = FontAwesome.Sharp.IconChar.CheckDouble
        Me.btnBackup.IconColor = System.Drawing.Color.Gray
        Me.btnBackup.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnBackup.IconSize = 18
        Me.btnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBackup.Location = New System.Drawing.Point(292, 10)
        Me.btnBackup.Margin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(96, 30)
        Me.btnBackup.TabIndex = 3
        Me.btnBackup.Text = "BACKUP"
        Me.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBackup.UseVisualStyleBackColor = False
        '
        'topPanel
        '
        Me.topPanel.AutoSize = True
        Me.topPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.topPanel.Controls.Add(Me.btnChoosePath)
        Me.topPanel.Controls.Add(Me.lblPath)
        Me.topPanel.Controls.Add(Me.tbPath)
        Me.topPanel.Controls.Add(Me.chbDifferentialMode)
        Me.topPanel.Controls.Add(Me.lblDatabase)
        Me.topPanel.Controls.Add(Me.tbDatabase)
        Me.topPanel.Controls.Add(Me.bottomPanelBorder)
        Me.topPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.topPanel.Location = New System.Drawing.Point(0, 38)
        Me.topPanel.Name = "topPanel"
        Me.topPanel.Size = New System.Drawing.Size(398, 201)
        Me.topPanel.TabIndex = 18
        '
        'lblDatabase
        '
        Me.lblDatabase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDatabase.AutoSize = True
        Me.lblDatabase.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.lblDatabase.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDatabase.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblDatabase.Location = New System.Drawing.Point(15, 17)
        Me.lblDatabase.Margin = New System.Windows.Forms.Padding(15)
        Me.lblDatabase.Name = "lblDatabase"
        Me.lblDatabase.Size = New System.Drawing.Size(71, 17)
        Me.lblDatabase.TabIndex = 20
        Me.lblDatabase.Text = "DATABASE"
        '
        'tbDatabase
        '
        Me.tbDatabase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbDatabase.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.tbDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbDatabase.Enabled = False
        Me.tbDatabase.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDatabase.ForeColor = System.Drawing.Color.LightGray
        Me.tbDatabase.Location = New System.Drawing.Point(137, 15)
        Me.tbDatabase.Margin = New System.Windows.Forms.Padding(15)
        Me.tbDatabase.Name = "tbDatabase"
        Me.tbDatabase.Size = New System.Drawing.Size(246, 25)
        Me.tbDatabase.TabIndex = 19
        '
        'chbDifferentialMode
        '
        Me.chbDifferentialMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chbDifferentialMode.AutoSize = True
        Me.chbDifferentialMode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(194, Byte), Integer))
        Me.chbDifferentialMode.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.chbDifferentialMode.ForeColor = System.Drawing.Color.LightGray
        Me.chbDifferentialMode.Location = New System.Drawing.Point(15, 110)
        Me.chbDifferentialMode.Margin = New System.Windows.Forms.Padding(15)
        Me.chbDifferentialMode.Name = "chbDifferentialMode"
        Me.chbDifferentialMode.Size = New System.Drawing.Size(90, 21)
        Me.chbDifferentialMode.TabIndex = 21
        Me.chbDifferentialMode.Text = "Differential"
        Me.chbDifferentialMode.UseVisualStyleBackColor = True
        '
        'lblPath
        '
        Me.lblPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPath.AutoSize = True
        Me.lblPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.lblPath.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPath.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblPath.Location = New System.Drawing.Point(15, 66)
        Me.lblPath.Margin = New System.Windows.Forms.Padding(15)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(40, 17)
        Me.lblPath.TabIndex = 23
        Me.lblPath.Text = "PATH"
        '
        'tbPath
        '
        Me.tbPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.tbPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbPath.Enabled = False
        Me.tbPath.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPath.ForeColor = System.Drawing.Color.LightGray
        Me.tbPath.Location = New System.Drawing.Point(137, 64)
        Me.tbPath.Margin = New System.Windows.Forms.Padding(15)
        Me.tbPath.Name = "tbPath"
        Me.tbPath.Size = New System.Drawing.Size(218, 25)
        Me.tbPath.TabIndex = 22
        '
        'btnChoosePath
        '
        Me.btnChoosePath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChoosePath.FlatAppearance.BorderSize = 0
        Me.btnChoosePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnChoosePath.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChoosePath.ForeColor = System.Drawing.Color.White
        Me.btnChoosePath.IconChar = FontAwesome.Sharp.IconChar.FolderOpen
        Me.btnChoosePath.IconColor = System.Drawing.Color.White
        Me.btnChoosePath.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnChoosePath.IconSize = 24
        Me.btnChoosePath.Location = New System.Drawing.Point(358, 64)
        Me.btnChoosePath.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.btnChoosePath.Name = "btnChoosePath"
        Me.btnChoosePath.Size = New System.Drawing.Size(26, 25)
        Me.btnChoosePath.TabIndex = 24
        Me.btnChoosePath.UseVisualStyleBackColor = False
        '
        'DatabaseBackupForm
        '
        Me.AcceptButton = Me.btnBackup
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DodgerBlue
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(400, 241)
        Me.Controls.Add(Me.mainPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DatabaseBackupForm"
        Me.Padding = New System.Windows.Forms.Padding(1)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DatabaseBackupForm"
        Me.mainPanel.ResumeLayout(False)
        Me.mainPanel.PerformLayout()
        Me.bottomPanelBorder.ResumeLayout(False)
        Me.bottomPanelBorder.PerformLayout()
        Me.bottomPanel.ResumeLayout(False)
        Me.topPanel.ResumeLayout(False)
        Me.topPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents mainPanel As Panel
    Friend WithEvents topPanel As Panel
    Friend WithEvents bottomPanelBorder As Panel
    Friend WithEvents bottomPanel As FlowLayoutPanel
    Friend WithEvents btnBackup As FontAwesome.Sharp.IconButton
    Friend WithEvents btnCancel As FontAwesome.Sharp.IconButton
    Friend WithEvents lblDatabase As Label
    Friend WithEvents tbDatabase As TextBox
    Friend WithEvents lblPath As Label
    Friend WithEvents tbPath As TextBox
    Friend WithEvents chbDifferentialMode As CheckBox
    Friend WithEvents btnChoosePath As FontAwesome.Sharp.IconButton
End Class
