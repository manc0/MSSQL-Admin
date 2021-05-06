
Imports FontAwesome.Sharp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MyMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnOpen = New FontAwesome.Sharp.IconMenuItem()
        Me.btnNew = New FontAwesome.Sharp.IconMenuItem()
        Me.tsSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSaveSql = New FontAwesome.Sharp.IconMenuItem()
        Me.btnSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSaveAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnExport = New FontAwesome.Sharp.IconMenuItem()
        Me.tsSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnClearOutput = New FontAwesome.Sharp.IconMenuItem()
        Me.btnClearXpath = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCloseAllEditors = New FontAwesome.Sharp.IconMenuItem()
        Me.btnCloseAllTables = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnExit = New FontAwesome.Sharp.IconMenuItem()
        Me.tsmiEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnUndo = New FontAwesome.Sharp.IconMenuItem()
        Me.btnRedo = New FontAwesome.Sharp.IconMenuItem()
        Me.tsSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCut = New FontAwesome.Sharp.IconMenuItem()
        Me.btnCopy = New FontAwesome.Sharp.IconMenuItem()
        Me.btnPaste = New FontAwesome.Sharp.IconMenuItem()
        Me.btnSelectAll = New FontAwesome.Sharp.IconMenuItem()
        Me.tsSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnReplace = New FontAwesome.Sharp.IconMenuItem()
        Me.btnFind = New FontAwesome.Sharp.IconMenuItem()
        Me.tsmiView = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnFullscreen = New FontAwesome.Sharp.IconMenuItem()
        Me.tsSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnZoomIn = New FontAwesome.Sharp.IconMenuItem()
        Me.btnZoomOut = New FontAwesome.Sharp.IconMenuItem()
        Me.btnResetZoom = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnTableMode = New FontAwesome.Sharp.IconMenuItem()
        Me.leftPanel = New System.Windows.Forms.Panel()
        Me.tbTimeout = New System.Windows.Forms.TextBox()
        Me.btnReload = New FontAwesome.Sharp.IconButton()
        Me.btnExecute = New FontAwesome.Sharp.IconButton()
        Me.btnSubmit = New FontAwesome.Sharp.IconButton()
        Me.lblTables = New System.Windows.Forms.Label()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.lblDatabases = New System.Windows.Forms.Label()
        Me.lbTableList = New System.Windows.Forms.ListBox()
        Me.tbServer = New System.Windows.Forms.TextBox()
        Me.tbUser = New System.Windows.Forms.TextBox()
        Me.btnConnect = New FontAwesome.Sharp.IconButton()
        Me.tbPass = New System.Windows.Forms.TextBox()
        Me.cbDatabases = New System.Windows.Forms.ComboBox()
        Me.btnDisconnect = New FontAwesome.Sharp.IconButton()
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
        Me.tsSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.splitter1 = New System.Windows.Forms.Splitter()
        Me.mainPanel = New System.Windows.Forms.Panel()
        Me.TablesTabControl = New MSSQLA.UserInterface.CustomTabControl()
        Me.EditorsTabControl = New MSSQLA.UserInterface.CustomTabControl()
        Me.btnOutput = New FontAwesome.Sharp.IconToolStripButton()
        Me.btnXpath = New FontAwesome.Sharp.IconToolStripButton()
        Me.MyMenuStrip.SuspendLayout()
        Me.leftPanel.SuspendLayout()
        Me.bottomPanel.SuspendLayout()
        Me.xpathPanel.SuspendLayout()
        Me.xpathEvaluatorPanel.SuspendLayout()
        Me.MyToolStrip.SuspendLayout()
        Me.mainPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'MyMenuStrip
        '
        Me.MyMenuStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.MyMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.tsmiFile, Me.tsmiEdit, Me.tsmiView})
        Me.MyMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MyMenuStrip.Name = "MyMenuStrip"
        Me.MyMenuStrip.Padding = New System.Windows.Forms.Padding(0)
        Me.MyMenuStrip.Size = New System.Drawing.Size(1222, 31)
        Me.MyMenuStrip.TabIndex = 0
        Me.MyMenuStrip.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.MSSQLA.UserInterface.My.Resources.Resources.icon
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Padding = New System.Windows.Forms.Padding(5)
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(30, 31)
        Me.ToolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsmiFile
        '
        Me.tsmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOpen, Me.btnNew, Me.tsSeparator7, Me.btnSaveSql, Me.btnSaveAs, Me.btnSaveAll, Me.btnExport, Me.tsSeparator6, Me.btnClearOutput, Me.btnClearXpath, Me.tsSeparator1, Me.btnCloseAllEditors, Me.btnCloseAllTables, Me.tsSeparator5, Me.btnExit})
        Me.tsmiFile.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.tsmiFile.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.tsmiFile.Name = "tsmiFile"
        Me.tsmiFile.Padding = New System.Windows.Forms.Padding(15, 5, 15, 5)
        Me.tsmiFile.Size = New System.Drawing.Size(61, 31)
        Me.tsmiFile.Text = "&File"
        '
        'btnOpen
        '
        Me.btnOpen.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnOpen.ForeColor = System.Drawing.Color.White
        Me.btnOpen.IconChar = FontAwesome.Sharp.IconChar.FileAlt
        Me.btnOpen.IconColor = System.Drawing.Color.White
        Me.btnOpen.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Padding = New System.Windows.Forms.Padding(2)
        Me.btnOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.btnOpen.Size = New System.Drawing.Size(230, 24)
        Me.btnOpen.Text = "Open File..."
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnNew.ForeColor = System.Drawing.Color.White
        Me.btnNew.IconChar = FontAwesome.Sharp.IconChar.FileCode
        Me.btnNew.IconColor = System.Drawing.Color.White
        Me.btnNew.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Padding = New System.Windows.Forms.Padding(2)
        Me.btnNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.btnNew.Size = New System.Drawing.Size(230, 24)
        Me.btnNew.Text = "New File"
        '
        'tsSeparator7
        '
        Me.tsSeparator7.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator7.ForeColor = System.Drawing.Color.White
        Me.tsSeparator7.Name = "tsSeparator7"
        Me.tsSeparator7.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator7.Size = New System.Drawing.Size(223, 6)
        '
        'btnSaveSql
        '
        Me.btnSaveSql.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnSaveSql.ForeColor = System.Drawing.Color.White
        Me.btnSaveSql.IconChar = FontAwesome.Sharp.IconChar.Save
        Me.btnSaveSql.IconColor = System.Drawing.Color.White
        Me.btnSaveSql.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnSaveSql.Name = "btnSaveSql"
        Me.btnSaveSql.Padding = New System.Windows.Forms.Padding(2)
        Me.btnSaveSql.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.btnSaveSql.Size = New System.Drawing.Size(230, 24)
        Me.btnSaveSql.Text = "Save "
        '
        'btnSaveAs
        '
        Me.btnSaveAs.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnSaveAs.ForeColor = System.Drawing.Color.White
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Padding = New System.Windows.Forms.Padding(2)
        Me.btnSaveAs.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.btnSaveAs.Size = New System.Drawing.Size(230, 24)
        Me.btnSaveAs.Text = "Save As..."
        '
        'btnSaveAll
        '
        Me.btnSaveAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnSaveAll.ForeColor = System.Drawing.Color.White
        Me.btnSaveAll.Name = "btnSaveAll"
        Me.btnSaveAll.Padding = New System.Windows.Forms.Padding(2)
        Me.btnSaveAll.ShortcutKeys = CType(((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.btnSaveAll.Size = New System.Drawing.Size(230, 24)
        Me.btnSaveAll.Text = "Save All"
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnExport.ForeColor = System.Drawing.Color.White
        Me.btnExport.IconChar = FontAwesome.Sharp.IconChar.Table
        Me.btnExport.IconColor = System.Drawing.Color.White
        Me.btnExport.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Padding = New System.Windows.Forms.Padding(2)
        Me.btnExport.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.btnExport.Size = New System.Drawing.Size(230, 24)
        Me.btnExport.Text = "Export Table..."
        '
        'tsSeparator6
        '
        Me.tsSeparator6.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator6.ForeColor = System.Drawing.Color.White
        Me.tsSeparator6.Name = "tsSeparator6"
        Me.tsSeparator6.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator6.Size = New System.Drawing.Size(223, 6)
        '
        'btnClearOutput
        '
        Me.btnClearOutput.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnClearOutput.ForeColor = System.Drawing.Color.White
        Me.btnClearOutput.IconChar = FontAwesome.Sharp.IconChar.TrashAlt
        Me.btnClearOutput.IconColor = System.Drawing.Color.White
        Me.btnClearOutput.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnClearOutput.Name = "btnClearOutput"
        Me.btnClearOutput.Padding = New System.Windows.Forms.Padding(2)
        Me.btnClearOutput.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.btnClearOutput.Size = New System.Drawing.Size(230, 24)
        Me.btnClearOutput.Text = "Clear Output"
        '
        'btnClearXpath
        '
        Me.btnClearXpath.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnClearXpath.ForeColor = System.Drawing.Color.White
        Me.btnClearXpath.Name = "btnClearXpath"
        Me.btnClearXpath.Padding = New System.Windows.Forms.Padding(2)
        Me.btnClearXpath.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.btnClearXpath.Size = New System.Drawing.Size(230, 24)
        Me.btnClearXpath.Text = "Clear XPath"
        '
        'tsSeparator1
        '
        Me.tsSeparator1.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator1.ForeColor = System.Drawing.Color.White
        Me.tsSeparator1.Name = "tsSeparator1"
        Me.tsSeparator1.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator1.Size = New System.Drawing.Size(223, 6)
        '
        'btnCloseAllEditors
        '
        Me.btnCloseAllEditors.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnCloseAllEditors.ForeColor = System.Drawing.Color.White
        Me.btnCloseAllEditors.IconChar = FontAwesome.Sharp.IconChar.Times
        Me.btnCloseAllEditors.IconColor = System.Drawing.Color.White
        Me.btnCloseAllEditors.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnCloseAllEditors.Name = "btnCloseAllEditors"
        Me.btnCloseAllEditors.Padding = New System.Windows.Forms.Padding(2)
        Me.btnCloseAllEditors.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.btnCloseAllEditors.Size = New System.Drawing.Size(230, 24)
        Me.btnCloseAllEditors.Text = "Close All Editors"
        '
        'btnCloseAllTables
        '
        Me.btnCloseAllTables.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnCloseAllTables.ForeColor = System.Drawing.Color.White
        Me.btnCloseAllTables.Name = "btnCloseAllTables"
        Me.btnCloseAllTables.Padding = New System.Windows.Forms.Padding(2)
        Me.btnCloseAllTables.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.btnCloseAllTables.Size = New System.Drawing.Size(230, 24)
        Me.btnCloseAllTables.Text = "Close All Tables"
        '
        'tsSeparator5
        '
        Me.tsSeparator5.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator5.ForeColor = System.Drawing.Color.White
        Me.tsSeparator5.Name = "tsSeparator5"
        Me.tsSeparator5.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator5.Size = New System.Drawing.Size(223, 6)
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.IconChar = FontAwesome.Sharp.IconChar.DoorOpen
        Me.btnExit.IconColor = System.Drawing.Color.White
        Me.btnExit.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Padding = New System.Windows.Forms.Padding(2)
        Me.btnExit.Size = New System.Drawing.Size(230, 24)
        Me.btnExit.Text = "Exit"
        '
        'tsmiEdit
        '
        Me.tsmiEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnUndo, Me.btnRedo, Me.tsSeparator2, Me.btnCut, Me.btnCopy, Me.btnPaste, Me.btnSelectAll, Me.tsSeparator3, Me.btnReplace, Me.btnFind})
        Me.tsmiEdit.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.tsmiEdit.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.tsmiEdit.Name = "tsmiEdit"
        Me.tsmiEdit.Padding = New System.Windows.Forms.Padding(15, 5, 15, 5)
        Me.tsmiEdit.Size = New System.Drawing.Size(64, 31)
        Me.tsmiEdit.Text = "&Edit"
        '
        'btnUndo
        '
        Me.btnUndo.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnUndo.ForeColor = System.Drawing.Color.White
        Me.btnUndo.IconChar = FontAwesome.Sharp.IconChar.Undo
        Me.btnUndo.IconColor = System.Drawing.Color.White
        Me.btnUndo.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnUndo.Name = "btnUndo"
        Me.btnUndo.Padding = New System.Windows.Forms.Padding(2)
        Me.btnUndo.ShortcutKeyDisplayString = "Ctrl+Z"
        Me.btnUndo.Size = New System.Drawing.Size(184, 24)
        Me.btnUndo.Text = "Undo"
        '
        'btnRedo
        '
        Me.btnRedo.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnRedo.ForeColor = System.Drawing.Color.White
        Me.btnRedo.IconChar = FontAwesome.Sharp.IconChar.Redo
        Me.btnRedo.IconColor = System.Drawing.Color.White
        Me.btnRedo.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnRedo.Name = "btnRedo"
        Me.btnRedo.Padding = New System.Windows.Forms.Padding(2)
        Me.btnRedo.ShortcutKeyDisplayString = "Ctrl+Y"
        Me.btnRedo.Size = New System.Drawing.Size(184, 24)
        Me.btnRedo.Text = "Redo"
        '
        'tsSeparator2
        '
        Me.tsSeparator2.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator2.ForeColor = System.Drawing.Color.White
        Me.tsSeparator2.Name = "tsSeparator2"
        Me.tsSeparator2.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator2.Size = New System.Drawing.Size(177, 6)
        '
        'btnCut
        '
        Me.btnCut.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnCut.ForeColor = System.Drawing.Color.White
        Me.btnCut.IconChar = FontAwesome.Sharp.IconChar.Cut
        Me.btnCut.IconColor = System.Drawing.Color.White
        Me.btnCut.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnCut.Name = "btnCut"
        Me.btnCut.Padding = New System.Windows.Forms.Padding(2)
        Me.btnCut.ShortcutKeyDisplayString = "Ctrl+X"
        Me.btnCut.Size = New System.Drawing.Size(184, 24)
        Me.btnCut.Text = "Cut"
        '
        'btnCopy
        '
        Me.btnCopy.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnCopy.ForeColor = System.Drawing.Color.White
        Me.btnCopy.IconChar = FontAwesome.Sharp.IconChar.Copy
        Me.btnCopy.IconColor = System.Drawing.Color.White
        Me.btnCopy.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Padding = New System.Windows.Forms.Padding(2)
        Me.btnCopy.ShortcutKeyDisplayString = "Ctrl+C"
        Me.btnCopy.Size = New System.Drawing.Size(184, 24)
        Me.btnCopy.Text = "Copy"
        '
        'btnPaste
        '
        Me.btnPaste.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnPaste.ForeColor = System.Drawing.Color.White
        Me.btnPaste.IconChar = FontAwesome.Sharp.IconChar.Paste
        Me.btnPaste.IconColor = System.Drawing.Color.White
        Me.btnPaste.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnPaste.Name = "btnPaste"
        Me.btnPaste.Padding = New System.Windows.Forms.Padding(2)
        Me.btnPaste.ShortcutKeyDisplayString = "Ctrl+V"
        Me.btnPaste.Size = New System.Drawing.Size(184, 24)
        Me.btnPaste.Text = "Paste"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnSelectAll.ForeColor = System.Drawing.Color.White
        Me.btnSelectAll.IconChar = FontAwesome.Sharp.IconChar.Stream
        Me.btnSelectAll.IconColor = System.Drawing.Color.White
        Me.btnSelectAll.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Padding = New System.Windows.Forms.Padding(2)
        Me.btnSelectAll.ShortcutKeyDisplayString = "Ctrl+A"
        Me.btnSelectAll.Size = New System.Drawing.Size(184, 24)
        Me.btnSelectAll.Text = "Select All"
        '
        'tsSeparator3
        '
        Me.tsSeparator3.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator3.ForeColor = System.Drawing.Color.White
        Me.tsSeparator3.Name = "tsSeparator3"
        Me.tsSeparator3.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator3.Size = New System.Drawing.Size(177, 6)
        '
        'btnReplace
        '
        Me.btnReplace.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnReplace.ForeColor = System.Drawing.Color.White
        Me.btnReplace.IconChar = FontAwesome.Sharp.IconChar.Wpforms
        Me.btnReplace.IconColor = System.Drawing.Color.White
        Me.btnReplace.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnReplace.Name = "btnReplace"
        Me.btnReplace.Padding = New System.Windows.Forms.Padding(2)
        Me.btnReplace.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.btnReplace.Size = New System.Drawing.Size(184, 24)
        Me.btnReplace.Text = "Replace"
        '
        'btnFind
        '
        Me.btnFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnFind.ForeColor = System.Drawing.Color.White
        Me.btnFind.IconChar = FontAwesome.Sharp.IconChar.Search
        Me.btnFind.IconColor = System.Drawing.Color.White
        Me.btnFind.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Padding = New System.Windows.Forms.Padding(2)
        Me.btnFind.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.btnFind.Size = New System.Drawing.Size(184, 24)
        Me.btnFind.Text = "Find"
        '
        'tsmiView
        '
        Me.tsmiView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnFullscreen, Me.tsSeparator9, Me.btnZoomIn, Me.btnZoomOut, Me.btnResetZoom, Me.tsSeparator8, Me.btnTableMode})
        Me.tsmiView.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.tsmiView.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.tsmiView.Name = "tsmiView"
        Me.tsmiView.Padding = New System.Windows.Forms.Padding(15, 5, 15, 5)
        Me.tsmiView.Size = New System.Drawing.Size(69, 31)
        Me.tsmiView.Text = "&View"
        '
        'btnFullscreen
        '
        Me.btnFullscreen.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnFullscreen.CheckOnClick = True
        Me.btnFullscreen.ForeColor = System.Drawing.Color.White
        Me.btnFullscreen.IconChar = FontAwesome.Sharp.IconChar.ExpandAlt
        Me.btnFullscreen.IconColor = System.Drawing.Color.White
        Me.btnFullscreen.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnFullscreen.Name = "btnFullscreen"
        Me.btnFullscreen.Padding = New System.Windows.Forms.Padding(2)
        Me.btnFullscreen.ShortcutKeyDisplayString = ""
        Me.btnFullscreen.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F11), System.Windows.Forms.Keys)
        Me.btnFullscreen.Size = New System.Drawing.Size(244, 24)
        Me.btnFullscreen.Text = "Full Screen"
        '
        'tsSeparator9
        '
        Me.tsSeparator9.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator9.ForeColor = System.Drawing.Color.White
        Me.tsSeparator9.Name = "tsSeparator9"
        Me.tsSeparator9.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator9.Size = New System.Drawing.Size(237, 6)
        '
        'btnZoomIn
        '
        Me.btnZoomIn.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnZoomIn.ForeColor = System.Drawing.Color.White
        Me.btnZoomIn.IconChar = FontAwesome.Sharp.IconChar.SearchPlus
        Me.btnZoomIn.IconColor = System.Drawing.Color.White
        Me.btnZoomIn.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnZoomIn.Name = "btnZoomIn"
        Me.btnZoomIn.Padding = New System.Windows.Forms.Padding(2)
        Me.btnZoomIn.ShortcutKeyDisplayString = "Ctrl++"
        Me.btnZoomIn.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Oemplus), System.Windows.Forms.Keys)
        Me.btnZoomIn.Size = New System.Drawing.Size(244, 24)
        Me.btnZoomIn.Text = "Zoom In"
        '
        'btnZoomOut
        '
        Me.btnZoomOut.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnZoomOut.ForeColor = System.Drawing.Color.White
        Me.btnZoomOut.IconChar = FontAwesome.Sharp.IconChar.SearchMinus
        Me.btnZoomOut.IconColor = System.Drawing.Color.White
        Me.btnZoomOut.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnZoomOut.Name = "btnZoomOut"
        Me.btnZoomOut.Padding = New System.Windows.Forms.Padding(2)
        Me.btnZoomOut.ShortcutKeyDisplayString = "Ctrl+-"
        Me.btnZoomOut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.OemMinus), System.Windows.Forms.Keys)
        Me.btnZoomOut.Size = New System.Drawing.Size(244, 24)
        Me.btnZoomOut.Text = "Zoom Out"
        '
        'btnResetZoom
        '
        Me.btnResetZoom.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnResetZoom.ForeColor = System.Drawing.Color.White
        Me.btnResetZoom.Name = "btnResetZoom"
        Me.btnResetZoom.Padding = New System.Windows.Forms.Padding(2)
        Me.btnResetZoom.ShortcutKeyDisplayString = ""
        Me.btnResetZoom.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.NumPad0), System.Windows.Forms.Keys)
        Me.btnResetZoom.Size = New System.Drawing.Size(244, 24)
        Me.btnResetZoom.Text = "Reset Zoom"
        '
        'tsSeparator8
        '
        Me.tsSeparator8.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.tsSeparator8.ForeColor = System.Drawing.Color.White
        Me.tsSeparator8.Name = "tsSeparator8"
        Me.tsSeparator8.Padding = New System.Windows.Forms.Padding(2)
        Me.tsSeparator8.Size = New System.Drawing.Size(237, 6)
        '
        'btnTableMode
        '
        Me.btnTableMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.btnTableMode.Checked = True
        Me.btnTableMode.CheckOnClick = True
        Me.btnTableMode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.btnTableMode.ForeColor = System.Drawing.Color.White
        Me.btnTableMode.IconChar = FontAwesome.Sharp.IconChar.Check
        Me.btnTableMode.IconColor = System.Drawing.Color.CornflowerBlue
        Me.btnTableMode.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnTableMode.Name = "btnTableMode"
        Me.btnTableMode.Padding = New System.Windows.Forms.Padding(2)
        Me.btnTableMode.Size = New System.Drawing.Size(244, 24)
        Me.btnTableMode.Text = "Fill Table Columns"
        '
        'leftPanel
        '
        Me.leftPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.leftPanel.Controls.Add(Me.tbTimeout)
        Me.leftPanel.Controls.Add(Me.btnReload)
        Me.leftPanel.Controls.Add(Me.btnExecute)
        Me.leftPanel.Controls.Add(Me.btnSubmit)
        Me.leftPanel.Controls.Add(Me.lblTables)
        Me.leftPanel.Controls.Add(Me.lblServer)
        Me.leftPanel.Controls.Add(Me.lblDatabases)
        Me.leftPanel.Controls.Add(Me.lbTableList)
        Me.leftPanel.Controls.Add(Me.tbServer)
        Me.leftPanel.Controls.Add(Me.tbUser)
        Me.leftPanel.Controls.Add(Me.btnConnect)
        Me.leftPanel.Controls.Add(Me.tbPass)
        Me.leftPanel.Controls.Add(Me.cbDatabases)
        Me.leftPanel.Controls.Add(Me.btnDisconnect)
        Me.leftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.leftPanel.Location = New System.Drawing.Point(0, 31)
        Me.leftPanel.Name = "leftPanel"
        Me.leftPanel.Size = New System.Drawing.Size(269, 708)
        Me.leftPanel.TabIndex = 1
        '
        'tbTimeout
        '
        Me.tbTimeout.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbTimeout.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.tbTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbTimeout.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTimeout.ForeColor = System.Drawing.Color.Gray
        Me.tbTimeout.Location = New System.Drawing.Point(202, 629)
        Me.tbTimeout.Margin = New System.Windows.Forms.Padding(8, 10, 8, 0)
        Me.tbTimeout.MaxLength = 2
        Me.tbTimeout.Name = "tbTimeout"
        Me.tbTimeout.Size = New System.Drawing.Size(53, 25)
        Me.tbTimeout.TabIndex = 14
        Me.tbTimeout.Text = "Timeout"
        '
        'btnReload
        '
        Me.btnReload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReload.Enabled = False
        Me.btnReload.FlatAppearance.BorderSize = 0
        Me.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReload.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReload.ForeColor = System.Drawing.Color.White
        Me.btnReload.IconChar = FontAwesome.Sharp.IconChar.SyncAlt
        Me.btnReload.IconColor = System.Drawing.Color.White
        Me.btnReload.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnReload.IconSize = 24
        Me.btnReload.Location = New System.Drawing.Point(227, 30)
        Me.btnReload.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.btnReload.Name = "btnReload"
        Me.btnReload.Size = New System.Drawing.Size(28, 25)
        Me.btnReload.TabIndex = 13
        Me.btnReload.UseVisualStyleBackColor = False
        '
        'btnExecute
        '
        Me.btnExecute.BackColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnExecute.Enabled = False
        Me.btnExecute.FlatAppearance.BorderSize = 0
        Me.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExecute.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExecute.ForeColor = System.Drawing.Color.White
        Me.btnExecute.IconChar = FontAwesome.Sharp.IconChar.PlayCircle
        Me.btnExecute.IconColor = System.Drawing.Color.Lime
        Me.btnExecute.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnExecute.IconSize = 24
        Me.btnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExecute.Location = New System.Drawing.Point(15, 68)
        Me.btnExecute.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(117, 30)
        Me.btnExecute.TabIndex = 2
        Me.btnExecute.Text = "EXECUTE"
        Me.btnExecute.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExecute.UseVisualStyleBackColor = False
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
        Me.btnSubmit.IconChar = FontAwesome.Sharp.IconChar.Upload
        Me.btnSubmit.IconColor = System.Drawing.Color.White
        Me.btnSubmit.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnSubmit.IconSize = 24
        Me.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSubmit.Location = New System.Drawing.Point(138, 68)
        Me.btnSubmit.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(117, 30)
        Me.btnSubmit.TabIndex = 3
        Me.btnSubmit.Text = "SUBMIT"
        Me.btnSubmit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSubmit.UseVisualStyleBackColor = False
        '
        'lblTables
        '
        Me.lblTables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTables.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTables.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTables.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblTables.Location = New System.Drawing.Point(0, 111)
        Me.lblTables.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lblTables.Name = "lblTables"
        Me.lblTables.Size = New System.Drawing.Size(274, 20)
        Me.lblTables.TabIndex = 12
        Me.lblTables.Text = "OBJECT EXPLORER"
        '
        'lblServer
        '
        Me.lblServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServer.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblServer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServer.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblServer.Location = New System.Drawing.Point(0, 529)
        Me.lblServer.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(271, 20)
        Me.lblServer.TabIndex = 9
        Me.lblServer.Text = "CONNECTION"
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
        Me.lbTableList.Size = New System.Drawing.Size(240, 374)
        Me.lbTableList.TabIndex = 4
        '
        'tbServer
        '
        Me.tbServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbServer.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.tbServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbServer.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbServer.ForeColor = System.Drawing.Color.Gray
        Me.tbServer.Location = New System.Drawing.Point(15, 559)
        Me.tbServer.Margin = New System.Windows.Forms.Padding(8, 10, 8, 0)
        Me.tbServer.Name = "tbServer"
        Me.tbServer.Size = New System.Drawing.Size(240, 25)
        Me.tbServer.TabIndex = 5
        Me.tbServer.Text = "Server"
        '
        'tbUser
        '
        Me.tbUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.tbUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbUser.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbUser.ForeColor = System.Drawing.Color.Gray
        Me.tbUser.Location = New System.Drawing.Point(15, 594)
        Me.tbUser.Margin = New System.Windows.Forms.Padding(8, 10, 8, 0)
        Me.tbUser.Name = "tbUser"
        Me.tbUser.Size = New System.Drawing.Size(240, 25)
        Me.tbUser.TabIndex = 6
        Me.tbUser.Text = "User"
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
        Me.btnConnect.IconChar = FontAwesome.Sharp.IconChar.Database
        Me.btnConnect.IconColor = System.Drawing.Color.White
        Me.btnConnect.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnConnect.IconSize = 24
        Me.btnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConnect.Location = New System.Drawing.Point(15, 664)
        Me.btnConnect.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(117, 30)
        Me.btnConnect.TabIndex = 8
        Me.btnConnect.Text = "CONNECT"
        Me.btnConnect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'tbPass
        '
        Me.tbPass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbPass.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.tbPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbPass.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPass.ForeColor = System.Drawing.Color.Gray
        Me.tbPass.Location = New System.Drawing.Point(15, 629)
        Me.tbPass.Margin = New System.Windows.Forms.Padding(8, 10, 8, 0)
        Me.tbPass.Name = "tbPass"
        Me.tbPass.Size = New System.Drawing.Size(182, 25)
        Me.tbPass.TabIndex = 7
        Me.tbPass.Text = "Password"
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
        Me.cbDatabases.Size = New System.Drawing.Size(206, 25)
        Me.cbDatabases.TabIndex = 1
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
        Me.btnDisconnect.IconChar = FontAwesome.Sharp.IconChar.Ban
        Me.btnDisconnect.IconColor = System.Drawing.Color.White
        Me.btnDisconnect.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnDisconnect.IconSize = 24
        Me.btnDisconnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDisconnect.Location = New System.Drawing.Point(138, 664)
        Me.btnDisconnect.Margin = New System.Windows.Forms.Padding(3, 10, 3, 0)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(117, 30)
        Me.btnDisconnect.TabIndex = 9
        Me.btnDisconnect.Text = "DISCONNECT"
        Me.btnDisconnect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDisconnect.UseVisualStyleBackColor = False
        '
        'splitter2
        '
        Me.splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(79, Byte), Integer))
        Me.splitter2.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.splitter2.Location = New System.Drawing.Point(0, 289)
        Me.splitter2.Name = "splitter2"
        Me.splitter2.Size = New System.Drawing.Size(953, 2)
        Me.splitter2.TabIndex = 11
        Me.splitter2.TabStop = False
        Me.splitter2.Visible = False
        '
        'bottomPanel
        '
        Me.bottomPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.bottomPanel.Controls.Add(Me.xpathPanel)
        Me.bottomPanel.Controls.Add(Me.outputArea)
        Me.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.bottomPanel.Location = New System.Drawing.Point(0, 509)
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
        Me.xpathExpression.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.xpathExpression.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.xpathExpression.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xpathExpression.ForeColor = System.Drawing.Color.LightGray
        Me.xpathExpression.Location = New System.Drawing.Point(36, 6)
        Me.xpathExpression.Margin = New System.Windows.Forms.Padding(8, 10, 8, 0)
        Me.xpathExpression.Name = "xpathExpression"
        Me.xpathExpression.Size = New System.Drawing.Size(889, 19)
        Me.xpathExpression.TabIndex = 12
        Me.xpathExpression.TabStop = False
        Me.xpathExpression.Text = "//"
        Me.xpathExpression.WordWrap = False
        '
        'outputArea
        '
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
        Me.MyToolStrip.Location = New System.Drawing.Point(0, 682)
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
        'tsSeparator4
        '
        Me.tsSeparator4.ForeColor = System.Drawing.Color.LightGray
        Me.tsSeparator4.Name = "tsSeparator4"
        Me.tsSeparator4.Size = New System.Drawing.Size(6, 24)
        '
        'splitter1
        '
        Me.splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(79, Byte), Integer))
        Me.splitter1.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.splitter1.Location = New System.Drawing.Point(0, 507)
        Me.splitter1.Name = "splitter1"
        Me.splitter1.Size = New System.Drawing.Size(953, 2)
        Me.splitter1.TabIndex = 13
        Me.splitter1.TabStop = False
        Me.splitter1.Visible = False
        '
        'mainPanel
        '
        Me.mainPanel.Controls.Add(Me.TablesTabControl)
        Me.mainPanel.Controls.Add(Me.splitter2)
        Me.mainPanel.Controls.Add(Me.splitter1)
        Me.mainPanel.Controls.Add(Me.bottomPanel)
        Me.mainPanel.Controls.Add(Me.MyToolStrip)
        Me.mainPanel.Controls.Add(Me.EditorsTabControl)
        Me.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainPanel.Location = New System.Drawing.Point(269, 31)
        Me.mainPanel.Name = "mainPanel"
        Me.mainPanel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.mainPanel.Size = New System.Drawing.Size(953, 708)
        Me.mainPanel.TabIndex = 3
        '
        'TablesTabControl
        '
        Me.TablesTabControl.DefaultTabColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.TablesTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablesTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TablesTabControl.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.TablesTabControl.HasAddButton = False
        Me.TablesTabControl.HotTabColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.TablesTabControl.Location = New System.Drawing.Point(0, 291)
        Me.TablesTabControl.Margin = New System.Windows.Forms.Padding(0)
        Me.TablesTabControl.Name = "TablesTabControl"
        Me.TablesTabControl.Padding = New System.Drawing.Point(0, 0)
        Me.TablesTabControl.SelectedIndex = 0
        Me.TablesTabControl.SelectedTabColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.TablesTabControl.Size = New System.Drawing.Size(953, 216)
        Me.TablesTabControl.TabForeColor = System.Drawing.Color.White
        Me.TablesTabControl.TabIndex = 14
        Me.TablesTabControl.Visible = False
        '
        'EditorsTabControl
        '
        Me.EditorsTabControl.DefaultTabColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.EditorsTabControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.EditorsTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.EditorsTabControl.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.EditorsTabControl.HasAddButton = True
        Me.EditorsTabControl.HotTabColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.EditorsTabControl.Location = New System.Drawing.Point(0, 5)
        Me.EditorsTabControl.Margin = New System.Windows.Forms.Padding(0)
        Me.EditorsTabControl.Name = "EditorsTabControl"
        Me.EditorsTabControl.Padding = New System.Drawing.Point(0, 0)
        Me.EditorsTabControl.SelectedIndex = 0
        Me.EditorsTabControl.SelectedTabColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.EditorsTabControl.Size = New System.Drawing.Size(953, 284)
        Me.EditorsTabControl.TabForeColor = System.Drawing.Color.White
        Me.EditorsTabControl.TabIndex = 16
        Me.EditorsTabControl.Visible = False
        '
        'btnOutput
        '
        Me.btnOutput.AutoToolTip = False
        Me.btnOutput.CheckOnClick = True
        Me.btnOutput.ForeColor = System.Drawing.Color.DodgerBlue
        Me.btnOutput.IconChar = FontAwesome.Sharp.IconChar.Terminal
        Me.btnOutput.IconColor = System.Drawing.Color.CornflowerBlue
        Me.btnOutput.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnOutput.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(68, 21)
        Me.btnOutput.Text = "Output"
        '
        'btnXpath
        '
        Me.btnXpath.AutoToolTip = False
        Me.btnXpath.CheckOnClick = True
        Me.btnXpath.ForeColor = System.Drawing.Color.DodgerBlue
        Me.btnXpath.IconChar = FontAwesome.Sharp.IconChar.Code
        Me.btnXpath.IconColor = System.Drawing.Color.CornflowerBlue
        Me.btnXpath.IconFont = FontAwesome.Sharp.IconFont.[Auto]
        Me.btnXpath.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnXpath.Name = "btnXpath"
        Me.btnXpath.Size = New System.Drawing.Size(119, 21)
        Me.btnXpath.Text = "XPath Evaluator"
        '
        'MainForm
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1222, 739)
        Me.Controls.Add(Me.mainPanel)
        Me.Controls.Add(Me.leftPanel)
        Me.Controls.Add(Me.MyMenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MyMenuStrip
        Me.MinimumSize = New System.Drawing.Size(600, 600)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MSSQL Admin"
        Me.MyMenuStrip.ResumeLayout(False)
        Me.MyMenuStrip.PerformLayout()
        Me.leftPanel.ResumeLayout(False)
        Me.leftPanel.PerformLayout()
        Me.bottomPanel.ResumeLayout(False)
        Me.xpathPanel.ResumeLayout(False)
        Me.xpathEvaluatorPanel.ResumeLayout(False)
        Me.xpathEvaluatorPanel.PerformLayout()
        Me.MyToolStrip.ResumeLayout(False)
        Me.MyToolStrip.PerformLayout()
        Me.mainPanel.ResumeLayout(False)
        Me.mainPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MyMenuStrip As MenuStrip
    Friend WithEvents tsmiFile As ToolStripMenuItem
    Friend WithEvents leftPanel As Panel
    Friend WithEvents tsmiEdit As ToolStripMenuItem
    Friend WithEvents btnConnect As IconButton
    Friend WithEvents tbPass As TextBox
    Friend WithEvents cbDatabases As ComboBox
    Friend WithEvents lblDatabases As Label
    Friend WithEvents lblServer As Label
    Friend WithEvents tbServer As TextBox
    Friend WithEvents tbUser As TextBox
    Friend WithEvents btnExecute As IconButton
    Friend WithEvents lbTableList As ListBox
    Friend WithEvents lblTables As Label
    Friend WithEvents splitter2 As Splitter
    Friend WithEvents bottomPanel As Panel
    Friend WithEvents splitter1 As Splitter
    Friend WithEvents mainPanel As Panel
    Friend WithEvents outputArea As RichTextBox
    Friend WithEvents MyToolStrip As ToolStrip
    Friend WithEvents lblConnStatus As ToolStripLabel
    Friend WithEvents tsSeparator4 As ToolStripSeparator
    Friend WithEvents btnSubmit As IconButton
    Friend WithEvents tsSeparator1 As ToolStripSeparator
    Friend WithEvents tsSeparator2 As ToolStripSeparator
    Friend WithEvents tsSeparator3 As ToolStripSeparator
    Friend WithEvents btnDisconnect As IconButton
    Friend WithEvents btnClearXpath As ToolStripMenuItem
    Friend WithEvents xpathPanel As Panel
    Friend WithEvents xpathArea As RichTextBox
    Friend WithEvents xpathEvaluatorPanel As Panel
    Friend WithEvents btnExecuteXpath As Button
    Friend WithEvents xpathExpression As TextBox
    Friend WithEvents btnReload As IconButton
    Friend WithEvents btnCloseAllTables As ToolStripMenuItem
    Friend WithEvents tsSeparator5 As ToolStripSeparator
    Friend WithEvents btnSaveAs As ToolStripMenuItem
    Friend WithEvents tsSeparator6 As ToolStripSeparator
    Friend WithEvents TablesTabControl As CustomTabControl
    Friend WithEvents EditorsTabControl As CustomTabControl
    Friend WithEvents tsSeparator7 As ToolStripSeparator
    Friend WithEvents btnSaveAll As ToolStripMenuItem
    Friend WithEvents tsmiView As ToolStripMenuItem
    Friend WithEvents btnResetZoom As ToolStripMenuItem
    Friend WithEvents tsSeparator8 As ToolStripSeparator
    Friend WithEvents tsSeparator9 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents btnOpen As IconMenuItem
    Friend WithEvents btnSaveSql As IconMenuItem
    Friend WithEvents btnNew As IconMenuItem
    Friend WithEvents btnExport As IconMenuItem
    Friend WithEvents btnClearOutput As IconMenuItem
    Friend WithEvents btnCloseAllEditors As IconMenuItem
    Friend WithEvents btnExit As IconMenuItem
    Friend WithEvents btnUndo As IconMenuItem
    Friend WithEvents btnRedo As IconMenuItem
    Friend WithEvents btnCut As IconMenuItem
    Friend WithEvents btnCopy As IconMenuItem
    Friend WithEvents btnPaste As IconMenuItem
    Friend WithEvents btnSelectAll As IconMenuItem
    Friend WithEvents btnReplace As IconMenuItem
    Friend WithEvents btnFind As IconMenuItem
    Friend WithEvents btnFullscreen As IconMenuItem
    Friend WithEvents btnZoomIn As IconMenuItem
    Friend WithEvents btnZoomOut As IconMenuItem
    Friend WithEvents btnTableMode As IconMenuItem
    Friend WithEvents tbTimeout As TextBox
    Friend WithEvents btnOutput As IconToolStripButton
    Friend WithEvents btnXpath As IconToolStripButton
End Class