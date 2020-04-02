using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapExceed
{
    //---------------------------------------------
    public class Ard_prms
    {
        private string tag = "";
        private string value = "";
        public Ard_prms()
        {
        }
        public Ard_prms(string t, string v)
        {
            tag = t;
            value = v;
        }
        public string Value
        {
            get { return value; }
            set { value = this.value; }
        }
        public String Tag
        {
            get { return tag; }
            set { SetTag(value); }
        }
        public void SetTag(string s)
        {
            string ss = s.Trim();
            if (ss != "")
            {
                if (ss[0] == '*')
                {
                    ss = ss.Substring(1).Trim();
                }
            }
            tag = ss;
        }
        public bool CompareTag(string s)
        {
            return (string.Compare(s, this.tag, true) == 0);
        }
        public bool CompareTag(TSParams.N n)
        {
            TSParams p = new TSParams();

            return (string.Compare(p.Tag(n), this.tag, true) == 0);
        }
        public int GetValueInt(int def)
        {
            int v;

            if (Int32.TryParse(value, out v))
            {

                return v;
            }
            else
            {
                return def;
            }
        }
        public float GetValueFloat(float def)
        {
            float v;

            if (float.TryParse(value, out v))
            {

                return v;
            }
            else
            {
                return def;
            }
        }
        public bool GetValueBool(bool def)
        {
            bool v;

            if (bool.TryParse(value, out v))
            {

                return v;
            }
            else
            {
                return def;
            }

        }
 
    }
}
