using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace AE_RemapExceed
{
	public class TSScript
	{
		private const string header = @"//JavaScript";
		private const string ScriptLayer = "AE_RemapLayer.jsx";
		private const string ScriptLayerAll = "AE_RemapLayerAll.jsx";
		private const string ScriptFolderName = "Scripts";

		//リソースからデータを読み取り
		//-------------------------------------------------------
		private TSGrid tsg;
		private TSData tsd;
		private TSSelection sel;
		private string m_ScriptFolderPath = "";
		private string m_ScriptLayerPath = "";
		private string m_ScriptLayerAllPath = "";


        public bool IsLoadScriptFile = true;
		//-------------------------------------------------------
		public TSScript(TSGrid g)
		{
			tsg = g;
			tsd = g.tsd;
			sel = g.sel;
			ChkScriotFolder();
		}
		//-------------------------------------------------------
		public string ScriptFolder
		{
			get { return m_ScriptFolderPath; }
		}
		//-------------------------------------------------------
		public TSGrid TSGrid
		{
			get { return tsg; }
			set
			{
				tsg = value;
				tsd = value.tsd;
				sel = value.sel;
			}
		}
		//----------------------------------------------------------------------
		public void ChkScriotFolder( )
		{
			string s = Path.GetDirectoryName(Application.ExecutablePath);
			string sf = Path.Combine(s, ScriptFolderName);
			if (Directory.Exists(sf) == false)
			{
				try
				{
					Directory.CreateDirectory(sf);
				}
				catch
				{
					return;
				}
			}
			m_ScriptFolderPath = sf;
			string p = Path.Combine(sf, ScriptLayer);
			if (File.Exists(p) == false)
			{
				ScriptSave(p,AE_RemapExceed.Properties.Resources.ScriptLayer);
			}
			m_ScriptLayerPath = p;
			p = Path.Combine(sf, ScriptLayerAll);
			if (File.Exists(p) == false)
			{
				ScriptSave(p, AE_RemapExceed.Properties.Resources.ScriptLayerAll);
			}
			m_ScriptLayerAllPath = p;
		}		

		//-------------------------------------------------------
		public void ScriptSave(string path, string s)
		{
			string h = s.Substring(0, header.Length);
			if (string.Compare(header, h, true) != 0)
			{
				s = header + "\n" + s;
			}

			File.WriteAllText(path, s, Encoding.GetEncoding("utf-8"));

		}
		//-------------------------------------------------------
		public string ScriptLoad(string path)
		{
			string ret = string.Empty;
			if (File.Exists(path) == false) { return ret; }
 			
			ret = File.ReadAllText(path, Encoding.GetEncoding("utf-8"));
			string h = ret.Substring(0, header.Length);
			if (string.Compare(header, h, true) != 0)
			{
				return string.Empty;
			}			
			return ret;
		}
		//-------------------------------------------------------
		public string MakeScriptLayer( )
		{
			if (tsg == null)
				return string.Empty;
            string ret = "";
            if (IsLoadScriptFile == true)
            {
                ret = ScriptLoad(m_ScriptLayerPath);
            }
            if (ret == "") { ret = AE_RemapExceed.Properties.Resources.ScriptLayer; }

			string oName = "RX";
			Regex r = new Regex("<RX>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, oName);

			r = new Regex("<frameCount>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, tsd.FrameCount.ToString());

			r = new Regex("<frameRate>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, ((int)tsd.FrameRate).ToString());

			r = new Regex("<caption>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, "\"" + tsd.CellCaption(tsg.sel.Index) + "\"");

			string cellData = "";
			string lineHead = "\t" + oName +".setKeyData(";
			string lineFoot = ");\n";

            if (tsd.IsCellDataEmpty(tsg.sel.Index) == false)
            {
                int[] c = tsd.GetCellDataTrue(tsg.sel.Index);
                if (c.Length > 0)
                {
                    cellData += lineHead + "0," + c[0].ToString() + lineFoot;
                    if (c.Length > 1)
                    {
                        for (int j = 1; j < c.Length; j++)
                        {
                            if (c[j - 1] != c[j])
                            {
                                cellData += lineHead + j.ToString() + "," + c[j].ToString() + lineFoot;
                            }
                        }
                    }
                }
            }else{
                return string.Empty;
            }
			r = new Regex("<cellData>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, cellData);

			lineHead = "\t" + oName + ".setMemo(";
			List<memoData> md = tsd.GetMemoDataTrue();
			string memoData = "";
			if (md.Count > 0)
			{

				for (int i = 0; i < md.Count; i++)
				{
					memoData += lineHead + md[i].Frame.ToString() + ",\"" + md[i].Memo + "\""+ lineFoot;
				}
			}
			r = new Regex("<memoData>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, memoData);

			return ret;
		}
		//-------------------------------------------------------
		public string MakeScriptAll()
		{
			if (tsg == null) return string.Empty;
            string ret = "";
            if (IsLoadScriptFile == true)
            {
                ret = ScriptLoad(m_ScriptLayerPath);
            }
            if (ret == "") { ret = AE_RemapExceed.Properties.Resources.ScriptLayerAll; }


			Regex r = new Regex("<cellIndex>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, sel.Index.ToString());

			r = new Regex("<cellCount>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, tsd.CellCount.ToString());

			r = new Regex("<frameCount>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, tsd.FrameCount.ToString());

			r = new Regex("<frameRate>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, ((int)tsd.FrameRate).ToString());

			string cellData = "";
			string lineHead = "setKeyData(";
			string lineFoot = ");\n";
			for (int i = 0; i < tsd.CellCount; i++)
			{

                if (tsd.IsCellDataEmpty(i) == false)
                {
                    int[] c = tsd.GetCellDataTrue(i);
                    if (c.Length > 0)
                    {
                        cellData += lineHead + i.ToString() + ",0," + c[0].ToString() + lineFoot;
                        for (int j = 1; j < c.Length; j++)
                        {
                            if (c[j - 1] != c[j])
                            {
                                cellData += lineHead + i.ToString() + "," + j.ToString() + "," + c[j].ToString() + lineFoot;
                            }
                        }
                    }
                }
			}
            if (cellData == "") { return string.Empty; }
			r = new Regex("<cellData>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, cellData);
			string cap = "";
			for (int i = 0; i < tsd.CellCount; i++)
			{
				cap += "setCaption(" + i.ToString() + ",\"" + tsd.CellCaption(i) + "\");\n";
			}
			r = new Regex("<cellCaptionData>", RegexOptions.IgnoreCase);
			ret = r.Replace(ret, cap);
			return ret;
		}

		//-------------------------------------------------------
        public bool layerToClipboard()
		{
			string s = MakeScriptLayer();
            if (s != "")
            {
                Clipboard.SetText(s);
                return true;
            }
            else
            {
                return false;
            }
		}
        //-------------------------------------------------------
        public bool layerAllToClipboard()
        {
            string s = MakeScriptAll();
            if (s != "")
            {
                Clipboard.SetText(s);
                return true;
            }
            else
            {
                return false;
            }
        }
        //-------------------------------------------------------
		public bool layerSaveToFile(string path)
		{
			string s = MakeScriptLayer();
			//書き込み先のテキストファイル
			if (s != "")
			{
				System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");

				System.IO.File.WriteAllText(path, s, enc);
				return true;
			}
			else
			{
				return false;
			}
		}
        //-------------------------------------------------------
        public bool layerAllSaveToFile(string path)
        {
            string s = MakeScriptAll();
			if (s != "")
			{
				//書き込み先のテキストファイル
				System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");

				System.IO.File.WriteAllText(path, s, enc);
				return true;
			}
			else
			{
				return false;
			}

        }
        //-------------------------------------------------------

	}
}
