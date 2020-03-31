using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public class IntEditD : TextBox
	{
		public event EventHandler ValueChanged;

		//----------------------------
		public int Value
		{
			get { return GetValue(); }
			set
			{
				this.Text = value.ToString();
				OnValueChanged(new EventArgs());
			}
		}
		//----------------------------
		private int GetValue()
		{
			int v = 0;
			if (int.TryParse(this.Text, out v))
			{
				return v;
			}
			else
			{
				return 0;
			}
		}
		//----------------------------
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			int v = 0;
			string s = this.Text + e.KeyChar;
			if (((this.Text == "") || (this.Text == "0")) && (e.KeyChar == '-'))
			{
				OnValueChanged(new EventArgs());
			}
			else if ((int.TryParse(s, out v)) || (e.KeyChar == '\b'))
			{
				OnValueChanged(new EventArgs());
			}
			else
			{
				e.Handled = true;
			}

			base.OnKeyPress(e);
		}
		//----------------------------
		protected virtual void OnValueChanged(EventArgs e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
			}
		}
	}

}
