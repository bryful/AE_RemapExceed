using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public partial class ColorCaption : UserControl
	{
		Color m_Color = new Color();
		public ColorCaption()
		{
			InitializeComponent();
		}
		//------------------------------------------
		public Color Color
		{
			get { return m_Color; }
			set { 
				m_Color = value;
				this.pictureBox1.Refresh();
			}
		}
		//------------------------------------------
		public String Caption
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}
		//------------------------------------------
		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			SolidBrush b = new SolidBrush(m_Color);
			Pen p = new Pen(Color.Black,1);
			try
			{
				Rectangle rct = new Rectangle(0, 0, pictureBox1.Width-1, pictureBox1.Height-1);
				g.FillRectangle(b, rct );
				g.DrawRectangle(p, rct);
			}
			finally
			{
				b.Dispose();
				p.Dispose();
			}

		}
		//------------------------------------------
		private void pictureBox1_Click(object sender, EventArgs e)
		{
			ShowColorPicker();
		}
		//------------------------------------------
		private void ShowColorPicker()
		{
			colorDialog1.Color = this.Color;
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				this.Color = colorDialog1.Color;
				this.Refresh();
			}
		}
		//------------------------------------------
		private void label1_Click(object sender, EventArgs e)
		{
			ShowColorPicker();
		}
		//------------------------------------------

	}
}
