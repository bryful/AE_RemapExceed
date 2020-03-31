using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AE_RemapExceed
{
  /*
   * 		//関連付ける拡張子
			string extension = boldMake.Properties.Resources.SaveExt;
      string extension2 = boldMake.Properties.Resources.BackupFileExt;
      
      //実行するコマンドライン
			string commandline = "\"" + Application.ExecutablePath + "\" %1";
			//ファイルタイプ名
			string fileType = Application.ProductName;
      string fileType2 = Application.ProductName+" BackupFile";
      //説明（必要なし）
			string description = "ボールドデータファイル";
      string description2 = "ボールドバックアップファイル";
      //動詞
			string verb = "open";
			//動詞の説明（エクスプローラのコンテキストメニューに表示される）
			//（必要なし）
			string verb_description = boldMake.Properties.Resources.AppName+"で開く(&O)";
			//アイコンのパスとインデックス
			string iconPath = Application.ExecutablePath;

   */
  public class Extention
  {
    private string m_ext ="";
    private string m_fileType ="";
    private string m_description ="";
    private int m_iconIndex = 1;
    public string ext
    {
      get { return m_ext; }
      set
      {
        string s = value;
        m_ext = "";
        if (s == "") return;
        if (s[0] != '.') s = '.' + s;
        m_ext = s;
      }
    }
    public string fileType
    {
      get { return m_fileType; }
      set { m_fileType = value; }
    }
    public string description
    {
      get { return m_description; }
      set { m_description = value; }
    }
    public int iconIndex
    {
      get { return m_iconIndex; }
      set { m_iconIndex = value; }
    }

  }

  public class ExtentionSetup
  {
    private string commandline = "\"" + Application.ExecutablePath + "\" \"%1\"";
    private string verb = "open";
    private string verb_description = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + "で開く(&O)";
    private string iconPath = Application.ExecutablePath;
    
    private List<Extention> m_docExtentions = new List<Extention>();

    [DllImport("shell32.dll")]
    private extern static void SHChangeNotify(int wEventIs, int uFlags, int dwItem1, int dwItem2);
    //--------------------------------------------
    public ExtentionSetup()
    {
    }
    //--------------------------------------------
    public int Count
    {
      get { return m_docExtentions.Count; }
    }
    //--------------------------------------------
    private int FindExt(string ext)
    {
      int r = -1;
      if (m_docExtentions.Count <= 0) return r;

      string e = ext.ToUpper();
      for (int i = 0; i < m_docExtentions.Count; i++)
      {
        if (m_docExtentions[i].ext.ToUpper() == e)
        {
          r = i;
          return r;
        }
      }
      return r;
    }
    //--------------------------------------------
    private void Append(Extention ex)
    {

      Extention e = new Extention();
      e.ext = ex.ext;
      e.fileType = ex.fileType;
      e.description = ex.description;
      e.iconIndex = ex.iconIndex;
      m_docExtentions.Add(e);
    }
    //--------------------------------------------
    public bool Add(Extention ex)
    {
      bool err = false;
      if (ex.ext == "") err = true;
      if (ex.fileType == "") err = true;
      if (ex.description == "") err = true;
      if (err == true) return false;
      if (m_docExtentions.Count <= 0)
      {
        Append(ex);
      }
      else
      {
        int idx = FindExt(ex.ext);
        if (idx >= 0)
        {
          m_docExtentions[idx].ext = ex.ext;
          m_docExtentions[idx].fileType = ex.fileType;
          m_docExtentions[idx].description = ex.description;
          m_docExtentions[idx].iconIndex = ex.iconIndex;
        }
        else
        {
          Append(ex);
        }

      }
      return true;
    }
    //--------------------------------------------
    private void instExtention(Extention ex)
    {
      //ファイルタイプを登録
      Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(ex.ext);
      regkey.SetValue("", ex.fileType);
      regkey.Close();

      //ファイルタイプとその説明を登録
      Microsoft.Win32.RegistryKey shellkey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(ex.fileType);
      shellkey.SetValue("", ex.description);

      //動詞とその説明を登録
      shellkey = shellkey.CreateSubKey("shell\\" + verb);
      shellkey.SetValue("", verb_description);

      //コマンドラインを登録
      shellkey = shellkey.CreateSubKey("command");
      shellkey.SetValue("", commandline);
      shellkey.Close();

      //アイコンの登録
      Microsoft.Win32.RegistryKey iconkey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(ex.fileType + "\\DefaultIcon");
      iconkey.SetValue("", iconPath + "," + ex.iconIndex.ToString());
      iconkey.Close();
    }
    //--------------------------------------------
    private void uninstExtention(Extention ex)
    {

      Microsoft.Win32.RegistryKey regkey1 = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ex.ext, true);
      if (regkey1 != null)
        Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(ex.ext);

      Microsoft.Win32.RegistryKey regkey2 = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ex.fileType, true);
      if (regkey2 != null)
        Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(ex.fileType);
    }
    //--------------------------------------------
    public void Inst()
    {
      if (m_docExtentions.Count > 0)
      {
        for (int i = 0; i < m_docExtentions.Count; i++)
        {
          instExtention(m_docExtentions[i]);
        }
      }
       SHChangeNotify(0x8000000, 0x1000, 0, 0);
    }
    //--------------------------------------------
    public void Clear()
    {
      m_docExtentions.Clear();
    }
    //--------------------------------------------
    public void Uninst()
    {
      if (m_docExtentions.Count > 0)
      {
        for (int i = 0; i < m_docExtentions.Count; i++)
        {
          uninstExtention(m_docExtentions[i]);
        }
      }
       SHChangeNotify(0x8000000, 0x1000, 0, 0);
    }
    //--------------------------------------------
  }
}
