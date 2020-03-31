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
	public partial class AutoInputDlg : Form
	{
		public AutoInputDlg()
		{
			InitializeComponent();
		}
		public int Start
		{
			get { return edStart.Value; }
			set { edStart.Value = value; }
		}
		public int Last
		{
			get { return edLast.Value; }
			set { edLast.Value = value; }
		}
		public int Koma
		{
			get { return edKoma.Value; }
			set { edKoma.Value = value; }
		}		
		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}

	}
}
