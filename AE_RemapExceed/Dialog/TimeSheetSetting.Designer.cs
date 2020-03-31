namespace AE_RemapExceed
{
	partial class TimeSheetSetting
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
		private void InitializeComponent( )
		{
            this.label6 = new System.Windows.Forms.Label();
            this.lbFrame = new System.Windows.Forms.Label();
            this.cmbFps = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbKoma = new System.Windows.Forms.Label();
            this.rbSec = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lbSec = new System.Windows.Forms.Label();
            this.rbFrame = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbZeroStart = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPageSec = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSheet = new System.Windows.Forms.TabPage();
            this.tpCut = new System.Windows.Forms.TabPage();
            this.cmbCAMPANY_NAME = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbCutNo = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbSCECNE = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbOPUS = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbSubTitle = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbTitle = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbUPDATE_USER = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCREATE_USER = new System.Windows.Forms.ComboBox();
            this.tpComment = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbInput = new System.Windows.Forms.ListBox();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.edFrameOffset = new AE_RemapExceed.IntEditD();
            this.edFrame = new AE_RemapExceed.IntEdit();
            this.edKoma = new AE_RemapExceed.IntEdit();
            this.edSec = new AE_RemapExceed.IntEdit();
            this.edCellCount = new AE_RemapExceed.IntEdit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSheet.SuspendLayout();
            this.tpCut.SuspendLayout();
            this.tpComment.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(20, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "フレームレート";
            // 
            // lbFrame
            // 
            this.lbFrame.AutoSize = true;
            this.lbFrame.Location = new System.Drawing.Point(225, 47);
            this.lbFrame.Name = "lbFrame";
            this.lbFrame.Size = new System.Drawing.Size(42, 12);
            this.lbFrame.TabIndex = 11;
            this.lbFrame.Text = "フレーム";
            // 
            // cmbFps
            // 
            this.cmbFps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFps.FormattingEnabled = true;
            this.cmbFps.Items.AddRange(new object[] {
            "12",
            "15",
            "24",
            "30"});
            this.cmbFps.Location = new System.Drawing.Point(102, 128);
            this.cmbFps.Name = "cmbFps";
            this.cmbFps.Size = new System.Drawing.Size(69, 20);
            this.cmbFps.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(23, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "セルレイヤ数";
            // 
            // lbKoma
            // 
            this.lbKoma.AutoSize = true;
            this.lbKoma.Location = new System.Drawing.Point(225, 22);
            this.lbKoma.Name = "lbKoma";
            this.lbKoma.Size = new System.Drawing.Size(22, 12);
            this.lbKoma.TabIndex = 9;
            this.lbKoma.Text = "コマ";
            // 
            // rbSec
            // 
            this.rbSec.AutoSize = true;
            this.rbSec.Location = new System.Drawing.Point(28, 22);
            this.rbSec.Name = "rbSec";
            this.rbSec.Size = new System.Drawing.Size(35, 16);
            this.rbSec.TabIndex = 2;
            this.rbSec.Text = "秒";
            this.rbSec.UseVisualStyleBackColor = true;
            this.rbSec.Click += new System.EventHandler(this.rbSec_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(15, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "秒数";
            // 
            // lbSec
            // 
            this.lbSec.AutoSize = true;
            this.lbSec.Location = new System.Drawing.Point(152, 22);
            this.lbSec.Name = "lbSec";
            this.lbSec.Size = new System.Drawing.Size(23, 12);
            this.lbSec.TabIndex = 7;
            this.lbSec.Text = "秒+";
            // 
            // rbFrame
            // 
            this.rbFrame.AutoSize = true;
            this.rbFrame.Checked = true;
            this.rbFrame.Location = new System.Drawing.Point(28, 47);
            this.rbFrame.Name = "rbFrame";
            this.rbFrame.Size = new System.Drawing.Size(60, 16);
            this.rbFrame.TabIndex = 4;
            this.rbFrame.TabStop = true;
            this.rbFrame.Text = "フレーム";
            this.rbFrame.UseVisualStyleBackColor = true;
            this.rbFrame.Click += new System.EventHandler(this.rfFrame_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(160, 255);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(241, 255);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(8, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "フレームオフセット";
            // 
            // cbZeroStart
            // 
            this.cbZeroStart.AutoSize = true;
            this.cbZeroStart.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cbZeroStart.Location = new System.Drawing.Point(198, 165);
            this.cbZeroStart.Name = "cbZeroStart";
            this.cbZeroStart.Size = new System.Drawing.Size(101, 16);
            this.cbZeroStart.TabIndex = 22;
            this.cbZeroStart.Text = "0スタートにする";
            this.cbZeroStart.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(165, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "ページ秒数";
            // 
            // cmbPageSec
            // 
            this.cmbPageSec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageSec.FormattingEnabled = true;
            this.cmbPageSec.Items.AddRange(new object[] {
            "6秒",
            "3秒"});
            this.cmbPageSec.Location = new System.Drawing.Point(235, 98);
            this.cmbPageSec.Name = "cmbPageSec";
            this.cmbPageSec.Size = new System.Drawing.Size(70, 20);
            this.cmbPageSec.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.edFrame);
            this.panel1.Controls.Add(this.lbKoma);
            this.panel1.Controls.Add(this.rbSec);
            this.panel1.Controls.Add(this.rbFrame);
            this.panel1.Controls.Add(this.edKoma);
            this.panel1.Controls.Add(this.lbFrame);
            this.panel1.Controls.Add(this.edSec);
            this.panel1.Controls.Add(this.lbSec);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(297, 79);
            this.panel1.TabIndex = 19;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSheet);
            this.tabControl1.Controls.Add(this.tpCut);
            this.tabControl1.Controls.Add(this.tpComment);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(329, 237);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpSheet
            // 
            this.tpSheet.BackColor = System.Drawing.SystemColors.Control;
            this.tpSheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpSheet.Controls.Add(this.edFrameOffset);
            this.tpSheet.Controls.Add(this.panel1);
            this.tpSheet.Controls.Add(this.label4);
            this.tpSheet.Controls.Add(this.label1);
            this.tpSheet.Controls.Add(this.cbZeroStart);
            this.tpSheet.Controls.Add(this.label6);
            this.tpSheet.Controls.Add(this.label3);
            this.tpSheet.Controls.Add(this.cmbFps);
            this.tpSheet.Controls.Add(this.cmbPageSec);
            this.tpSheet.Controls.Add(this.edCellCount);
            this.tpSheet.Location = new System.Drawing.Point(4, 21);
            this.tpSheet.Name = "tpSheet";
            this.tpSheet.Padding = new System.Windows.Forms.Padding(3);
            this.tpSheet.Size = new System.Drawing.Size(321, 212);
            this.tpSheet.TabIndex = 0;
            this.tpSheet.Text = "基本設定";
            // 
            // tpCut
            // 
            this.tpCut.BackColor = System.Drawing.SystemColors.Control;
            this.tpCut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpCut.Controls.Add(this.cmbCAMPANY_NAME);
            this.tpCut.Controls.Add(this.label13);
            this.tpCut.Controls.Add(this.label12);
            this.tpCut.Controls.Add(this.cmbCutNo);
            this.tpCut.Controls.Add(this.label11);
            this.tpCut.Controls.Add(this.cmbSCECNE);
            this.tpCut.Controls.Add(this.label10);
            this.tpCut.Controls.Add(this.cmbOPUS);
            this.tpCut.Controls.Add(this.label9);
            this.tpCut.Controls.Add(this.cmbSubTitle);
            this.tpCut.Controls.Add(this.label8);
            this.tpCut.Controls.Add(this.cmbTitle);
            this.tpCut.Controls.Add(this.label7);
            this.tpCut.Controls.Add(this.cmbUPDATE_USER);
            this.tpCut.Controls.Add(this.label5);
            this.tpCut.Controls.Add(this.cmbCREATE_USER);
            this.tpCut.Location = new System.Drawing.Point(4, 21);
            this.tpCut.Name = "tpCut";
            this.tpCut.Padding = new System.Windows.Forms.Padding(3);
            this.tpCut.Size = new System.Drawing.Size(321, 212);
            this.tpCut.TabIndex = 1;
            this.tpCut.Text = "カット情報";
            // 
            // cmbCAMPANY_NAME
            // 
            this.cmbCAMPANY_NAME.FormattingEnabled = true;
            this.cmbCAMPANY_NAME.Location = new System.Drawing.Point(85, 175);
            this.cmbCAMPANY_NAME.Name = "cmbCAMPANY_NAME";
            this.cmbCAMPANY_NAME.Size = new System.Drawing.Size(190, 20);
            this.cmbCAMPANY_NAME.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(38, 178);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 14;
            this.label13.Text = "会社名";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 106);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "カットナンバー";
            // 
            // cmbCutNo
            // 
            this.cmbCutNo.FormattingEnabled = true;
            this.cmbCutNo.Location = new System.Drawing.Point(85, 103);
            this.cmbCutNo.Name = "cmbCutNo";
            this.cmbCutNo.Size = new System.Drawing.Size(123, 20);
            this.cmbCutNo.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(174, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 12);
            this.label11.TabIndex = 11;
            this.label11.Text = "シーン";
            // 
            // cmbSCECNE
            // 
            this.cmbSCECNE.FormattingEnabled = true;
            this.cmbSCECNE.Location = new System.Drawing.Point(214, 74);
            this.cmbSCECNE.Name = "cmbSCECNE";
            this.cmbSCECNE.Size = new System.Drawing.Size(85, 20);
            this.cmbSCECNE.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "話数";
            // 
            // cmbOPUS
            // 
            this.cmbOPUS.FormattingEnabled = true;
            this.cmbOPUS.Location = new System.Drawing.Point(85, 72);
            this.cmbOPUS.Name = "cmbOPUS";
            this.cmbOPUS.Size = new System.Drawing.Size(74, 20);
            this.cmbOPUS.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "サブタイトル";
            // 
            // cmbSubTitle
            // 
            this.cmbSubTitle.FormattingEnabled = true;
            this.cmbSubTitle.Location = new System.Drawing.Point(85, 43);
            this.cmbSubTitle.Name = "cmbSubTitle";
            this.cmbSubTitle.Size = new System.Drawing.Size(190, 20);
            this.cmbSubTitle.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(38, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "作品名";
            // 
            // cmbTitle
            // 
            this.cmbTitle.FormattingEnabled = true;
            this.cmbTitle.Location = new System.Drawing.Point(85, 14);
            this.cmbTitle.Name = "cmbTitle";
            this.cmbTitle.Size = new System.Drawing.Size(190, 20);
            this.cmbTitle.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(181, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "修正者";
            // 
            // cmbUPDATE_USER
            // 
            this.cmbUPDATE_USER.FormattingEnabled = true;
            this.cmbUPDATE_USER.Location = new System.Drawing.Point(224, 142);
            this.cmbUPDATE_USER.Name = "cmbUPDATE_USER";
            this.cmbUPDATE_USER.Size = new System.Drawing.Size(90, 20);
            this.cmbUPDATE_USER.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "担当者";
            // 
            // cmbCREATE_USER
            // 
            this.cmbCREATE_USER.FormattingEnabled = true;
            this.cmbCREATE_USER.Location = new System.Drawing.Point(85, 142);
            this.cmbCREATE_USER.Name = "cmbCREATE_USER";
            this.cmbCREATE_USER.Size = new System.Drawing.Size(90, 20);
            this.cmbCREATE_USER.TabIndex = 0;
            // 
            // tpComment
            // 
            this.tpComment.BackColor = System.Drawing.SystemColors.Control;
            this.tpComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpComment.Controls.Add(this.label14);
            this.tpComment.Controls.Add(this.btnClear);
            this.tpComment.Controls.Add(this.lbInput);
            this.tpComment.Controls.Add(this.tbComment);
            this.tpComment.Location = new System.Drawing.Point(4, 21);
            this.tpComment.Name = "tpComment";
            this.tpComment.Padding = new System.Windows.Forms.Padding(3);
            this.tpComment.Size = new System.Drawing.Size(321, 212);
            this.tpComment.TabIndex = 2;
            this.tpComment.Text = "コメント";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 12);
            this.label14.TabIndex = 3;
            this.label14.Text = "コメント";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(8, 169);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbInput
            // 
            this.lbInput.FormattingEnabled = true;
            this.lbInput.ItemHeight = 12;
            this.lbInput.Items.AddRange(new object[] {
            "画面動",
            "透過光",
            "流PAN",
            "-",
            "DF",
            "FOG",
            "/",
            "パラ",
            "Follow",
            "TU",
            "TB",
            "QTU",
            "QTB",
            "FI",
            "FO",
            "WI",
            "WO",
            "Fix",
            "*",
            "start",
            "last"});
            this.lbInput.Location = new System.Drawing.Point(241, 126);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(72, 76);
            this.lbInput.TabIndex = 1;
            this.lbInput.DoubleClick += new System.EventHandler(this.lbInput_DoubleClick);
            // 
            // tbComment
            // 
            this.tbComment.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbComment.Location = new System.Drawing.Point(6, 18);
            this.tbComment.Multiline = true;
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(307, 102);
            this.tbComment.TabIndex = 0;
            this.tbComment.WordWrap = false;
            this.tbComment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbComment_KeyPress);
            // 
            // edFrameOffset
            // 
            this.edFrameOffset.Location = new System.Drawing.Point(102, 162);
            this.edFrameOffset.Name = "edFrameOffset";
            this.edFrameOffset.Size = new System.Drawing.Size(75, 19);
            this.edFrameOffset.TabIndex = 24;
            this.edFrameOffset.Text = "0";
            this.edFrameOffset.Value = 0;
            // 
            // edFrame
            // 
            this.edFrame.Location = new System.Drawing.Point(94, 44);
            this.edFrame.Name = "edFrame";
            this.edFrame.Size = new System.Drawing.Size(125, 19);
            this.edFrame.TabIndex = 16;
            this.edFrame.Text = "0";
            this.edFrame.Value = 0;
            // 
            // edKoma
            // 
            this.edKoma.Location = new System.Drawing.Point(181, 19);
            this.edKoma.Name = "edKoma";
            this.edKoma.Size = new System.Drawing.Size(38, 19);
            this.edKoma.TabIndex = 15;
            this.edKoma.Text = "0";
            this.edKoma.Value = 0;
            // 
            // edSec
            // 
            this.edSec.Location = new System.Drawing.Point(94, 19);
            this.edSec.Name = "edSec";
            this.edSec.Size = new System.Drawing.Size(52, 19);
            this.edSec.TabIndex = 14;
            this.edSec.Text = "0";
            this.edSec.Value = 0;
            // 
            // edCellCount
            // 
            this.edCellCount.Location = new System.Drawing.Point(102, 99);
            this.edCellCount.Name = "edCellCount";
            this.edCellCount.Size = new System.Drawing.Size(52, 19);
            this.edCellCount.TabIndex = 13;
            this.edCellCount.Text = "0";
            this.edCellCount.Value = 0;
            // 
            // TimeSheetSetting
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(355, 290);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimeSheetSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "タイムシート設定";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpSheet.ResumeLayout(false);
            this.tpSheet.PerformLayout();
            this.tpCut.ResumeLayout(false);
            this.tpCut.PerformLayout();
            this.tpComment.ResumeLayout(false);
            this.tpComment.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lbFrame;
		private System.Windows.Forms.ComboBox cmbFps;
		private System.Windows.Forms.Label lbKoma;
		private System.Windows.Forms.Label lbSec;
		private System.Windows.Forms.RadioButton rbFrame;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton rbSec;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private IntEdit edFrame;
		private IntEdit edKoma;
		private IntEdit edSec;
		private IntEdit edCellCount;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cmbPageSec;
		private System.Windows.Forms.CheckBox cbZeroStart;
		private System.Windows.Forms.Label label4;
		private IntEditD edFrameOffset;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpSheet;
		private System.Windows.Forms.TabPage tpCut;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox cmbCutNo;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox cmbSCECNE;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox cmbOPUS;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cmbSubTitle;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cmbTitle;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cmbUPDATE_USER;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cmbCREATE_USER;
		private System.Windows.Forms.TabPage tpComment;
		private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.ComboBox cmbCAMPANY_NAME;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListBox lbInput;
	}
}