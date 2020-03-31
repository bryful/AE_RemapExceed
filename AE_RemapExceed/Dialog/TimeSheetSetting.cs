using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AE_RemapExceed
{
	public partial class TimeSheetSetting : Form
	{
		private bool m_IsSecInput = false;
		public TSHistory tsh = new TSHistory();

		//---------------------------------------------------------------------
		public TimeSheetSetting()
		{

			InitializeComponent();
			m_IsSecInput = false;

			SetSecInput();
			
			FrameRate = TSdef.FrameRate;
			CellCount = TSdef.CellCount;
			PageSec = TSdef.PageSec;
			FrameCount = TSdef.FrameCount;
			ZeroStart = TSdef.ZeroStart;
			FrameOffset = TSdef.FrameOffset;


			tsh.SetComboBox(SheetInfo.TITLE, cmbTitle);
			tsh.SetComboBox(SheetInfo.SUB_TITLE, cmbSubTitle);
			tsh.SetComboBox(SheetInfo.OPUS, cmbOPUS);
			tsh.SetComboBox(SheetInfo.SCECNE, cmbSCECNE);
			tsh.SetComboBox(SheetInfo.CUT, cmbCutNo);
			tsh.SetComboBox(SheetInfo.CREATE_USER, cmbCREATE_USER);
			tsh.SetComboBox(SheetInfo.UPDATE_USER, cmbUPDATE_USER);
			tsh.SetComboBox(SheetInfo.CAMPANY_NAME, cmbCAMPANY_NAME);

			tsh.LoadHistory();
		}
        //---------------------------------------------------------------------
		public void SetSecInput()
		{
			edSec.Enabled = edKoma.Enabled = lbSec.Enabled = lbKoma.Enabled = m_IsSecInput;
			edFrame.Enabled = lbFrame.Enabled = !m_IsSecInput;
		}
		//---------------------------------------------------------------------
		public bool SecInputMode
		{
			get { return m_IsSecInput; }
			set
			{
				m_IsSecInput = value;
				rbSec.Checked = value;
				rbFrame.Checked = !value;
				SetSecInput();
			}
		}
		//---------------------------------------------------------------------
		private void rfFrame_Click(object sender, EventArgs e)
		{
			SecInputMode = false;
		}
		//---------------------------------------------------------------------
		private void rbSec_Click(object sender, EventArgs e)
		{
			SecInputMode = true;
		}
		//---------------------------------------------------------------------
		public TSPageSec PageSec
		{
			get
			{
				if (cmbPageSec.SelectedIndex != 0) { return TSPageSec.sec3; }
				else { return TSPageSec.sec6; }
			}
			set
			{
				if (value == TSPageSec.sec6)
				{
					cmbPageSec.SelectedIndex = 0;
				}
				else
				{
					cmbPageSec.SelectedIndex = 1;
				}
			}
		}
		//---------------------------------------------------------------------
		public TSFps FrameRate
		{
			get {
				TSFps ret = TSFps.fps24;
				switch (cmbFps.SelectedIndex)
				{
					case 0: ret = TSFps.fps12; break;
					case 1: ret = TSFps.fps15; break;
					case 2: ret = TSFps.fps24; break;
					case 3: ret = TSFps.fps30; break;
				}
				return ret;
			}
			set { 
				switch (value)
				{
					case TSFps.fps12: cmbFps.SelectedIndex = 0; break;
					case TSFps.fps15: cmbFps.SelectedIndex = 1; break;
					case TSFps.fps24: cmbFps.SelectedIndex = 2; break;
					case TSFps.fps30: cmbFps.SelectedIndex = 3; break;
					default: cmbFps.SelectedIndex = 2; break;
				}
			}
		}
		//---------------------------------------------------------------------
		public int FrameCount
		{
			get {
				if (m_IsSecInput)
				{
					return edSec.Value * (int)FrameRate + edKoma.Value;
				}
				else
				{
					return edFrame.Value;
				}
			}
			set
			{
				edFrame.Value = value;
				edSec.Value = value / (int)FrameRate;
				edKoma.Value = value % (int)FrameRate;
			}
		}
		//---------------------------------------------------------------------
		public int CellCount
		{
			get { return edCellCount.Value; }
			set { 
				edCellCount.Value = value;
			}
		}
		//---------------------------------------------------------------------
		public bool ZeroStart
		{
			get { return cbZeroStart.Checked; }
			set { cbZeroStart.Checked = value; }
		}
		//---------------------------------------------------------------------
		public int FrameOffset
		{
			get { return edFrameOffset.Value; }
			set { edFrameOffset.Value = value; }
		}

        //---------------------------------------------------------------------
		//コメント入力時のリターン対策
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl1.SelectedIndex != 2)
			{
				this.AcceptButton = btnOK;
			}
			else
			{
				this.AcceptButton = null;
			}
		}
		//---------------------------------------------------------------------
		public string[] Comment
		{
			get { return tbComment.Lines; }
			set { tbComment.Lines = value; }
		}
		//---------------------------------------------------------------------
		public void TSDataToComb(TSData tsd)
		{
			tsh.TSData = tsd;
			tsh.TSDataToComb();
		}
		//---------------------------------------------------------------------
		public void CombToTSData(TSData tsd)
		{
			tsh.TSData = tsd;
			tsh.CombToData();
		}
		//---------------------------------------------------------------------
       
        protected override void OnClosed(EventArgs e)
        {
  			tsh.SaveHistory();
            base.OnClosed(e);
        }
        //---------------------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
			//return base.ProcessCmdKey(ref msg, keyData);
		}

  
        //---------------------------------------------------------------------
        private void lbInput_DoubleClick(object sender, EventArgs e)
        {
            string inp = "";
            if (lbInput.SelectedIndex >= 0)
            {
                inp = lbInput.Items[lbInput.SelectedIndex].ToString();

            }
            else
            {
                return;
            }
            int st = tbComment.SelectionStart;
            int l = tbComment.SelectionLength;
            int la = tbComment.Text.Length;
            if (l == 0)
            {
                if (st >= la) 
                { 
                    tbComment.Text += inp;
                    tbComment.SelectionStart = tbComment.Text.Length;
                }
                else
                {
                    tbComment.Text = tbComment.Text.Insert(st,inp);
                    tbComment.SelectionStart = st + inp.Length;
                }
            }
            else
            {
                string org =tbComment.Text;
                string s = org.Substring(0, st);
                s += inp;
                s += org.Substring(st + l);
                tbComment.Text = s;
                tbComment.SelectionStart = st + inp.Length;
            }
            tbComment.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbComment.Text = "";
            tbComment.Select();
        }

        private void tbComment_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar != '\b')
            {
                if (e.KeyChar == 0xd)
                {
                    if (tbComment.Lines.Length >= 5) e.Handled = true;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int cc = 0;
            string md = "";
            if (PageSec == TSPageSec.sec3)
            {
                cc = TSPrint.CellCount3;
                md = "3秒シートでは";
            }
            else
            {
                cc = TSPrint.CellCount;
                md = "6秒シートでは";
            }
            if (CellCount < 6)
            {
                MessageBox.Show("セルレイヤ数は6以下には設定できません！");
                this.DialogResult = DialogResult.None;
                return;
            }
            else if (CellCount >cc)
            {
                if (MessageBox.Show(md + "セルレイヤが"+cc.ToString()+"枚以上は、印刷されません。注意してください。", "注意!", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }
            if (FrameCount < 9)
            {
                MessageBox.Show("フレーム数は9以下にはできません。");
                this.DialogResult = DialogResult.None;
                return;
            }
            this.DialogResult = DialogResult.OK;

        }
        //---------------------------------------------------------------------
	}
}
