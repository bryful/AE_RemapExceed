namespace AE_RemapExceed
{
	partial class SystemSettingDlg
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
			this.label1 = new System.Windows.Forms.Label();
			this.btnInst = new System.Windows.Forms.Button();
			this.btnUninst = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(34, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(147, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "拡張子の登録を行ないます";
			// 
			// btnInst
			// 
			this.btnInst.Location = new System.Drawing.Point(36, 52);
			this.btnInst.Name = "btnInst";
			this.btnInst.Size = new System.Drawing.Size(111, 39);
			this.btnInst.TabIndex = 1;
			this.btnInst.Text = "拡張子の登録";
			this.btnInst.UseVisualStyleBackColor = true;
			this.btnInst.Click += new System.EventHandler(this.btnInst_Click);
			// 
			// btnUninst
			// 
			this.btnUninst.Location = new System.Drawing.Point(168, 52);
			this.btnUninst.Name = "btnUninst";
			this.btnUninst.Size = new System.Drawing.Size(111, 39);
			this.btnUninst.TabIndex = 2;
			this.btnUninst.Text = "登録を削除";
			this.btnUninst.UseVisualStyleBackColor = true;
			this.btnUninst.Click += new System.EventHandler(this.btnUninst_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnClose.Location = new System.Drawing.Point(204, 116);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "閉じる";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// SystemSettingDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(326, 151);
			this.ControlBox = false;
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnUninst);
			this.Controls.Add(this.btnInst);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SystemSettingDlg";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "システム設定";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnInst;
		private System.Windows.Forms.Button btnUninst;
		private System.Windows.Forms.Button btnClose;
	}
}