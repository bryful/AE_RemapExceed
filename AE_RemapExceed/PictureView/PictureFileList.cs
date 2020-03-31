using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AE_RemapExceed
{
    public class PictureFileList
    {
        private string m_Path = "";
        private string m_TargetFileName = "";
        private List<string> m_List = new List<string>();
        private int m_Index = 0;
 		//------------------------------------------------------------
		public PictureFileList()
		{
			m_Path = "";
			m_TargetFileName = "";
			m_Index = -1;
		}
		//------------------------------------------------------------
        public PictureFileList(string s)
        {
            GetPath(s);
        }
        //------------------------------------------------------------
        public void Clear()
        {
            m_Path = "";
            m_List.Clear();
        }
        //------------------------------------------------------------
        public string Path
        {
            get { return m_Path; }
            set { GetPath(value);}
        }
		//------------------------------------------------------------
		public int Count
		{
			get { return m_List.Count; }
		}
        //------------------------------------------------------------
        public string TargetFileName
        {
            get { return m_TargetFileName; }
            set 
			{
				GetPath(value);	
            }
        }
		//------------------------------------------------------------
		public string TargetFileNameFull
		{
			get 
			{
				if (m_TargetFileName != "")
				{
					return System.IO.Path.Combine(m_Path, m_TargetFileName);
				}
				else
				{
					return "";
				}
			}
		}
        //------------------------------------------------------------
        public List<string> FileList
        {
            get { return m_List; }
        }
        //------------------------------------------------------------
        public int Index
        {
            get
            {
                if (m_List.Count > 0)
                {
                    return m_Index;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if ((value >= 0) && (value < m_List.Count))
                {
                    m_Index = value;
                    m_TargetFileName = m_List[value];
                }
            }
        }
        //------------------------------------------------------------
        public int GetIndex(string p)
        {
            int ret = -1;
            if (m_List.Count <= 0) return ret;
            string n = System.IO.Path.GetFileName(p);
            for (int i = 0; i < m_List.Count; i++)
            {
                if (string.Compare(n, m_List[i], true) == 0)
                {
                    ret = i;
                    break;
                }
            }
 
            return ret;
        }
        //------------------------------------------------------------
        public bool GetFileList(string pp)
        {
            if (System.IO.Directory.Exists(pp) == false)
            {
                Clear();
                return false;
            }
            string[] sa = System.IO.Directory.GetFiles(pp);
            m_List.Clear();
            if (sa.Length <= 0) return false;
            foreach (string s in sa)
            {
                string e = System.IO.Path.GetExtension(s);
                if (
                    (string.Compare(e, ".tga", true) == 0)
                    || (string.Compare(e, ".jpg", true) == 0)
                    || (string.Compare(e, ".jpeg", true) == 0)
                    || (string.Compare(e, ".png", true) == 0)
                    || (string.Compare(e, ".tiff", true) == 0))
                {
                    m_List.Add(System.IO.Path.GetFileName(s));
                }
            }
            if (m_List.Count <= 0)
            {
                Clear();
                return false;
            }
            m_Index = -1;
			if (m_TargetFileName != "")
			{
				m_Index = GetIndex(m_TargetFileName);
				if (m_Index == -1) { m_TargetFileName = ""; }

			}
			else
			{
				m_Index = 0;
				m_TargetFileName = m_List[0];
			}
            m_Path = pp;
            return true;
        }
        //------------------------------------------------------------
        public bool GetPath(string p)
        {
            Clear();
            string n = "";
            if (System.IO.File.Exists(p) == true)
            {
                n = System.IO.Path.GetDirectoryName(p);
                m_TargetFileName = System.IO.Path.GetFileName(p);
            }
            else if (System.IO.Directory.Exists(p) == true)
            {
                n = p;
                m_TargetFileName = "";
            }
            else
            {
                return false;
            }
            return GetFileList(n);
        }
        //------------------------------------------------------------
    }
}
