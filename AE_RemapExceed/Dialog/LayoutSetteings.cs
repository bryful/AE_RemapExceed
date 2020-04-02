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
	public partial class LayoutSetteings : Form
	{
		public LayoutSetteings()
		{
			InitializeComponent();
		}
		//-----------------------------------------
		public int CellWidth
		{
			get { return edCellWidth.Value; }
			set { edCellWidth.Value = value; }
		}
		//-----------------------------------------
		public int CellHeight
		{
			get { return edCellHeight.Value; }
			set { edCellHeight.Value = value; }
		}
		//-----------------------------------------
		public int CaptionHeight
		{
			get { return edCaptionHeight.Value; }
			set { edCaptionHeight.Value = value; }
		}
		//-----------------------------------------
		public int FrameWidth
		{
			get { return edFrameWidth.Value; }
			set { edFrameWidth.Value = value; }
		}
		//-----------------------------------------
		public int MemoWidth
		{
			get { return edMemoWidth.Value; }
			set { edMemoWidth.Value = value; }
		}

		private void btnDef_Click(object sender, EventArgs e)
		{
			edCellWidth.Value = TSdef.CellWidth;
			edCellHeight.Value = TSdef.CellHeight;
			edCaptionHeight.Value = TSdef.CaptionHeight;
			edFrameWidth.Value = TSdef.FrameWidth;
			//edMemoWidth.Value = TSdef.MemoWidth;
		}
		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}

	}
}
