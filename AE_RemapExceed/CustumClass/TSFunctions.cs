using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Codeplex.Data;
using BRY;


namespace AE_RemapExceed
{
	public enum funcCmd
	{
		//File
		/*00*/New = 0,
		/*01*/Open,
		/*02*/Save,
		/*03*/SaveAs,
        /*04*/Quit,
		//Edit
		/*05*/Copy,
		/*06*/Cut,
		/*07*/Paste,
		/*08*/ColorSetting,
		/*09*/LayoutSetting,
		/*10*/KeySetting,

		//10Key
		/*11*/ValueInput,
		/*12*/ValueAutoInc,
		/*13*/ValueAutoDec,
		/*14*/ValueAutoSame,
		/*15*/ValueBack,
		/*16*/ValueDelete,
		SelectionALL,
		SelectionToEND,
		LayerMoveToLeft,
		LayerMoveToRight,
		LayerDataToClipboard,
		PageUp,
		PageDown,
		JumpTop,
		JumpEnd,

		SelTailInc,
		SelTailDec,
		SelHeadInc,
		SelHeadDec,

		LayerRemove,
		LayerInsert,
		LayerRename,

		FrameInsert,
		FrameDelete,

		AutoInput,

        ValueEdit,
        Selecton1,
        Selecton2,
        Selecton3,
        Selecton4,
        Selecton5,
        Selecton6,
        Selecton7,
        Selecton8,
        Selecton9,
        Selecton10,
        Selecton11,
        Selecton12,

        SelectionUp,
        SelectionRight,
        SelectionDown,
        SelectionLeft,

        Print,
        PrintPreview,
        PageSetup,
		PrintSetting,

		About,
		ClearAll,
		ClearLayer,


		Count
	}

	public delegate void funcEmt();
	public delegate void funcNumEmt(int v);
	public delegate void funcSelMove(Keys k);
	public delegate void menuFunc(object sender, EventArgs e);
	//------------------------------------------------
	public class TSFunctions
	{
		public const string Header = "AE_Remap KeyTable";
		public string[,] funcName = new string[(int)funcCmd.Count,2]
		{
			{"New","シート設定"},
			{"Open","読み込み"},
			{"Save","保存"},
			{"SaveAs","別名で保存"},
            {"Quit","終了"},

			{"Copy","コピー"},
			{"Cut","切り取り"},
			{"Paste","貼り込み"},

			{"ColorSetting","カラー設定"},
			{"LayoutSetting","グリッドサイズ設定"},
			{"KeySetting","キーボード設定"},


			{"ValueInput","セル番号入力"},
			{"ValueAutoInc","自動入力＋"},
			{"ValueAutoDec","自動入力－"},
			{"ValueAutoSame","前のコマと同じ値を入力"},
			{"ValueBack","BackSpace"},
			{"ValueDelete","入力値をクリア"},
			{"SelectionALL","全選択"},
			{"SelectionToEND","ラストまで選択"},
			{"LayerMoveToLeft","レイヤを左へ移動"},
			{"LayerMoveToRight","レイヤを右へ移動"},
			{"LayerDataToClipboard","KeyFrameDataをクリップボードへ"},
			{"PageUp","PageUp"},
			{"PageDown","PageDown"},
			{"JumpTop","先頭へ"},
			{"JumpEnd","最後へ"},
			{"SelTailInc","選択範囲の末尾を増やす"},
			{"SelTailDec","選択範囲の末尾を減らす"},
			{"SelHeadInc","選択範囲の先頭を増やす"},
			{"SelHeadDec","選択範囲の先頭を減らす"},
			{"LayerRemove","セルレイヤを削除"},
			{"LayerInsert","セルレイヤを挿入"},
			{"LayerRename","セルレイヤ名を変更"},

			{"FrameInsert","フレームを挿入"},
			{"FrameDelete","フレームを削除"},
			{"AutoInput","セルのリピート入力"},
			{"ValueEdit","値の編集"},

			{"Selection1","選択範囲1k"},
			{"Selection2","選択範囲2k"},
			{"Selection3","選択範囲3k"},
			{"Selection4","選択範囲4k"},
			{"Selection5","選択範囲5k"},
			{"Selection6","選択範囲6k"},
			{"Selection7","選択範囲7k"},
			{"Selection8","選択範囲8k"},
			{"Selection9","選択範囲9k"},
			{"Selection10","選択範囲10k"},
			{"Selection11","選択範囲11k"},
			{"Selection12","選択範囲12k"},

            {"SelectionUp","選択範囲を上に移動"},
            {"SelectionRight","選択範囲を右に移動"},
            {"SelectionDown","選択範囲を下に移動"},
            {"SelectionLeft","選択範囲を左に移動"},

            {"Print","印刷"},
            {"PrintPreview","印刷プレビュー"},
            {"PageSetup","ページ設定"},
			{"PrintSetting","プリント設定"},

			{"About","Aboutダイアログ"},

			{"ClaerAll","全クリア"},
			{"ClaerLayer","レイヤクリア"},

		};
		//キーバインド関係
		//------------------------------------------------
		public class funcClass
		{
			public Keys key;
			public Keys keySub;
			public funcEmt func;
			public funcClass()
			{
				this.key = Keys.None;
				this.keySub = Keys.None;
				this.func = null;
			}
			public funcClass(Keys km, funcEmt f)
			{
				this.key = km;
				this.keySub = Keys.None;
				this.func = f;
			}
			public void Assign(funcClass c)
			{
				key = c.key;
				keySub = c.keySub;
				func = c.func;
			}
		}
		//------------------------------------------------
		private funcClass[] funcTable = new funcClass[(int)funcCmd.Count];

        public int [] FuncTableAll
        {
            get
            {
                int[] ret = new int [(int)funcCmd.Count*2];

                for ( int i=0; i< (int)funcCmd.Count; i++)
                {
                    ret[i * 2 + 0] = (int)funcTable[i].key;
                    ret[i * 2 + 1] = (int)funcTable[i].keySub;
                }
                return ret;
            }
            set
            {
                if (value.Length < (int)funcCmd.Count * 2) return;
                int idx = 0;
                for (int i = 0; i < (int)funcCmd.Count; i++)
                {
                    funcTable[i].key = (Keys)value[idx];
                    idx++;
                    funcTable[i].keySub = (Keys)value[idx];
                    idx++;

                }
            }
        }

		//キーコードを修正、テンキー対策
		private int[] keyMap = new int[256];
		//数字入力
		private funcNumEmt NumFunction = null;
		private funcSelMove SelMoveFunction = null;
		//----------------------------------------------------------------------------------------
		public TSFunctions()
		{
			//テーブルの初期化
			for (int i = 0; i < (int)funcCmd.Count; i++)
			{
				funcTable[i] = new funcClass();
			}
			funcKeyInt();
			InitKeyMap();
		}
		//------------------------------------------------
		public void Assign(TSFunctions f)
		{
			for (int i = 0; i < (int)funcCmd.Count; i++)
			{
				funcTable[i].Assign(f.funcTable[i]);
			}
		}
		//------------------------------------------------
		public void funcKeyInt()
		{
			//File
			setKeyTable(funcCmd.New, Keys.Control | Keys.N, Keys.None);
			setKeyTable(funcCmd.Open, Keys.Control | Keys.O, Keys.None);
			setKeyTable(funcCmd.Save, Keys.Control | Keys.S, Keys.None);
			setKeyTable(funcCmd.SaveAs, Keys.Control | Keys.Shift | Keys.S, Keys.None);
            setKeyTable(funcCmd.Quit, Keys.Control | Keys.Q, Keys.None);
			//Edit
			setKeyTable(funcCmd.Copy, Keys.Control | Keys.C, Keys.None);
			setKeyTable(funcCmd.Cut, Keys.Control | Keys.X, Keys.None);
			setKeyTable(funcCmd.Paste, Keys.Control | Keys.V, Keys.None);
			setKeyTable(funcCmd.ColorSetting, Keys.Control | Keys.Alt | Keys.L, Keys.None);
			setKeyTable(funcCmd.LayoutSetting, Keys.Control | Keys.Alt | Keys.G, Keys.None);
			setKeyTable(funcCmd.KeySetting, Keys.Control | Keys.Alt | Keys.K, Keys.None);

			//Layer
			setKeyTable(funcCmd.LayerDataToClipboard, Keys.Control | Keys.None, Keys.None);

			setKeyTable(funcCmd.ValueInput, Keys.Return,Keys.None);
			setKeyTable(funcCmd.ValueAutoInc, Keys.Add, Keys.None);
			setKeyTable(funcCmd.ValueAutoDec, Keys.Subtract, Keys.None);
			setKeyTable(funcCmd.ValueAutoSame, Keys.Decimal, Keys.None);
			setKeyTable(funcCmd.ValueBack, Keys.Back, Keys.None);
			setKeyTable(funcCmd.ValueDelete, Keys.Delete, Keys.None);

			setKeyTable(funcCmd.SelectionALL, Keys.A | Keys.Control, Keys.None);
			setKeyTable(funcCmd.SelectionToEND, Keys.End | Keys.Control, Keys.None);

			setKeyTable(funcCmd.LayerMoveToLeft, Keys.Oemcomma, Keys.None);
			setKeyTable(funcCmd.LayerMoveToRight, Keys.OemPeriod, Keys.None);

			setKeyTable(funcCmd.PageUp, Keys.PageUp, Keys.None);
			setKeyTable(funcCmd.PageDown, Keys.PageDown, Keys.None);
			setKeyTable(funcCmd.JumpTop, Keys.Home, Keys.None);
			setKeyTable(funcCmd.JumpEnd, Keys.End, Keys.None);
			setKeyTable(funcCmd.JumpEnd, Keys.End, Keys.None);
			
			setKeyTable(funcCmd.SelTailInc, Keys.X, Keys.Multiply);
			setKeyTable(funcCmd.SelTailDec, Keys.Z, Keys.Divide);
			setKeyTable(funcCmd.SelHeadInc, Keys.None, Keys.None);
			setKeyTable(funcCmd.SelHeadDec, Keys.None, Keys.None);

			setKeyTable(funcCmd.LayerRemove, Keys.None, Keys.None);
			setKeyTable(funcCmd.LayerInsert, Keys.None, Keys.None);
			setKeyTable(funcCmd.LayerRename, Keys.None, Keys.None);

			setKeyTable(funcCmd.FrameInsert, Keys.None, Keys.None);
			setKeyTable(funcCmd.FrameDelete, Keys.None, Keys.None);

			setKeyTable(funcCmd.AutoInput, Keys.Control | Keys.J, Keys.None);
			setKeyTable(funcCmd.ValueEdit, Keys.Control | Keys.Y, Keys.None);

            setKeyTable(funcCmd.Selecton1, Keys.Control | Keys.D1, Keys.F1);
            setKeyTable(funcCmd.Selecton2, Keys.Control | Keys.D2, Keys.F2);
            setKeyTable(funcCmd.Selecton3, Keys.Control | Keys.D3, Keys.F3);
            setKeyTable(funcCmd.Selecton4, Keys.Control | Keys.D4, Keys.F4);
            setKeyTable(funcCmd.Selecton5, Keys.Control | Keys.D5, Keys.F5);
            setKeyTable(funcCmd.Selecton6, Keys.Control | Keys.D6, Keys.F6);
            setKeyTable(funcCmd.Selecton7, Keys.Control | Keys.D7, Keys.F7);
            setKeyTable(funcCmd.Selecton8, Keys.Control | Keys.D8, Keys.F8);
            setKeyTable(funcCmd.Selecton9, Keys.Control | Keys.D9, Keys.F9);
            setKeyTable(funcCmd.Selecton10, Keys.None, Keys.F10);
            setKeyTable(funcCmd.Selecton11, Keys.None, Keys.F11);
            setKeyTable(funcCmd.Selecton12, Keys.None, Keys.F12);

            setKeyTable(funcCmd.SelectionUp, Keys.None, Keys.None);
            setKeyTable(funcCmd.SelectionRight, Keys.None, Keys.None);
            setKeyTable(funcCmd.SelectionDown, Keys.None, Keys.None);
            setKeyTable(funcCmd.SelectionLeft, Keys.None, Keys.None);

            setKeyTable(funcCmd.Print, Keys.Control | Keys.P, Keys.None);
            setKeyTable(funcCmd.PrintPreview, Keys.Control | Keys.Shift | Keys.P, Keys.None);
            setKeyTable(funcCmd.PageSetup, Keys.None, Keys.None);
			setKeyTable(funcCmd.PrintSetting, Keys.None, Keys.None);

            setKeyTable(funcCmd.About, Keys.None, Keys.None);
			setKeyTable(funcCmd.ClearAll, Keys.Control| Keys.Shift | Keys.Delete , Keys.None);
			setKeyTable(funcCmd.ClearLayer, Keys.Control | Keys.Delete, Keys.None);
		}
		//------------------------------------------------
		private void InitKeyMap()
		{
			for (int i = 0; i < 256; i++)
			{
				keyMap[i] = i;
			}
			int j=0;
			for (int i = (int)Keys.NumPad0; i <= (int)Keys.NumPad9; i++)
			{
				keyMap[i] = (int)Keys.D0 + j;
				j++;
			}
			keyMap[(int)Keys.OemQuestion] = (int)Keys.Divide;
			keyMap[(int)Keys.Oem1] = (int)Keys.Multiply;
			keyMap[(int)Keys.OemMinus] = (int)Keys.Subtract;
			keyMap[(int)Keys.Oemplus] = (int)Keys.Add;

		}
		//----------------------------------------------------------------------------------------
		public Keys GetKeyMap(Keys k)
		{
			long h = (long)k & 0xFFFF0000;
			long l = (long)k & 0x000000FF;

			l = keyMap[l];

			return (Keys)(h + l);
		}
		//----------------------------------------------------------------------------------------
		public void exec(Keys k)
		{
			if (k == Keys.None) return;
			if (k == Keys.Help) return;
			Keys k2 = GetKeyMap(k);
			Keys kc = (Keys)((int)k2 & 0xFF);
			if ((kc >= Keys.D0) && (k2 <= Keys.D9))
			{
				if (NumFunction != null) NumFunction((int)k2 - (int)Keys.D0);
				return;
			}
			else if ( (kc >= Keys.Left) &&(kc <= Keys.Down))
			{
				if (SelMoveFunction != null) SelMoveFunction(k2);
				return;
			}

			for (int i = 0; i < (int)funcCmd.Count; i++)
			{
				if ( (funcTable[i].key == k2)||(funcTable[i].keySub == k2) )
				{
					if (funcTable[i].func != null)
					{
						funcTable[i].func();
						return;
					}
				}
			}

		}
		//----------------------------------------------------------------------------------------
		public void exec(funcCmd f)
		{
			if (funcTable[(int)f].func != null)
			{
				funcTable[(int)f].func();
			}
		}
		//----------------------------------------------------------------------------------------
		public void setKey(funcCmd f,Keys k)
		{
			funcTable[(int)f].key = GetKeyMap(k);
		}
		//----------------------------------------------------------------------------------------
		public void setFunc(funcCmd f, funcEmt e)
		{
			funcTable[(int)f].func = e;
		}
		//----------------------------------------------------------------------------------------
		public void setNumFunc(funcNumEmt e)
		{
			NumFunction = e;
		}
		//----------------------------------------------------------------------------------------
		public void setSelMoveFunc(funcSelMove e)
		{
			SelMoveFunction = e;
		}
		//----------------------------------------------------------------------------------------
		public Keys getKeyTable(funcCmd cmd)
		{
			return (Keys)funcTable[(int)cmd].key;
		}
		//----------------------------------------------------------------------------------------
		public Keys getKeyTableSub(funcCmd cmd)
		{
			return funcTable[(int)cmd].keySub;
		}
		//----------------------------------------------------------------------------------------
		public void setKeyTable(funcCmd cmd, Keys k)
		{
			funcTable[(int)cmd].key = k;
		}
		//----------------------------------------------------------------------------------------
		public void setKeyTableSub(funcCmd cmd, Keys k)
		{
			funcTable[(int)cmd].keySub = k;
		}
		//----------------------------------------------------------------------------------------
		public void setKeyTable(funcCmd cmd, Keys km, Keys ks)
		{
			funcTable[(int)cmd].key = km;
			funcTable[(int)cmd].keySub = ks;
		}
		//----------------------------------------------------------------------------------------
		public int FindKeyTable(Keys k)
		{
			for (int i = 0; i < (int)funcCmd.Count; i++)
			{
				if ( (funcTable[i].key == k)||(funcTable[i].keySub == k) )
				{
					return i;
				}
			}
			return -1;
		}
		//----------------------------------------------------------------------------------------
		public string KeyString(Keys k)
		{

			return "0x" +k.ToString("X");

		}
		//----------------------------------------------------------------------------------------
		public bool SaveToFile(string path)
		{
			string s = Header + "\r";
			for (int i = 0; i < funcTable.Length; i++)
			{
				s += funcName[i,0] + " = " + KeyString(funcTable[i].key) + "," + KeyString(funcTable[i].keySub)  + "\r";
			}
			System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
			System.IO.File.WriteAllText(path, s, enc);

			return true;
		}
        public bool SaveToFileJ(string path)
        {
            bool ret = false;
            dynamic dat = new DynamicJson();
            dat["Header"] = Header;
            var dat2 = new object[funcTable.Length];
            for (int i = 0; i < funcTable.Length; i++)
            {
                dynamic dat3 = new DynamicJson();
                dat3["funcName"] = funcName[i, 0];
                dat3["key"] = (double)funcTable[i].key;
                dat3["keysub"] = (double)funcTable[i].keySub;
                dat2[i] = dat3;
            }
            string js = dat.ToString();
            File.WriteAllText(path, js, Encoding.GetEncoding("utf-8"));
            ret = File.Exists(path);
            return ret;
        }
		//----------------------------------------------------------------------------------------
		public Keys toKeys(string s)
		{
			Keys k;
			try
			{
				k = (Keys)Convert.ToInt32(s, 16);
			}
			catch
			{
				k = Keys.None;
			}
			return k;

		}
		//----------------------------------------------------------------------------------------
		public void setKeys(funcCmd cmd, string tag)
		{
			string[] sa = tag.Split(',');
			if (sa.Length < 2) return;
			sa[0] = sa[0].Trim();
			sa[1] = sa[1].Trim();

			funcTable[(int)cmd].key = toKeys(sa[0]);
			funcTable[(int)cmd].keySub = toKeys(sa[1]);
		}
		//----------------------------------------------------------------------------------------
		public int FindFuncName(string s)
		{
			int ret = -1;
			string ss = s.Trim();
			if (ss == string.Empty) return ret;
			try
			{
				for (int i = 0; i < funcName.Length; i++)
				{
					if (string.Compare(ss, funcName[i, 0], true) == 0)
					{
						ret = i;
						break;
					}
				}
			}
			catch
			{
			}
			return ret;
		}
		//----------------------------------------------------------------------------------------
		public bool LoadFromFile(string path)
		{
			System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
			if (File.Exists(path) == false) { return false; }
			string[] lines = System.IO.File.ReadAllLines(path, enc);

			if (lines.Length <= 1) return false;
			if (lines[0] != Header) return false;

			for (int i = 1; i < lines.Length; i++)
			{
				string[] sa = lines[i].Split('=');
				if (sa.Length < 2) continue;
				int idx = FindFuncName(sa[0]);
				if (idx >= 0)
				{
					setKeys((funcCmd)idx, sa[1]); 
				}
			}
			return true;
		}
		//----------------------------------------------------------------------------------------
	}

}
