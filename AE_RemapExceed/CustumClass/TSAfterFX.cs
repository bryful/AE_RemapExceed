using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE_RemapExceed
{
	//シート情報の構造体
	public class RemapkeyData
	{
		public int frame;	//
		public int num;
		public int empty
		{
			get
			{
				if (num == 0)
				{
					return 0;
				}
				else
				{
					return 1;
				}
			}
		}
		public RemapkeyData(int f, int n)
		{
			frame = f;
			num = n;
		}
		public RemapkeyData()
		{
			frame = 0;
			num = 0;
		}
	}
	//空セルの処理
	public enum EmptyCell
	{
		Opacity =0,
		BlindsJpn,
		BlindsEng,
		LastFrame
	}
	//文字列定数
	public class AEdef
	{
		public const string UPS = "Units Per Second";
		public const string SrcWidth = "Source Width";
		public const string SrcHeight = "Source Height";
		public const string SrcAspect = "Source Pixel Aspect Ratio";
		public const string CmpAspect = "Comp Pixel Aspect Ratio";

		public const string Frame = "Frame";
		public const string percent = "percent";
		public const string seconds = "seconds";

		public const string TimeRemap = "Time Remap";

		public const string Transform = "Transform";
		public const string Opacity = "Opacity";

		public const string Effects = "Effects";
		public const string BlindsJpn = "ブラインド #1";
		public const string BlindsJpnP = "変換終了 #2";
		public const string BlindsEng = "Venetian Blinds #1";
		public const string BlindsEngP = "ransition Completion #2";
		public const string HeaderCS3 = "Adobe After Effects 8.0 Keyframe Data";
		public const string Footer = "End of Keyframe Data";
		public const string TAB = "\t";
		public const string CRLF = "\r\n";

	}
	public class AE_KeyFrameData
	{
		private int[] m_sheet;
		private List<RemapkeyData> m_cells = new List<RemapkeyData>();
		private List<RemapkeyData> m_empty = new List<RemapkeyData>();

		public EmptyCell EmpyMode = EmptyCell.BlindsJpn;
		
		//clipboard Params
		public float UnitsPerSecond = 24.0f;
		public int SrcWidth = 1920;
		public int SrcHeight = 1080;
		public float SrcPixAspect = 1.0f;
		public float CmpPixAspect = 1.0f;

		public int LastFrame = 1440;

		//----------------------------------------------------------
		public AE_KeyFrameData( )
		{
			m_sheet = null;
			m_cells.Clear();
			m_empty.Clear();
		}
		//----------------------------------------------------------
		public AE_KeyFrameData(int[] ary)
		{
			m_sheet = null;
			m_cells.Clear();
			m_empty.Clear();
			getSheet(ary);
		}
		//----------------------------------------------------------
		public void getSheet(int[] ary)
		{
			m_sheet = ary;
			getKeyFrame();
		}
		//----------------------------------------------------------
		public int KeyCount
		{
			get { return m_cells.Count; }
		}
		//----------------------------------------------------------
		public void getKeyFrame( )
		{
			m_cells.Clear();
			m_empty.Clear();
			if ((m_sheet == null) || (m_sheet.Length == 0))
				return;
			//最初の1個を複写
			m_cells.Add(new RemapkeyData(0,m_sheet[0]));
			int l = m_sheet.Length;
			if ( l== 1)
			{
				//
			}
			else if (l == 2)
			{
				//要素が2個ならそのまま複写
				m_cells.Add(new RemapkeyData(1, m_sheet[1]));
			}
			else
			{
				for (int i = 1; i < l - 1; i++)
				{
					//重複した値は無視する
					if (m_sheet[i - 1] != m_sheet[i])
					{
						m_cells.Add(new RemapkeyData(i, m_sheet[i]));
					}
				}
				//最後の要素は必ず複写
				//m_cells.Add(new RemapkeyData(l-1, m_sheet[l-1]));
			}
			//最初の1個
			m_empty.Add(new RemapkeyData(m_cells[0].frame, m_cells[0].empty));
			for (int i = 1; i < m_cells.Count; i++)
			{
				if (m_cells[i - 1].empty != m_cells[i].empty)
				{
					m_empty.Add(new RemapkeyData(m_cells[i].frame, m_cells[i].empty));

				}
			}
		}
		//----------------------------------------------------------
		private string IntToStr(int v)
		{
			return v.ToString();
		}
		private string FloatToStr(float v)
		{
			return v.ToString();
		}
		private string FrameToSecStr(int v, float ups)
		{
			float frm = 0;
			if (v != 0)
			{
				frm = (v - 1) / ups;
			}

			return frm.ToString();
		}
		//----------------------------------------------------------
		public string MakeHeader()
		{
			string cData = "";
			cData += AEdef.HeaderCS3 + AEdef.CRLF;
			cData += AEdef.CRLF;
			
			cData += AEdef.TAB + AEdef.UPS + AEdef.TAB + FloatToStr(UnitsPerSecond) + AEdef.CRLF;
			cData += AEdef.TAB + AEdef.SrcWidth + AEdef.TAB + IntToStr(SrcWidth) + AEdef.CRLF;
			cData += AEdef.TAB + AEdef.SrcHeight + AEdef.TAB + IntToStr(SrcHeight) + AEdef.CRLF;
			cData += AEdef.TAB + AEdef.SrcAspect + AEdef.TAB + FloatToStr(SrcPixAspect) + AEdef.CRLF;
			cData += AEdef.TAB + AEdef.CmpAspect + AEdef.TAB + FloatToStr(CmpPixAspect) + AEdef.CRLF;
			cData += AEdef.CRLF;


			return cData;

		}
		//----------------------------------------------------------
		public string MakeKeyDataOpacity( )
		{
			string cData = "";
			cData += AEdef.Transform +AEdef.TAB+ AEdef.Opacity + AEdef.CRLF;
			cData += AEdef.TAB + AEdef.Frame + AEdef.TAB + AEdef.percent + AEdef.TAB + AEdef.CRLF;
			int l = m_empty.Count;
			if (l > 0)
			{
				for (int i = 0; i < l; i++)
				{
					string s = AEdef.TAB;
					s += IntToStr(m_empty[i].frame) + AEdef.TAB;
					if (m_empty[i].num == 0)
					{
						s += "0" + AEdef.TAB;
					}
					else
					{
						s += "100" + AEdef.TAB;
					}
						cData += s + AEdef.CRLF;
				}
				cData += AEdef.CRLF;
				
			}
			return cData;

		}
		//----------------------------------------------------------
		public string MakeKeyDataTimeRemap( )
		{
			string cData = "";
			cData += AEdef.TimeRemap + AEdef.CRLF;
			cData += AEdef.TAB + AEdef.Frame + AEdef.TAB + AEdef.seconds + AEdef.TAB + AEdef.CRLF;
			int l = m_cells.Count;
			if (l > 0)
			{
				for (int i = 0; i < l; i++)
				{
					string s = AEdef.TAB;
					s += IntToStr(m_cells[i].frame) + AEdef.TAB;
					if (m_cells[i].num == 0)
					{
						s += (LastFrame/UnitsPerSecond).ToString() + AEdef.TAB;
					}
					else
					{
						s += FrameToSecStr(m_cells[i].num, UnitsPerSecond) + AEdef.TAB;
					}
					cData += s + AEdef.CRLF;
				}
				cData += AEdef.CRLF;

			}
			return cData;
		}
		//----------------------------------------------------------
		public string MakeKeyDataBlind()
		{
			string cData = "";
			if (EmpyMode == EmptyCell.BlindsJpn)
			{
				cData += AEdef.Effects + AEdef.TAB + AEdef.BlindsJpn + AEdef.TAB + AEdef.BlindsJpnP + AEdef.CRLF;
			}
			else
			{
				cData += AEdef.Effects + AEdef.TAB + AEdef.BlindsEng + AEdef.TAB + AEdef.BlindsEngP + AEdef.CRLF;
			}
			cData += AEdef.TAB + AEdef.Frame + AEdef.TAB + AEdef.percent + AEdef.TAB + AEdef.CRLF;
			int l = m_empty.Count;
			if (l > 0)
			{
				for (int i = 0; i < l; i++)
				{
					string s = AEdef.TAB;
					s += IntToStr(m_empty[i].frame) + AEdef.TAB;
					if (m_empty[i].num == 0)
					{
						s += "100" + AEdef.TAB;
					}
					else
					{
						s += "0" + AEdef.TAB;
					}
					cData += s + AEdef.CRLF;
				}
				cData += AEdef.CRLF;

			}
			return cData;

		}
		//----------------------------------------------------------
		public string MakeKeyFrameData()
		{
			if (m_cells.Count <= 0) return "";
			
			string cData = "";
			cData += MakeHeader();

			switch (EmpyMode)
			{
				case EmptyCell.Opacity:
					cData += MakeKeyDataTimeRemap();
					cData += MakeKeyDataOpacity();
					break;
				case EmptyCell.BlindsJpn:
				case EmptyCell.BlindsEng:
					cData += MakeKeyDataBlind();
					cData += MakeKeyDataTimeRemap();
					break;
				case EmptyCell.LastFrame:
					cData += MakeKeyDataTimeRemap();
					break;
			}
			cData += AEdef.Footer + AEdef.CRLF;
			return cData;
		}
		//----------------------------------------------------------


	}
}
