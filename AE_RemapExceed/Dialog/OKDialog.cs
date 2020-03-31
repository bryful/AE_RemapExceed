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
	public partial class OKDialog : Form
	{
		public OKDialog(string cmt, int mm)
		{
			InitializeComponent();
            label1.Text = cmt;
            setLayout();
            timer1.Interval = mm;
		}
        public int Interval
        {
            get { return timer1.Interval; }
            set { timer1.Interval = value; }

        }
        public string Caption
        {
            get { return label1.Text; }
            set { label1.Text = value; setLayout(); }

        }
        public void setLayout()
        {
            this.ClientSize = new Size(label1.Width + 16, label1.Height + 8);
            label1.Left = 8;
            label1.Top = 4;

        }
		public void SetPos(int x, int y)
		{
			this.Left = x;
			this.Top = y;
		}
		public void execute()
		{
			timer1.Enabled = true;
			this.ShowDialog();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.Close();
		}

		private void OKDialog_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Rectangle rct = new Rectangle(0, 0, this.Width-1, this.Height-1);
			Pen p = new Pen(Color.Black, 1);
			try
			{
				g.DrawRectangle(p, rct);
			}
			finally
			{
				p.Dispose();
			}

		}

		private void OKDialog_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void OKDialog_KeyDown(object sender, KeyEventArgs e)
		{
			this.Close();
		}
		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}

	}
}
