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
	public partial class ColorSetting : Form
	{
		private TSColors tsc_org;
		private TSColors tsc = new TSColors();
		private ColorCaption[] cc = new ColorCaption[(int)TSColorIndex.Count];
		public ColorSetting(TSColors c)
		{
			InitializeComponent();
			tsc_org = c;
			tsc.Assign(c);
			this.SuspendLayout();
			for (int i = 0; i < (int)TSColorIndex.Count; i++)
			{
				//インスタンス作成
				this.cc[i] = new ColorCaption();
				//プロパティ設定
				this.cc[i].Name = "cc" + i.ToString();
				this.cc[i].Caption = tsc.CaptionStr[i];
				this.cc[i].Size = new Size(260, 18);
				this.cc[i].Location = new Point(16 +(i/10)*270, 32 + (i % 10)*20);
			}

			//フォームにコントロールを追加
			this.Controls.AddRange(this.cc);
			this.ResumeLayout(false);


			toCaption();
		}
		public TSColors Cols
		{
			set { tsc = value; }
			get { return tsc; }
		}
		private void toCaption()
		{
			for (int i = 0; i < (int)TSColorIndex.Count; i++)
			{
				cc[i].Color = tsc.Col[i];
			}
		}
		private void fromCaption()
		{
			for (int i = 0; i < (int)TSColorIndex.Count; i++)
			{
				tsc.Col[i] = cc[i].Color;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			fromCaption();
		}

		private void col00_Click(object sender, EventArgs e)
		{
		}
		//-----------------------------------------------------------------------
		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				//tsc.save(saveFileDialog1.FileName);
			}
		}
		//-----------------------------------------------------------------------
		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				//if (tsc.load(openFileDialog1.FileName))
				//{
				//	toCaption();
				//}
			}
		}

		private void TSColorSetting_Load(object sender, EventArgs e)
		{

		}

		private void btnDef_Click(object sender, EventArgs e)
		{
			tsc.Init();
			toCaption();
			this.Refresh();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			tsc.Assign(tsc_org);
			toCaption();
			this.Refresh();
		}
		//-----------------------------------------------------------------------
		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
