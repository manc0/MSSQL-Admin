
Imports ScintillaNET_FindReplaceDialog

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MyMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnExport = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSaveSql = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnClearOutput = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnClearXpath = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnRedo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnReplace = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnFind = New System.Windows.Forms.ToolStripMenuItem()
        Me.leftPanel = New System.Windows.Forms.Panel()
        Me.btnDisconnect = New System.Windows.Forms.Button()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.lblTables = New System.Windows.Forms.Label()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.lblPass = New System.Windows.Forms.Label()
        Me.lblDatabases = New System.Windows.Forms.Label()
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.lbTableList = New System.Windows.Forms.ListBox()
        Me.tbServer = New System.Windows.Forms.TextBox()
        Me.tbUser = New System.Windows.Forms.TextBox()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.tbPass = New System.Windows.Forms.TextBox()
        Me.cbDatabases = New System.Windows.Forms.ComboBox()
        Me.MyScintilla = New ScintillaNET.Scintilla()
        Me.topPanel = New System.Windows.Forms.Panel()
        Me.splitter2 = New System.Windows.Forms.Splitter()
        Me.bottomPanel = New System.Windows.Forms.Panel()
        Me.xpathPanel = New System.Windows.Forms.Panel()
        Me.xpathArea = New System.Windows.Forms.RichTextBox()
        Me.xpathEvaluatorPanel = New System.Windows.Forms.Panel()
        Me.btnExecuteXpath = New System.Windows.Forms.Button()
        Me.xpathExpression = New System.Windows.Forms.TextBox()
        Me.outputArea = New System.Windows.Forms.RichTextBox()
        Me.MyToolStrip = New System.Windows.Forms.ToolStrip()
        Me.lblConnStatus = New System.Windows.Forms.ToolStripLabel()
        Me.btnOutput = New System.Windows.Forms.ToolStripButton()
        Me.tsSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnXpath = New System.Windows.Forms.ToolStripButton()
        Me.splitter1 = New System.Windows.Forms.Splitter()
        Me.mainPanel = New System.Windows.Forms.Panel()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.MyAutocompleteMenu = New AutocompleteMenuNS.AutocompleteMenu()
        Me.MyImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.MyMenuStrip.SuspendLayout()
        Me.leftPanel.SuspendLayout()
        Me.topPanel.SuspendLayout()
        Me.bottomPanel.SuspendLayout()
        Me.xpathPanel.SuspendLayout()
        Me.xpathEvaluatorPanel.SuspendLayout()
        Me.MyToolStrip.SuspendLayout()
        Me.mainPanel.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MyMenuStrip
        '
        Me.MyMenuStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.MyMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFile, Me.tsmiEdit})
        Me.MyMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MyMenuStrip.Name = "MyMenuStrip"
        Me.MyMenuStrip.Padding = New System.Windows.Forms.Padding(0)
        Me.MyMenuStrip.Size = New System.Drawing.Size(1222, 29)
        Me.MyMenuStrip.TabIndex = 0
        Me.MyMenuStrip.Text = "MenuStrip1"
        '
        'tsmiFile
        '
        Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnExport, Me.btnSaveSql, Me.btnClearOutput, Me.btnClearXpath, Me.tsSeparator1, Me.btnExit})
        Me.tsmiFile.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.tsmiFile.ForeColor = System.Drawing.Color.LightSlateGray
        Me.tsmiFile.Name = "tsmiFile"
        Me.tsmiFile.Padding = New System.Windows.Forms.Padding(4)
        Me.tsmiFile.Size = New System.Drawing.Size(39, 29)
        Me.tsmiFile.Text = "File"
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnExport.ForeColor = System.Drawing.Color.White
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Padding = New System.Windows.Forms.Padding(2)
        Me.btnExport.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.btnExport.Size = New System.Drawing.Size(244, 24)
        Me.btnExport.Text = "Export Table..."
        '
        'btnSaveSql
        '
        Me.btnSaveSql.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnSaveSql.ForeColor = System.Drawing.Color.White
        Me.btnSaveSql.Name = "btnSaveSql"
        Me.btnSaveSql.Padding = New System.Windows.Forms.Padding(2)
        Me.btnSaveSql.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.btnSaveSql.Size = New System.Drawing.Size(244, 24)
        Me.btnSaveSql.Text = "Save Query As SQL..."
        '
        'btnClearOutput
        '
        Me.btnClearOutput.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnClearOutput.ForeColor = System.Drawing.Color.White
        Me.btnClearOutput.Name = "btnClearOutput"
        Me.btnClearOutput.Padding = New System.Windows.Forms.Padding(2)
        Me.btnClearOutput.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.btnClearOutput.Size = New System.Drawing.Size(244, 24)
        Me.btnClearOutput.Text = "Clear Output"
        '
        'btnClearXpath
        '
        Me.btnClearXpath.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnClearXpath.ForeColor = System.Drawing.Color.White
        Me.btnClearXpath.Name = "btnClearXpath"
        Me.btnClearXpath.Padding = New System.Windows.Forms.Padding(2)
        Me.btnClearXpath.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.btnClearXpath.Size = New System.Drawing.Size(244, 24)
        Me.btnClearXpath.Text = "Clear XPath"
        '
        'tsSeparator1
        '
        Me.tsSeparator1.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator1.ForeColor = System.Drawing.Color.White
        Me.tsSeparator1.Name = "tsSeparator1"
        Me.tsSeparator1.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator1.Size = New System.Drawing.Size(237, 6)
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Padding = New System.Windows.Forms.Padding(2)
        Me.btnExit.Size = New System.Drawing.Size(244, 24)
        Me.btnExit.Text = "Exit"
        '
        'tsmiEdit
        '
        Me.tsmiEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnUndo, Me.btnRedo, Me.tsSeparator2, Me.btnCut, Me.btnCopy, Me.btnPaste, Me.btnSelectAll, Me.tsSeparator3, Me.btnReplace, Me.btnFind})
        Me.tsmiEdit.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.tsmiEdit.ForeColor = System.Drawing.Color.LightSlateGray
        Me.tsmiEdit.Name = "tsmiEdit"
        Me.tsmiEdit.Padding = New System.Windows.Forms.Padding(4)
        Me.tsmiEdit.Size = New System.Drawing.Size(42, 29)
        Me.tsmiEdit.Text = "Edit"
        '
        'btnUndo
        '
        Me.btnUndo.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnUndo.ForeColor = System.Drawing.Color.White
        Me.btnUndo.Name = "btnUndo"
        Me.btnUndo.Padding = New System.Windows.Forms.Padding(2)
        Me.btnUndo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.btnUndo.Size = New System.Drawing.Size(177, 24)
        Me.btnUndo.Text = "Undo"
        '
        'btnRedo
        '
        Me.btnRedo.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnRedo.ForeColor = System.Drawing.Color.White
        Me.btnRedo.Name = "btnRedo"
        Me.btnRedo.Padding = New System.Windows.Forms.Padding(2)
        Me.btnRedo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.btnRedo.Size = New System.Drawing.Size(177, 24)
        Me.btnRedo.Text = "Redo"
        '
        'tsSeparator2
        '
        Me.tsSeparator2.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator2.ForeColor = System.Drawing.Color.White
        Me.tsSeparator2.Name = "tsSeparator2"
        Me.tsSeparator2.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator2.Size = New System.Drawing.Size(170, 6)
        '
        'btnCut
        '
        Me.btnCut.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnCut.ForeColor = System.Drawing.Color.White
        Me.btnCut.Name = "btnCut"
        Me.btnCut.Padding = New System.Windows.Forms.Padding(2)
        Me.btnCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.btnCut.Size = New System.Drawing.Size(177, 24)
        Me.btnCut.Text = "Cut"
        '
        'btnCopy
        '
        Me.btnCopy.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnCopy.ForeColor = System.Drawing.Color.White
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Padding = New System.Windows.Forms.Padding(2)
        Me.btnCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.btnCopy.Size = New System.Drawing.Size(177, 24)
        Me.btnCopy.Text = "Copy"
        '
        'btnPaste
        '
        Me.btnPaste.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnPaste.ForeColor = System.Drawing.Color.White
        Me.btnPaste.Name = "btnPaste"
        Me.btnPaste.Padding = New System.Windows.Forms.Padding(2)
        Me.btnPaste.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.btnPaste.Size = New System.Drawing.Size(177, 24)
        Me.btnPaste.Text = "Paste"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnSelectAll.ForeColor = System.Drawing.Color.White
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Padding = New System.Windows.Forms.Padding(2)
        Me.btnSelectAll.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.btnSelectAll.Size = New System.Drawing.Size(177, 24)
        Me.btnSelectAll.Text = "Select All"
        '
        'tsSeparator3
        '
        Me.tsSeparator3.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator3.ForeColor = System.Drawing.Color.White
        Me.tsSeparator3.Name = "tsSeparator3"
        Me.tsSeparator3.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator3.Size = New System.Drawing.Size(170, 6)
        '
        'btnReplace
        '
        Me.btnReplace.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnReplace.ForeColor = System.Drawing.Color.White
        Me.btnReplace.Name = "btnReplace"
        Me.btnReplace.Padding = New System.Windows.Forms.Padding(2)
        Me.btnReplace.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.btnReplace.Size = New System.Drawing.Size(177, 24)
        Me.btnReplace.Text = "Replace"
        '
        'btnFind
        '
        Me.btnFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnFind.ForeColor = System.Drawing.Color.White
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Padding = New System.Windows.Forms.Padding(2)
        Me.btnFind.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.btnFind.Size = New System.Drawing.Size(177, 24)
        Me.btnFind.Text = "Find"
        '
        'leftPanel
        '
        Me.leftPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.leftPanel.Controls.Add(Me.btnDisconnect)
        Me.leftPanel.Controls.Add(Me.btnSubmit)
        Me.leftPanel.Controls.Add(Me.lblTables)
        Me.leftPanel.Controls.Add(Me.lblServer)
        Me.leftPanel.Controls.Add(Me.lblUser)
        Me.leftPanel.Controls.Add(Me.lblPass)
        Me.leftPanel.Controls.Add(Me.lblDatabases)
        Me.leftPanel.Controls.Add(Me.btnExecute)
        Me.leftPanel.Controls.Add(Me.lbTableList)
        Me.leftPanel.Controls.Add(Me.tbServer)
        Me.leftPanel.Controls.Add(Me.tbUser)
        Me.leftPanel.Controls.Add(Me.btnConnect)
        Me.leftPanel.Controls.Add(Me.tbPass)
        Me.leftPanel.Controls.Add(Me.cbDatabases)
        Me.leftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.leftPanel.Location = New System.Drawing.Point(0, 29)
        Me.leftPanel.Name = "leftPanel"
        Me.leftPanel.Size = New System.Drawing.Size(269, 710)
        Me.leftPanel.TabIndex = 1
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDisconnect.BackColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnDisconnect.Enabled = False
        Me.btnDisconnect.FlatAppearance.BorderSize = 0
        Me.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDisconnect.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDisconnect.ForeColor = System.Drawing.Color.White
        Me.btnDisconnect.Location = New System.Drawing.Point(138, 663)
        Me.btnDisconnect.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(117, 30)
        Me.btnDisconnect.TabIndex = 9
        Me.btnDisconnect.Text = "DISCONNECT"
        Me.btnDisconnect.UseVisualStyleBackColor = False
        '
        'btnSubmit
        '
        Me.btnSubmit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSubmit.BackColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnSubmit.Enabled = False
        Me.btnSubmit.FlatAppearance.BorderSize = 0
        Me.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubmit.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubmit.ForeColor = System.Drawing.Color.White
        Me.btnSubmit.Location = New System.Drawing.Point(138, 68)
        Me.btnSubmit.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(117, 30)
        Me.btnSubmit.TabIndex = 3
        Me.btnSubmit.Text = "SUBMIT"
        Me.btnSubmit.UseVisualStyleBackColor = False
        '
        'lblTables
        '
        Me.lblTables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTables.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTables.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTables.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblTables.Location = New System.Drawing.Point(3, 111)
        Me.lblTables.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lblTables.Name = "lblTables"
        Me.lblTables.Size = New System.Drawing.Size(271, 20)
        Me.lblTables.TabIndex = 12
        Me.lblTables.Text = "TABLES"
        '
        'lblServer
        '
        Me.lblServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServer.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblServer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServer.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblServer.Location = New System.Drawing.Point(0, 468)
        Me.lblServer.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(271, 20)
        Me.lblServer.TabIndex = 9
        Me.lblServer.Text = "SERVER"
        '
        'lblUser
        '
        Me.lblUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUser.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblUser.Location = New System.Drawing.Point(-2, 533)
        Me.lblUser.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(271, 20)
        Me.lblUser.TabIndex = 7
        Me.lblUser.Text = "USER"
        '
        'lblPass
        '
        Me.lblPass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPass.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblPass.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPass.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblPass.Location = New System.Drawing.Point(-2, 598)
        Me.lblPass.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lblPass.Name = "lblPass"
        Me.lblPass.Size = New System.Drawing.Size(271, 20)
        Me.lblPass.TabIndex = 5
        Me.lblPass.Text = "PASSWORD"
        '
        'lblDatabases
        '
        Me.lblDatabases.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDatabases.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblDatabases.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDatabases.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblDatabases.Location = New System.Drawing.Point(0, 0)
        Me.lblDatabases.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lblDatabases.Name = "lblDatabases"
        Me.lblDatabases.Size = New System.Drawing.Size(274, 20)
        Me.lblDatabases.TabIndex = 0
        Me.lblDatabases.Text = "DATABASES"
        '
        'btnExecute
        '
        Me.btnExecute.BackColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnExecute.Enabled = False
        Me.btnExecute.FlatAppearance.BorderSize = 0
        Me.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExecute.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExecute.ForeColor = System.Drawing.Color.White
        Me.btnExecute.Location = New System.Drawing.Point(15, 68)
        Me.btnExecute.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(117, 30)
        Me.btnExecute.TabIndex = 2
        Me.btnExecute.Text = "EXECUTE"
        Me.btnExecute.UseVisualStyleBackColor = False
        '
        'lbTableList
        '
        Me.lbTableList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTableList.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.lbTableList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lbTableList.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTableList.ForeColor = System.Drawing.Color.LightGray
        Me.lbTableList.FormattingEnabled = True
        Me.lbTableList.ItemHeight = 17
        Me.lbTableList.Location = New System.Drawing.Point(15, 141)
        Me.lbTableList.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lbTableList.Name = "lbTableList"
        Me.lbTableList.Size = New System.Drawing.Size(240, 306)
        Me.lbTableList.TabIndex = 4
        '
        'tbServer
        '
        Me.tbServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyAutocompleteMenu.SetAutocompleteMenu(Me.tbServer, Nothing)
        Me.tbServer.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.tbServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbServer.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbServer.ForeColor = System.Drawing.Color.LightGray
        Me.tbServer.Location = New System.Drawing.Point(15, 498)
        Me.tbServer.Margin = New System.Windows.Forms.Padding(8, 10, 8, 0)
        Me.tbServer.Name = "tbServer"
        Me.tbServer.Size = New System.Drawing.Size(240, 25)
        Me.tbServer.TabIndex = 5
        '
        'tbUser
        '
        Me.tbUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyAutocompleteMenu.SetAutocompleteMenu(Me.tbUser, Nothing)
        Me.tbUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.tbUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbUser.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbUser.ForeColor = System.Drawing.Color.LightGray
        Me.tbUser.Location = New System.Drawing.Point(15, 563)
        Me.tbUser.Margin = New System.Windows.Forms.Padding(8, 10, 8, 0)
        Me.tbUser.Name = "tbUser"
        Me.tbUser.Size = New System.Drawing.Size(240, 25)
        Me.tbUser.TabIndex = 6
        '
        'btnConnect
        '
        Me.btnConnect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConnect.BackColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnConnect.FlatAppearance.BorderSize = 0
        Me.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConnect.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConnect.ForeColor = System.Drawing.Color.White
        Me.btnConnect.Location = New System.Drawing.Point(15, 663)
        Me.btnConnect.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(117, 30)
        Me.btnConnect.TabIndex = 8
        Me.btnConnect.Text = "CONNECT"
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'tbPass
        '
        Me.tbPass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyAutocompleteMenu.SetAutocompleteMenu(Me.tbPass, Nothing)
        Me.tbPass.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.tbPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbPass.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPass.ForeColor = System.Drawing.Color.LightGray
        Me.tbPass.Location = New System.Drawing.Point(15, 628)
        Me.tbPass.Margin = New System.Windows.Forms.Padding(8, 10, 8, 0)
        Me.tbPass.Name = "tbPass"
        Me.tbPass.Size = New System.Drawing.Size(240, 25)
        Me.tbPass.TabIndex = 7
        Me.tbPass.UseSystemPasswordChar = True
        '
        'cbDatabases
        '
        Me.cbDatabases.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbDatabases.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.cbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDatabases.Enabled = False
        Me.cbDatabases.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDatabases.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.cbDatabases.FormattingEnabled = True
        Me.cbDatabases.Location = New System.Drawing.Point(15, 30)
        Me.cbDatabases.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.cbDatabases.Name = "cbDatabases"
        Me.cbDatabases.Size = New System.Drawing.Size(240, 25)
        Me.cbDatabases.TabIndex = 1
        '
        'MyScintilla
        '
        Me.MyScintilla.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MyScintilla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyScintilla.Location = New System.Drawing.Point(0, 20)
        Me.MyScintilla.Name = "MyScintilla"
        Me.MyScintilla.Size = New System.Drawing.Size(953, 264)
        Me.MyScintilla.TabIndex = 14
        Me.MyScintilla.WrapMode = ScintillaNET.WrapMode.Word
        '
        'topPanel
        '
        Me.topPanel.Controls.Add(Me.MyScintilla)
        Me.topPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.topPanel.Location = New System.Drawing.Point(0, 0)
        Me.topPanel.Name = "topPanel"
        Me.topPanel.Padding = New System.Windows.Forms.Padding(0, 20, 0, 0)
        Me.topPanel.Size = New System.Drawing.Size(953, 284)
        Me.topPanel.TabIndex = 10
        '
        'splitter2
        '
        Me.splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(79, Byte), Integer))
        Me.splitter2.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.splitter2.Location = New System.Drawing.Point(0, 284)
        Me.splitter2.Name = "splitter2"
        Me.splitter2.Size = New System.Drawing.Size(953, 1)
        Me.splitter2.TabIndex = 11
        Me.splitter2.TabStop = False
        '
        'bottomPanel
        '
        Me.bottomPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.bottomPanel.Controls.Add(Me.xpathPanel)
        Me.bottomPanel.Controls.Add(Me.outputArea)
        Me.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.bottomPanel.Location = New System.Drawing.Point(0, 511)
        Me.bottomPanel.Name = "bottomPanel"
        Me.bottomPanel.Padding = New System.Windows.Forms.Padding(10)
        Me.bottomPanel.Size = New System.Drawing.Size(953, 173)
        Me.bottomPanel.TabIndex = 12
        Me.bottomPanel.Visible = False
        '
        'xpathPanel
        '
        Me.xpathPanel.Controls.Add(Me.xpathArea)
        Me.xpathPanel.Controls.Add(Me.xpathEvaluatorPanel)
        Me.xpathPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xpathPanel.Location = New System.Drawing.Point(10, 10)
        Me.xpathPanel.Name = "xpathPanel"
        Me.xpathPanel.Size = New System.Drawing.Size(933, 153)
        Me.xpathPanel.TabIndex = 13
        Me.xpathPanel.Visible = False
        '
        'xpathArea
        '
        Me.xpathArea.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyAutocompleteMenu.SetAutocompleteMenu(Me.xpathArea, Nothing)
        Me.xpathArea.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.xpathArea.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.xpathArea.DetectUrls = False
        Me.xpathArea.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xpathArea.ForeColor = System.Drawing.Color.LightGray
        Me.xpathArea.Location = New System.Drawing.Point(0, 39)
        Me.xpathArea.Name = "xpathArea"
        Me.xpathArea.ReadOnly = True
        Me.xpathArea.Size = New System.Drawing.Size(933, 114)
        Me.xpathArea.TabIndex = 15
        Me.xpathArea.TabStop = False
        Me.xpathArea.Text = ""
        Me.xpathArea.WordWrap = False
        '
        'xpathEvaluatorPanel
        '
        Me.xpathEvaluatorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.xpathEvaluatorPanel.Controls.Add(Me.btnExecuteXpath)
        Me.xpathEvaluatorPanel.Controls.Add(Me.xpathExpression)
        Me.xpathEvaluatorPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.xpathEvaluatorPanel.Location = New System.Drawing.Point(0, 0)
        Me.xpathEvaluatorPanel.Name = "xpathEvaluatorPanel"
        Me.xpathEvaluatorPanel.Padding = New System.Windows.Forms.Padding(2)
        Me.xpathEvaluatorPanel.Size = New System.Drawing.Size(933, 32)
        Me.xpathEvaluatorPanel.TabIndex = 16
        '
        'btnExecuteXpath
        '
        Me.btnExecuteXpath.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.btnExecuteXpath.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnExecuteXpath.FlatAppearance.BorderSize = 0
        Me.btnExecuteXpath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExecuteXpath.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExecuteXpath.ForeColor = System.Drawing.Color.White
        Me.btnExecuteXpath.Location = New System.Drawing.Point(2, 2)
        Me.btnExecuteXpath.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.btnExecuteXpath.Name = "btnExecuteXpath"
        Me.btnExecuteXpath.Size = New System.Drawing.Size(26, 26)
        Me.btnExecuteXpath.TabIndex = 13
        Me.btnExecuteXpath.Text = ">"
        Me.btnExecuteXpath.UseVisualStyleBackColor = False
        '
        'xpathExpression
        '
        Me.xpathExpression.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyAutocompleteMenu.SetAutocompleteMenu(Me.xpathExpression, Nothing)
        Me.xpathExpression.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.xpathExpression.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.xpathExpression.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xpathExpression.ForeColor = System.Drawing.Color.LightGray
        Me.xpathExpression.Location = New System.Drawing.Point(36, 6)
        Me.xpathExpression.Margin = New System.Windows.Forms.Padding(8, 10, 8, 0)
        Me.xpathExpression.Name = "xpathExpression"
        Me.xpathExpression.ShortcutsEnabled = False
        Me.xpathExpression.Size = New System.Drawing.Size(889, 19)
        Me.xpathExpression.TabIndex = 12
        Me.xpathExpression.TabStop = False
        Me.xpathExpression.Text = "//"
        Me.xpathExpression.WordWrap = False
        '
        'outputArea
        '
        Me.MyAutocompleteMenu.SetAutocompleteMenu(Me.outputArea, Nothing)
        Me.outputArea.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.outputArea.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.outputArea.DetectUrls = False
        Me.outputArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.outputArea.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.outputArea.ForeColor = System.Drawing.Color.LightGray
        Me.outputArea.Location = New System.Drawing.Point(10, 10)
        Me.outputArea.Name = "outputArea"
        Me.outputArea.ReadOnly = True
        Me.outputArea.Size = New System.Drawing.Size(933, 153)
        Me.outputArea.TabIndex = 12
        Me.outputArea.TabStop = False
        Me.outputArea.Text = ""
        Me.outputArea.WordWrap = False
        '
        'MyToolStrip
        '
        Me.MyToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.MyToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MyToolStrip.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.MyToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblConnStatus, Me.btnOutput, Me.tsSeparator4, Me.btnXpath})
        Me.MyToolStrip.Location = New System.Drawing.Point(0, 684)
        Me.MyToolStrip.Name = "MyToolStrip"
        Me.MyToolStrip.Padding = New System.Windows.Forms.Padding(2, 2, 2, 0)
        Me.MyToolStrip.Size = New System.Drawing.Size(953, 26)
        Me.MyToolStrip.TabIndex = 2
        Me.MyToolStrip.Text = "ToolStrip1"
        '
        'lblConnStatus
        '
        Me.lblConnStatus.ActiveLinkColor = System.Drawing.Color.Red
        Me.lblConnStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lblConnStatus.ForeColor = System.Drawing.Color.IndianRed
        Me.lblConnStatus.Name = "lblConnStatus"
        Me.lblConnStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblConnStatus.Size = New System.Drawing.Size(86, 21)
        Me.lblConnStatus.Text = "Disconnected"
        '
        'btnOutput
        '
        Me.btnOutput.AutoToolTip = False
        Me.btnOutput.CheckOnClick = True
        Me.btnOutput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnOutput.ForeColor = System.Drawing.Color.LightSlateGray
        Me.btnOutput.Image = CType(resources.GetObject("btnOutput.Image"), System.Drawing.Image)
        Me.btnOutput.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(52, 21)
        Me.btnOutput.Text = "Output"
        '
        'tsSeparator4
        '
        Me.tsSeparator4.ForeColor = System.Drawing.Color.LightGray
        Me.tsSeparator4.Name = "tsSeparator4"
        Me.tsSeparator4.Size = New System.Drawing.Size(6, 24)
        '
        'btnXpath
        '
        Me.btnXpath.AutoToolTip = False
        Me.btnXpath.CheckOnClick = True
        Me.btnXpath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnXpath.ForeColor = System.Drawing.Color.LightSlateGray
        Me.btnXpath.Image = CType(resources.GetObject("btnXpath.Image"), System.Drawing.Image)
        Me.btnXpath.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnXpath.Name = "btnXpath"
        Me.btnXpath.Size = New System.Drawing.Size(103, 21)
        Me.btnXpath.Text = "XPath Evaluator"
        '
        'splitter1
        '
        Me.splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(79, Byte), Integer))
        Me.splitter1.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.splitter1.Location = New System.Drawing.Point(0, 510)
        Me.splitter1.Name = "splitter1"
        Me.splitter1.Size = New System.Drawing.Size(953, 1)
        Me.splitter1.TabIndex = 13
        Me.splitter1.TabStop = False
        Me.splitter1.Visible = False
        '
        'mainPanel
        '
        Me.mainPanel.Controls.Add(Me.dgv)
        Me.mainPanel.Controls.Add(Me.splitter1)
        Me.mainPanel.Controls.Add(Me.bottomPanel)
        Me.mainPanel.Controls.Add(Me.splitter2)
        Me.mainPanel.Controls.Add(Me.topPanel)
        Me.mainPanel.Controls.Add(Me.MyToolStrip)
        Me.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainPanel.Location = New System.Drawing.Point(269, 29)
        Me.mainPanel.Name = "mainPanel"
        Me.mainPanel.Size = New System.Drawing.Size(953, 710)
        Me.mainPanel.TabIndex = 3
        '
        'dgv
        '
        Me.dgv.AllowUserToResizeRows = False
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(79, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.ColumnHeadersHeight = 28
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(200, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.EnableHeadersVisualStyles = False
        Me.dgv.GridColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgv.Location = New System.Drawing.Point(0, 285)
        Me.dgv.Name = "dgv"
        Me.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(79, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(200, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.RowHeadersWidth = 30
        Me.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv.RowTemplate.Height = 26
        Me.dgv.Size = New System.Drawing.Size(953, 225)
        Me.dgv.TabIndex = 10
        Me.dgv.TabStop = False
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
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1222, 739)
        Me.Controls.Add(Me.mainPanel)
        Me.Controls.Add(Me.leftPanel)
        Me.Controls.Add(Me.MyMenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MyMenuStrip
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MSSQL Admin"
        Me.MyMenuStrip.ResumeLayout(False)
        Me.MyMenuStrip.PerformLayout()
        Me.leftPanel.ResumeLayout(False)
        Me.leftPanel.PerformLayout()
        Me.topPanel.ResumeLayout(False)
        Me.bottomPanel.ResumeLayout(False)
        Me.xpathPanel.ResumeLayout(False)
        Me.xpathEvaluatorPanel.ResumeLayout(False)
        Me.xpathEvaluatorPanel.PerformLayout()
        Me.MyToolStrip.ResumeLayout(False)
        Me.MyToolStrip.PerformLayout()
        Me.mainPanel.ResumeLayout(False)
        Me.mainPanel.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MyMenuStrip As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents leftPanel As Panel
    Friend WithEvents MyScintilla As ScintillaNET.Scintilla
    Friend WithEvents tsmiEdit As ToolStripMenuItem
    Friend WithEvents btnConnect As Button
    Friend WithEvents tbPass As TextBox
    Friend WithEvents cbDatabases As ComboBox
    Friend WithEvents lblDatabases As Label
    Friend WithEvents lblPass As Label
    Friend WithEvents lblServer As Label
    Friend WithEvents tbServer As TextBox
    Friend WithEvents lblUser As Label
    Friend WithEvents tbUser As TextBox
    Friend WithEvents btnExecute As Button
    Friend WithEvents lbTableList As ListBox
    Friend WithEvents lblTables As Label
    Friend WithEvents topPanel As Panel
    Friend WithEvents splitter2 As Splitter
    Friend WithEvents bottomPanel As Panel
    Friend WithEvents splitter1 As Splitter
    Friend WithEvents mainPanel As Panel
    Friend WithEvents dgv As DataGridView
    Friend WithEvents outputArea As RichTextBox
    Friend WithEvents MyToolStrip As ToolStrip
    Friend WithEvents lblConnStatus As ToolStripLabel
    Friend WithEvents btnOutput As ToolStripButton
    Friend WithEvents tsSeparator4 As ToolStripSeparator
    Friend WithEvents btnXpath As ToolStripButton
    Friend WithEvents btnSubmit As Button
    Friend WithEvents MyAutocompleteMenu As AutocompleteMenuNS.AutocompleteMenu
    Friend WithEvents MyImageList As ImageList
    Friend WithEvents btnClearOutput As ToolStripMenuItem
    Friend WithEvents btnExport As ToolStripMenuItem
    Friend WithEvents btnExit As ToolStripMenuItem
    Friend WithEvents tsSeparator1 As ToolStripSeparator
    Friend WithEvents btnUndo As ToolStripMenuItem
    Friend WithEvents btnRedo As ToolStripMenuItem
    Friend WithEvents tsSeparator2 As ToolStripSeparator
    Friend WithEvents btnCut As ToolStripMenuItem
    Friend WithEvents btnCopy As ToolStripMenuItem
    Friend WithEvents btnPaste As ToolStripMenuItem
    Friend WithEvents tsSeparator3 As ToolStripSeparator
    Friend WithEvents btnFind As ToolStripMenuItem
    Friend WithEvents btnReplace As ToolStripMenuItem
    Friend WithEvents MyFindReplace As FindReplace
    Friend WithEvents btnSaveSql As ToolStripMenuItem
    Friend WithEvents btnDisconnect As Button
    Friend WithEvents btnSelectAll As ToolStripMenuItem
    Friend WithEvents btnClearXpath As ToolStripMenuItem
    Friend WithEvents xpathPanel As Panel
    Friend WithEvents xpathArea As RichTextBox
    Friend WithEvents xpathEvaluatorPanel As Panel
    Friend WithEvents btnExecuteXpath As Button
    Friend WithEvents xpathExpression As TextBox
End Class