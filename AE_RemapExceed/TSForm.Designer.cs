namespace AE_RemapExceed
{
	partial class TSForm
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TSForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.FileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuSepa1 = new System.Windows.Forms.ToolStripSeparator();
			this.FileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.FileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.FileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.FilePrint = new System.Windows.Forms.ToolStripMenuItem();
			this.FilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
			this.FilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuSepa4 = new System.Windows.Forms.ToolStripSeparator();
			this.FileQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.EditCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.EditCutMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.EditPasteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.ClearALLMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ClearLayerMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuSepa5 = new System.Windows.Forms.ToolStripSeparator();
			this.SettingMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.cmSettings = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.LayoutSettingDlg = new System.Windows.Forms.ToolStripMenuItem();
			this.ColorSettingDlg = new System.Windows.Forms.ToolStripMenuItem();
			this.PrintSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.LayerMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.cmLayer = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.layerInsert = new System.Windows.Forms.ToolStripMenuItem();
			this.layerRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.LayerRename = new System.Windows.Forms.ToolStripMenuItem();
			this.FrameMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.cmFrame = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.FrameDisp = new System.Windows.Forms.ToolStripMenuItem();
			this.cmFrameDisp = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmFrameDisp_Frame = new System.Windows.Forms.ToolStripMenuItem();
			this.cmFrameDisp_PageFrame = new System.Windows.Forms.ToolStripMenuItem();
			this.cmFrameDisp_PageSecFrame = new System.Windows.Forms.ToolStripMenuItem();
			this.cmFrameDisp_SecFrame = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuSepa8 = new System.Windows.Forms.ToolStripSeparator();
			this.frameInsert = new System.Windows.Forms.ToolStripMenuItem();
			this.frameDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.autoInput = new System.Windows.Forms.ToolStripMenuItem();
			this.valueEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.WindowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.PreviewDlg = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.ScriptToClipMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.tsNav1 = new AE_RemapExceed.TSNav();
			this.tsGrid1 = new AE_RemapExceed.TSGrid();
			this.tsCellCaption1 = new AE_RemapExceed.TSCellCaption();
			this.tsFrame1 = new AE_RemapExceed.TSFrame();
			this.tsInput1 = new AE_RemapExceed.TSInput();
			this.menuStrip1.SuspendLayout();
			this.cmSettings.SuspendLayout();
			this.cmLayer.SuspendLayout();
			this.cmFrame.SuspendLayout();
			this.cmFrameDisp.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.EditMenu,
            this.LayerMenu,
            this.FrameMenu,
            this.WindowMenu,
            this.HelpMenu});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(403, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Enter += new System.EventHandler(this.MainForm_Enter);
			// 
			// FileMenu
			// 
			this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNew,
            this.MenuSepa1,
            this.FileOpen,
            this.FileSave,
            this.FileSaveAs,
            this.toolStripMenuItem1,
            this.FilePrint,
            this.FilePrintPreview,
            this.FilePageSetup,
            this.MenuSepa4,
            this.FileQuit});
			this.FileMenu.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FileMenu.Name = "FileMenu";
			this.FileMenu.Size = new System.Drawing.Size(56, 20);
			this.FileMenu.Text = "ファイル";
			this.FileMenu.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
			// 
			// FileNew
			// 
			this.FileNew.Name = "FileNew";
			this.FileNew.Size = new System.Drawing.Size(146, 22);
			this.FileNew.Text = "シート設定";
			this.FileNew.Click += new System.EventHandler(this.menu_Click);
			// 
			// MenuSepa1
			// 
			this.MenuSepa1.Name = "MenuSepa1";
			this.MenuSepa1.Size = new System.Drawing.Size(143, 6);
			// 
			// FileOpen
			// 
			this.FileOpen.Name = "FileOpen";
			this.FileOpen.Size = new System.Drawing.Size(146, 22);
			this.FileOpen.Text = "開く";
			this.FileOpen.Click += new System.EventHandler(this.menu_Click);
			// 
			// FileSave
			// 
			this.FileSave.Name = "FileSave";
			this.FileSave.Size = new System.Drawing.Size(146, 22);
			this.FileSave.Text = "保存";
			this.FileSave.Click += new System.EventHandler(this.menu_Click);
			// 
			// FileSaveAs
			// 
			this.FileSaveAs.Name = "FileSaveAs";
			this.FileSaveAs.Size = new System.Drawing.Size(146, 22);
			this.FileSaveAs.Text = "別名で保存";
			this.FileSaveAs.Click += new System.EventHandler(this.menu_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(143, 6);
			// 
			// FilePrint
			// 
			this.FilePrint.Name = "FilePrint";
			this.FilePrint.Size = new System.Drawing.Size(146, 22);
			this.FilePrint.Text = "Print";
			this.FilePrint.Click += new System.EventHandler(this.menu_Click);
			// 
			// FilePrintPreview
			// 
			this.FilePrintPreview.Name = "FilePrintPreview";
			this.FilePrintPreview.Size = new System.Drawing.Size(146, 22);
			this.FilePrintPreview.Text = "PrintPreview";
			this.FilePrintPreview.Click += new System.EventHandler(this.menu_Click);
			// 
			// FilePageSetup
			// 
			this.FilePageSetup.Name = "FilePageSetup";
			this.FilePageSetup.Size = new System.Drawing.Size(146, 22);
			this.FilePageSetup.Text = "ページ設定";
			this.FilePageSetup.Click += new System.EventHandler(this.menu_Click);
			// 
			// MenuSepa4
			// 
			this.MenuSepa4.Name = "MenuSepa4";
			this.MenuSepa4.Size = new System.Drawing.Size(143, 6);
			// 
			// FileQuit
			// 
			this.FileQuit.Name = "FileQuit";
			this.FileQuit.Size = new System.Drawing.Size(146, 22);
			this.FileQuit.Text = "終了";
			this.FileQuit.Click += new System.EventHandler(this.menu_Click);
			// 
			// EditMenu
			// 
			this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditCopyMenu,
            this.EditCutMenu,
            this.EditPasteMenu,
            this.toolStripMenuItem2,
            this.ClearALLMenu,
            this.ClearLayerMenu,
            this.MenuSepa5,
            this.SettingMenu});
			this.EditMenu.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.EditMenu.Name = "EditMenu";
			this.EditMenu.Size = new System.Drawing.Size(45, 20);
			this.EditMenu.Text = "編集";
			this.EditMenu.Click += new System.EventHandler(this.EditMenu_Click);
			// 
			// EditCopyMenu
			// 
			this.EditCopyMenu.Name = "EditCopyMenu";
			this.EditCopyMenu.Size = new System.Drawing.Size(152, 22);
			this.EditCopyMenu.Text = "コピー";
			this.EditCopyMenu.Click += new System.EventHandler(this.menu_Click);
			// 
			// EditCutMenu
			// 
			this.EditCutMenu.Name = "EditCutMenu";
			this.EditCutMenu.Size = new System.Drawing.Size(152, 22);
			this.EditCutMenu.Text = "カット";
			this.EditCutMenu.Click += new System.EventHandler(this.menu_Click);
			// 
			// EditPasteMenu
			// 
			this.EditPasteMenu.Name = "EditPasteMenu";
			this.EditPasteMenu.Size = new System.Drawing.Size(152, 22);
			this.EditPasteMenu.Text = "貼り付け";
			this.EditPasteMenu.Click += new System.EventHandler(this.menu_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
			// 
			// ClearALLMenu
			// 
			this.ClearALLMenu.Name = "ClearALLMenu";
			this.ClearALLMenu.Size = new System.Drawing.Size(152, 22);
			this.ClearALLMenu.Text = "全クリア";
			this.ClearALLMenu.Click += new System.EventHandler(this.menu_Click);
			// 
			// ClearLayerMenu
			// 
			this.ClearLayerMenu.Name = "ClearLayerMenu";
			this.ClearLayerMenu.Size = new System.Drawing.Size(152, 22);
			this.ClearLayerMenu.Text = "レイヤーをクリア";
			this.ClearLayerMenu.Click += new System.EventHandler(this.menu_Click);
			// 
			// MenuSepa5
			// 
			this.MenuSepa5.Name = "MenuSepa5";
			this.MenuSepa5.Size = new System.Drawing.Size(149, 6);
			// 
			// SettingMenu
			// 
			this.SettingMenu.DropDown = this.cmSettings;
			this.SettingMenu.Name = "SettingMenu";
			this.SettingMenu.Size = new System.Drawing.Size(152, 22);
			this.SettingMenu.Text = "設定...";
			// 
			// cmSettings
			// 
			this.cmSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LayoutSettingDlg,
            this.ColorSettingDlg,
            this.PrintSettings});
			this.cmSettings.Name = "cmSettings";
			this.cmSettings.OwnerItem = this.SettingMenu;
			this.cmSettings.Size = new System.Drawing.Size(142, 70);
			// 
			// LayoutSettingDlg
			// 
			this.LayoutSettingDlg.Name = "LayoutSettingDlg";
			this.LayoutSettingDlg.Size = new System.Drawing.Size(141, 22);
			this.LayoutSettingDlg.Text = "グリッド";
			this.LayoutSettingDlg.Click += new System.EventHandler(this.menu_Click);
			// 
			// ColorSettingDlg
			// 
			this.ColorSettingDlg.Name = "ColorSettingDlg";
			this.ColorSettingDlg.Size = new System.Drawing.Size(141, 22);
			this.ColorSettingDlg.Text = "カラー設定...";
			this.ColorSettingDlg.Click += new System.EventHandler(this.menu_Click);
			// 
			// PrintSettings
			// 
			this.PrintSettings.Name = "PrintSettings";
			this.PrintSettings.Size = new System.Drawing.Size(141, 22);
			this.PrintSettings.Text = "PrintSettings";
			this.PrintSettings.Click += new System.EventHandler(this.menu_Click);
			// 
			// LayerMenu
			// 
			this.LayerMenu.DropDown = this.cmLayer;
			this.LayerMenu.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.LayerMenu.Name = "LayerMenu";
			this.LayerMenu.Size = new System.Drawing.Size(49, 20);
			this.LayerMenu.Text = "レイヤ";
			// 
			// cmLayer
			// 
			this.cmLayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerInsert,
            this.layerRemove,
            this.LayerRename});
			this.cmLayer.Name = "cmLayer";
			this.cmLayer.OwnerItem = this.LayerMenu;
			this.cmLayer.Size = new System.Drawing.Size(145, 70);
			// 
			// layerInsert
			// 
			this.layerInsert.Name = "layerInsert";
			this.layerInsert.Size = new System.Drawing.Size(144, 22);
			this.layerInsert.Text = "LayerInsert";
			this.layerInsert.Click += new System.EventHandler(this.menu_Click);
			// 
			// layerRemove
			// 
			this.layerRemove.Name = "layerRemove";
			this.layerRemove.Size = new System.Drawing.Size(144, 22);
			this.layerRemove.Text = "LayerRemove";
			this.layerRemove.Click += new System.EventHandler(this.menu_Click);
			// 
			// LayerRename
			// 
			this.LayerRename.Name = "LayerRename";
			this.LayerRename.Size = new System.Drawing.Size(144, 22);
			this.LayerRename.Text = "LayerRename";
			this.LayerRename.Click += new System.EventHandler(this.menu_Click);
			// 
			// FrameMenu
			// 
			this.FrameMenu.DropDown = this.cmFrame;
			this.FrameMenu.Name = "FrameMenu";
			this.FrameMenu.Size = new System.Drawing.Size(60, 20);
			this.FrameMenu.Text = "フレーム";
			// 
			// cmFrame
			// 
			this.cmFrame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FrameDisp,
            this.MenuSepa8,
            this.frameInsert,
            this.frameDelete,
            this.autoInput,
            this.valueEdit});
			this.cmFrame.Name = "contextMenuStrip1";
			this.cmFrame.OwnerItem = this.FrameMenu;
			this.cmFrame.Size = new System.Drawing.Size(139, 120);
			// 
			// FrameDisp
			// 
			this.FrameDisp.DropDown = this.cmFrameDisp;
			this.FrameDisp.Name = "FrameDisp";
			this.FrameDisp.Size = new System.Drawing.Size(138, 22);
			this.FrameDisp.Text = "フレーム表示";
			// 
			// cmFrameDisp
			// 
			this.cmFrameDisp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmFrameDisp_Frame,
            this.cmFrameDisp_PageFrame,
            this.cmFrameDisp_PageSecFrame,
            this.cmFrameDisp_SecFrame});
			this.cmFrameDisp.Name = "cmFrameDisp";
			this.cmFrameDisp.OwnerItem = this.FrameDisp;
			this.cmFrameDisp.Size = new System.Drawing.Size(209, 92);
			// 
			// cmFrameDisp_Frame
			// 
			this.cmFrameDisp_Frame.Name = "cmFrameDisp_Frame";
			this.cmFrameDisp_Frame.Size = new System.Drawing.Size(208, 22);
			this.cmFrameDisp_Frame.Text = "FrameDsip_Frame";
			this.cmFrameDisp_Frame.Click += new System.EventHandler(this.FrameDisp_Frame_Click);
			// 
			// cmFrameDisp_PageFrame
			// 
			this.cmFrameDisp_PageFrame.Name = "cmFrameDisp_PageFrame";
			this.cmFrameDisp_PageFrame.Size = new System.Drawing.Size(208, 22);
			this.cmFrameDisp_PageFrame.Text = "FrameDsip_PageFrame";
			this.cmFrameDisp_PageFrame.Click += new System.EventHandler(this.FrameDisp_PageFrame_Click);
			// 
			// cmFrameDisp_PageSecFrame
			// 
			this.cmFrameDisp_PageSecFrame.Name = "cmFrameDisp_PageSecFrame";
			this.cmFrameDisp_PageSecFrame.Size = new System.Drawing.Size(208, 22);
			this.cmFrameDisp_PageSecFrame.Text = "FrameDsip_PageSecFrame";
			this.cmFrameDisp_PageSecFrame.Click += new System.EventHandler(this.FrameDisp_PageSecFrame_Click);
			// 
			// cmFrameDisp_SecFrame
			// 
			this.cmFrameDisp_SecFrame.Name = "cmFrameDisp_SecFrame";
			this.cmFrameDisp_SecFrame.Size = new System.Drawing.Size(208, 22);
			this.cmFrameDisp_SecFrame.Text = "FrameDsip_SecFrame";
			this.cmFrameDisp_SecFrame.Click += new System.EventHandler(this.FrameDisp_SecFrame_Click);
			// 
			// MenuSepa8
			// 
			this.MenuSepa8.Name = "MenuSepa8";
			this.MenuSepa8.Size = new System.Drawing.Size(135, 6);
			// 
			// frameInsert
			// 
			this.frameInsert.Name = "frameInsert";
			this.frameInsert.Size = new System.Drawing.Size(138, 22);
			this.frameInsert.Text = "FrameInsert";
			this.frameInsert.Click += new System.EventHandler(this.menu_Click);
			// 
			// frameDelete
			// 
			this.frameDelete.Name = "frameDelete";
			this.frameDelete.Size = new System.Drawing.Size(138, 22);
			this.frameDelete.Text = "FrameDelete";
			this.frameDelete.Click += new System.EventHandler(this.menu_Click);
			// 
			// autoInput
			// 
			this.autoInput.Name = "autoInput";
			this.autoInput.Size = new System.Drawing.Size(138, 22);
			this.autoInput.Text = "AutoInput";
			this.autoInput.Click += new System.EventHandler(this.menu_Click);
			// 
			// valueEdit
			// 
			this.valueEdit.Name = "valueEdit";
			this.valueEdit.Size = new System.Drawing.Size(138, 22);
			this.valueEdit.Text = "valueEdit";
			this.valueEdit.Click += new System.EventHandler(this.menu_Click);
			// 
			// WindowMenu
			// 
			this.WindowMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PreviewDlg});
			this.WindowMenu.Name = "WindowMenu";
			this.WindowMenu.Size = new System.Drawing.Size(65, 20);
			this.WindowMenu.Text = "ウィンドウ";
			this.WindowMenu.Click += new System.EventHandler(this.WindowMenu_Click);
			// 
			// PreviewDlg
			// 
			this.PreviewDlg.Name = "PreviewDlg";
			this.PreviewDlg.Size = new System.Drawing.Size(143, 22);
			this.PreviewDlg.Text = "プレビューNav";
			this.PreviewDlg.Click += new System.EventHandler(this.PreviewDlg_Click);
			// 
			// HelpMenu
			// 
			this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpAbout});
			this.HelpMenu.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.HelpMenu.Name = "HelpMenu";
			this.HelpMenu.Size = new System.Drawing.Size(43, 20);
			this.HelpMenu.Text = "Help";
			// 
			// HelpAbout
			// 
			this.HelpAbout.Name = "HelpAbout";
			this.HelpAbout.Size = new System.Drawing.Size(116, 22);
			this.HelpAbout.Text = "About...";
			this.HelpAbout.Click += new System.EventHandler(this.menu_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 522);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(403, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			this.statusStrip1.Enter += new System.EventHandler(this.MainForm_Enter);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.AutoSize = false;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(150, 17);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// ScriptToClipMenu
			// 
			this.ScriptToClipMenu.Name = "ScriptToClipMenu";
			this.ScriptToClipMenu.Size = new System.Drawing.Size(152, 22);
			this.ScriptToClipMenu.Text = "ScriptToClip";
			this.ScriptToClipMenu.Click += new System.EventHandler(this.menu_Click);
			// 
			// tsNav1
			// 
			this.tsNav1.Font = new System.Drawing.Font("MS UI Gothic", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tsNav1.Location = new System.Drawing.Point(373, 51);
			this.tsNav1.Name = "tsNav1";
			this.tsNav1.Size = new System.Drawing.Size(21, 446);
			this.tsNav1.TabIndex = 12;
			this.tsNav1.TabStop = false;
			this.tsNav1.Text = "tsNav1";
			this.tsNav1.TSGrid = this.tsGrid1;
			// 
			// tsGrid1
			// 
			this.tsGrid1.CausesValidation = false;
			this.tsGrid1.CellIndex = 0;
			this.tsGrid1.ContextMenuStrip = this.cmFrame;
			this.tsGrid1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tsGrid1.Location = new System.Drawing.Point(87, 51);
			this.tsGrid1.Name = "tsGrid1";
			this.tsGrid1.OffsetY = 0;
			this.tsGrid1.Size = new System.Drawing.Size(280, 448);
			this.tsGrid1.TabIndex = 4;
			this.tsGrid1.TabStop = false;
			this.tsGrid1.Text = "tsGrid1";
			this.tsGrid1.TSCellCaption = this.tsCellCaption1;
			this.tsGrid1.TSForm = null;
			this.tsGrid1.TSFrame = this.tsFrame1;
			this.tsGrid1.TSInput = this.tsInput1;
			this.tsGrid1.TSNav = this.tsNav1;
			this.tsGrid1.SelectionChanged += new System.EventHandler(this.tsGrid1_SelectionChanged);
			this.tsGrid1.KeyBindChanged += new System.EventHandler(this.tsGrid1_KeyBindChanged);
			this.tsGrid1.FileLoaded += new System.EventHandler(this.tsGrid1_FileLoaded);
			this.tsGrid1.SizeChanged += new System.EventHandler(this.tsGrid1_SizeChanged);
			// 
			// tsCellCaption1
			// 
			this.tsCellCaption1.ContextMenuStrip = this.cmLayer;
			this.tsCellCaption1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tsCellCaption1.Location = new System.Drawing.Point(87, 27);
			this.tsCellCaption1.Name = "tsCellCaption1";
			this.tsCellCaption1.Size = new System.Drawing.Size(280, 21);
			this.tsCellCaption1.TabIndex = 8;
			this.tsCellCaption1.TabStop = false;
			this.tsCellCaption1.Text = "メモ";
			this.tsCellCaption1.TSGrid = this.tsGrid1;
			// 
			// tsFrame1
			// 
			this.tsFrame1.ContextMenuStrip = this.cmFrame;
			this.tsFrame1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.tsFrame1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tsFrame1.Location = new System.Drawing.Point(0, 53);
			this.tsFrame1.Name = "tsFrame1";
			this.tsFrame1.Size = new System.Drawing.Size(80, 446);
			this.tsFrame1.TabIndex = 7;
			this.tsFrame1.TabStop = false;
			this.tsFrame1.Text = "tsFrame1";
			this.tsFrame1.TSGrid = this.tsGrid1;
			this.tsFrame1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tsFrame1_MouseDoubleClick);
			// 
			// tsInput1
			// 
			this.tsInput1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tsInput1.Location = new System.Drawing.Point(0, 25);
			this.tsInput1.Name = "tsInput1";
			this.tsInput1.Size = new System.Drawing.Size(80, 20);
			this.tsInput1.TabIndex = 10;
			this.tsInput1.TabStop = false;
			this.tsInput1.Text = "tsInput1";
			this.tsInput1.TSGrid = this.tsGrid1;
			// 
			// TSForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 11F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(403, 544);
			this.Controls.Add(this.tsNav1);
			this.Controls.Add(this.tsInput1);
			this.Controls.Add(this.tsCellCaption1);
			this.Controls.Add(this.tsFrame1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.tsGrid1);
			this.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "TSForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "AE_Remap Exceed";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.Enter += new System.EventHandler(this.MainForm_Enter);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.MainForm_Layout);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForm_PreviewKeyDown);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.cmSettings.ResumeLayout(false);
			this.cmLayer.ResumeLayout(false);
			this.cmFrame.ResumeLayout(false);
			this.cmFrameDisp.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private TSGrid tsGrid1;
		private TSFrame tsFrame1;
		private TSCellCaption tsCellCaption1;
		private System.Windows.Forms.ToolStripMenuItem FileMenu;
		private System.Windows.Forms.ToolStripMenuItem FileOpen;
		private System.Windows.Forms.ToolStripMenuItem FileSave;
		private System.Windows.Forms.ToolStripMenuItem FileQuit;
		private System.Windows.Forms.ToolStripMenuItem EditMenu;
		private TSInput tsInput1;
		private System.Windows.Forms.ToolStripMenuItem EditCopyMenu;
		private System.Windows.Forms.ToolStripMenuItem EditCutMenu;
		private System.Windows.Forms.ToolStripMenuItem EditPasteMenu;
		private System.Windows.Forms.ToolStripSeparator MenuSepa5;
		private System.Windows.Forms.ToolStripMenuItem LayerMenu;
		private System.Windows.Forms.ToolStripMenuItem HelpMenu;
		private System.Windows.Forms.ToolStripMenuItem FrameMenu;
		private TSNav tsNav1;
		private System.Windows.Forms.ToolStripMenuItem HelpAbout;
		private System.Windows.Forms.ToolStripMenuItem FileSaveAs;
		private System.Windows.Forms.ToolStripMenuItem FileNew;
		private System.Windows.Forms.ToolStripSeparator MenuSepa1;
		private System.Windows.Forms.ContextMenuStrip cmFrame;
		private System.Windows.Forms.ContextMenuStrip cmSettings;
		private System.Windows.Forms.ToolStripMenuItem ColorSettingDlg;
		private System.Windows.Forms.ToolStripMenuItem LayoutSettingDlg;
		private System.Windows.Forms.ContextMenuStrip cmLayer;
		private System.Windows.Forms.ToolStripMenuItem layerInsert;
		private System.Windows.Forms.ToolStripMenuItem layerRemove;
		private System.Windows.Forms.ToolStripMenuItem LayerRename;
		private System.Windows.Forms.ToolStripSeparator MenuSepa8;
		private System.Windows.Forms.ToolStripMenuItem frameInsert;
		private System.Windows.Forms.ToolStripMenuItem frameDelete;
		private System.Windows.Forms.ToolStripMenuItem autoInput;
		private System.Windows.Forms.ToolStripSeparator MenuSepa4;
		private System.Windows.Forms.ToolStripMenuItem ScriptToClipMenu;
		private System.Windows.Forms.ContextMenuStrip cmFrameDisp;
		private System.Windows.Forms.ToolStripMenuItem cmFrameDisp_Frame;
		private System.Windows.Forms.ToolStripMenuItem cmFrameDisp_PageFrame;
		private System.Windows.Forms.ToolStripMenuItem cmFrameDisp_PageSecFrame;
		private System.Windows.Forms.ToolStripMenuItem cmFrameDisp_SecFrame;
		private System.Windows.Forms.ToolStripMenuItem FrameDisp;
        private System.Windows.Forms.ToolStripMenuItem valueEdit;
        private System.Windows.Forms.ToolStripMenuItem WindowMenu;
        private System.Windows.Forms.ToolStripMenuItem PreviewDlg;
        private System.Windows.Forms.ToolStripMenuItem FilePrintPreview;
        private System.Windows.Forms.ToolStripMenuItem FilePrint;
        private System.Windows.Forms.ToolStripMenuItem FilePageSetup;
		private System.Windows.Forms.ToolStripMenuItem PrintSettings;
        private System.Windows.Forms.ToolStripMenuItem SettingMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem ClearALLMenu;
		private System.Windows.Forms.ToolStripMenuItem ClearLayerMenu;
	}
}

