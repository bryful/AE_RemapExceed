namespace AE_RemapExceed
{
	partial class AutoInputDlg
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.edKoma = new AE_RemapExceed.IntEdit();
			this.edLast = new AE_RemapExceed.IntEdit();
			this.edStart = new AE_RemapExceed.IntEdit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(29, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "セルの";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(146, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "番から";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(146, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "番めまで";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(29, 44);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 12);
			this.label4.TabIndex = 1;
			this.label4.Text = "セルの";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(146, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95, 12);
			this.label5.TabIndex = 7;
			this.label5.Text = "コマ打ちで繰り返す";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(67, 101);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(148, 101);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// edKoma
			// 
			this.edKoma.Location = new System.Drawing.Point(70, 66);
			this.edKoma.Name = "edKoma";
			this.edKoma.Size = new System.Drawing.Size(70, 19);
			this.edKoma.TabIndex = 4;
			this.edKoma.Text = "0";
			this.edKoma.Value = 0;
			// 
			// edLast
			// 
			this.edLast.Location = new System.Drawing.Point(70, 41);
			this.edLast.Name = "edLast";
			this.edLast.Size = new System.Drawing.Size(70, 19);
			this.edLast.TabIndex = 3;
			this.edLast.Text = "0";
			this.edLast.Value = 0;
			// 
			// edStart
			// 
			this.edStart.Location = new System.Drawing.Point(70, 16);
			this.edStart.Name = "edStart";
			this.edStart.Size = new System.Drawing.Size(70, 19);
			this.edStart.TabIndex = 2;
			this.edStart.Text = "0";
			this.edStart.Value = 0;
			// 
			// AutoInputDlg
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(265, 140);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.edKoma);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.edLast);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.edStart);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AutoInputDlg";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "セルのリピート入力";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private IntEdit edStart;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private IntEdit edLast;
		private System.Windows.Forms.Label label4;
		private IntEdit edKoma;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}