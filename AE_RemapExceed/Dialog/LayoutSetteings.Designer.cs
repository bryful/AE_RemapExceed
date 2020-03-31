namespace AE_RemapExceed
{
	partial class LayoutSetteings
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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.edMemoWidth = new AE_RemapExceed.CapIntEdit();
			this.edFrameWidth = new AE_RemapExceed.CapIntEdit();
			this.edCellHeight = new AE_RemapExceed.CapIntEdit();
			this.edCaptionHeight = new AE_RemapExceed.CapIntEdit();
			this.edCellWidth = new AE_RemapExceed.CapIntEdit();
			this.btnDef = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(95, 187);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(176, 187);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// edMemoWidth
			// 
			this.edMemoWidth.Caption = "メモの横幅";
			this.edMemoWidth.Location = new System.Drawing.Point(12, 140);
			this.edMemoWidth.Name = "edMemoWidth";
			this.edMemoWidth.Size = new System.Drawing.Size(226, 26);
			this.edMemoWidth.TabIndex = 4;
			this.edMemoWidth.Value = 0;
			// 
			// edFrameWidth
			// 
			this.edFrameWidth.Caption = "フレームの横幅";
			this.edFrameWidth.Location = new System.Drawing.Point(12, 108);
			this.edFrameWidth.Name = "edFrameWidth";
			this.edFrameWidth.Size = new System.Drawing.Size(226, 26);
			this.edFrameWidth.TabIndex = 3;
			this.edFrameWidth.Value = 0;
			// 
			// edCellHeight
			// 
			this.edCellHeight.Caption = "グリッドの高さ";
			this.edCellHeight.Location = new System.Drawing.Point(12, 44);
			this.edCellHeight.Name = "edCellHeight";
			this.edCellHeight.Size = new System.Drawing.Size(226, 26);
			this.edCellHeight.TabIndex = 2;
			this.edCellHeight.Value = 0;
			// 
			// edCaptionHeight
			// 
			this.edCaptionHeight.Caption = "キャプションの高さ";
			this.edCaptionHeight.Location = new System.Drawing.Point(12, 76);
			this.edCaptionHeight.Name = "edCaptionHeight";
			this.edCaptionHeight.Size = new System.Drawing.Size(226, 26);
			this.edCaptionHeight.TabIndex = 1;
			this.edCaptionHeight.Value = 0;
			// 
			// edCellWidth
			// 
			this.edCellWidth.Caption = "セルグリッドの横幅";
			this.edCellWidth.Location = new System.Drawing.Point(12, 12);
			this.edCellWidth.Name = "edCellWidth";
			this.edCellWidth.Size = new System.Drawing.Size(226, 26);
			this.edCellWidth.TabIndex = 0;
			this.edCellWidth.Value = 0;
			// 
			// btnDef
			// 
			this.btnDef.Location = new System.Drawing.Point(12, 187);
			this.btnDef.Name = "btnDef";
			this.btnDef.Size = new System.Drawing.Size(62, 23);
			this.btnDef.TabIndex = 7;
			this.btnDef.Text = "デフォルト";
			this.btnDef.UseVisualStyleBackColor = true;
			this.btnDef.Click += new System.EventHandler(this.btnDef_Click);
			// 
			// LayoutSetteings
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(263, 225);
			this.ControlBox = false;
			this.Controls.Add(this.btnDef);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.edMemoWidth);
			this.Controls.Add(this.edFrameWidth);
			this.Controls.Add(this.edCellHeight);
			this.Controls.Add(this.edCaptionHeight);
			this.Controls.Add(this.edCellWidth);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LayoutSetteings";
			this.RightToLeftLayout = true;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "グリッドのサイズ";
			this.ResumeLayout(false);

		}

		#endregion

		private CapIntEdit edCellWidth;
		private CapIntEdit edCaptionHeight;
		private CapIntEdit edCellHeight;
		private CapIntEdit edFrameWidth;
		private CapIntEdit edMemoWidth;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnDef;


	}
}