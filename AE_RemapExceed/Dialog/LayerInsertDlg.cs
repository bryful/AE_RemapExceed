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
	public partial class LayerInsertDlg : Form
	{
		public LayerInsertDlg()
		{
			InitializeComponent();
		}
		//----------------------------------------------------------------
		private void edName_TextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = (edName.Text != "");
		}
		//----------------------------------------------------------------
		public string Caption
		{
			get { return edName.Text; }
			set { edName.Text = value; }
		}
		//----------------------------------------------------------------
		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}

	}
}
