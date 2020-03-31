using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace AE_RemapExceed
{
	public class KeyDataBtn : Button
	{
		public event EventHandler KeyDataChanged;
		private Keys m_KeyData;
		//-----------------------------------------------------------------------
		protected virtual void OnKeyDataChanged(EventArgs e)
		{
			if (KeyDataChanged != null)
			{
				KeyDataChanged(this, e);
			}
		}
		//---------------------------------------------------------
		public KeyDataBtn()
		{
			this.BackColor = Color.LightGray;
			SetKeyCode(Keys.None);
		}
		//---------------------------------------------------------
		protected override void OnMouseClick(MouseEventArgs e)
		{
			KeyDataDialog kd = new KeyDataDialog(m_KeyData);
	
			Point p = this.PointToScreen(e.Location);
			p.X -= kd.Width / 2;
			p.Y -= kd.Height / 2;
			kd.Location = p;
			if (kd.ShowDialog() == DialogResult.OK)
			{
				SetKeyCode( kd.KeyData );
				OnKeyDataChanged(new EventArgs());
			}
			base.OnMouseClick(e);
		}
		//---------------------------------------------------------
		public Keys KeyData
		{
			get { return m_KeyData; }
			set { SetKeyCode( value); }
		}
		//---------------------------------------------------------
		public void SetKeyCode(Keys k)
		{


			long kH = (long)k & 0xFFFF0000;
			long kL = (long)k & 0xFFFF;

			string s = ((Keys)kL).ToString();

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
			this.Text = s;
			m_KeyData = k;
		}
		//---------------------------------------------------------
		public bool IsNonDup
		{
			get { return (this.BackColor == Color.LightGray); }
			set
			{
				if (value)
				{
					this.BackColor = Color.LightGray;
				}
				else
				{
					this.BackColor = Color.Red;
				}
			}
		}
	}
}
