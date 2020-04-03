/*--------------------------------------------------------------------------
* 
* このコードはDynamicJsonを使用しています。
* 
* DynamicJson
* ver 1.2.0.0 (May. 21th, 2010)
*
* created and maintained by neuecc <ils@neue.cc>
* licensed under Microsoft Public License(Ms-PL)
* http://neue.cc/
* http://dynamicjson.codeplex.com/
*--------------------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

using Codeplex.Data;
using System.Collections;

namespace BRY
{
    public class JsonPref
    {
        private string _filePath = "";
        dynamic json = new DynamicJson();
        public JsonPref(string appName = "")
        {
            if (appName == "") appName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            _filePath = Path.Combine(Application.UserAppDataPath, appName + ".json");
        }
        public override string ToString()
        {
            return json.ToString();
        }
        public string ToJson()
        {
            return json.ToString();
        }
        public void Parse(string js)
        {
            json = DynamicJson.Parse(js);
        }
        //----------------------------------------------------------------
        /// <summary>
        /// 保存する
        /// </summary>
        /// <param name="p">保存パス</param>
        /// <returns></returns>
        public bool Save(string p)
        {
            bool ret = false;

            try
            {
                string js = json.ToString();
                File.WriteAllText(p, js, Encoding.GetEncoding("utf-8"));
                ret = true;
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        /// <summary>
        /// デフォルトのパスに保存
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return Save(_filePath);
        }
        /// <summary>
        /// 指定したパスを読み込む
        /// </summary>
        /// <param name="p">読み込むパス</param>
        /// <returns></returns>
        public bool Load(string p)
        {
            bool ret = false;

            try
            {
                if (File.Exists(p) == true)
                {
                    string str = File.ReadAllText(p, Encoding.GetEncoding("utf-8"));
                    if (str != "")
                    {
                        json = DynamicJson.Parse(str);
                        ret = true;
                    }
                }
            }
            catch
            {
                json = new DynamicJson();
                ret = false;
            }
            return ret;
        }
        /// <summary>
        /// デフォルトのパスを読み込む
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            return Load(_filePath);
        }
        //----------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetBool(string key, bool value)
        {
            json[key] = (bool)value;
        }
        public bool GetBool(string key, out bool ok)
        {
            bool ret = false;
            ok = false;
            if (json.IsDefined(key) == true)
            {
                ret = (bool)json[key];
                ok = true;
            }
            return ret;
        }
        public bool TryGetBool(string key, ref bool value)
        {
            bool ret = false;
            if (json.IsDefined(key) == true)
            {
                value = (bool)json[key];
                ret = true;
            }
            return ret;
        }
        //--------------------------------------------------
        public void SetInt(string key, int value)
        {
            json[key] = (double)value;
        }
        public int GetInt(string key, out bool ok)
        {
            int ret = 0;
            ok = false;
            if (json.IsDefined(key) == true)
            {
                ret = (int)json[key];
                ok = true;
            }
            return ret;
        }
        public bool TryGetInt(string key, ref int value)
        {
            bool ret = false;
            if (json.IsDefined(key) == true)
            {
                value = (int)json[key];
                ret = true;
            }
            return ret;
        }
        //--------------------------------------------------
        public void SetDouble(string key, double value)
        {
            json[key] = (double)value;
        }
        public double GetDouble(string key, out bool ok)
        {
            double ret = 0;
            ok = false;
            if (json.IsDefined(key) == true)
            {
                ret = (double)json[key];
                ok = true;
            }
            return ret;
        }
        //--------------------------------------------------
        public void SetString(string key, string value)
        {
            json[key] = (string)value;
        }
        public string GetString(string key, out bool ok)
        {
            string ret = "";
            ok = false;
            if (json.IsDefined(key) == true)
            {
                ret = (string)json[key];
                ok = true;
            }
            return ret;
        }
        //-********************************************************************************
        public void SetBoolArray(string key, bool[] value)
        {
            json[key] = value;
        }
        public bool[] GetBoolArray(string key, out bool ok)
        {
            bool[] ret = new bool[0];
            ok = false;
            if (json.IsDefined(key) == true)
            {
                if (json[key].IsArray)
                {
                    ret = (bool[])json[key];
                    ok = true;
                }
            }
            return ret;
        }
        //-********************************************************************************
        public void SetIntArray(string key, int[] value)
        {
            json[key] = value;
        }
        public int[] GetIntArray(string key, out bool ok)
        {
            int[] ret = new int[0];
            ok = false;
            if (json.IsDefined(key) == true)
            {
                if (json[key].IsArray)
                {
                    ret = (int[])json[key];
                    ok = true;
                }
            }
            return ret;
        }
        //-********************************************************************************
        public void SetColorArray(string key, Color[] value)
        {
            int[] v = new int[value.Length];
            for ( int i=0; i< value.Length; i++)
            {
                v[i] = value[i].ToArgb();
            }
            json[key] = v;
        }
        public Color [] GetColorArray(string key, out bool ok)
        {
            Color[] ret = new Color[0];
            ok = false;
            if (json.IsDefined(key) == true)
            {
                if (json[key].IsArray)
                {
                    int[] a = (int[])json[key];
                    if(a.Length>0)
                    {
                        ret = new Color[a.Length];
                        for (int i=0; i< a.Length;i++)
                        {
                            ret[i] = Color.FromArgb(a[i]);
                        }
                    }

                    ok = true;
                }
            }
            return ret;
        }
        //-********************************************************************************
        public void SetDoubleArray(string key, double[] value)
        {
            json[key] = value;
        }
        public double[] GetDoubleArray(string key, out bool ok)
        {
            double[] ret = new double[0];
            ok = false;
            if (json.IsDefined(key) == true)
            {
                if (json[key].IsArray)
                {
                    ret = (double[])json[key];
                    ok = true;
                }
            }
            return ret;
        }
        //-********************************************************************************
        public void SetObject(string key, object value)
        {
            json[key] = value;
        }
        public object GetObject(string key, out bool ok)
        {
            var ret = new object();
            ok = false;
            if (json.IsDefined(key) == true) { 
                ret = json[key];
                ok = true;
            }
            return ret;
        }
        public object[] GetObjectArray(string key, out bool ok)
        {
            var ret = new object[0];
            ok = false;
            if (json.IsDefined(key) == true)
            {
                if (json[key].IsArray)
                {
                    ret = json[key];
                    ok = true;
                }
            }
            return ret;
        }
        //-********************************************************************************
        public void SetStringArray(string key, string[] value)
        {
            json[key] = value;
        }
        public string[] GetStringArray(string key, out bool ok)
        {
            string[] ret = new string[0];
            ok = false;
            if (json.IsDefined(key) == true)
            {
                if (json[key].IsArray)
                {
                    ret = json[key];
                    ok = true;
                }
            }
            return ret;
        }

        //-********************************************************************************
        public void SetSize(string key, Size sz)
        {
            var s = new { Width = sz.Width, Height = sz.Height };
            json[key] = s;
        }
        public Size GetSize(string key, out bool ok)
        {
            ok = false;
            Size ret = new Size(0, 0);
            ok = false;
            if (json.IsDefined(key) == true)
            {
                var a = json[key];
                if ((a.IsDefined("Width") == true) && (a.IsDefined("Height") == true))
                {
                    ret.Width = (int)((dynamic)json[key].Width);
                    ret.Height = (int)((dynamic)json[key].Height);
                    ok = true;
                }
            }
            return ret;
        }
        //-********************************************************************************
        public void SetPoint(string key, Point p)
        {
            var s = new { X = p.X, Y = p.Y };
            json[key] = s;
        }
        public Point GetPoint(string key, out bool ok)
        {
            ok = false;
            Point ret = new Point(0, 0);
            if (json.IsDefined(key) == true)
            {
                var a = json[key];
                if ((a.IsDefined("X") == true) && (a.IsDefined("Y")))
                {
                    ret.X = (int)((dynamic)json[key].X);
                    ret.Y = (int)((dynamic)json[key].Y);
                    ok = true;
                }
            }
            return ret;
        }

    }
}
