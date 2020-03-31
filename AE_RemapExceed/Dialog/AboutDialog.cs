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
	public partial class AboutDialog : Form
	{
		public AboutDialog( )
		{
			InitializeComponent();
			lbVaersion.Text = AE_RemapExceed.Properties.Resources.VersionStr;
		}

		private void AboutDialog_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		public string Version
		{
			get { return lbVaersion.Text; }
			set { lbVaersion.Text = value; }
		}

		private void AboutDialog_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.Close();
		}

		private void AboutDialog_Paint(object sender, PaintEventArgs e)
		{
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			Pen p = new Pen(Color.Black);
			SolidBrush b = new SolidBrush(Color.SkyBlue);

			Graphics g = e.Graphics;
			try
			{
				g.DrawRectangle(p, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
				g.FillRectangle(b,pictureBox2.Bounds);
			}
			finally
			{
				p.Dispose();
				b.Dispose();
			}

			base.OnPaint(e);
		}
		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}

	}
}
