using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public class IntEdit : TextBox
	{
		public event EventHandler ValueChanged;

		//----------------------------
		public int Value
		{
			get { return GetValue(); }
			set { 
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
			if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
			{
				e.Handled = true;
			}
			else
			{
				OnValueChanged(new EventArgs());
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
