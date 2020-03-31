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
	public partial class RemapSettingDlg : Form
	{
		public RemapSettingDlg( )
		{
			InitializeComponent();
		}
        public bool IsLoadScriptFile
        {
            get { return !(cbIsLoadScriptFile.Checked); }
            set { cbIsLoadScriptFile.Checked = !(value);}
        }
		//------------------------------------------------
		public int SrcWidth
		{
			get { return edSrcWidth.Value; }
			set { edSrcWidth.Value  =value; }
		}
		//------------------------------------------------
		public int SrcHeight
		{
			get { return edSrcHeight.Value; }
			set { edSrcHeight.Value = value; }
		}
		//------------------------------------------------
		public float SrcAspect
		{
			get { return edSrcAspect.Value; }
			set { edSrcAspect.Value = value; }
		}
		//------------------------------------------------
		public float CmpAspect
		{
			get { return edCmpAspect.Value; }
			set { edCmpAspect.Value = value; }
		}
		//------------------------------------------------
		public EmptyCell EmptyCell
		{
			get
			{
				switch (cmbEmptyCell.SelectedIndex)
				{
					case 0: return EmptyCell.Opacity; 
					case 1: return EmptyCell.BlindsJpn;
					case 2: return EmptyCell.BlindsEng;
					default:
					case 3: return EmptyCell.LastFrame;
				}
				
			}
			set
			{
				int si = 3;
				switch (value)
				{
					case EmptyCell.Opacity: si = 0; break;
					case EmptyCell.BlindsJpn: si = 1; break;
					case EmptyCell.BlindsEng: si = 2; break;
					default:
					 si = 3; break;
				}
				cmbEmptyCell.SelectedIndex = si;
				edLastFrame.Enabled = (cmbEmptyCell.SelectedIndex == 3);
			}
		}
		public int LastFrame
		{
			get { return edLastFrame.Value; }
			set { edLastFrame.Value = value; }
		}
		//------------------------------------------------
		private void cmbEmptyCell_SelectedIndexChanged(object sender, EventArgs e)
		{
			edLastFrame.Enabled = (cmbEmptyCell.SelectedIndex == 3);
		}
		//--------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}

	}
}
