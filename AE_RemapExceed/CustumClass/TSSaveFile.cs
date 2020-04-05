using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace AE_RemapExceed
{
	public class TSSaveFile
	{
		//ardに使う文字定数
		public const string D_Header = "#TimeSheetGrid SheetData";
		public const string D_Comment = "*Comment";
		public const string D_Param = "*ParamStart";
		public const string D_FrameEnabled = "*FrameEnabled";
		public const string D_CellName = "*CellName";
		public const string D_Memo = "*Memo";
		public const string D_CellDataStart = "*CellDataStart";
		public const string D_Cell = "*Cell";
		public const string D_CellEnd = "*CellEnd";
		public const string D_End = "*End";
		public const string D_LayerCount = "LayerCount";
		public const string D_FrameCount = "FrameCount";
		public const string D_PageSec = "PageSec";
		public const string D_FrameRate = "CmpFps";

		public const string D_CREATE_USER = "CREATE_USER";
		public const string D_UPDATE_USER = "UPDATE_USER";
		public const string D_CREATE_TIME = "CREATE_TIME";
		public const string D_UPDATE_TIME = "UPDATE_TIME";
		public const string D_TITLE = "TITLE";
		public const string D_SUB_TITLE = "SUB_TITLE";
		public const string D_OPUS = "OPUS";
		public const string D_SCECNE = "SCECNE";
		public const string D_CUT = "CUT";
        public const string D_CAMPANY_NAME = "CAMPANY_NAME";


		public const string ARDExt = ".ard";
		private const string TAB = "\t";

		//--------------------------------
		public class Lines
		{
			public int start;
			public int end;
			public Lines( )
			{
				start = 0;
				end = 0;
			}
			public Lines(int s, int l)
			{
				start = s;
				end = l;
			}
		}
	
		//データクラス
		private TSData data = null;
		//*********
		//--------------------------------------------------------------------------
		public TSSaveFile(TSData d)
		{
			data = d;
		}
		//****************************************************************************
		//****************************************************************************
		//--------------------------------------------------------------------------
		public bool LoadFromFile(string path)
		{
			//ファイルがなければエラー
			if (System.IO.File.Exists(path) == false)
			{
				return false;
			}
			System.IO.StreamReader sr = new System.IO.StreamReader(path);
			try
			{
				List<string> rd = new List<string>();

				while (sr.Peek() >= 0)
				{
					rd.Add(sr.ReadLine());
				}
                if (GetSaveData(rd))
                {
                    data.SheetName = Path.GetFileNameWithoutExtension(path);
                    data.FileName = path;
                    return true;
                }
                else
                {
                    return false;
                }
            }
			finally
			{
				sr.Close();
			}
		}
		//****************************************************************************
		//****************************************************************************
		//-------------------------------------------------------------------
		public bool SaveToFile(string path, bool IsName = true)
		{

			if (Directory.Exists(Path.GetDirectoryName(path)) == false)
			{
				return false;
			}
			System.IO.StreamWriter sw = new System.IO.StreamWriter(path);
			try
			{
				List<string> sdata = SetSaveData();
				if (sdata.Count > 2)
				{
					foreach (string s in sdata)
					{
						sw.WriteLine(s);
					}
					if (IsName)
					{
						data.SheetName = Path.GetFileNameWithoutExtension(path);
						data.FileName = path;
					}
                    return true;
				}
				else
				{
					return  false;
				}
			}
			finally
			{
				sw.Close();
			}
		}		//-------------------------------------------------------------------
		public void SaveToClipboard()
		{

			List<string> data = SetSaveData();
			string s = "";
			for (int i = 0; i < data.Count; i++)
			{
				s += data[i] + "\r";
			}
			Clipboard.SetText(s);

		}
		//******************************************************************************
		/*
		 * 文字列配列の操作
		 */
		//******************************************************************************
		//----------------------------------------------------------
		/*
		 * D_End行をさがす。無ければ行末を返す。
		 */
		public int FindEnd(List<string> data)
		{
			if (data.Count <= 0)
				return -1;
			int v = data.Count - 1;

			string end = D_End.ToLower();
			for (int i = 0; i < data.Count; i++)
			{
				string s = data[i].Trim().ToLower();
				if (s == end)
				{
					v = i;
					break;
				}
			}
			return v;
		}
		//----------------------------------------------------------
		/*
		 * 指定されたTagの範囲を返す。
		 * 開始行が見つからなかったら、-1,-1を返す。
		 * 終了行が見つからなかったら行末を返す。
		 */ 
		private Lines FindBlock(List<string> data, string tag)
		{
			int endIdx = FindEnd(data);
			if (endIdx <= 1)
			{
				return new Lines(-1, -1);
			}
			string nTag = tag.ToLower();
			string cellDataStart = D_CellDataStart.ToLower();
			//
			int start = -1;

			//スタート位置を探す
			for (int i = 1; i < endIdx; i++)
			{
				string s = data[i].Trim().ToLower();
				if (s == "")
				{
				}
				else if (s == nTag)
				{
					start = i + 1; //次の行から
					break;
				}
			}
			//無い
			if (start < 0)
			{
				return new Lines(-1, -1);
			}
			//ラスト位置を探す。見つからなかったら行末とする
			int last = endIdx - 1;


			//cellDataだけは特別扱い
			if (nTag != cellDataStart)
			{
				for (int i = start; i < endIdx; i++)
				{
					string s = data[i].Trim().ToLower();
					if (s == "")
					{
					}
					else if (s[0] == '*') // 他のコマンドが着たら終わる
					{
						last = i - 1;
						break;
					}
				}
			}

			return new Lines(start, last);
		}
		//----------------------------------------------------------
		private int FindArd_prms(List<Ard_prms> prms, string tag)
		{
			int ret = -1;
			if ((prms.Count <= 0) || (tag == "")) { return ret; }
			for (int i = 0; i < prms.Count; i++)
			{
				if (prms[i].CompareTag(tag))
				{
					ret = i;
					break;
				}
			}
			return ret;
		}
		//******************************************************************************
		/*
		 * 文字列リストからTSDataを設定
		 */
		//******************************************************************************
		public bool GetSaveData(List<string> rd)
		{
			if (data == null) { return false; }
			if (rd.Count <= 1) { return false; }
			if (rd[0] != D_Header) { return false; }
            
            data.ClearDellData();
			
            int endIdx = FindEnd(rd);
			//---------
			//コメントブロックを読み取る
			List<string> cmt = new List<string>();
			Lines ln = FindBlock(rd, D_Comment);
			if ((ln.start >= 2) && (ln.start <= ln.end))
			{
				for (int i = ln.start; i <= ln.end; i++)
				{
					cmt.Add(rd[i].Trim());
				}
				if (cmt.Count >= 2)
				{
					for (int i = cmt.Count - 1; i >= 1; i--)
					{
						if ((cmt[i] == "") && (cmt[i - 1] == ""))
						{
							cmt.RemoveAt(i);
						}
					}
				}
			}
			//コメントは無くてもOK
			//---------
			//パラメータブロックを読み取る
			ln = FindBlock(rd, D_Param);
			List<Ard_prms> prms = new List<Ard_prms>();
			if ((ln.start >= 2) && (ln.start <= ln.end))
			{
				for (int i = ln.start; i <= ln.end; i++)
				{
					string s = rd[i].Trim();
					string[] sa = s.Split('\t');
					if (sa.Length >= 2)
					{
						if ((sa[0] != "") && (sa[1] != ""))
						{
							prms.Add(new Ard_prms(sa[0], sa[1]));
						}
					}
				}
			}
			else
			{
				//無かったらエラー
				return false;
			}

			//FrameCount/CellCountは先に確保
			int idx = FindArd_prms(prms, D_LayerCount);
			if (idx < 0)
			{
				return false;
			}
			int cellCount = prms[idx].GetValueInt(0);
			if (cellCount <= 0)
			{
				return false;
			}
			idx = FindArd_prms(prms, D_FrameCount);
			if (idx < 0)
			{
				return false;
			}
			int frameCount = prms[idx].GetValueInt(0);
			if (frameCount <= 0)
			{
				return false;
			}
			//---------
			//CellCaption
			ln = FindBlock(rd, D_CellName);
			string[] cellName = new string[cellCount];
			if ((ln.start >= 2) && (ln.start <= ln.end))
			{
				for (int i = ln.start; i <= ln.end; i++)
				{
					string s = rd[i].Trim();
					string[] sa = s.Split('\t');
					if (sa.Length >= 2)
					{
						int frm;
						if (Int32.TryParse(sa[0], out frm))
						{
							cellName[frm] = sa[1];
						}
					}
				}
			}
			//---------
			//Memo
			ln = FindBlock(rd, D_Memo);
			string[] mm = new string[frameCount];
			if ((ln.start >= 2) && (ln.start <= ln.end))
			{
				for (int i = ln.start; i <= ln.end; i++)
				{
					string s = rd[i].Trim();
					string[] sa = s.Split('\t');
					if (sa.Length >= 2)
					{
						int frm;
						if (Int32.TryParse(sa[0], out frm))
						{
							if (frm >= 1)
							{
								mm[frm - 1] = sa[1];
							}
						}
					}
				}
			}

			//---------
			//FrameEnabled;
			ln = FindBlock(rd, D_FrameEnabled);
			int[] frameEnabled = new int[frameCount];

			if ((ln.start >= 2) && (ln.start <= ln.end))
			{
				for (int i = 0; i < frameCount; i++) { frameEnabled[i] = -100; }
				for (int i = ln.start; i <= ln.end; i++)
				{
					string s = rd[i].Trim();
					string[] sa = s.Split('\t');
					if (sa.Length >= 2)
					{
						int frm;
						if (Int32.TryParse(sa[0], out frm))
						{
							if (frm >= 1)
							{
								int v;
								if (Int32.TryParse(sa[1], out v))
								{
									if (v == 0) { v = 0; } else { v = -1; }
									frameEnabled[frm - 1] = v;
								}
							}
						}
					}
				}
				//フレームを増やす
				if (frameEnabled[0] == -100)
					frameEnabled[0] = 0;
				for (int i = 1; i < frameCount; i++)
				{
					if (frameEnabled[i] == -100)
						frameEnabled[i] = frameEnabled[i - 1];
				}

			}
			//---------
			ln = FindBlock(rd, D_CellDataStart);
			int[][] cd = new int[cellCount][];
			for (int i = 0; i < cellCount; i++)
			{
				cd[i] = new int[frameCount];
				for (int j = 0; j < frameCount; j++)
				{
					cd[i][j] = -1;
				}
			}
			if ((ln.start >= 2) && (ln.start <= ln.end))
			{
				int targetCell = 0;

				for (int i = ln.start; i <= ln.end; i++)
				{
					string s = rd[i].Trim();
					string[] sa = s.Split('\t');
					if (sa.Length >= 2)
					{
						if (String.Compare(sa[0], D_Cell) == 0)
						{
							int v;
							if (Int32.TryParse(sa[1], out v))
							{
								if (v >= 0) { targetCell = v; }
							}
						}
						else if (String.Compare(sa[0], D_CellEnd, true) == 0)
						{
							targetCell = -1;
						}
						else
						{
							if ((targetCell >= 0) && (targetCell < cellCount))
							{
								int frm;
								if (Int32.TryParse(sa[0], out frm))
								{
									frm--;//AE_Remapは1スタート
									if ((frm >= 0) && (frm < frameCount))
									{
										int v;
										if (Int32.TryParse(sa[1], out v))
										{
											if (v < 0) { v = 0; }
											cd[targetCell][frm] = v;
										}
									}
								}
							}
						}
					}
				}
				for (int i = 0; i < cellCount; i++)
				{
					if (cd[i][0] == -1) { cd[i][0] = 0; }
					for (int j = 1; j < frameCount; j++)
					{
						if (cd[i][j] == -1) { cd[i][j] = cd[i][j - 1]; }
					}
				}

			}
			//------------------------
			//反映させる
			data.Comment.Clear();
			foreach (string s in cmt)
			{
				data.Comment.Add(s);
			}
			data.Params.Clear();
			foreach (Ard_prms s in prms)
			{
				data.Params.Add(s);
			}
			data.FromParams();

			data.SetCellCaption(cellName);
			data.setFrameEnabled(frameEnabled);
			data.SetCellData(cd);
			return true;
		}
		//******************************************************************************
		/*
		 * TSDataをから文字列リストを作成
		 */
		//******************************************************************************
		public string FrameStr(int f)
		{
			return f.ToString("#####");
		}
		//----------------------------------------------------------
		public List<string> SetSaveData( )
		{
			List<string> lines = new List<string>();
			if (data == null) { return lines; }
			//ヘッダー
			lines.Add(D_Header);
			lines.Add("");
			//コメント
			lines.Add(D_Comment);
			if (data.Comment.Count > 0)
			{
				foreach (string s in data.Comment)
				{
					lines.Add(s);
				}
			}
			lines.Add("");
			//パラメータブロック
			data.ChkTimes();
			lines.Add(D_Param);
			data.ToParams();
			foreach (Ard_prms p in data.Params)
			{
				lines.Add(p.Tag + TAB + p.Value);
			}
			lines.Add("");
			//CellName
			lines.Add(D_CellName);
			for (int i = 0; i < data.CellCount; i++)
			{
				string s = i.ToString() + TAB + data.CellCaption(i);
				lines.Add(s);
			}
			lines.Add("");
			//FrameEnabled
			lines.Add(D_FrameEnabled);
			int[] fe = data.getFrameEnabled();
			for (int i = 0; i < fe.Length; i++)
			{
				if (fe[i] < 0) { fe[i] = 1; } else { fe[i] = 0;}
			}

			lines.Add(FrameStr(1) + TAB + fe[0].ToString());
			for (int i = 1; i < fe.Length; i++)
			{
				if (fe[i - 1] != fe[i])
				{
					lines.Add(FrameStr(i+1) + TAB + fe[i].ToString());
				}
			}
			lines.Add("");
			//cellData
			lines.Add(D_CellDataStart);
			for (int i = 0; i < data.CellCount; i++)
			{
				if (data.IsCellData(i))
				{

					lines.Add(D_Cell + TAB + i.ToString());
					int c = data.GetCellData(i, 0);
					lines.Add(FrameStr(1) + TAB + c.ToString());
					int bef = c;
					for (int frm = 1; frm < data.FrameCount; frm++)
					{
						c = data.GetCellData(i, frm);
						if (c != bef)
						{
							lines.Add(FrameStr(frm + 1) + TAB + c.ToString());
						}
						bef = c;
					}
					lines.Add(D_CellEnd + TAB + i.ToString());
				}
			}
			lines.Add(D_End);
			return lines;
		}
		//----------------------------------------------------------

	}
}
/*
#TimeSheetGrid SheetData

*ParamStart
LayerCount	6
FrameCount	108
SrcWidth	720
SrcHeight	540
PageFrame	180
CmpFps	24
SrcAspect	1
CmpAspect	1
EmptyCellDef	0
AE_Version	65
remaping	TRUE

*CellName
0	A

*CellDataStart
*Cell	0
0001	1
0073	0
*CellEnd	0


*End

*/