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
	public partial class PrintSettingDlg : Form
	{
		public TSHistory tsh = new TSHistory();
        private TSForm mf;
		private TSData tsd;
		public PrintSettingDlg()
		{
			InitializeComponent();
			tsh.SetComboBox(SheetInfo.TITLE, cmbTITLE);
			tsh.SetComboBox(SheetInfo.SUB_TITLE, cmbSUB_TITLE);
			tsh.SetComboBox(SheetInfo.OPUS, cmbOPUS);
			tsh.SetComboBox(SheetInfo.SCECNE, cmbSCECNE);
			tsh.SetComboBox(SheetInfo.CUT, cmbCUT);
			tsh.SetComboBox(SheetInfo.CREATE_USER, cmbCREATE_USER);
			tsh.SetComboBox(SheetInfo.UPDATE_USER, cmbUPDATE_USER);
			tsh.SetComboBox(SheetInfo.CAMPANY_NAME, cmbCAMPANY_NAME);

			tsh.SetCheckBox(SheetInfo.TITLE, cbTITLE);
			tsh.SetCheckBox(SheetInfo.SUB_TITLE, cbSUB_TITLE);
			tsh.SetCheckBox(SheetInfo.OPUS, cbOPUS);
			tsh.SetCheckBox(SheetInfo.SCECNE, cbSCECNE);
			tsh.SetCheckBox(SheetInfo.CUT, cbCUT);
			tsh.SetCheckBox(SheetInfo.CREATE_USER, cbCREATE_USER);
			tsh.SetCheckBox(SheetInfo.UPDATE_USER, cbUPDATE_USER);
			tsh.SetCheckBox(SheetInfo.CAMPANY_NAME, cbCAMPANY_NAME);
			
			tsh.LoadHistory();

		}
		//*************************************************************
		public string[] Comment
		{
			get { return tbComment.Lines; }
			set { tbComment.Lines = value; }
		}
		//*************************************************************
		public bool IsPrintComment
		{
			get { return cbComment.Checked; }
			set { cbComment.Checked = value; }
		}
        //*************************************************************
        public bool IsPrintMemo
        {
            get { return cbMemo.Checked; }
            set { cbMemo.Checked = value; }
        }
        //*************************************************************
        public CmtAligns CommentAlign
        {
            get { return cmtAlign1.SelectedIndex; }
            set { cmtAlign1.SelectedIndex = value; }
        }
        //*************************************************************
		public void TSDataToComb(TSData tsd)
		{
			tsh.TSData = tsd;
			tsh.TSDataToComb();
		}
		//*************************************************************
		public void CombToTSData(TSData tsd)
		{
			tsh.TSData = tsd;
			tsh.CombToData();
		}
        //*************************************************************
        public TSForm MainForm
        {
            get { return mf; }
            set 
			{ 
				mf = value;
				tsd = mf.TSData;
			}
        }
        //*************************************************************
		private void PrintSettingDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			tsh.SaveHistory();
		}

        //*************************************************************
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
                    tbComment.Text = tbComment.Text.Insert(st, inp);
                    tbComment.SelectionStart = st + inp.Length;
                }
            }
            else
            {
                string org = tbComment.Text;
                string s = org.Substring(0, st);
                s += inp;
                s += org.Substring(st + l);
                tbComment.Text = s;
                tbComment.SelectionStart = st + inp.Length;
            }
            tbComment.Focus();
        }

        //*************************************************************
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbComment.Text = "";
        }

        //*************************************************************
        private void Menu_PageSetup_Click(object sender, EventArgs e)
        {
            if (mf != null)
            {
				CombToTSData(tsd);
				tsd.CommentLines = tbComment.Lines;
				tsd.IsPrintComment = cbComment.Checked;
                tsd.CommentAlign = cmtAlign1.SelectedIndex;
                mf.Exec(funcCmd.PageSetup);
            }
        }

        //*************************************************************
        private void MenuPrintPreview_Click(object sender, EventArgs e)
        {
            if (mf != null)
            {
				CombToTSData(tsd);
				tsd.CommentLines = tbComment.Lines;
				tsd.IsPrintComment = cbComment.Checked;
                tsd.CommentAlign = cmtAlign1.SelectedIndex;
                mf.Exec(funcCmd.PrintPreview);
            }
        }

        //*************************************************************
        private void MenuPrint_Click(object sender, EventArgs e)
        {
            if (mf != null)
            {
				CombToTSData(tsd);
				tsd.CommentLines = tbComment.Lines;
				tsd.IsPrintComment = cbComment.Checked;
                tsd.CommentAlign = cmtAlign1.SelectedIndex;
                mf.Exec(funcCmd.Print);
            }
        }
        //*************************************************************
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return false;
            //return base.ProcessCmdKey(ref msg, keyData);
        }

        //*************************************************************
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

		//*************************************************************
		
	}
}
