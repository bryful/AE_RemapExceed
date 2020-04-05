using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public partial class NameDialg : Form
	{
		public NameDialg()
		{
			InitializeComponent();
		}
		public string SheetName
		{
			get { return textBox1.Text.Trim(); }
			set { textBox1.Text = value.Trim(); }
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = (textBox1.Text.Trim() != "");
		}
	}
}
