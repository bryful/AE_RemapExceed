namespace AE_RemapExceed
{
	partial class RemapSettingDlg
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbEmptyCell = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.edLastFrame = new AE_RemapExceed.IntEdit();
            this.edCmpAspect = new AE_RemapExceed.FloatEdit();
            this.edSrcAspect = new AE_RemapExceed.FloatEdit();
            this.edSrcHeight = new AE_RemapExceed.IntEdit();
            this.edSrcWidth = new AE_RemapExceed.IntEdit();
            this.cbIsLoadScriptFile = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.edCmpAspect);
            this.groupBox1.Controls.Add(this.edSrcAspect);
            this.groupBox1.Controls.Add(this.edSrcHeight);
            this.groupBox1.Controls.Add(this.edSrcWidth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(254, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "After Effects";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "設定しなくても特に支障ありません。";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(240, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "KeyFrameDataを作成する時に使用するパラメータ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Comp Pixel Aspect Ratio";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Source Pixel Aspect Ratio";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Source Height";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Source Width";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(329, 238);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(410, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbIsLoadScriptFile);
            this.groupBox2.Controls.Add(this.edLastFrame);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmbEmptyCell);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(13, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 208);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Remap Clipboard";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "無理やりの時の数値";
            // 
            // cmbEmptyCell
            // 
            this.cmbEmptyCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmptyCell.FormattingEnabled = true;
            this.cmbEmptyCell.Items.AddRange(new object[] {
            "不透明度",
            "ブラインド プラグイン",
            "Venetian Blinds Plugin",
            "無理やり大きな数値を入れる"});
            this.cmbEmptyCell.Location = new System.Drawing.Point(21, 39);
            this.cmbEmptyCell.Name = "cmbEmptyCell";
            this.cmbEmptyCell.Size = new System.Drawing.Size(191, 20);
            this.cmbEmptyCell.TabIndex = 1;
            this.cmbEmptyCell.SelectedIndexChanged += new System.EventHandler(this.cmbEmptyCell_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "空セルの扱い";
            // 
            // edLastFrame
            // 
            this.edLastFrame.Enabled = false;
            this.edLastFrame.Location = new System.Drawing.Point(128, 83);
            this.edLastFrame.Name = "edLastFrame";
            this.edLastFrame.Size = new System.Drawing.Size(84, 19);
            this.edLastFrame.TabIndex = 3;
            this.edLastFrame.Text = "0";
            this.edLastFrame.Value = 0;
            // 
            // edCmpAspect
            // 
            this.edCmpAspect.Location = new System.Drawing.Point(156, 137);
            this.edCmpAspect.Name = "edCmpAspect";
            this.edCmpAspect.Size = new System.Drawing.Size(100, 19);
            this.edCmpAspect.TabIndex = 3;
            this.edCmpAspect.Text = "0";
            this.edCmpAspect.Value = 0F;
            // 
            // edSrcAspect
            // 
            this.edSrcAspect.Location = new System.Drawing.Point(156, 112);
            this.edSrcAspect.Name = "edSrcAspect";
            this.edSrcAspect.Size = new System.Drawing.Size(100, 19);
            this.edSrcAspect.TabIndex = 2;
            this.edSrcAspect.Text = "0";
            this.edSrcAspect.Value = 0F;
            // 
            // edSrcHeight
            // 
            this.edSrcHeight.Location = new System.Drawing.Point(156, 87);
            this.edSrcHeight.Name = "edSrcHeight";
            this.edSrcHeight.Size = new System.Drawing.Size(100, 19);
            this.edSrcHeight.TabIndex = 1;
            this.edSrcHeight.Text = "0";
            this.edSrcHeight.Value = 0;
            // 
            // edSrcWidth
            // 
            this.edSrcWidth.Location = new System.Drawing.Point(156, 62);
            this.edSrcWidth.Name = "edSrcWidth";
            this.edSrcWidth.Size = new System.Drawing.Size(100, 19);
            this.edSrcWidth.TabIndex = 0;
            this.edSrcWidth.Text = "0";
            this.edSrcWidth.Value = 0;
            // 
            // cbIsLoadScriptFile
            // 
            this.cbIsLoadScriptFile.AutoSize = true;
            this.cbIsLoadScriptFile.Location = new System.Drawing.Point(21, 124);
            this.cbIsLoadScriptFile.Name = "cbIsLoadScriptFile";
            this.cbIsLoadScriptFile.Size = new System.Drawing.Size(204, 16);
            this.cbIsLoadScriptFile.TabIndex = 4;
            this.cbIsLoadScriptFile.Text = "実行時Scriptsフォルダを読み込まない";
            this.cbIsLoadScriptFile.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(141, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "！この設定は保存されません";
            // 
            // RemapSettingDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(543, 273);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RemapSettingDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RemapSetting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private FloatEdit edCmpAspect;
		private FloatEdit edSrcAspect;
		private IntEdit edSrcHeight;
		private IntEdit edSrcWidth;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox cmbEmptyCell;
		private System.Windows.Forms.Label label7;
		private IntEdit edLastFrame;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbIsLoadScriptFile;
	}
}