using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public class FloatEdit : TextBox
	{
		public event EventHandler ValueChanged;

		//----------------------------
		public float Value
		{
			get
			{
				float v = 0;
				if (float.TryParse(this.Text, out v))
				{
					return v;
				}
				else
				{
					return 0.0f;
				}
			}
			set
			{
				this.Text = value.ToString();
				OnValueChanged(new EventArgs());
			}
		}
		//----------------------------
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			float v = 0;
			string s = this.Text + e.KeyChar;
			if ((float.TryParse(s, out v))||((e.KeyChar == '\b')))
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
