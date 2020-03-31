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
    public partial class PictureViewForm : Form
    {
		private const string Header = "PicturePreview";
		private const string UserFolderName = "Prefs";
		private const string m_PrefFileName = "PicturePreview.pref";
		public string m_UserPath = "";

		private MainForm mf;
		private PictureFileList m_Plist = new PictureFileList();

        private bool reFlag = false;

		private Size orgSize = new Size(0, 0);
		private pv_Mode pv_now = pv_Mode.view;
		//********************************************************************
		private void PrefPath()
		{
			m_UserPath = "";
			string s = Path.GetDirectoryName(Application.ExecutablePath);
			string up = Path.Combine(s, UserFolderName);
			if (Directory.Exists(up) == false)
			{
				try
				{
					Directory.CreateDirectory(up);
				}
				catch
				{
					return ;
				}
			}
			m_UserPath = Path.Combine(up, Environment.UserName);
			if (Directory.Exists(m_UserPath) == false)
			{
				try
				{
					Directory.CreateDirectory(m_UserPath);
				}
				catch
				{
					return;
				}
			}
			
		}
		//********************************************************************
		public void SavePref()
		{
			PrefPath();
			string p = Path.Combine(m_UserPath, m_PrefFileName);
			string s = "";
			s += Header +"\n";
			s += "Width = " + this.Width.ToString()+"\n";
			s += "Height = " + this.Height.ToString() + "\n";
			s += "Left = " + this.Left.ToString() + "\n";
			s += "Top = " + this.Top.ToString() + "\n";
			s += "pv_now = "  + ((int)pv_now).ToString() + "\n";
			try
			{
				File.WriteAllText(p, s, Encoding.GetEncoding("utf-8"));
				return;
			}
			catch
			{
				return;
			}
		}
		//********************************************************************
		public void LoadPref()
		{
			PrefPath();
			string p = Path.Combine(m_UserPath, m_PrefFileName);
			int w = -1;
			int h = -1;
			int t = -99999;
			int l = -99999;
			int m = -1;
			try
			{
				if (File.Exists(p) == true)
				{
					string[] lines = File.ReadAllLines(p, Encoding.GetEncoding("utf-8"));
					if (lines[0].Trim() == Header)
					{

						foreach (string s in lines)
						{
							if (s != "")
							{
								int v = -99999;
								string[] line = s.Split('=');
								if (line.Length >= 2)
								{
									if (string.Compare(line[0].Trim(), "Width") == 0)
									{
										if (int.TryParse(line[1].Trim(), out v)) w = v;
									}
									else if (string.Compare(line[0].Trim(), "Height") == 0)
									{
										if (int.TryParse(line[1].Trim(), out v)) h = v;
									}
									else if (string.Compare(line[0].Trim(), "Left") == 0)
									{
										if (int.TryParse(line[1].Trim(), out v)) l = v;
									}
									else if (string.Compare(line[0].Trim(), "Top") == 0)
									{
										if (int.TryParse(line[1].Trim(), out v)) t = v;
									}
									else if (string.Compare(line[0].Trim(), "pv_now") == 0)
									{
										if (int.TryParse(line[1].Trim(), out v)) m = v;
									}
								}
							}
						}

					}
				}
				if ((w > 0) && (h > 0))
				{
					this.Width = w;
					this.Height = h;
				}
				if ((t != -99999) && (l != -99999))
				{
					this.Top = t;
					this.Left = l;
				}
				else
				{
					this.Top = this.Owner.Top + 25;
					this.Left = this.Owner.Left + this.Owner.Width + 4;

				}
				if ((m >= 0) && (m < (int)pv_Mode.Count))
				{
					pv_now = (pv_Mode)m;
				}
				else
				{
					pv_now = pv_Mode.view;
				}

			}
			catch
			{
				return;
			}
		}
		//********************************************************************
		public PictureViewForm(MainForm f)
        {
            InitializeComponent();
			lbInfo.Text = "";
			ModeNav.Tag = (int)pv_Mode.nav;
			ModeView.Tag = (int)pv_Mode.view;
			ModeNavView.Tag = (int)pv_Mode.navView;
			ModeViewNav.Tag = (int)pv_Mode.viewNav;

			btnScaaleHarf.Tag = 0;
			btnScale1.Tag = 1;
			btnScale2.Tag = 2;
			btnScale3.Tag = 3;
			btnScale4.Tag = 4;
			this.Owner =f;
			this.mf = f;

			LoadPref();
			orgSize = new Size(this.Width, this.Height);
			SetDispMode(pv_now);
        }

		//********************************************************************
		private void PictureViewForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			SavePref();
		}
		//********************************************************************
		public void PrevPicture()
		{
			if ((m_Plist.Count > 0))
			{
				int idx = m_Plist.Index - 1;
				if (idx < 0) idx = m_Plist.Count - 1;
				m_Plist.Index = idx;
				DispPicture();
			}
		}
		//********************************************************************
		public void NextPicture()
		{
			if ((m_Plist.Count > 0))
			{
				int idx = m_Plist.Index + 1;
				if (idx >= m_Plist.Count) idx = 0;
				m_Plist.Index = idx;
				DispPicture();
			}
		}
		//********************************************************************
		public void DispPicture()
		{
			string s = "";
			if ((m_Plist.Count > 0))
			{
				s = m_Plist.TargetFileName + " " + (m_Plist.Index + 1).ToString() + "/" + m_Plist.Count.ToString();
				pictureView1.OpenFile(m_Plist.TargetFileNameFull);

				btnNext.Enabled = btnPrev.Enabled = true;
				btnScaaleHarf.Enabled =
				btnScale1.Enabled =
				btnScale2.Enabled =
				btnScale3.Enabled =
				btnScale4.Enabled = true;
				lbInfo.Text = s;
				this.Text = s;
			}
			else
			{
				btnNext.Enabled = btnPrev.Enabled = false;
				btnScaaleHarf.Enabled =
				btnScale1.Enabled =
				btnScale2.Enabled =
				btnScale3.Enabled =
				btnScale4.Enabled = false;
				lbInfo.Text = "";
				this.Text = this.Name;
			}

		}
        //********************************************************************
		private void ShowToolStripBtn(bool sw)
		{
			btnFileOpen.Visible =
			btnSelectFolder.Visible =
			btnScaaleHarf.Visible =
			btnScale1.Visible =
			btnScale2.Visible =
			btnScale3.Visible =
			btnScale4.Visible =
			btnHor.Visible = 
			btnPrev.Visible =
			btnNext.Visible =
			sepa1.Visible =
			sepa2.Visible =
			sepa3.Visible =
			sepa4.Visible =
			sw;

		}
		//********************************************************************
		public void SetDispMode(pv_Mode md)
        {
			this.SuspendLayout();
			ModeNav.Checked = false;
			ModeView.Checked = false;
			ModeViewNav.Checked = false;
			ModeNavView.Checked = false;

			int inter = 2;
			int th = toolStrip1.Height;
			int sh = statusStrip1.Height;
			int ww = this.Width -this.ClientSize.Width;
			int hh = this.Height -this.ClientSize.Height;
			int MinH = hh + th + sh + gbNav.Height;
			switch (md)
			{
				case pv_Mode.nav:
					pv_now = pv_Mode.nav;
					ModeMenu.Text = "Nav";
					ModeNav.Checked = true;
					ShowToolStripBtn(false);
					pictureView1.Enabled = pictureView1.Visible = false;
					gbNav.Anchor = AnchorStyles.Left | AnchorStyles.Top;
					gbNav.Enabled = gbNav.Visible = true;
					gbNav.Top = th + inter;
					gbNav.Left = inter;
					orgSize = new System.Drawing.Size(this.Width, this.Height);
					int www = gbNav.Width + inter + inter + ww;
					int hhh = gbNav.Height + inter + inter + th + sh + hh;
					this.MaximumSize  =
					this.MinimumSize = new Size(www, hhh);
					this.Width = www;
					this.Height = hhh;
					break;
				case pv_Mode.view:
					pv_now = pv_Mode.view;
					ModeMenu.Text = "View";
					ShowToolStripBtn(true);
					ModeView.Checked = true;
					pictureView1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
					pictureView1.Enabled = pictureView1.Visible = true;
					gbNav.Enabled = gbNav.Visible = false;
					this.MaximumSize = new Size(0, 0);
					this.MinimumSize = new Size(200, 100);
					if (orgSize.Width != 0)
					{
						Width = orgSize.Width;
						Height = orgSize.Height;
						orgSize = new Size(0, 0);
					}

					pictureView1.Top = th + inter;
					pictureView1.Left = inter;
					pictureView1.Width = ClientSize.Width - inter - inter;
					pictureView1.Height = ClientSize.Height - (th + sh + inter + inter);
					break;
				case pv_Mode.navView:
					ModeMenu.Text = "Nav + View";
					pv_now = pv_Mode.navView;
					ShowToolStripBtn(true);
					ModeNavView.Checked = true;
					pictureView1.Enabled = pictureView1.Visible = true;
					pictureView1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
					gbNav.Anchor = AnchorStyles.Left | AnchorStyles.Top;
					gbNav.Enabled = gbNav.Visible = true;
					this.MaximumSize = new Size(0, 0);
					this.MinimumSize = new Size(gbNav.Width + 200, MinH);
					if (orgSize.Width != 0)
					{
						Width = orgSize.Width;
						Height = orgSize.Height;
						orgSize = new Size(0, 0);
					}
					gbNav.Top = th + inter;
					gbNav.Left = inter;
					pictureView1.Top = th + inter;
					pictureView1.Left = gbNav.Left + gbNav.Width + inter;
					pictureView1.Width = ClientSize.Width -(gbNav.Width + inter * 3 );
					pictureView1.Height = ClientSize.Height -(th +sh + inter + inter ); 
					break;
				case pv_Mode.viewNav:
				default:
					ModeMenu.Text = "View + Nav";
					pv_now = pv_Mode.viewNav;
					ShowToolStripBtn(true);
					ModeViewNav.Checked = true;
					pictureView1.Enabled = pictureView1.Visible = true;
					pictureView1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
					gbNav.Anchor = AnchorStyles.Right | AnchorStyles.Top;
					gbNav.Enabled = gbNav.Visible = true;
					this.MaximumSize = new Size(0, 0);
					this.MinimumSize = new Size(gbNav.Width + 200, MinH);
					if (orgSize.Width != 0)
					{
						Width = orgSize.Width;
						Height = orgSize.Height;
						orgSize = new Size(0, 0);
					}
					pictureView1.Top = th + inter;
					pictureView1.Left = inter;
					pictureView1.Width = ClientSize.Width - (gbNav.Width + inter * 3);
					pictureView1.Height = ClientSize.Height - (th + sh + inter + inter); 
					gbNav.Top = th + inter;
					gbNav.Left = pictureView1.Left + pictureView1.Width + inter;
					break;
			}
			this.ResumeLayout();
	   }
	
        //********************************************************************
		private void btnSelectFolder_Click_1(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				m_Plist.Path = folderBrowserDialog1.SelectedPath;
				if (m_Plist.Count > 0)
				{
					DispPicture();
				}
			}
		}

		//********************************************************************
		private void btnFileOpen_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{

				m_Plist.Path = openFileDialog1.FileName;
				if (m_Plist.Count > 0)
				{
					DispPicture();
				}
			}

		}
		//********************************************************************
		private void btnPrev_Click(object sender, EventArgs e)
		{
			PrevPicture();
		}

        //********************************************************************
        private void btnNext_Click(object sender, EventArgs e)
		{
			NextPicture();
		}

        //********************************************************************
        private void btnScale_Click(object sender, EventArgs e)
		{
			int v = (int)((ToolStripButton)sender).Tag;
			
			btnScaaleHarf.Checked = false;
			btnScale1.Checked = false;
			btnScale2.Checked = false;
			btnScale3.Checked = false;
			btnScale4.Checked = false;

			float sl = 1.0f;
			switch (v)
			{
				case 0: 
					sl = 0.5f; 
					btnScaaleHarf.Checked = true;
					break;
				case 2:
					sl = 2f;
					btnScale2.Checked = true;
					break;
				case 3:
					sl = 3f;
					btnScale3.Checked = true;
					break;
				case 4:
					sl = 4f;
					btnScale4.Checked = true;
					break;
				case 1: 
				default:
					sl = 1f;
					btnScale1.Checked = true;
					break;
			}
			pictureView1.SetRatio(sl);

		}

  
	
        //********************************************************************
        private void btnHor_Click(object sender, EventArgs e)
		{
			pictureView1.DrawHor = !pictureView1.DrawHor;
			btnHor.Checked = pictureView1.DrawHor;
		}
		//********************************************************************
		//********************************************************************
		public MainForm MainForm
		{
			get { return mf; }
			set { mf = value; }
		}
        //********************************************************************
        private void setExec(object sender,PreviewKeyDownEventArgs e)
        {
            if (mf != null)
            {
                mf.KeyExec(e.KeyData);
            }
            pictureView1.Focus();
        }
        //********************************************************************
        private void PictureViewForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            setExec(sender,e);
        }
        //********************************************************************
        private void PictureViewForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
                e.Effect = DragDropEffects.Copy;
            else
                //ファイル以外は受け付けない
                e.Effect = DragDropEffects.None;

        }
        //********************************************************************
        private void PictureViewForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (fileNames.Length > 0)
            {
                for (int i = 0; i < fileNames.Length; i++)
                {
                    string ext = System.IO.Path.GetExtension(fileNames[i]);
                    if ((string.Compare(ext, ".tga") == 0) || (string.Compare(ext, ".jpg") == 0) || (string.Compare(ext, ".png") == 0) || (string.Compare(ext, ".tif") == 0))
                    {
                        m_Plist.Path = fileNames[i];
                        if (m_Plist.Count > 0)
                        {
                            DispPicture();
                            return;
                        }
                    }
                }
            }
        }
        //********************************************************************
        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (mf == null) return;
            this.Select();
            string n = ((NavBtn)sender).Name;
            switch (n)
            {
                case "btnS1": mf.Exec(funcCmd.Selecton1); break;
                case "btnS2": mf.Exec(funcCmd.Selecton2); break;
                case "btnS3": mf.Exec(funcCmd.Selecton3); break;
                case "btnS4": mf.Exec(funcCmd.Selecton4); break;
                case "btnS5": mf.Exec(funcCmd.Selecton5); break;
                case "btnS6": mf.Exec(funcCmd.Selecton6); break;

                case "btnUp": mf.Exec(funcCmd.SelectionUp); break;
                case "btnDown": mf.Exec(funcCmd.SelectionDown); break;
                case "btnLeft": mf.Exec(funcCmd.SelectionLeft); break;
                case "btnRight": mf.Exec(funcCmd.SelectionRight); break;
                case "btnEntry": mf.Exec(funcCmd.ValueInput); break;
                case "btnBS": mf.Exec(funcCmd.ValueBack); break;
                case "btnDel": mf.Exec(funcCmd.ValueDelete); break;
                case "btnV0": mf.KeyExec(Keys.NumPad0); break;
                case "btnV1": mf.KeyExec(Keys.NumPad1); break;
                case "btnV2": mf.KeyExec(Keys.NumPad2); break;
                case "btnV3": mf.KeyExec(Keys.NumPad3); break;
                case "btnV4": mf.KeyExec(Keys.NumPad4); break;
                case "btnV5": mf.KeyExec(Keys.NumPad5); break;
                case "btnV6": mf.KeyExec(Keys.NumPad6); break;
                case "btnV7": mf.KeyExec(Keys.NumPad7); break;
                case "btnV8": mf.KeyExec(Keys.NumPad8); break;
                case "btnV9": mf.KeyExec(Keys.NumPad9); break;
                case "btnSDec": mf.Exec(funcCmd.SelTailDec); break;
                case "btnSInc": mf.Exec(funcCmd.SelTailInc); break;
                case "btnPlus": mf.Exec(funcCmd.ValueAutoInc); break;
                case "btnMinus": mf.Exec(funcCmd.ValueAutoDec); break;
                case "btnDot": mf.Exec(funcCmd.ValueAutoSame); break;
                case "btnSAll": mf.Exec(funcCmd.SelectionALL); break;
                case "btnStoEnd": mf.Exec(funcCmd.SelectionToEND); break;
                case "btnPageDown": mf.Exec(funcCmd.PageDown); break;
                case "btnPageUp": mf.Exec(funcCmd.PageUp); break;
                case "btnTop": mf.Exec(funcCmd.JumpTop); break;
                case "btnEnd": mf.Exec(funcCmd.JumpEnd); break;

            }
        }

		//********************************************************************
		private void pictureView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            setExec(sender, e);
        }

		//********************************************************************
		private void cntrol_Enter(object sender, EventArgs e)
        {
            pictureView1.Focus();
        }


        //********************************************************************
        public bool DirectInput
        {
            get { return cbDirectInput.Checked; }
            set 
            {
                reFlag = true;
                cbDirectInput.Checked = value;
                reFlag = false;
            }
        }

        private void cbDirectInput_Click(object sender, EventArgs e)
        {
            if (reFlag == true) return;
            if (mf != null)
            {
                mf.DireitInput = cbDirectInput.Checked;
            }
        }

		private void ModeMenu_Click(object sender, EventArgs e)
		{
			int v = (int)((ToolStripMenuItem)sender).Tag;

			if ((v >= 0) && (v < (int)pv_Mode.Count))
			{
				SetDispMode((pv_Mode)v);
			}
		}


		private void closeMenu_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

        private void DisposePicture()
        {
            if ( m_Plist.Count>0) m_Plist.Clear();
            pictureView1.ClearPicture();
            pictureView1.Invalidate();
        }

        private void DisposePictureMenu_Click(object sender, EventArgs e)
        {
            DisposePicture();
        }


        //********************************************************************
    }
	//********************************************************************
	public enum pv_Mode
	{
		nav = 0,
		view,
		navView,
		viewNav,
		Count
	}
	//********************************************************************
}
