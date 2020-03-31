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
	public partial class CapIntEdit : UserControl
	{
		public CapIntEdit()
		{
			InitializeComponent();
		}
		public string Caption
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}
		public int Value
		{
			get { return intEdit1.Value; }
			set { intEdit1.Value = value; }
		}
	}
}
