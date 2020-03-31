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
	public partial class MemoEdit : Form
	{
        private bool m_input = false;
		public MemoEdit()
		{
			InitializeComponent();
            SetInputMode(m_input);
		}
		//---------------------------------------------------
		public string Memo
		{
			get { return tbMemo.Text; }
			set { tbMemo.Text = tbOrg.Text = value; }
		}
		//---------------------------------------------------
		public void setFrame(TSData tsd, int f)
		{
			groupBox1.Text = tsd.FrameStr2(f);
		}
		//---------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}
        //---------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbMemo.Text = "";
        }
        //---------------------------------------------------
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                if (tbMemo.Text == "")
                {
                    tbMemo.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
                }
                else
                {
                    tbMemo.Text += " " + listBox1.Items[listBox1.SelectedIndex].ToString();
                }
            }

        }

        //---------------------------------------------------
        private void btnInput_Click(object sender, EventArgs e)
        {

            SetInputMode(!m_input);
        }
        //---------------------------------------------------
        public void SetInputMode(bool sw)
        {
            m_input = sw;
            if (sw == true)
            {
                btnInput.Text = "<<";
                this.Width = 300;
            }
            else
            {
                btnInput.Text = ">>";
                this.Width = 220;
            }
        }
        //---------------------------------------------------
        public bool InputMode
        {
            get { return m_input; }
            set { SetInputMode(value); }
        }
        //---------------------------------------------------
    }
}
