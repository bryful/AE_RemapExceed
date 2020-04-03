using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace AE_RemapExceed
{
	//**************************************************************
	//表示色管理クラス
	public enum TSColorIndex
	{
		BaseLine = 0,
		CellBase1,
		CellBase2,
		CellLine,
		Selection,
		CapLine,
		FrameLine,
		MemoLine,
		MemoBase,
		FrameBase,
		FrameSelection,
		CaptionBase,
		CaptionSelection,
		InputBase,
		Text,
		None,
		NoneText,
		Count
	}
	//**************************************************************
	public class TSColors
	{
		private Color[] col = new Color[(int)TSColorIndex.Count];

		private string[] captionStr = new string[(int)TSColorIndex.Count]{
			"基本線",
			"セル背景色(奇数列)",
			"セル背景色(偶数列)",
			"セル境界線",
			"セル選択色",
			"キャプション境界線",
			"フレーム境界線",
			"メモ境界線",
			"メモ背景色",
			"フレーム背景色",
			"フレーム選択色",
			"キャプション",
			"キャプション選択色",
			"入力",
			"文字色",
			"無効フレーム",
			"無効フレーム文字",
		};
        



        public TSColors()
		{
			Init();
		}
		public string[] CaptionStr
		{
			get { return captionStr;}
            set { captionStr = value; }
        }
        public Color[] Col
		{
			get { return col; }
			set { col = value; }
		}
        public int[] ColorInt
        {
            get
            {
                int[] ret = new int[(int)TSColorIndex.Count];
                for(int i=0; i< (int)TSColorIndex.Count;i++)
                {
                    ret[i] = col[i].ToArgb();
                }
                return ret;
            }
            set
            {
                if (value.Length < (int)TSColorIndex.Count) return;
                for (int i = 0; i < (int)TSColorIndex.Count; i++)
                {
                    col[i] = Color.FromArgb(value[i]);
                }
            }
        }

		public Color BaseLine
		{
			get { return col[(int)TSColorIndex.BaseLine]; }
			set { col[(int)TSColorIndex.BaseLine] = value; }
		}
		public Color CellBase1
		{
			get { return col[(int)TSColorIndex.CellBase1]; }
			set { col[(int)TSColorIndex.CellBase1] = value; }
		}
		public Color CellBase2
		{
			get { return col[(int)TSColorIndex.CellBase2]; }
			set { col[(int)TSColorIndex.CellBase2] = value; }
		}
		public Color CellLine
		{
			get { return col[(int)TSColorIndex.CellLine]; }
			set { col[(int)TSColorIndex.CellLine] = value; }
		}
		public Color FrameLine
		{
			get { return col[(int)TSColorIndex.FrameLine]; }
			set { col[(int)TSColorIndex.FrameLine] = value; }
		}
		public Color CapLine
		{
			get { return col[(int)TSColorIndex.CapLine]; }
			set { col[(int)TSColorIndex.CapLine] = value; }
		}
		public Color MemoLine
		{
			get { return col[(int)TSColorIndex.MemoLine]; }
			set { col[(int)TSColorIndex.MemoLine] = value; }
		}
		public Color MemoBase
		{
			get { return col[(int)TSColorIndex.MemoBase]; }
			set { col[(int)TSColorIndex.MemoBase] = value; }
		}
		public Color FrameBase
		{
			get { return col[(int)TSColorIndex.FrameBase]; }
			set { col[(int)TSColorIndex.FrameBase] = value; }
		}
		public Color FrameSelection
		{
			get { return col[(int)TSColorIndex.FrameSelection]; }
			set { col[(int)TSColorIndex.FrameSelection] = value; }
		}
		public Color CaptionBase
		{
			get { return col[(int)TSColorIndex.CaptionBase]; }
			set { col[(int)TSColorIndex.CaptionBase] = value; }
		}
		public Color CaptionSelection
		{
			get { return col[(int)TSColorIndex.CaptionSelection]; }
			set { col[(int)TSColorIndex.CaptionSelection] = value; }
		}
		public Color Selection
		{
			get { return col[(int)TSColorIndex.Selection]; }
			set { col[(int)TSColorIndex.Selection] = value; }
		}
		public Color InputBase
		{
			get { return col[(int)TSColorIndex.InputBase]; }
			set { col[(int)TSColorIndex.InputBase] = value; }
		}
		public Color Text
		{
			get { return col[(int)TSColorIndex.Text]; }
			set { col[(int)TSColorIndex.Text] = value; }
		}
		public Color None
		{
			get { return col[(int)TSColorIndex.None]; }
			set { col[(int)TSColorIndex.None] = value; }
		}
		public Color NoneText
		{
			get { return col[(int)TSColorIndex.NoneText]; }
			set { col[(int)TSColorIndex.NoneText] = value; }
		}
		public void Init()
		{
			BaseLine = Color.FromArgb(0x00, 0x00, 0x00);
			CellBase1 = Color.FromArgb(0xFF, 0xFF, 0xFF);
			CellBase2 = Color.FromArgb(0xF0, 0xF0, 0xF0);

			CellLine = Color.FromArgb(0x82, 0x82, 0x82);
			CapLine = Color.FromArgb(0x30, 0x30, 0x30);
			FrameLine = Color.FromArgb(0x30, 0x30, 0x30);
			MemoLine = Color.FromArgb(0x78, 0x78, 0x78);
			Selection = Color.FromArgb(0xA8, 0xCA, 0xD9);
			MemoBase = Color.FromArgb(0xFF, 0xFF, 0xFF);
			FrameBase = Color.FromArgb(0xD1, 0xD1, 0xD1);
			FrameSelection = Color.FromArgb(0xFF, 0x75, 0x75);
			CaptionBase = Color.FromArgb(0xBE, 0xBE, 0xBE);
			CaptionSelection = Color.FromArgb(0xFF, 0x60, 0x60);
			InputBase = Color.FromArgb(0xFF, 0xFF, 0xFF);
			Text = Color.FromArgb(0x00, 0x00, 0x00);
			None = Color.FromArgb(0x90, 0x90, 0x90);
			NoneText = Color.FromArgb(0xC3, 0xC3, 0xC3);

		}
		//------------------------------------------------------
		public void Assign(TSColors tsc)
		{
			for (int i = 0; i < (int)TSColorIndex.Count; i++)
			{
				this.col[i] = tsc.col[i];
			}
		}

	}
	//**************************************************************
}
