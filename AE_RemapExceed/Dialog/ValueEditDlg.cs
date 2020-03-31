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
    public partial class ValueEditDlg : Form
    {

        public ValueEditDlg()
        {
            InitializeComponent();
        }
        //-----------------------------------------------
        public void SetMode(ValueEditMode md)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            switch (md)
            {
                case ValueEditMode.add:
                    radioButton2.Checked = true;
                    break;
                case ValueEditMode.dec:
                    radioButton3.Checked = true;
                    break;
                default:
                    radioButton1.Checked = true;
                    break;
            }
        }
        //-----------------------------------------------
        public ValueEditMode GetMode()
        {
            if (radioButton1.Checked) { return ValueEditMode.direct; }
            else if (radioButton2.Checked) { return ValueEditMode.add; }
            else if (radioButton3.Checked) { return ValueEditMode.dec; }
            else { return ValueEditMode.direct; }
        }
        //-----------------------------------------------
        public int Value
        {
            get { return intEdit1.Value; }
            set { intEdit1.Value = value; }
        }
        //-----------------------------------------------
        public ValueEditMode Mode
        {
            get { return GetMode(); } 
            set { SetMode(value); } 
        }

		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}

    }
 }
