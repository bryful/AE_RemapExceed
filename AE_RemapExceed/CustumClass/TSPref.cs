using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public class TSPref
	{
		//------------------------
		private const string UserFolderName = "Prefs";
		private const string m_PrefFileName = "AE_Remap.pref";
		private const string m_KeyBindFileName = "AE_Remap.keys";
		private const string m_ColorsFileName = "AE_Remap.cols";

		public int Left;
		public int Top;
		public int Height;
		public int CellCount;
		public int FrameCount;
		public string m_UserPath = "";

		public TSGrid tsg;
		//----------------------------------------------------------------------
		public TSPref(TSGrid t)
		{
			tsg = t;
			ChkUserFolder();
		}
		//----------------------------------------------------------------------
		public string ColorFileName
		{
			get
			{
				if (m_UserPath == "") { return ""; }
				else
				{
					return Path.Combine(m_UserPath, m_ColorsFileName);
				}
			}
		}
		//----------------------------------------------------------------------
		public string KeyBindFileName
		{
			get
			{
				if (m_UserPath == "") { return ""; }
				else
				{
					return Path.Combine(m_UserPath, m_KeyBindFileName);
				}
			}
		}
		//----------------------------------------------------------------------
		public string PrefFileName
		{
			get
			{
				if (m_UserPath == "") { return ""; }
				else
				{
					return Path.Combine(m_UserPath, m_PrefFileName);
				}
			}
		}
		//----------------------------------------------------------------------
		public void ChkUserFolder( )
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
					return;
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
				}
			}
		}
		//----------------------------------------------------------------------
		public void ToData(List<ard_prms> lines)
		{
			if (lines.Count <= 0) { return; }
			if (tsg == null) { return; }
			TSParams p = new TSParams();

			for (int i = 0; i < lines.Count; i++)
			{
				if (lines[i].CompareTag(TSParams.N.Left))
				{
					Left = lines[i].GetValueInt(60);
				}
				else if (lines[i].CompareTag(TSParams.N.Top))
				{
					Top = lines[i].GetValueInt(60);
				}
				else if (lines[i].CompareTag(TSParams.N.Height))
				{
					Height = lines[i].GetValueInt(300);
				}
				else if (lines[i].CompareTag(TSParams.N.CellCount))
				{
					CellCount = lines[i].GetValueInt(TSdef.CellCount);
				}
				else if (lines[i].CompareTag(TSParams.N.FrameCount))
				{
					FrameCount = lines[i].GetValueInt(TSdef.FrameCount);
				}
				else if (lines[i].CompareTag(TSParams.N.CellWidth))
				{
					tsg.tsd.CellWidth = lines[i].GetValueInt(TSdef.CellWidth);
				}
				else if (lines[i].CompareTag(TSParams.N.CellHeight))
				{
					tsg.tsd.CellHeight = lines[i].GetValueInt(TSdef.CellHeight);
				}
				else if (lines[i].CompareTag(TSParams.N.FrameWidth))
				{
					tsg.tsd.FrameWidth = lines[i].GetValueInt(TSdef.FrameWidth);
				}
				else if (lines[i].CompareTag(TSParams.N.MemoWidth))
				{
					tsg.tsd.MemoWidth = lines[i].GetValueInt(TSdef.MemoWidth);
				}
				else if (lines[i].CompareTag(TSParams.N.CaptionHeight))
				{
					tsg.tsd.CaptionHeight = lines[i].GetValueInt(TSdef.CaptionHeight);
				}
				else if (lines[i].CompareTag(TSParams.N.FrameOffset))
				{
					tsg.tsd.FrameOffset = lines[i].GetValueInt(TSdef.FrameOffset);
				}
				else if (lines[i].CompareTag(TSParams.N.ZeroStart))
				{
					tsg.tsd.ZeroStart = lines[i].GetValueBool(TSdef.ZeroStart);
				}
				else if (lines[i].CompareTag(TSParams.N.FrameDisp))
				{
					tsg.tsd.FrameDisp = (TSFrameDisp)lines[i].GetValueInt((int)TSdef.FrameDisp);
				}
				else if (lines[i].CompareTag(TSParams.N.SrcWidth))
				{
					tsg.tsd.SrcWidth = lines[i].GetValueInt((int)TSdef.SrcWidth);
				}
				else if (lines[i].CompareTag(TSParams.N.SrcHeight))
				{
					tsg.tsd.SrcHeight = lines[i].GetValueInt((int)TSdef.SrcHeight);
				}
				else if (lines[i].CompareTag(TSParams.N.PageSec))
				{
					tsg.tsd.PageSec = (TSPageSec)(lines[i].GetValueInt((int)TSdef.PageSec));
				}
				else if (lines[i].CompareTag(TSParams.N.FrameRate))
				{
					tsg.tsd.FrameRate = (TSFps)(lines[i].GetValueInt((int)TSdef.FrameRate));
				}
				else if (lines[i].CompareTag(TSParams.N.SrcAspect))
				{
					tsg.tsd.SrcAspect = lines[i].GetValueFloat((int)TSdef.SrcAspect);
				}
				else if (lines[i].CompareTag(TSParams.N.CmpAspect))
				{
					tsg.tsd.CmpAspect = lines[i].GetValueFloat((int)TSdef.CmpAspect);
				}
				else if (lines[i].CompareTag(TSParams.N.EmptyCell))
				{
					tsg.tsd.EmptyCell = (EmptyCell)lines[i].GetValueInt((int)TSdef.EmptyCellDef);
				}
				else if (lines[i].CompareTag(TSParams.N.remaping))
				{
					tsg.tsd.remaping = lines[i].GetValueBool(TSdef.remaping);
				}
				else if (lines[i].CompareTag(TSParams.N.AutoInputStart))
				{
					tsg.tsd.AutoInputStart = lines[i].GetValueInt(TSdef.AutoInputStart);
					if (tsg.tsd.AutoInputStart < 0) tsg.tsd.AutoInputStart = 0;
				}
				else if (lines[i].CompareTag(TSParams.N.AutoInputLast))
				{
					tsg.tsd.AutoInputLast = lines[i].GetValueInt(TSdef.AutoInputLast);
					if (tsg.tsd.AutoInputLast < 0) tsg.tsd.AutoInputLast = 0;
				}
				else if (lines[i].CompareTag(TSParams.N.AutoInputKoma))
				{
					tsg.tsd.AutoInputKoma = lines[i].GetValueInt(TSdef.AutoInputKoma);
					if (tsg.tsd.AutoInputKoma < 1) tsg.tsd.AutoInputKoma = 1;
				}
				else if (lines[i].CompareTag(TSParams.N.SecInputMode))
				{
					tsg.tsd.SecInputMode = lines[i].GetValueBool(TSdef.SecInputMode);
				}
				else if (lines[i].CompareTag(TSParams.N.LastFrame))
				{
					tsg.tsd.LastFrame = lines[i].GetValueInt(TSdef.LastFrame);
				}
				else if (lines[i].CompareTag(TSParams.N.IsPrintTITLE))
				{
					tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.TITLE] = lines[i].GetValueBool(true);
				}
				else if (lines[i].CompareTag(TSParams.N.IsPrintSUB_TITLE))
				{
					tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.SUB_TITLE] = lines[i].GetValueBool(true);
				}
				else if (lines[i].CompareTag(TSParams.N.IsPrintOPUS))
				{
					tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.OPUS] = lines[i].GetValueBool(true);
				}
				else if (lines[i].CompareTag(TSParams.N.IsPrintSCECNE))
				{
					tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.SCECNE] = lines[i].GetValueBool(true);
				}
				else if (lines[i].CompareTag(TSParams.N.IsPrintCUT))
				{
					tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.CUT] = lines[i].GetValueBool(true);
				}
				else if (lines[i].CompareTag(TSParams.N.IsPrintCREATE_USER))
				{
					tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.CREATE_USER] = lines[i].GetValueBool(false);
				}
				else if (lines[i].CompareTag(TSParams.N.IsPrintUPDATE_USER))
				{
					tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.UPDATE_USER] = lines[i].GetValueBool(false);
				}
				else if (lines[i].CompareTag(TSParams.N.IsPrintCAMPANY_NAME))
				{
					tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.CAMPANY_NAME] = lines[i].GetValueBool(true);
				}
				else if (lines[i].CompareTag(TSParams.N.IsPrintComment))
				{
					tsg.tsd.IsPrintComment = lines[i].GetValueBool(true);
				}
                else if (lines[i].CompareTag(TSParams.N.IsPrintMemo))
                {
                    tsg.tsd.IsPrintMemo = lines[i].GetValueBool(true);
                }
                else if (lines[i].CompareTag(TSParams.N.CommentAlign))
                {
                    tsg.tsd.CommentAlign = (CmtAligns)lines[i].GetValueInt((int)CmtAligns.LeftTop);
                }
            }

		}
		//----------------------------------------------------------------------
		public bool LoadFromFile(string path)
		{
			if ((tsg == null) || (path == "")) { return false; }
			if (File.Exists(path) == false) { return false; }

			List<ard_prms> lines = new List<ard_prms>();

			string[] rd = System.IO.File.ReadAllLines(path, Encoding.GetEncoding("utf-8"));
			if (rd.Length <= 0) return false;
			for (int i = 0; i < rd.Length; i++)
			{
				if (rd[i] == string.Empty) { continue; }
				string[] sa = rd[i].Split('=');
				if (sa.Length >= 2)
				{
					lines.Add(new ard_prms(sa[0].Trim(), sa[1].Trim()));
				}
			}
			if (lines.Count <= 0) { return false; }
			ToData(lines);
			return true;

		}
		//----------------------------------------------------------------------
		public bool SaveToFile(string path)
		{
			if ((tsg == null)||(path == "")) { return false; }
			string s = "";
			TSParams p = new TSParams();

			s += p.Tag(TSParams.N.Left) + " = " + Left.ToString() +"\n";
			s += p.Tag(TSParams.N.Top) + " = " + Top.ToString() + "\n";
			s += p.Tag(TSParams.N.Height) + " = " + Height.ToString() + "\n";
			s += p.Tag(TSParams.N.CellCount) + " = " + tsg.tsd.CellCount.ToString() + "\n";
			s += p.Tag(TSParams.N.FrameCount) + " = " + tsg.tsd.FrameCount.ToString() + "\n";
			s += p.Tag(TSParams.N.CellWidth) + " = " + tsg.tsd.CellWidth.ToString() + "\n";
			s += p.Tag(TSParams.N.CellHeight) + " = " + tsg.tsd.CellHeight.ToString() + "\n";
			s += p.Tag(TSParams.N.FrameWidth) + " = " + tsg.tsd.FrameWidth.ToString() + "\n";
			s += p.Tag(TSParams.N.MemoWidth) + " = " + tsg.tsd.MemoWidth.ToString() + "\n";
			s += p.Tag(TSParams.N.CaptionHeight) + " = " + tsg.tsd.CaptionHeight.ToString() + "\n";
			s += p.Tag(TSParams.N.FrameOffset) + " = " + tsg.tsd.FrameOffset.ToString() + "\n";
			s += p.Tag(TSParams.N.ZeroStart) + " = " + tsg.tsd.ZeroStart.ToString() + "\n";
			s += p.Tag(TSParams.N.FrameDisp) + " = " + ((int)tsg.tsd.FrameDisp).ToString() + "\n";
			s += p.Tag(TSParams.N.SrcWidth) + " = " + tsg.tsd.SrcWidth.ToString() + "\n";
			s += p.Tag(TSParams.N.SrcHeight) + " = " + tsg.tsd.SrcHeight.ToString() + "\n";
			s += p.Tag(TSParams.N.PageSec) + " = " + ((int)tsg.tsd.PageSec).ToString() + "\n";
			s += p.Tag(TSParams.N.FrameRate) + " = " + ((int)tsg.tsd.FrameRate).ToString() + "\n";
			s += p.Tag(TSParams.N.SrcAspect) + " = " + tsg.tsd.SrcAspect.ToString() + "\n";
			s += p.Tag(TSParams.N.CmpAspect) + " = " + tsg.tsd.CmpAspect.ToString() + "\n";
			s += p.Tag(TSParams.N.AE_Vaersion) + " = " + tsg.tsd.AE_Vaersion.ToString() + "\n";
			s += p.Tag(TSParams.N.EmptyCell) + " = " + ((int)tsg.tsd.EmptyCell).ToString() + "\n";
			s += p.Tag(TSParams.N.remaping) + " = " + tsg.tsd.remaping.ToString() + "\n";
			s += p.Tag(TSParams.N.AutoInputStart) + " = " + tsg.tsd.AutoInputStart.ToString() + "\n";
			s += p.Tag(TSParams.N.AutoInputLast) + " = " + tsg.tsd.AutoInputLast.ToString() + "\n";
			s += p.Tag(TSParams.N.AutoInputKoma) + " = " + tsg.tsd.AutoInputKoma.ToString() + "\n";
			s += p.Tag(TSParams.N.SecInputMode) + " = " + tsg.tsd.SecInputMode.ToString() + "\n";
			s += p.Tag(TSParams.N.LastFrame) + " = " + tsg.tsd.LastFrame.ToString() + "\n";
			s += p.Tag(TSParams.N.IsPrintTITLE) + " = " + tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.TITLE].ToString() + "\n";
			s += p.Tag(TSParams.N.IsPrintSUB_TITLE) + " = " + tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.SUB_TITLE].ToString() + "\n";
			s += p.Tag(TSParams.N.IsPrintOPUS) + " = " + tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.OPUS].ToString() + "\n";
			s += p.Tag(TSParams.N.IsPrintSCECNE) + " = " + tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.SCECNE].ToString() + "\n";
			s += p.Tag(TSParams.N.IsPrintCUT) + " = " + tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.CUT].ToString() + "\n";
			s += p.Tag(TSParams.N.IsPrintCREATE_USER) + " = " + tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.CREATE_USER].ToString() + "\n";
			s += p.Tag(TSParams.N.IsPrintUPDATE_USER) + " = " + tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.UPDATE_USER].ToString() + "\n";
			s += p.Tag(TSParams.N.IsPrintCAMPANY_NAME) + " = " + tsg.tsd.IsPrintSheetInfo[(int)SheetInfo.CAMPANY_NAME].ToString() + "\n";
			s += p.Tag(TSParams.N.IsPrintComment) + " = " + tsg.tsd.IsPrintComment.ToString() + "\n";
            s += p.Tag(TSParams.N.IsPrintMemo) + " = " + tsg.tsd.IsPrintMemo.ToString() + "\n";
            s += p.Tag(TSParams.N.CommentAlign) + " = " + ((int)tsg.tsd.CommentAlign).ToString() + "\n";
		
			try
			{
				File.WriteAllText(path, s, Encoding.GetEncoding("utf-8"));
				return true;
			}
			catch
			{
				return false;
			}

		}
		//----------------------------------------------------------------------
		public void PrefSave( )
		{
			if (tsg == null)
				return;
			SaveToFile(PrefFileName);
			tsg.funcs.SaveToFile(KeyBindFileName);
			tsg.cols.save(ColorFileName);
		}
		//----------------------------------------------------------------------
		public bool PrefLoad( )
		{
			if (tsg == null) { return false; }
			tsg.cols.load(ColorFileName);
			tsg.funcs.LoadFromFile(KeyBindFileName);
			return LoadFromFile(PrefFileName);
		}
		//----------------------------------------------------------------------
	}
}
