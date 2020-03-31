namespace AE_RemapExceed
{
	partial class KeyBind
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

		#region コンポーネント デザイナで生成されたコード

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.keyDataBtn2 = new AE_RemapExceed.KeyDataBtn();
            this.keyDataBtn1 = new AE_RemapExceed.KeyDataBtn();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(258, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // keyDataBtn2
            // 
            this.keyDataBtn2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.keyDataBtn2.BackColor = System.Drawing.Color.LightGray;
            this.keyDataBtn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.keyDataBtn2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyDataBtn2.IsNonDup = true;
            this.keyDataBtn2.KeyData = System.Windows.Forms.Keys.None;
            this.keyDataBtn2.Location = new System.Drawing.Point(130, 0);
            this.keyDataBtn2.Name = "keyDataBtn2";
            this.keyDataBtn2.Size = new System.Drawing.Size(122, 23);
            this.keyDataBtn2.TabIndex = 2;
            this.keyDataBtn2.Text = "None";
            this.keyDataBtn2.UseVisualStyleBackColor = false;
            this.keyDataBtn2.KeyDataChanged += new System.EventHandler(this.keyDataBtn1_KeyDataChanged);
            // 
            // keyDataBtn1
            // 
            this.keyDataBtn1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.keyDataBtn1.BackColor = System.Drawing.Color.LightGray;
            this.keyDataBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.keyDataBtn1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyDataBtn1.IsNonDup = true;
            this.keyDataBtn1.KeyData = System.Windows.Forms.Keys.None;
            this.keyDataBtn1.Location = new System.Drawing.Point(0, 0);
            this.keyDataBtn1.Name = "keyDataBtn1";
            this.keyDataBtn1.Size = new System.Drawing.Size(122, 23);
            this.keyDataBtn1.TabIndex = 1;
            this.keyDataBtn1.Text = "None";
            this.keyDataBtn1.UseVisualStyleBackColor = false;
            this.keyDataBtn1.KeyDataChanged += new System.EventHandler(this.keyDataBtn1_KeyDataChanged);
            // 
            // KeyBind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.keyDataBtn2);
            this.Controls.Add(this.keyDataBtn1);
            this.Controls.Add(this.label1);
            this.Name = "KeyBind";
            this.Size = new System.Drawing.Size(481, 23);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyBind_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private KeyDataBtn keyDataBtn1;
		private KeyDataBtn keyDataBtn2;
	}
}
