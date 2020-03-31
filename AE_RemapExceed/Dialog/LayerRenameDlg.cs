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
	public partial class LayerRenameDlg : Form
	{
		//----------------------------------------------------------------------
		public LayerRenameDlg()
		{
			InitializeComponent();
		}
		//----------------------------------------------------------------------
		private void edNew_TextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = (edName.Text.Trim() != edNew.Text.Trim());
		}
		//----------------------------------------------------------------------
		public string CellName
		{
			get { return edNew.Text; }
			set { edNew.Text = edName.Text = value.Trim(); }
		}
		//----------------------------------------------------------------------
		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}

	}
}
