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
	public partial class SystemSettingDlg : Form
	{
		ExtentionSetup ExtSetng = new ExtentionSetup();
		Extention ex = new Extention();
		public SystemSettingDlg( )
		{
			InitializeComponent();
			ex.ext = ".ard";
			ex.fileType = "ARDf";
			ex.description = "AE_Remap ard File";
			ex.iconIndex = 1;
			ExtSetng.Add(ex);
		}
		public void Inst()
		{
			ExtSetng.Inst();
		}
		public void Uninst( )
		{
			ExtSetng.Uninst();
		}

		private void btnInst_Click(object sender, EventArgs e)
		{
			Inst();
		}

		private void btnUninst_Click(object sender, EventArgs e)
		{
			Uninst();
		}
		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}


	}
}
