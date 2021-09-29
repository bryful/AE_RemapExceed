using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Codeplex.Data;
    
namespace AE_RemapExceed
{
	

 
    //**************************************************************
    //タイムシートの基本データのクラス
    public class TSData
	{

		//=====================================================
		// ARDファイル用データ
		//=====================================================
		//コメント
		public List<string> Comment = new List<string>();

		//Paramブロック
		public List<Ard_prms> Params = new List<Ard_prms>();

		private int m_CellCount = TSdef.CellCount;
		private int m_FrameCount = TSdef.FrameCount;
		private int m_FrameCountTrue = TSdef.FrameCount;	//これはSaveDataには含まれない
		private TSFps m_FrameRate = TSFps.fps24;
		public TSPageSec PageSec = TSdef.PageSec;

		public DateTime CREATE_TIME = new DateTime(1963, 9, 9);
		public DateTime UPDATE_TIME = new DateTime(1963, 9, 9);

		public string[] SheetInfoTbl = new string[(int)SheetInfo.Count];
		public bool[] IsPrintSheetInfo = new bool[(int)SheetInfo.Count];
        public bool IsPrintComment = true;

        public string SheetName = "";
        public string FileName = "";

        //-------------------------------
        //CellNameブロック
        private string[] cellCaption = new string[TSdef.CellCount];
		//FrameEnabledブロック
		private int[] frameEnabled = new int[TSdef.FrameCount];
		//cellDataブロック
		private int[][] cellData = new int[TSdef.CellCount][];
		//-------------------------------
		public int[] CellLayer(int idx)
		{
			int[] ret = new int[0];

			if ((idx >= 0) && (idx < m_CellCount))
			{
				ret = new int[m_FrameCount];

				for (int i=0; i < m_FrameCount; i++)
				{
					ret[i] = cellData[idx][i];
				}

			}
			return ret;
		}
		//-------------------------------
		public void SetCellLayer(int idx,int [] lyr)
		{
			if ((idx >= 0) && (idx < m_CellCount))
			{
				int cnt = lyr.Length;
				if (cnt > m_FrameCount) cnt = m_FrameCount;
				for (int i = 0; i < cnt; i++)
				{
					cellData[idx][i] =lyr[i];
				}


			}
		}
		//-------------------------------
		public  List<List<int>> CellLayerT(int idx)
		{
			List<List<int>> ret = new List<List<int>>();
			if ((idx >= 0) && (idx < m_CellCount))
			{
				for(int i=0; i<m_FrameCount;i++)
				{
					List<int> a = new List<int>();
					a.Add(i);
					a.Add(cellData[idx][i]);
					ret.Add(a);
				}

				for (int i=m_FrameCount-1; i>=1;i--)
				{
					if (ret[i-1][1]== ret[i][1])
					{
						ret.RemoveAt(i);
					}
				}

			}
			return ret;
		}
		//-------------------------------
		//Undo用
		private int[][] cellDataBak = new int[TSdef.FrameCount][];

		private bool m_undoFlag;


		//Grid表示用の変数

		public int CellWidth = TSdef.CellWidth;
		public int CellHeight = TSdef.CellHeight;
		public int FrameWidth = TSdef.FrameWidth;
		public int CaptionHeight = TSdef.CaptionHeight;
		public int FrameOffset = TSdef.FrameOffset;

        public int[] SizeParams
        {
            get
            {
                return new int[4] { CellWidth, CellHeight, FrameWidth, CaptionHeight };
            }
            set
            {
                if(value.Length>=4)
                {
                    CellWidth = value[0];
                    CellHeight = value[1];
                    FrameWidth = value[2];
                    CaptionHeight = value[3];
                    SetSize(CellWidth, CellHeight);
                }
            }
        }
        public int[] ParamsInt
        {
            get
            {
                return new int[12]
                {
                CellWidth,
                CellHeight,
                FrameWidth,
                CaptionHeight,
                FrameOffset,
                (int)FrameDisp,
                (int)PageSec,
                AutoInputStart,
                AutoInputLast,
                AutoInputKoma,
                LastFrame,
                (int)CommentAlign
                };
            }
            set
            {
                if (value.Length < 12) return;
                CellWidth = value[0];
                CellHeight = value[1];
                FrameWidth = value[2];
                CaptionHeight = value[3];
                FrameOffset = value[4];
                FrameDisp = (TSFrameDisp)value[5];
                PageSec = (TSPageSec)value[6];
                AutoInputStart = value[7];
                AutoInputLast = value[8];
                AutoInputKoma = value[9];
                LastFrame = value[10];
                CommentAlign = (CmtAligns)value[11];
            }

        }
        public bool [] ParamsBool
        {
            get
            {
                return new bool[11]
                {
                    ZeroStart,
                    SecInputMode,
                    IsPrintSheetInfo[0],
                    IsPrintSheetInfo[1],
                    IsPrintSheetInfo[2],
                    IsPrintSheetInfo[3],
                    IsPrintSheetInfo[4],
                    IsPrintSheetInfo[5],
                    IsPrintSheetInfo[6],
                    IsPrintSheetInfo[7],
                    IsPrintComment
               };
            }
            set
            {
                if (value.Length < 11) return;
                ZeroStart = value[0];
                SecInputMode = value[1];
                IsPrintSheetInfo[0] = value[2];
                IsPrintSheetInfo[1] = value[3];
                IsPrintSheetInfo[2] = value[4];
                IsPrintSheetInfo[3] = value[5];
                IsPrintSheetInfo[4] = value[6];
                IsPrintSheetInfo[5] = value[7];
                IsPrintSheetInfo[6] = value[8];
                IsPrintSheetInfo[7] = value[9];
                IsPrintComment = value[10];
            }
        }


		public TSFrameDisp FrameDisp = TSdef.FrameDisp;
		public bool ZeroStart = TSdef.ZeroStart;

		public int AutoInputStart = TSdef.AutoInputStart;
		public int AutoInputLast = TSdef.AutoInputLast;
		public int AutoInputKoma = TSdef.AutoInputKoma;
		public bool SecInputMode = TSdef.SecInputMode;
		public int LastFrame = TSdef.LastFrame;

        public ValueEditMode ValueEditMode = ValueEditMode.direct;
        public int ValueEditValue = 0;

		private int m_HorLine = TSdef.HorLine;

        public CmtAligns CommentAlign = CmtAligns.LeftTop;


		//----------------------------------------------------------
		public TSData( )
		{
			//配列の初期化
			for (int i = 0; i < cellData.Length; i++)
			{
				cellData[i] = new int[TSdef.FrameCount];
				cellDataBak[i] = new int[TSdef.FrameCount];
			}
			for (int i = 0; i < frameEnabled.Length; i++)
			{
				frameEnabled[i] = i;
			}

			ClearDellData();
			
			checkFrameEnabed();
			SetDefaultCaption();

			m_undoFlag = false;

			for (int i = 0; i < (int)SheetInfo.Count; i++)
			{
				IsPrintSheetInfo[i] = true;
			}
            IsPrintSheetInfo[(int)SheetInfo.CREATE_USER] = false;
            IsPrintSheetInfo[(int)SheetInfo.UPDATE_USER] = false;
			

		}
		//----------------------------------------------------------
		public void ClearDellData()
		{
			for (int j = 0; j < this.m_FrameCount; j++)
			{
				for (int i = 0; i < this.m_CellCount; i++)
				{
					this.cellData[i][j] = TSdef.none;
					this.cellDataBak[i][j] = TSdef.none;
				}
				frameEnabled[j] = j; 
			}
			m_undoFlag = false;


            CREATE_USER = "";
		    UPDATE_USER = "";
		    CREATE_TIME = new DateTime(1963, 9, 9);
		    UPDATE_TIME = new DateTime(1963, 9, 9);

		    TITLE = "";
		    SUB_TITLE = "";
		    OPUS = "";
		    SCECNE = "";
		    CUT = "";
            CAMPANY_NAME = "";
            Comment.Clear();

		}
 
		//----------------------------------------------------------
		public void SetDefaultCaption( )
		{
			if (this.cellCaption.Length <= 0)
			{
				return;
			}
			for (int i = 0; i < this.cellCaption.Length; i++)
			{
				string s = this.cellCaption[i];
				if ((s == null) || (s.Trim() == ""))
				{
					this.cellCaption[i] = Char.ConvertFromUtf32('A' + i);
				}
			}
		}
		//----------------------------------------------------------
        public void SetSize(int cell, int frame)
        {
            SetSize(cell, frame, false);
        }
        //----------------------------------------------------------
        public void SetSize(int cell, int frame, bool flg)
		{
			if (cell < 3)
			{
				cell = 3;
			}
			if (frame < 6)
			{
				frame = 6;
			}
			Array.Resize(ref cellData, cell);
			Array.Resize(ref cellDataBak, cell);
			Array.Resize(ref cellCaption, cell);
			SetDefaultCaption();

			
			for (int i = 0; i < cell; i++)
			{
				if (cellData[i] == null) { cellData[i] = new int[frame]; }
				else if (cellData[i].Length != frame) { Array.Resize(ref cellData[i], frame); }
				if (cellDataBak[i] == null) { cellDataBak[i] = new int[frame]; }
				else if (cellDataBak[i].Length != frame) { Array.Resize(ref cellDataBak[i], frame); }


				
			}
			Array.Resize(ref frameEnabled, frame);
			checkFrameEnabed();

            this.m_CellCount = cell;
			this.m_FrameCount = frame;
		}
		//----------------------------------------------------------
		public void checkFrameEnabed( )
		{
			int cnt = 0;
			for (int i = 0; i < frameEnabled.Length; i++)
			{
				if (frameEnabled[i] >= 0)
				{
					frameEnabled[i] = cnt;
					cnt++;
				}
			}
			m_FrameCountTrue = cnt;
		}
		//----------------------------------------------------------
		public void SetFrameRate(TSFps fps)
		{
			this.m_FrameRate = fps;

			if (((int)fps % 5) == 0) { m_HorLine = 5; }
			else if (((int)fps % 6) == 0) { m_HorLine = 6; }
			else { m_HorLine = 100; }
		}
		//----------------------------------------------------------
		public string GetInfo(SheetInfo si)
		{
			return SheetInfoTbl[(int)si];
		}
		//----------------------------------------------------------
		public void SetInfo(SheetInfo si,string s)
		{
			SheetInfoTbl[(int)si] = s.Trim();
		}
		//----------------------------------------------------------
		public bool GetIsPtintInfo(SheetInfo si)
		{
			return IsPrintSheetInfo[(int)si];
		}
		//----------------------------------------------------------
		public void SetIsPtintInfo(SheetInfo si, bool b)
		{
			IsPrintSheetInfo[(int)si] = b;
		}
		//----------------------------------------------------------
		public string TITLE
		{
			get { return SheetInfoTbl[(int)SheetInfo.TITLE]; }
			set { SheetInfoTbl[(int)SheetInfo.TITLE] = value; }
		}
		//----------------------------------------------------------
		public string SUB_TITLE
		{
			get { return SheetInfoTbl[(int)SheetInfo.SUB_TITLE]; }
			set { SheetInfoTbl[(int)SheetInfo.SUB_TITLE] = value; }
		}
		//----------------------------------------------------------
		public string OPUS
		{
			get { return SheetInfoTbl[(int)SheetInfo.OPUS]; }
			set { SheetInfoTbl[(int)SheetInfo.OPUS] = value; }
		}
		//----------------------------------------------------------
		public string SCECNE
		{
			get { return SheetInfoTbl[(int)SheetInfo.SCECNE]; }
			set { SheetInfoTbl[(int)SheetInfo.SCECNE] = value; }
		}
		//----------------------------------------------------------
		public string CUT
		{
			get { return SheetInfoTbl[(int)SheetInfo.CUT]; }
			set { SheetInfoTbl[(int)SheetInfo.CUT] = value; }
		}
		//----------------------------------------------------------
		public string CREATE_USER
		{
			get { return SheetInfoTbl[(int)SheetInfo.CREATE_USER]; }
			set { SheetInfoTbl[(int)SheetInfo.CREATE_USER] = value; }
		}
		//----------------------------------------------------------
		public string UPDATE_USER
		{
			get { return SheetInfoTbl[(int)SheetInfo.UPDATE_USER]; }
			set { SheetInfoTbl[(int)SheetInfo.UPDATE_USER] = value; }
		}
		//----------------------------------------------------------
		public string CAMPANY_NAME
		{
			get { return SheetInfoTbl[(int)SheetInfo.CAMPANY_NAME]; }
			set { SheetInfoTbl[(int)SheetInfo.CAMPANY_NAME] = value; }
		}
		//----------------------------------------------------------
		public int FrameCount
		{
			set { this.SetSize(this.m_CellCount, value); }
			get { return this.m_FrameCount; }
		}
		public int PageFrame
		{
			get { return (int)m_FrameRate * (int)PageSec; }
		}
		public int FrameCountTrue
		{
			get { return this.m_FrameCountTrue; }
		}
		public int CellCount
		{
			set { this.SetSize(value, this.m_FrameCount); }
			get { return this.m_CellCount; }
		}
		public int HorLine
		{
			get { return m_HorLine; }
		}
		public TSFps FrameRate
		{
			set { SetFrameRate(value); }
			get { return this.m_FrameRate; }
		}
		public bool undoFlag
		{
			get { return m_undoFlag; }
		}
		public int widthMax
		{
			get { return m_CellCount * CellWidth; }
		}
		public int heightMax
		{
			get { return m_FrameCount * CellHeight; }
		}
		//----------------------------------------------------------
		public int[] GetLayerData(int idx)
		{
			List<int> ret = new List<int>();
			if ((idx < 0) || (idx >= m_CellCount))
				return new int[0];

			bool ZeroFlag = false;

			for (int i = 0; i < m_FrameCount; i++)
			{
				if (frameEnabled[i] >= 0)
				{
					int v = cellData[idx][i];
					if (v > 0)
						ZeroFlag = true;
					ret.Add(v);
				}
			}

			if ((ZeroFlag == true) && (ret.Count > 0))
			{
				int[] data = new int[ret.Count];
				for (int i = 0; i < ret.Count; i++)
				{
					data[i] = ret[i];
				}
				return data;
			}
			else
			{
				return new int[0];
			}
		}
		//----------------------------------------------------------
		public bool IsCellData(int idx)
		{
			bool ret = false;
			if ((idx < 0) || (idx >= m_CellCount)) { return ret; }

			for (int i = 0; i < m_FrameCount; i++)
			{
				if (cellData[idx][i] > 0)
				{
					ret = true;
					break;
				}
			}
			return ret;
	}
		//----------------------------------------------------------
		public int CellData(int c, int f)
		{
			if ((c < 0) || (c >= m_CellCount) || (f < 0) || (f >= m_FrameCount))
				return 0;
			return cellData[c][f];
		}
		//----------------------------------------------------------
		public int CellData(int c, int f, int v)
		{
			if ((c < 0) || (c >= m_CellCount) || (f < 0) || (f >= m_FrameCount))
				return 0;
			cellDataBak[c][f] = cellData[c][f];
			cellData[c][f] = v;
			m_undoFlag = true;
			return v;
		}
		//----------------------------------------------------------
		public void setFrameEnabled(int f, bool sw)
		{
			/*
			if ((f < 0) || (f >= m_FrameCount))
				return;
			if (sw)
			{
				frameEnabled[f] = 0;
			}
			else
			{
				frameEnabled[f] = -1;
			}
			*/
			checkFrameEnabed();
		}
		//----------------------------------------------------------
		public void setFrameEnabled(int f0, int f1, bool sw)
		{
			/*
			int ff0 = f0;
			int ff1 = f1;
			if (ff1 > ff0)
			{
				ff0 = f1;
				ff1 = f0;
			}
			if ((ff1 < 0) || (ff0 >= m_FrameCount))
				return;

			if (ff0 < 0)
				ff0 = 0;
			if (ff0 >= m_FrameCount)
				ff0 = m_FrameCount - 1;
			if (ff1 < 0)
				ff1 = 0;
			if (ff1 >= m_FrameCount)
				ff1 = m_FrameCount - 1;

			int v;
			if (sw)
			{
				v = 0;
			}
			else
			{
				v = -1;
			}
			for (int i = ff0; i <= ff1; i++)
			{
				frameEnabled[i] = v;
			}
			checkFrameEnabed();
			*/
		}
		//----------------------------------------------------------
		public bool setFrameEnabled(int [] ary)
		{
			/*
			if ( frameEnabled.Length != ary.Length) { return false; }
			for (int i = 0; i < frameEnabled.Length; i++)
			{
				frameEnabled[i] = ary[i];
			}
			checkFrameEnabed();
			*/
			return true;
		}
		//----------------------------------------------------------
		public int[] getFrameEnabled()
		{
			int[] ret = new int[m_FrameCount];
			//checkFrameEnabed();
			//for (int i = 0; i < m_FrameCount; i++) { ret[i] = frameEnabled[i]; }
			for (int i = 0; i < m_FrameCount; i++) { ret[i] = 0; }

			return ret;
		}
		//----------------------------------------------------------
		public int FrameEnabeld(int f)
		{
			return 0;
			/*
			int ret = 0;
			if (f < 0)
			{
				return 0;
			}
			else if (f >= m_FrameCount)
			{
				return m_FrameCount - 1;
			}
			else
			{

				ret = frameEnabled[f];
			}
			return ret;
			*/
		}
		//----------------------------------------------------------
		public void FrameEnabeld(TSSelection sel, bool sw)
		{
			/*
			int y0 = sel.Start;
			if (y0 < 0) y0 = 0;
			int y1 = sel.Last;
			if (y1 >= m_FrameCount) y1 = m_FrameCount;
			int v = -1;
			if (sw) v = 0;
			for (int i = y0; i <= y1; i++)
			{
				frameEnabled[i] = v;
			}
			checkFrameEnabed();
			*/
		}
		//----------------------------------------------------------
		public string CellCaption(int c)
		{
			if ((c < 0) || (c >= m_CellCount))
				return "";
			return cellCaption[c];
		}
		//----------------------------------------------------------
		public void CellCaption(int c, string s)
		{
			if ((c < 0) || (c >= m_CellCount))
				return;
			cellCaption[c] = s.Trim();
		}
		//----------------------------------------------------------
		public bool SetCellCaption(string [] ary)
		{
			if ( (ary.Length != cellCaption.Length ))  return false;
			for (var i = 0; i < cellCaption.Length; i++)
			{
				cellCaption[i] = ary[i];
			}
			return true;
		}
        //----------------------------------------------------------
        public string[] GetCellCaption()
        {
            return cellCaption;
        }
 

		//----------------------------------------------------------
		public int SetCellData(int c, int f, int v)
		{
			if ((c < 0) || (c >= m_CellCount) || (f < 0) || (f >= m_FrameCount))
				return 0;
			cellData[c][f] = v;
			return v;
		}
		//----------------------------------------------------------
		public void SetCellData(TSSelection sel, int v)
		{
			int c = sel.Index;
			if ((c < 0) || (c >= m_CellCount))
				return;
			int f0 = sel.Start;
			if (f0 >= m_FrameCount)
				return;
			int f1 = sel.Last;
			if (f1 < 0)
				return;

			for (int i = f0; i <= f1; i++)
			{
				if ((i >= 0) && (i < m_FrameCount))
				{
					cellData[c][i] = v;
				}
			}
		}
		//----------------------------------------------------------
		public void SetCellData(TSSelection sel, List<int> lst)
		{
			if (lst.Count <= 0)
				return;
			int c = sel.Index;
			if ((c < 0) || (c >= m_CellCount))
				return;

			int f0 = sel.Start;
			if (f0 >= m_FrameCount)
				return;
			int f1 = sel.Last;
			if (f1 < 0)
				return;


			int j = 0;
			for (int i = f0; i <= f1; i++)
			{
				if (j >= lst.Count)
					break;
				if ((i >= 0) && (i < m_FrameCount))
				{
					cellData[c][i] = lst[j];
				}
				j++;
			}
		}
		//----------------------------------------------------------
		public bool SetCellData(int[][] ary)
		{
			if (ary.Length != cellData.Length) { return false; }
			if (ary[0].Length != cellData[0].Length) { return false; }

			int cc = ary.Length;
			int fc = ary[0].Length;
			for (int i = 0; i < cc; i++)
			{
				for (int j = 0; j < fc; j++)
				{
					cellData[i][j] = ary[i][j];
				}

			}
			return true;

		}

		//----------------------------------------------------------
		public int GetCellData(int c, int f)
		{
			if ((c < 0) || (c >= m_CellCount) || (f < 0) || (f >= m_FrameCount))
				return 0;
			return cellData[c][f];
		}
		//----------------------------------------------------------
		public int [] GetCellData(int idx)
		{
			if ((idx < 0) || (idx >= m_CellCount)) { return new int[0]; }
			int[] ret = new int[m_FrameCount];
			for (int i = 0; i < m_FrameCount; i++)
			{
				ret[i] = cellData[idx][i];
			}
			return ret;

		}
		//----------------------------------------------------------
        public bool IsCellDataEmpty(int idx)
        {
            if ((idx < 0) || (idx >= m_CellCount)) { return true; }
            for (int i = 0; i < m_FrameCount; i++)
            {
                if (frameEnabled[i] >= 0)
                {
                    if (cellData[idx][i] > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
		//----------------------------------------------------------
		public int[] GetCellDataTrue(int idx)
		{
			if ((idx < 0) || (idx >= m_CellCount)) { return new int[0]; }		
			List<int> ll = new List<int>();

			checkFrameEnabed();
			for (int i = 0; i < m_FrameCount; i++)
			{
				if (frameEnabled[i] >= 0)
				{
					ll.Add(cellData[idx][i]);
				}
			}
			if (ll.Count > 0)
			{
				int[] ret = new int[ll.Count];
				for (int i = 0; i < ll.Count; i++)
				{
					ret[i] = ll[i];
				}
				return ret;
			}
			else
			{
				return new int[0];
			}

		}
		//----------------------------------------------------------
		public int[] GetCellData(TSSelection sel)
		{
			int c = sel.Index;
			if ((c < 0) || (c >= m_CellCount))
				return new int[0];
			int f0 = sel.Start;
			if (f0 >= m_FrameCount)
				return new int[0];
			if (f0 < 0) { f0 = 0; }
			int f1 = sel.Last;
			if (f1 < 0)
				return new int[0];
			if (f1 < 0) { f1 = 0; }

			int[] ret = new int[f1 - f0 + 1];

			int j = 0;
			for (int i = f0; i <= f1; i++)
			{
				ret[j] = cellData[c][i];
				j++;
			}
			return ret;
		}
		//----------------------------------------------------------
		public void pushBak( )
		{
			for (int j = 0; j < m_CellCount; j++)
			{
				for (int i = 0; i < m_FrameCount; i++)
				{
					cellDataBak[j][i] = cellData[j][i];
				}
			}
			m_undoFlag = true;
		}
		//----------------------------------------------------------
		public void popBak( )
		{
			for (int j = 0; j < m_CellCount; j++)
			{
				for (int i = 0; i < m_FrameCount; i++)
				{
					cellData[j][i] = cellDataBak[j][i];
				}
			}
			m_undoFlag = false;
		}
		//----------------------------------------------------------
		public bool Swap(int idx0, int idx1)
		{
			if ((idx0 < 0) || (idx0 >= m_CellCount) || (idx1 < 0) || (idx1 >= m_CellCount) || (idx0 == idx1))
				return false;

			string ss = cellCaption[idx0];
			cellCaption[idx0] = cellCaption[idx1];
			cellCaption[idx1] = ss;

			for (int i = 0; i < m_FrameCount; i++)
			{
				int v = cellData[idx0][i];
				cellData[idx0][i] = cellData[idx1][i];
				cellData[idx1][i] = v;
			}
			return true;



		}
		//----------------------------------------------------------
		public bool MoveToLeft(int idx)
		{
			if ((idx <= 0) || (idx >= m_CellCount))
				return false;
			return Swap(idx, idx - 1);
		}
		//----------------------------------------------------------
		public bool MoveToRight(int idx)
		{
			if ((idx < 0) || (idx >= m_CellCount - 1))
				return false;
			return Swap(idx, idx + 1);
		}
		//**************************************************************
		//Save
		//**************************************************************
		//----------------------------------------------------------
		public int FindArd_prms(List<Ard_prms> prms, string tag)
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
		//----------------------------------------------------------
		public void FromParams( )
		{
			for (int i = 0; i < Params.Count; i++)
			{
				string tag = Params[i].Tag;
				if (String.Compare(tag, TSSaveFile.D_LayerCount, true) == 0)
				{
					m_CellCount = Params[i].GetValueInt(TSdef.CellCount);
				}
				else if (String.Compare(tag, TSSaveFile.D_FrameCount, true) == 0)
				{
					m_FrameCount = Params[i].GetValueInt(TSdef.FrameCount);
				}
				else if (String.Compare(tag, TSSaveFile.D_FrameRate, true) == 0)
				{
					SetFrameRate((TSFps)Params[i].GetValueInt((int)TSdef.FrameRate));
				}
				else if (String.Compare(tag, TSSaveFile.D_PageSec, true) == 0)
				{
					PageSec = (TSPageSec)Params[i].GetValueInt((int)TSdef.PageSec);
				}
				else if (String.Compare(tag, TSSaveFile.D_CREATE_USER, true) == 0)
				{
					CREATE_USER = Params[i].Value;
				}
				else if (String.Compare(tag, TSSaveFile.D_UPDATE_USER, true) == 0)
				{
					UPDATE_USER = Params[i].Value;
				}
				else if (String.Compare(tag, TSSaveFile.D_CREATE_TIME, true) == 0)
				{
					string s = Params[i].Value;
					if (DateTime.TryParse(s, out CREATE_TIME) == false)
					{
						CREATE_TIME = new DateTime(1963, 9, 9);
					}
				}
				else if (String.Compare(tag, TSSaveFile.D_UPDATE_TIME, true) == 0)
				{
					string s = Params[i].Value;
					if (DateTime.TryParse(s, out UPDATE_TIME) == false)
					{
						UPDATE_TIME = new DateTime(1963, 9, 9);
					}
				}
				else if (String.Compare(tag, TSSaveFile.D_TITLE, true) == 0)
				{
					TITLE = Params[i].Value;
				}
				else if (String.Compare(tag, TSSaveFile.D_SUB_TITLE, true) == 0)
				{
					SUB_TITLE = Params[i].Value;
				}
				else if (String.Compare(tag, TSSaveFile.D_OPUS, true) == 0)
				{
					OPUS = Params[i].Value;
				}
				else if (String.Compare(tag, TSSaveFile.D_SCECNE, true) == 0)
				{
					SCECNE = Params[i].Value;
				}
				else if (String.Compare(tag, TSSaveFile.D_CUT, true) == 0)
				{
					CUT = Params[i].Value;
				}
                else if (String.Compare(tag, TSSaveFile.D_CAMPANY_NAME, true) == 0)
                {
                    CAMPANY_NAME = Params[i].Value;
                }
            }
			SetSize(m_CellCount, m_FrameCount);
		}
		//----------------------------------------------------------
		private void toParam(string t, string s)
		{
			int idx = FindArd_prms(Params, t);
			Ard_prms prm = new Ard_prms(t, s);
			if (idx >= 0) { Params[idx] = prm; } else { Params.Add(prm); }
		}
		//----------------------------------------------------------
		public void ToParams( )
		{
			toParam(TSSaveFile.D_LayerCount, m_CellCount.ToString());
			toParam(TSSaveFile.D_FrameCount, m_FrameCount.ToString());
			toParam(TSSaveFile.D_FrameRate, ((int)m_FrameRate).ToString());
			toParam(TSSaveFile.D_PageSec, ((int)PageSec).ToString());
			toParam(TSSaveFile.D_CREATE_USER, CREATE_USER);
			toParam(TSSaveFile.D_UPDATE_USER, UPDATE_USER);
			toParam(TSSaveFile.D_CREATE_TIME, CREATE_TIME.ToString());
			toParam(TSSaveFile.D_UPDATE_TIME, UPDATE_TIME.ToString());
			toParam(TSSaveFile.D_TITLE, TITLE);
			toParam(TSSaveFile.D_SUB_TITLE, SUB_TITLE);
			toParam(TSSaveFile.D_OPUS, OPUS);
			toParam(TSSaveFile.D_SCECNE, SCECNE);
			toParam(TSSaveFile.D_CUT, CUT);
            toParam(TSSaveFile.D_CAMPANY_NAME, CAMPANY_NAME);

		}
		//----------------------------------------------------------
		public string zero4(int v)
		{
			string s = "";
			if (v < 10) { s = "   " + v.ToString(); }
			else if (v < 100) { s = "  " + v.ToString(); }
			else if (v < 1000) { s = " " + v.ToString(); }
			else  { s = v.ToString(); }
			return s;
		}
		//----------------------------------------------------------
		public string zero3(int v)
		{
			string s = "";
			if (v < 10) { s = "  " + v.ToString(); }
			else if (v < 100) { s = " " + v.ToString(); }
			else { s = v.ToString(); }
			return s;
		}
		//----------------------------------------------------------
		public string FrameStr(int idx)
		{
			string s = "";
			int v;
			int pf = PageFrame;
			int zr = 1;
			if (ZeroStart) zr = 0; 
			switch (FrameDisp)
			{
				case TSFrameDisp.frame:
					s = (idx+zr).ToString();
					break;
				case TSFrameDisp.pageFrame:
					v = (idx % (int)FrameRate);
					if (v == 0)
					{
						s = (idx / pf + 1).ToString() + "P ";
					}
					v = (idx % pf) + zr;
					s += zero4(v);
					break;
				case TSFrameDisp.paseSecFrame:
					v = (idx % (int)FrameRate);
					if (v == 0)
					{
						s = (idx / pf + 1).ToString() + "P ";
						v = ((idx % pf) / (int)FrameRate);
						s += zero3(v) + " +";
					}
					v = (idx % (int)FrameRate) + zr;
					s += zero4(v);
					break;
				case TSFrameDisp.SecFrame:
					v = (idx % (int)FrameRate);
					if (v == 0)
					{
						v = (idx / (int)FrameRate);
						s += v.ToString() + " +";
					}
					v = (idx % (int)FrameRate) + zr;
					s += zero4(v);
					break;
			}
			return s;
		}
		//----------------------------------------------------------
		public string FrameStr2(int idx)
		{
			string s = "";
			int v;
			int pf = PageFrame;
			int zr = 1;
			if (ZeroStart) zr = 0;
			switch (FrameDisp)
			{
				case TSFrameDisp.frame:
					s = (idx + zr).ToString()+"フレーム";
					break;
				case TSFrameDisp.pageFrame:
					s = (idx / pf + 1).ToString() + "ページ";
					s += ((idx % pf) + zr).ToString()+"コマ";
					break;
				case TSFrameDisp.paseSecFrame:
					s = (idx / pf + 1).ToString() + "ページ";
					v = ((idx % pf) / (int)FrameRate);
					s += v.ToString() + "秒";
					v = (idx % (int)FrameRate) + zr;
					s += v.ToString() + "コマ";
					break;
				case TSFrameDisp.SecFrame:
					v = (idx / (int)FrameRate);
					s += v.ToString() + "秒";
					v = (idx % (int)FrameRate) + zr;
					s += v.ToString() +"コマ";
					break;
			}
			return s;
		}
		//----------------------------------------------------------
		public void InsertLayer(int idx,string cap)
		{
			SetSize(m_CellCount + 1, m_FrameCount);
			//この時点でm_CellCountは増えている
			for (int f = 0; f < m_FrameCount; f++)
			{
				for (int c = m_CellCount - 1 ; c >idx ; c--)
				{
					cellData[c][f] = cellData[c-1][f];
					cellData[c - 1][f] = 0;

				}
			}
			for (int c = m_CellCount - 1; c > idx; c--)
			{
				cellCaption[c] = cellCaption[c - 1];
			}
			cellCaption[idx] = cap.Trim();
			if ((cellCaption[idx] == null)||(cellCaption[idx] == "") ){ SetDefaultCaption();}
		}
		//----------------------------------------------------------
		public void RemoveLayer(int idx)
		{
			if ( m_CellCount<2) { return; }
			if ( idx != m_CellCount-1)
			{
				for ( int f = 0; f<m_FrameCount;f++)
				{
					for (int c = idx; c < m_CellCount - 1; c++)
					{
						cellData[c][f] = cellData[c + 1][f];
					}
				}
			}
			for (int c = idx; c < m_CellCount - 1; c++)
			{
				cellCaption[c] = cellCaption[c + 1];
			}
			SetSize(m_CellCount - 1, m_FrameCount);
		}
		//----------------------------------------------------------
		public void DeleteFrame(TSSelection sel)
		{
			int y0 = sel.Start;
			if (y0 < 0) y0 = 0;
			int y1 = sel.Last +1;
			if (y1 > m_FrameCount) { y1 = m_FrameCount; }
			int len = y1 - y0;
			if ((m_FrameCount - len) <= 0) { return; }
			for (int f = y1; f< m_FrameCount; f++)
			{
				for (int c = 0; c < m_CellCount; c++)
				{
					cellData[c][f - len] = cellData[c][f];
					cellData[c][f] = 0;
				}
			}

		}
		//----------------------------------------------------------
		public void InsertFrame(TSSelection sel)
		{
			int y0 = sel.Start;
			if (y0 < 0) y0 = 0;
			int y1 = sel.Last + 1;
			if (y1 > m_FrameCount) { y1 = m_FrameCount; }
			int len = y1 - y0;
			for (int f = m_FrameCount -1; f >=y1 ; f--)
			{
				for (int c = 0; c < m_CellCount; c++)
				{
					cellData[c][f] = cellData[c][f - len];
					cellData[c][f - len] = 0;
				}
			}
		}
		//----------------------------------------------------------
		public void InsertFrameWithDuration(TSSelection sel)
		{
			int y0 = sel.Start;
			if (y0 < 0) y0 = 0;
			int y1 = sel.Last + 1;
			if (y1 > m_FrameCount) { y1 = m_FrameCount; }
			int len = y1 - y0;
			SetSize(m_CellCount, m_FrameCount + len);
			for (int f = m_FrameCount - 1; f >= y1; f--)
			{
				for (int c = 0; c < m_CellCount; c++)
				{
					cellData[c][f] = cellData[c][f - len];
					cellData[c][f - len] = 0;
				}
				frameEnabled[f] = frameEnabled[f - len];
				frameEnabled[f - len] = -1;
			}
		}
		//----------------------------------------------------------
		public void DeleteFrameWithDuration(TSSelection sel)
		{
			int y0 = sel.Start;
			if (y0 < 0) y0 = 0;
			int y1 = sel.Last + 1;
			if (y1 > m_FrameCount) { y1 = m_FrameCount; }
			int len = y1 - y0;
			if ((m_FrameCount - len) <= 0) { return; }
			for (int f = y1; f < m_FrameCount; f++)
			{
				for (int c = 0; c < m_CellCount; c++)
				{
					cellData[c][f - len] = cellData[c][f];
					cellData[c][f] = 0;
				}
				frameEnabled[f - len] = frameEnabled[f];
				frameEnabled[f] = 0;

			}
			SetSize(m_CellCount, m_FrameCount - len);
		}
		//----------------------------------------------------------
		public void AutoInput(TSSelection sel, int start, int end,int koma)
		{
			//-input
			int idx = sel.Index;
			int y0 = sel.Start;
			if (y0 < 0) { y0 = 0; }
			int y1 = sel.Last +1;
			if (y1 > m_FrameCount) { y1 = m_FrameCount -1; }
			//
			int s0 = start;
			int s1 = end;
			int k = koma;
			if (k <= 0) k = 1;
			int ll = (s1 - s0 + 1) * k;
			int[] loop = new int[ll];
			int v =0;
			if (s0 == s1)
			{
				for (int j = 0; j < k; j++)
				{
					loop[j] = s0;
				}
			}
			else if (s0 < s1)
			{
				for (int i = s0; i <= s1; i++)
				{
					for (int j = 0; j < k; j++)
					{
						loop[v] = i;
						v++;
					}
				}
			}
			else if (s0 > s1)
			{
				for (int i = s0; i <= s1; i--)
				{
					for (int j = 0; j < k; j++)
					{
						loop[v] = i;
						v++;
					}
				}
			}
			v =0;
			for (int i = y0; i < y1; i++)
			{
				cellData[idx][i] = loop[v];
				v = (v + 1) % ll;
			}
		}
		public string[] CommentLines
		{
			get
			{
				string[] ln = new string[Comment.Count];
				for (int i=0; i<Comment.Count; i++)
				{
					ln[i] = Comment[i]; 
				}
				return ln;
			}
			set
			{
				Comment.Clear();
				for (int i = 0; i < value.Length; i++)
				{
					Comment.Add(value[i]);
				}

			}
		}
		//**************************************************************
		public void ChkTimes()
		{

			if (DateTime.Compare(CREATE_TIME, new DateTime(1963, 9, 9)) == 0)
			{
				CREATE_TIME = DateTime.Now;
				UPDATE_TIME = CREATE_TIME;
			}
			else
			{
				if (DateTime.Compare(CREATE_TIME, DateTime.Now) != 0)
				{
					UPDATE_TIME = DateTime.Now;
				}
			}
		}
        //**************************************************************
 
        public void CalcValue(TSSelection sel, int v, ValueEditMode md)
        {
            int idx  = sel.Index;
            if ( ( idx<0)||(idx>=m_CellCount) ) return;
            int y0 = sel.Start;
            if (y0 < 0) y0 = 0;
            int y1 = sel.Last;
            if (y1 >= m_FrameCount) y1 = m_FrameCount -1;

            switch (md)
            {
                case ValueEditMode.add:
                    for (int i = y0; i <= y1; i++)
                    {
                        int v2 = cellData[idx][i] + v;
                        
                        cellData[idx][i] = v2;
                    }
                    break;
                case ValueEditMode.dec:
                    for (int i = y0; i <= y1; i++)
                    {
                        int v2 = cellData[idx][i] - v;
                       
                        cellData[idx][i] = v2;
                    }
                    break;
                default:
                    for (int i = y0; i <= y1; i++)
                    {
                        int v2 = v;
             
                        cellData[idx][i] = v;
                    }
                    break;
            }
        }
        //**************************************************************
		public void ClearAll()
		{
			for (int i = 0; i < this.m_CellCount; i++)
			{
				ClearLayer(i);
			}
			for (int j = 0; j < this.m_FrameCount; j++)
			{
				frameEnabled[j] = j;
			}
		}
		//**************************************************************
		public void ClearLayer(int idx)
		{
			if ((idx < 0) || (idx >= m_CellCount)) return;
			for (int j = 0; j < this.m_FrameCount; j++)
			{
				this.cellData[idx][j] = TSdef.none;
			}
		}
		//**************************************************************
	}
    //**************************************************************
}
