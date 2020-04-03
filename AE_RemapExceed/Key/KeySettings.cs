using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public partial class KeySettings : Form
	{
		private TSFunctions tsfunc = new TSFunctions();
		private TSFunctions tsfuncBak = new TSFunctions();
		private KeyBind[] kb = new KeyBind[(int)funcCmd.Count];
		private string m_UserPath = "";
		//--------------------------------------------------------------------------------
		public TSFunctions TSFunctions
		{
			get { return tsfunc; }
		}
		//--------------------------------------------------------------------------------
		public KeySettings(TSFunctions f)
		{
			InitializeComponent();


			tsfunc.Assign(f);
			tsfuncBak.Assign(f);
			this.SuspendLayout();
			for (int i = 0; i < (int)funcCmd.Count; i++)
			{
				//インスタンス作成
				this.kb[i] = new KeyBind();
				//プロパティ設定
				this.kb[i].Name = "kb" + i.ToString();
				this.kb[i].Size = new Size(480, 24);
				this.kb[i].Location = new Point(20, 10 + i * 30);
				this.kb[i].Caption = tsfunc.funcName[i, 1];
				this.kb[i].KeyDataChanged += new System.EventHandler(this.KeyDataChanged);

			}
			FromFunc();

			//フォームにコントロールを追加
			this.panel1.Controls.AddRange(this.kb);
			this.ResumeLayout(false);
		}
		//--------------------------------------------------------------------------------
		private void toFunc()
		{
			for (int i = 0; i < (int)funcCmd.Count; i++)
			{
				tsfunc.setKeyTable((funcCmd)i,kb[i].KeyData);
				tsfunc.setKeyTableSub((funcCmd)i, kb[i].KeyDataSub);
			}
		}
		//--------------------------------------------------------------------------------
		private void FromFunc()
		{
			for (int i = 0; i < (int)funcCmd.Count; i++)
			{
				kb[i].KeyData = tsfunc.getKeyTable((funcCmd)i);
				kb[i].KeyDataSub = tsfunc.getKeyTableSub((funcCmd)i);
			}
		}
		//--------------------------------------------------------------------------------
		private bool CompareKey(Keys k0, Keys k1)
		{
			if ((k0 == Keys.None) || (k1 == Keys.None)) return false;
			return (k0 == k1);
		}
		public bool ChkKeyData()
		{
			bool ret = true;
			for (int i = 0; i < kb.Length; i++)
			{
				kb[i].IsDup = false;
				kb[i].IsDupSub = false;
			}
			for (int i = 0; i < kb.Length - 1; i++)
			{
				Keys k = kb[i].KeyData;
				Keys ks = kb[i].KeyDataSub;
				for (int j = i + 1; j < kb.Length; j++)
				{
					Keys k2 = kb[j].KeyData;
					Keys ks2 = kb[j].KeyDataSub;
					if (CompareKey(k, k2))
					{
						kb[i].IsDup = true;
						kb[j].IsDup = true;
						ret = false;
					}
					if (CompareKey(k, ks2))
					{
						kb[i].IsDup = true;
						kb[j].IsDupSub = true;
						ret = false;
					}
					if (CompareKey(ks, k2))
					{
						kb[i].IsDupSub = true;
						kb[j].IsDup = true;
						ret = false;
					}
					if (CompareKey(ks, ks2))
					{
						kb[i].IsDupSub = true;
						kb[j].IsDupSub = true;
						ret = false;
					}

				}
			}
			btnOK.Enabled = ret;
			if (ret) toFunc();
			return ret;

		}
		//--------------------------------------------------------------------------------
		private void KeySettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.DialogResult == DialogResult.OK)
			{
				e.Cancel = (ChkKeyData() == false);
			}
		}

		//--------------------------------------------------------------------------------
		private void KeyDataChanged(object sender, EventArgs e)

		{
			ChkKeyData();
		}
		//--------------------------------------------------------------------------------
		public Keys getKeyData(funcCmd c)
		{
			return kb[(int)c].KeyData;
		}
		//--------------------------------------------------------------------------------
		public Keys getKeyDataSub(funcCmd c)
		{
			return kb[(int)c].KeyDataSub;
		}

		//--------------------------------------------------------------------------------
		private void btnDef_Click(object sender, EventArgs e)
		{
			tsfunc.funcKeyInt();
			FromFunc();
			ChkKeyData();
		}

		private void btnUndo_Click(object sender, EventArgs e)
		{
			tsfunc.Assign(tsfuncBak);
			FromFunc();
			ChkKeyData();
		}

		//--------------------------------------------------------------------------------
		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveFileDialog sv = new SaveFileDialog();
			sv.Title = "Keysファイルの保存";
			sv.Filter = "Keysファイル(*.keys)|*.keys|すべてのファイル(*.*)|*.*";
			sv.FilterIndex = 1;
			sv.DefaultExt = "keys";
			sv.FileName = m_UserPath + @"\user.keys";
			if (sv.ShowDialog() == DialogResult.OK)
			{
				toFunc();
				tsfunc.SaveToFile(sv.FileName);
			}
		}

		//--------------------------------------------------------------------------------
		private void btnLoad_Click(object sender, EventArgs e)
		{
			OpenFileDialog op = new OpenFileDialog();
			op.Title = "Keysファイルの読み込み";
			op.Filter = "Keysファイル(*.keys)|*.keys|すべてのファイル(*.*)|*.*";
			op.FilterIndex = 1;
			op.DefaultExt = "keys";
			op.FileName = m_UserPath + @"\user.keys";
			if (op.ShowDialog() == DialogResult.OK)
			{
				tsfunc.LoadFromFile(op.FileName);
				FromFunc();
			}


		}

        private void KeySettings_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void KeySettings_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
		//--------------------------------------------------------------------------------
		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
