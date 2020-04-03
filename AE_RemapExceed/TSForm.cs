﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


using Codeplex.Data;
using BRY;

namespace AE_RemapExceed
{
    public enum EXEC_MODE
    {
        NONE = 0,
        EXPORT,
        IMPORT,
        EXPORT_LAYER,
        IMPORT_LAYER,
        LOAD,
        SAVE
    }
    public partial class TSForm : Form
	{

        public bool m_LayoutFlag = true;
        public PictureViewForm pvf = null;
        private NavBar m_NavBar = new NavBar();
        //-------------------------------------------------------------
        private void NavBarSetup()
        {
            m_NavBar.Form = this;
            m_NavBar.SizeSet();
            m_NavBar.LocSet();
            m_NavBar.Show();

        }
        //--------------------------------------------------------------------------------------
        public TSForm()
		{
            InitializeComponent();

			this.Text = AE_RemapExceed.Properties.Resources.AppName;
            tsGrid1.TSForm = this;
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.m_MouseWheel);

			
			if (PrefLoad()==false)
			{
				this.Left = 100;
				this.Top = 100;
			}
            tsGrid1.GetStatus();

            SetFrameDisp(tsGrid1.tsd.FrameDisp);
			SetLayout();
			toolStripStatusLabel1.Text = tsGrid1.SelInfo;
			ShortCutPre();

           
            NavBarSetup();
            m_NavBar.LocSet();


            string[] cmds;
            cmds = System.Environment.GetCommandLineArgs();
            GetCommand(cmds, String.Format("Constractor{0}",0));

            this.Invalidate();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
  
        }
        private void MainForm_Layout(object sender, LayoutEventArgs e)
        {

        }
        //-------------------------------------------------------------
        /// <summary>
        /// ダミー関数
        /// </summary>
        /// <param name="cmd"></param>
        public void GetCommand(string[] cmd,string cp="")
        {
            /*
             *  /export <path> セルデータを指定されたパスに保存
             *  
             *  /import <path> セルデータを読み込む。
             *  
             *  /layer
                */
            EXEC_MODE mode = EXEC_MODE.NONE;
            bool layer = false;
            string filename = "";

            //引数の解析
            if (cmd.Length > 1)
            {
                for(int i=1; i<cmd.Length;i++)
                {
                    string s = cmd[i];
                    if ((s[0]=='/')||(s[0] == '-')) //option
                    {
                        string s2 = s.Substring(1).ToLower();
                        switch(s2)
                        {
                            case "s":
                            case "save":
                                if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.SAVE;
                                break;
                            case "l":
                            case "o":
                            case "load":
                            case "open":
                                if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.LOAD;
                                break;

                            case "e":
                            case "export":
                            case "output":
                                if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.EXPORT;
                                break;
                            case "i":
                            case "import":
                                if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.IMPORT;
                                break;
                            case "layer":
                            case "cell":
                                layer = true; ;
                                break;
                        }

                    }
                    else
                    {
                        s = s.Trim();
                        if (s.Length >= 2)
                        {
                            if ((s[0]=='"')&& (s[s.Length-1] == '"'))
                            {
                                s = s.Substring(1, s.Length - 2);
                            }
                        }
                        s = s.Trim();
                        if (s != "")
                        {
                            if (filename == "") filename = s;
                        }

                    }
                }
                if ((mode == EXEC_MODE.EXPORT) || (mode == EXEC_MODE.IMPORT) || (mode == EXEC_MODE.SAVE) || (mode == EXEC_MODE.LOAD))
                {
                    if (filename == "") mode = EXEC_MODE.NONE;
                }
                if ((filename != "")&& (mode == EXEC_MODE.NONE)) mode = EXEC_MODE.LOAD;
                if (layer == true)
                {
                    if (mode == EXEC_MODE.EXPORT) mode = EXEC_MODE.EXPORT_LAYER;
                    else if (mode == EXEC_MODE.IMPORT) mode = EXEC_MODE.IMPORT_LAYER;
                }
            }

            //実際の動作
            switch(mode)
            {
                case EXEC_MODE.EXPORT:
                    tsGrid1.SaveToJsonFile(filename);
                    SetActive();
                    break;
            }
        }
        //--------------------------------------------------------------------------------------
        public void SetActive()
        {
            this.BringToFront();
            if (this.TopMost == false)
            {
                this.TopMost = true;
                this.TopMost = false;
            }

        }
        //--------------------------------------------------------------------------------------
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (tsGrid1.SaveFlag)
			{
				DialogResult r = MessageBox.Show("ファイルを保存しますか？", "AE_Remap Exceed", MessageBoxButtons.YesNoCancel);
				switch (r)
				{
					case DialogResult.OK:
						tsGrid1.SaveAs();
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						return;
					case DialogResult.No:
						break;
				}
			}

		}
		//--------------------------------------------------------------------------------------
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
            PrefSave();

        }
		//--------------------------------------------------------------------------------------
		private void tsGrid1_SizeChanged(object sender, EventArgs e)
		{
			SetLayout();
		}
        //--------------------------------------------------------------------------------------
        public void PrefSave()
        {
            //設定ファイルの保存
            JsonPref pref = new JsonPref();

            pref.SetPoint("Point", this.Location);
            pref.SetInt("Height", this.Height);
            pref.SetIntArray("ParamsInt", tsGrid1.tsd.ParamsInt);
            pref.SetBoolArray("ParamsBool", tsGrid1.tsd.ParamsBool);
            pref.SetIntArray("Colors", tsGrid1.cols.ColorInt);
            pref.SetIntArray("Keys", tsGrid1.funcs.FuncTableAll);
            pref.Save();

        }
        //--------------------------------------------------------------------------------------
        public bool PrefLoad()
        {
            bool ret = false;
            //設定ファイルの保存
            JsonPref pref = new JsonPref();

            if (pref.Load()==true)
            {
                bool ok = false;
                Point p = pref.GetPoint("Point", out ok);
                if (ok) this.Location = p;
                int h = pref.GetInt("Height", out ok);
                if (ok) this.Height = h;
                int[] a = pref.GetIntArray("ParamsInt", out ok);
                if (ok) tsGrid1.tsd.ParamsInt = a;
                bool[] b = pref.GetBoolArray("ParamsBool", out ok);
                if (ok) tsGrid1.tsd.ParamsBool = b;
                int[] c = pref.GetIntArray("Colors", out ok);
                if (ok) tsGrid1.cols.ColorInt = c;
                int[] d = pref.GetIntArray("Keys", out ok);
                if (ok) tsGrid1.funcs.FuncTableAll = d;
                ret = true;

            }
            return ret;



        }
        //**********************************************************************************
        //コントロールの配置
        //**********************************************************************************
        public void SetLayout()
		{
			if (m_LayoutFlag == false) return;
			bool bak = m_LayoutFlag;
			m_LayoutFlag = false;

			this.SuspendLayout();
			int ww = this.Width - this.ClientSize.Width;
			int hh = this.Height - this.ClientSize.Height;
			int SP = 2;

			//最大値を計算
			int wMax = tsGrid1.tsd.widthMax + tsGrid1.tsd.FrameWidth /*+ tsGrid1.tsd.MemoWidth*/ + SP*5 + ww + tsNav1.Width;
			int hMax = tsGrid1.tsd.heightMax + tsGrid1.tsd.CaptionHeight + statusStrip1.Height + menuStrip1.Height + SP * 3 + hh ;
			int hMin = tsGrid1.tsd.CellHeight + tsGrid1.tsd.CaptionHeight + statusStrip1.Height + menuStrip1.Height + SP * 3 + hh;
			
			this.MaximumSize = new Size(wMax, hMax);
			this.MinimumSize = new Size(wMax, hMin);
			

			int h = this.ClientSize.Height - (statusStrip1.Height + menuStrip1.Height +  SP*3);
			int t = menuStrip1.Top + menuStrip1.Height +SP;

			tsInput1.Top = t;
			tsInput1.Left = SP;
			tsInput1.Width = tsGrid1.tsd.FrameWidth;
			tsInput1.Height = tsGrid1.tsd.CaptionHeight;

			tsCellCaption1.Top = tsInput1.Top;
			tsCellCaption1.Left = tsInput1.Left + tsInput1.Width + SP;
			tsCellCaption1.Width = tsGrid1.tsd.widthMax;
			tsCellCaption1.Height = tsInput1.Height;

			tsGrid1.Top = tsInput1.Top + tsInput1.Height + SP;
			tsGrid1.Left = tsCellCaption1.Left;
			tsGrid1.Width = tsCellCaption1.Width;
			tsGrid1.Height = h - (tsInput1.Height);



			tsFrame1.Left = tsInput1.Left;
			tsFrame1.Top = tsGrid1.Top;
			tsFrame1.Width = tsInput1.Width;
			tsFrame1.Height = tsGrid1.Height;

            tsNav1.Top = tsGrid1.Top;
			tsNav1.Left = tsGrid1.Left + tsGrid1.Width + SP/*tsMemo1.Left + tsMemo1.Width*/ + SP;
			tsNav1.Height = tsGrid1.Height;

			tsGrid1.GetStatus();
			m_LayoutFlag = bak;
			tsGrid1.Focus();
			this.ResumeLayout();
		}
		//--------------------------------------------------------------------------------------
		private void m_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int offsetY = tsGrid1.OffsetY;
			int ch = tsGrid1.tsd.CellHeight;
			int oY = offsetY / ch;
			if ((offsetY % ch) > 0)
				oY++;
			int y = oY * ch;
			y -= (e.Delta / 120) * ch;
			if (offsetY != y)
				tsGrid1.OffsetY = y;
		}
		//--------------------------------------------------------------------------------------
		private void MainForm_Resize(object sender, EventArgs e)
		{
			SetLayout();
			this.Invalidate();
		}
        //--------------------------------------------------------------------------------------
        private void MainForm_Enter(object sender, EventArgs e)
		{
			tsGrid1.Focus();
		}
		//**********************************************************************************
		//Fileメニュー
		//**********************************************************************************
		private void fileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FileSave.Enabled = tsGrid1.SaveFlag;
		}
		//**********************************************************************************
		//Editメニュー
		//**********************************************************************************
		private void EditMenu_Click(object sender, EventArgs e)
		{
			EditPasteMenu.Enabled = tsGrid1.ClipEnabled;
		}
		//**********************************************************************************
		//ページ
		//**********************************************************************************
		private void FrameDisp_Frame_Click(object sender, EventArgs e)
		{
			SetFrameDisp(TSFrameDisp.frame);
			this.Refresh();
		}
		//----------------------------------------------------------------------------------
		private void FrameDisp_PageFrame_Click(object sender, EventArgs e)
		{
			SetFrameDisp(TSFrameDisp.pageFrame);
			this.Refresh();
		}
		//----------------------------------------------------------------------------------
		private void FrameDisp_PageSecFrame_Click(object sender, EventArgs e)
		{
			SetFrameDisp(TSFrameDisp.paseSecFrame);
			this.Refresh();
		}
		//----------------------------------------------------------------------------------
		private void FrameDisp_SecFrame_Click(object sender, EventArgs e)
		{
			SetFrameDisp(TSFrameDisp.SecFrame);
			this.Refresh();
		}
		//**********************************************************************************
		//ショートカット処理
		//**********************************************************************************
		private void setMenuItem(ToolStripMenuItem menu, funcCmd cmd)
		{
			try
			{
				menu.ShortcutKeys = tsGrid1.funcs.getKeyTable(cmd);
			}
			catch
			{
				menu.ShortcutKeys = Keys.None;
			}
			menu.Text = tsGrid1.funcs.funcName[(int)cmd, 1];
			menu.Tag = (int)cmd;
		}
		public void ShortCutPre()
		{
			if (tsGrid1.funcs != null)
			{
				//File
				setMenuItem(FileNew, funcCmd.New);
				setMenuItem(FileOpen, funcCmd.Open);
				setMenuItem(FileSave, funcCmd.Save);
				setMenuItem(FileSaveAs, funcCmd.SaveAs);
                setMenuItem(ExportArdjMenu, funcCmd.ExportArdj);
                setMenuItem(FilePrintPreview, funcCmd.PrintPreview);
                setMenuItem(FilePrint, funcCmd.Print);
                setMenuItem(FilePageSetup, funcCmd.PageSetup);
                setMenuItem(FileQuit, funcCmd.Quit);

 
				//Edit
				setMenuItem(EditCopyMenu, funcCmd.Copy);
				setMenuItem(EditCutMenu, funcCmd.Cut);
				setMenuItem(EditPasteMenu, funcCmd.Paste);
				setMenuItem(ColorSettingDlg, funcCmd.ColorSetting);
				setMenuItem(LayoutSettingDlg, funcCmd.LayoutSetting);
				setMenuItem(KeySettingDlg, funcCmd.KeySetting);
				setMenuItem(PrintSettings, funcCmd.PrintSetting);

				//Layer
                setMenuItem(layerInsert, funcCmd.LayerInsert);
				setMenuItem(layerRemove, funcCmd.LayerRemove);
				setMenuItem(LayerRename, funcCmd.LayerRename);
				//Frame
				setMenuItem(frameDelete, funcCmd.FrameDelete);
				setMenuItem(frameInsert, funcCmd.FrameInsert);
				setMenuItem(autoInput, funcCmd.AutoInput);
                setMenuItem(valueEdit, funcCmd.ValueEdit);

                setMenuItem(HelpAbout, funcCmd.About);




            }
        }
		//----------------------------------------------------------------------------------
		private void menu_Click(object sender, EventArgs e)
		{
			tsGrid1.funcs.exec( (funcCmd)((ToolStripMenuItem)sender).Tag);
		}
        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------
		public void SetFrameDisp(TSFrameDisp m)
		{
			cmFrameDisp_Frame.Checked = false;
			cmFrameDisp_PageFrame.Checked = false;
			cmFrameDisp_PageSecFrame.Checked = false;
			cmFrameDisp_SecFrame.Checked = false;
			
			tsGrid1.tsd.FrameDisp = m;
			switch (m)
			{
				case TSFrameDisp.frame:
					cmFrameDisp_Frame.Checked = true;
					break;
				case TSFrameDisp.pageFrame:
					cmFrameDisp_PageFrame.Checked = true;
					break;
				case TSFrameDisp.paseSecFrame:
					cmFrameDisp_PageSecFrame.Checked = true;
					break;
				case TSFrameDisp.SecFrame:
					cmFrameDisp_SecFrame.Checked = true;
					break;
			}
		}

		//----------------------------------------------------------------------------------
		private void frameEnabledONToolStripMenuItem_Click(object sender, EventArgs e)
		{
			tsGrid1.SetFrameEnabledON();
		}
		//----------------------------------------------------------------------------------
		private void frameEnabledOFFToolStripMenuItem_Click(object sender, EventArgs e)
		{
			tsGrid1.SetFrameEnabledOFF();
		}

		//**********************************************************************************
 		//
		//**********************************************************************************

		private void tsGrid1_SelectionChanged(object sender, EventArgs e)
		{
			toolStripStatusLabel1.Text = tsGrid1.SelInfo;
		}
        //----------------------------------------------------------------------------------
        private void tsGrid1_KeyBindChanged(object sender, EventArgs e)
		{
			ShortCutPre();
		}
		//----------------------------------------------------------------------------------
		private void tsFrame1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int v = (int)tsGrid1.tsd.FrameDisp;
			v += 1;
			if ((int)TSFrameDisp.Count <= v) { v = 0; }
			SetFrameDisp((TSFrameDisp)v);
			tsFrame1.Refresh();
			//MessageBox.Show("a");

		}
        //----------------------------------------------------------------------------------
		private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			tsGrid1.KeyExec(e.KeyData);
		}
        //----------------------------------------------------------------------------------
		private void tsGrid1_FileLoaded(object sender, EventArgs e)
		{
			this.Text = Path.GetFileName( tsGrid1.FileName);
		}
        //----------------------------------------------------------------------------------
        private void PreviewDlg_Click(object sender, EventArgs e)
        {
            if (pvf == null)
            {
                pvf = new PictureViewForm(this);
                pvf.Show();
                PreviewDlg.Checked = true;
               // pvf.DirectInput = tsGrid1.DirectInput;
            }
            else
            {
				pvf.Visible = !pvf.Visible;
				if (pvf.Visible == true)
				{
					pvf.Focus();
					PreviewDlg.Checked = true;
				}
				else
				{
					PreviewDlg.Checked = false;
				}
            }

        }

        //----------------------------------------------------------------------------------
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
                e.Effect = DragDropEffects.Copy;
            else
                //ファイル以外は受け付けない
                e.Effect = DragDropEffects.None;

        }
        //----------------------------------------------------------------------------------
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames =(string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (fileNames.Length > 0)
            {
                for (int i = 0; i < fileNames.Length; i++)
                {
                    if (File.Exists(fileNames[i]) == true)
                    {
                        tsGrid1.LoadFromFile(fileNames[i]);
                        break;
                    }
                }
            }
 
        }
		//----------------------------------------------------------------------------------
		public void KeyExec(Keys k)
		{
			tsGrid1.KeyExec(k);
		}
        public void Exec(funcCmd cmd)
        {
            tsGrid1.funcs.exec(cmd);
        }

        //----------------------------------------------------------------------------------


            /*
        private void DirectInput_Click(object sender, EventArgs e)
        {
            tsGrid1.DirectInput = !tsGrid1.DirectInput;
            if (pvf != null)
            {
                pvf.DirectInput = tsGrid1.DirectInput;
            }
        }
        */
      /*
        //----------------------------------------------------------------------------------
        private void tsInput1_DoubleClick(object sender, EventArgs e)
        {
            tsGrid1.DirectInput = !tsGrid1.DirectInput;
            if (pvf != null)
            {
                pvf.DirectInput = tsGrid1.DirectInput;
            }
        }
        */
        //----------------------------------------------------------------------------------
        private void WindowMenu_Click(object sender, EventArgs e)
		{
			if (pvf != null)
			{
				PreviewDlg.Checked = pvf.Visible;
			}
		}
        //----------------------------------------------------------------------------------
		
		public TSData TSData
		{
			get { return tsGrid1.tsd; }
			//set { tsGrid1.tsd = value; }
		}


    }
}