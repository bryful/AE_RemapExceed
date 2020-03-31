using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AE_RemapExceed
{
	public class TSHistory
	{
		private const string Header = "AE_Remap History";
		private const string UserFolderName = "Prefs";
		private const string m_HisFileName = "AE_Remap.his";
		public string m_UserPath = "";
		string[] Tags = new string[]
			{
				"*Title",
				"*SubTitle",
				"*OPUS",
				"*SCECNE",
				"*CutNo",
				"*CREATE_USER",
				"*UPDATE_USER",
				"*CAMPANY_NAME"
			};

		private ComboBox[] cmbTbl = new ComboBox[(int)SheetInfo.Count];
		private CheckBox[] cbTbl = new CheckBox[(int)SheetInfo.Count];

		private TSData tsd;


		//----------------------------------------------------
		public TSHistory()
		{
			ChkUserFolder();
		}
		//----------------------------------------------------
		public void ChkUserFolder()
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
		public string HisFileName
		{
			get
			{
				if (m_UserPath == "") { return ""; }
				else
				{
					return Path.Combine(m_UserPath, m_HisFileName);
				}
			}
		}	
		//----------------------------------------------------
		private string getCombText(ComboBox cmb)
		{
			if (cmb != null)
			{
				return cmb.Text;
			}
			else
			{
				return string.Empty;
			}

		}
		//----------------------------------------------------
		private void setCombText(ComboBox cmb, string s)
		{
			if (cmb != null)
			{
				cmb.Text = s;
			}
		}
		//----------------------------------------------------
		public void SetCheckBox(SheetInfo si, CheckBox cb)
		{
			cbTbl[(int)si] = cb;
		}
		//----------------------------------------------------
		public void SetComboBox(SheetInfo si, ComboBox cmb)
		{
			cmbTbl[(int)si] = cmb;
		}
		//----------------------------------------------------
		public TSData TSData
		{
			get { return tsd; }
			set { tsd = value; }
		}
		//----------------------------------------------------
		public void AddComb(ComboBox cmb, string s)
		{
			AddCompItems(cmb, s, false);
		}
		//----------------------------------------------------
		public void AddCombWith(ComboBox cmb, string s)
		{
			AddCompItems(cmb, s, true);
		}
		//----------------------------------------------------
		public void AddCompItems(ComboBox cmb, string s, bool sv)
		{
			string ss = s.Trim();
			if (ss == "")
				return;
			if (cmb.Items.Count == 0)
			{
				cmb.Items.Add(ss);
				if (sv == true)
				{
					cmb.Text = ss;
					//cmb.SelectedIndex = 0;
				}
			}
			else
			{
				int si = -1;
				for (int i = 0; i < cmb.Items.Count; i++)
				{
					if (cmb.Items[i].ToString() == ss)
					{
						si = i;
						break;
					}
				}
				if (si >= 0)
				{
					cmb.Items.RemoveAt(si);
				}
				cmb.Items.Insert(0, ss);
				if (sv == true)
				{
					cmb.SelectedIndex = 0;
					//cmb.Text = ss;
				}
			}

		}
		//----------------------------------------------------
		public string GetCombItemStrings(ComboBox cmb,string tag)
		{
			string s = string.Empty;
			if ((cmb == null) || (tag == "")) { return s; }

			s += tag + "\n";
			
			if (cmb.Text != string.Empty)
			{
				AddCompItems(cmb, cmb.Text, true);
			}

			int cnt = cmb.Items.Count;
			if (cnt > 0)
			{
				for (int i = 0; i < cnt; i++)
				{
					s += cmb.Items[i].ToString() +"\n";
				}
			}

			return s;
		}
       //---------------------------------------------------------------------
		public void SaveHistory()
		{
			string s = Header + "\n";
			for (int i = 0; i < (int)SheetInfo.Count; i++)
			{
				s += GetCombItemStrings(cmbTbl[i], Tags[i]);
			}
			s += "*End\n";
			try
			{
				File.WriteAllText(HisFileName, s, Encoding.GetEncoding("utf-8"));
			}
			catch
			{
			}
		}
		//----------------------------------------------------
		private int FindTag(string[] lines, int idx, string tag)
		{
			int ret = -1;
			int cnt = lines.Length;
			if ((cnt <= 0) || (tag == "")) return ret;

			if ((idx < 0) || (idx >= cnt)) return ret;
			if (tag[0] != '*') tag = "*" + tag;

			for (int i = idx; i < cnt; i++)
			{
				if (string.Compare(lines[i].Trim(), tag) == 0)
				{
					return i + 1;
				}
			}

			return ret;
		}
		//---------------------------------------------------------------------
		private int NextTag(string[] lines, int idx)
		{
			int cnt = lines.Length;
			int ret = lines.Length - 1;
			if (cnt <= 0) return ret;

			if ((idx < 0) || (idx >= cnt)) return ret;

			for (int i = idx; i < cnt; i++)
			{
				string line = lines[i].Trim();
				if (line.Length > 0)
				{
					if (line[0] == '*')
					{
						return i - 1;
					}
				}
			}

			return ret;
		}
		//---------------------------------------------------------------------
		public List<string> GetItems(string[] lines, string tag)
		{
			List<string> ret = new List<string>();
			int idx0;
			int idx1;

			idx0 = FindTag(lines, 0, tag);
			if (idx0 < 0) return ret;
			idx1 = NextTag(lines, idx0);

			for (int i = idx0; i <= idx1; i++)
			{
				ret.Add(lines[i].Trim());
			}

			return ret;
		}
		//---------------------------------------------------------------------
		private void SetCompItems(ComboBox cmb, List<string> itm)
		{
			if ((cmb == null) || (itm.Count <= 0)) return;

			cmb.Items.Clear();
			for (int i = 0; i < itm.Count; i++)
			{
				string s = itm[i].Trim();
				if (s != "")
				{
					cmb.Items.Add(s);
				}
			}
		}		
		//---------------------------------------------------------------------
		public void LoadHistory()
		{
			string[] sa;
			try
			{
				sa = File.ReadAllLines(HisFileName, Encoding.GetEncoding("utf-8"));
				if (sa.Length <= 2) return;
				if (sa[0].Trim() != Header) return;
			}
			catch
			{
				return;
			}

			List<string> ret = new List<string>();

			for (int i = 0; i < (int)SheetInfo.Count; i++)
			{
				SetCompItems(cmbTbl[i], GetItems(sa, Tags[i]));
			}

		}
		//---------------------------------------------------------------------
		public void TSDataToComb()
		{
			if (tsd != null)
			{
				for ( int i = 0; i<(int)SheetInfo.Count; i++)
				{
					if (cmbTbl[i] != null)
					{
						cmbTbl[i].Text = tsd.GetInfo((SheetInfo)i);
					}
					if (cbTbl[i] != null)
					{
						cbTbl[i].Checked = tsd.GetIsPtintInfo((SheetInfo)i);
					}
				}
			}
		}
		//---------------------------------------------------------------------
		public void CombToData()
		{
			if (tsd != null)
			{
				for (int i = 0; i < (int)SheetInfo.Count; i++)
				{
					if (cmbTbl[i] != null)
					{
						
                        tsd.SetInfo((SheetInfo)i, cmbTbl[i].Text);
					}
					if (cbTbl[i] != null)
					{
						tsd.SetIsPtintInfo((SheetInfo)i, cbTbl[i].Checked);
					}
				}
			}
		}
		//---------------------------------------------------------------------
  
	}
}
