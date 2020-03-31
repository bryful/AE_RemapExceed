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
	public partial class KeyBind : UserControl
	{
		public event EventHandler KeyDataChanged;
		private bool m_IsDup = false;
		private bool m_IsDupSub = false;

		//-----------------------------------------------------------------------
		protected virtual void OnKeyDataChanged(EventArgs e)
		{
			if (KeyDataChanged != null)
			{
				KeyDataChanged(this, e);
			}
		}
		//-----------------------------------------------------------------------
		private void keyDataBtn1_KeyDataChanged(object sender, EventArgs e)
		{
			OnKeyDataChanged(new EventArgs());
		}
		//-------------------------------------------
		public KeyBind()
		{
			InitializeComponent();
			keyDataBtn1.BackColor = Color.LightGray;
			keyDataBtn2.BackColor = Color.LightGray;
			m_IsDup = false;
			m_IsDupSub = false;
		}
		//-------------------------------------------
		public Keys KeyData
		{
			get { return keyDataBtn1.KeyData; }
			set { keyDataBtn1.KeyData = value; }
		}
		//-------------------------------------------
		public Keys KeyDataSub
		{
			get { return keyDataBtn2.KeyData; }
			set { keyDataBtn2.KeyData = value; }
		}
		//-------------------------------------------
		public string Caption
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}
		//-------------------------------------------
		public bool IsDup
		{
			get { return m_IsDup; }
			set 
			{ 
				m_IsDup = value;
				if (value) { keyDataBtn1.BackColor = Color.Red; } else { keyDataBtn1.BackColor = Color.LightGray; }
			}
		}
		//-------------------------------------------
		public bool IsDupSub
		{
			get { return m_IsDupSub; }
			set
			{
				m_IsDupSub = value;
				if (value) { keyDataBtn2.BackColor = Color.Red; } else { keyDataBtn2.BackColor = Color.LightGray; }
			}
		}

        private void KeyBind_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
		//-------------------------------------------
		
		//-------------------------------------------

	}
}
