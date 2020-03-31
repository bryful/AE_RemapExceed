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
	public partial class KeyDataDialog : Form
	{
		private Keys m_KeyData;
		//--------------------------------------------------------
		public KeyDataDialog(Keys k)
		{
			InitializeComponent();
			SetKeyCode(k);
		}
		//--------------------------------------------------------
		public void SetKeyCode(Keys k)
		{

			long kH = (long)k & 0xFFFF0000;
			long kL = (long)k & 0xFFFF;

			if ( ((Keys)kL == Keys.ShiftKey)||((Keys)kL == Keys.ControlKey)||((Keys)kL == Keys.Menu) ) return;
            if (((Keys)kL >= Keys.NumPad0) && ((Keys)kL <= Keys.NumPad9))
            {
                kL -= (long)Keys.NumPad0 - (long)Keys.D0;
            }
            if (((Keys)kL >= Keys.D0) && ((Keys)kL <= Keys.D9))
            {
                if ( kH ==0) return;
            }
			if (((Keys)kL >= Keys.Left) && ((Keys)kL <= Keys.Down)) return;
			if (((Keys)kL == Keys.IMEConvert) || ((Keys)kL == Keys.IMENonconvert)) return;

			string s = ((Keys)kL).ToString();

			if (s.IndexOf(",") >= 0) return;

			if (((Keys)kH & Keys.Alt) == Keys.Alt)
			{
				s = "Alt+" + s;
			}
			if (((Keys)kH & Keys.Shift) == Keys.Shift)
			{
				s = "Shift+" + s;
			}
			if (((Keys)kH & Keys.Control) == Keys.Control)
			{
				s = "Ctrl+" + s;
			}


			lbKeyData.Text = s;
			m_KeyData = (Keys)(kH | kL);
		}
		//--------------------------------------------------------
		public Keys KeyData
		{
			get { return m_KeyData; }
			set { m_KeyData = value; }
		}
		//--------------------------------------------------------
		public string KeyDataStr
		{
			get { return lbKeyData.Text; }
		}
		//--------------------------------------------------------

		private void btnReset_Click(object sender, EventArgs e)
		{
			SetKeyCode(Keys.None);
		}
		//--------------------------------------------------------
		private void KeyDataDialog_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		//--------------------------------------------------------
		private void btnReset_Enter(object sender, EventArgs e)
		{
			this.Focus();
		}
		//--------------------------------------------------------
		private void KeyDataDialog_KeyDown(object sender, KeyEventArgs e)
		{
            SetKeyCode(e.KeyData);
  	}

		//--------------------------------------------------------
		private void KeyDataDialog_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rct = new Rectangle(0, 0, this.Width-1, this.Height - 1);
			Pen p = new Pen(Color.Black);
			try
			{
				Graphics g = e.Graphics;
				g.DrawRectangle(p, rct);
			}
			finally
			{
				p.Dispose();
			}
		}


		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
