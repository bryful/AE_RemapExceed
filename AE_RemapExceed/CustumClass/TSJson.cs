using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Codeplex.Data;

namespace AE_RemapExceed
{
    public class TSJson
    {
        //ardに使う文字定数
        public const string D_Header = "Header";
        public const string D_Header2= "ardjV2";
        public const string D_CellCount = "cellCount";
        public const string D_FrameCount = "frameCount";
        public const string D_PageSec = "pageSec";
        public const string D_FrameRate = "frameRate";
        public const string D_Caption = "caption";
        public const string D_Cells = "cells";
        public const string D_SheetName = "sheetName";

        public const string D_CREATE_USER = "CREATE_USER";
        public const string D_UPDATE_USER = "UPDATE_USER";
        public const string D_CREATE_TIME = "CREATE_TIME";
        public const string D_UPDATE_TIME = "UPDATE_TIME";
        public const string D_TITLE = "TITLE";
        public const string D_SUB_TITLE = "SUB_TITLE";
        public const string D_OPUS = "OPUS";
        public const string D_SCECNE = "SCECNE";
        public const string D_CUT = "CUT";
        public const string D_CAMPANY_NAME = "CAMPANY_NAME";

        //データクラス
        private TSData data = null;
        public TSJson(TSData d = null)
        {
            data = d;
        }
        //------------------------------------------------------------------------------------------
        public string ToJson()
        {
            string ret = "";
            if (data == null) return ret;
            dynamic d = new DynamicJson();
            d[D_Header] = D_Header2;
            d[D_CellCount] = (double)data.CellCount;
            d[D_FrameCount] = (double)data.FrameCount;
            d[D_PageSec] = (double)data.PageSec;
            d[D_FrameRate] = (double)data.FrameRate;
            d[D_SheetName] = data.SheetName;

            d[D_CREATE_USER] = data.CREATE_USER;
            d[D_UPDATE_USER] = data.UPDATE_USER;

            d[D_CREATE_TIME] = data.CREATE_TIME.ToString();
            d[D_UPDATE_TIME] = data.UPDATE_TIME.ToString();
            d[D_TITLE] = data.TITLE;
            d[D_SUB_TITLE] = data.SUB_TITLE;
            d[D_OPUS] = data.OPUS;
            d[D_SCECNE] = data.SCECNE;
            d[D_CUT] = data.CUT;
            d[D_CAMPANY_NAME] = data.CAMPANY_NAME;

            d[D_Caption] = data.GetCellCaption();

            d[D_Cells] = GetLayeDataAll();

            ret = d.ToString();

            return ret;
        }
        //------------------------------------------------------------------------------------------
        private object[] GetLayeData(int idx)
        {
            var ret = new object[0];
            if ((data == null) || (data.FrameCount <= 0)) return ret;
            if ((idx < 0) || (idx >= data.CellCount)) return ret;

            if (data.IsCellData(idx) == false)
            {
                ret = new object[1];

                ret[0] = new int[2] { 0, 0 };
                return ret;
            }
            int[] c = data.GetCellData(idx);

            //とりあえず2次元配列に
            List<List<int>> ary = new List<List<int>>();
            for ( int i=0; i<c.Length; i++)
            {
                List<int> a = new List<int>();
                a.Add(i);
                a.Add(data.GetCellData(idx, i));
                ary.Add(a);
            }
            //同じ値を消す
            for(int i = ary.Count-1; i >= 1; i--)
            {
                if (ary[i][1]== ary[i - 1][1]){
                    ary.RemoveAt(i);
                }
            }
            ret = new object[ary.Count];
            for ( int i=0; i< ary.Count;i++)
            {
                ret[i] = new int[2] { ary[i][0], ary[i][1] };
            }
            return ret;
        }
        //------------------------------------------------------------------------------------------
        private object[] GetLayeDataAll()
        {
            var ret = new object[0];
            if ((data == null) || (data.FrameCount <= 0)) return ret;
            ret = new object[data.CellCount];
            for ( int i=0; i<data.CellCount;i++)
            {
                ret[i] = GetLayeData(i);
            }
            return ret;
        }
        //------------------------------------------------------------------------------------------
        public bool SaveToFile(string p)
        {
            bool ret = false;
            try
            {
                if (File.Exists(p)) File.Delete(p);
                File.WriteAllText(p, ToJson(), Encoding.GetEncoding("utf-8"));
                ret = File.Exists(p);
                if (ret)
                {
                    data.SheetName = Path.GetFileNameWithoutExtension(p);
                    data.FileName = p;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        //------------------------------------------------------------------------------------------
        public bool LoadFromFile(string p)
        {
            bool ret = false;
            try
            {
                if (File.Exists(p) == true)
                {
                    string str = File.ReadAllText(p, Encoding.GetEncoding("utf-8"));

                    var json = DynamicJson.Parse(str);

                    if (json.IsDefined(D_Header) == true) if (json[D_Header] != D_Header2) return ret;
                    if (json.IsDefined(D_CellCount) == true) data.CellCount = (int)json[D_CellCount];
                    if (json.IsDefined(D_FrameCount) == true) data.FrameCount = (int)json[D_FrameCount];

                    data.SetSize(data.CellCount, data.FrameCount);

                    if (json.IsDefined(D_PageSec) == true) data.PageSec = (TSPageSec)json[D_PageSec];
                    if (json.IsDefined(D_FrameRate) == true) data.FrameRate = (TSFps)json[D_FrameRate];
                    if (json.IsDefined(D_SheetName) == true) data.SheetName = json[D_SheetName];

                    if (json.IsDefined(D_CREATE_USER) == true) data.CREATE_USER = json[D_CREATE_USER];
                    if (json.IsDefined(D_UPDATE_USER) == true) data.UPDATE_USER = json[D_UPDATE_USER];


                    if (json.IsDefined(D_CREATE_TIME) == true) data.CREATE_TIME = DateTime.Parse(json[D_CREATE_TIME]);
                    if (json.IsDefined(D_UPDATE_TIME) == true) data.UPDATE_TIME = DateTime.Parse(json[D_UPDATE_TIME]);

                    if (json.IsDefined(D_TITLE) == true) data.TITLE = json[D_TITLE];
                    if (json.IsDefined(D_SUB_TITLE) == true) data.SUB_TITLE = json[D_SUB_TITLE];
                    if (json.IsDefined(D_OPUS) == true) data.OPUS = json[D_OPUS];
                    if (json.IsDefined(D_SCECNE) == true) data.OPUS = json[D_SCECNE];
                    if (json.IsDefined(D_CUT) == true) data.CUT = json[D_CUT];
                    if (json.IsDefined(D_CAMPANY_NAME) == true) data.CAMPANY_NAME = json[D_CAMPANY_NAME];

                    if (json.IsDefined(D_Caption) == true)
                    {
                        var ary = json[D_Caption];
                        data.SetCellCaption((string[])ary);
                    }

                    if (json.IsDefined(D_Cells) == true)
                    {
                        int c = data.CellCount;
                        int f = data.FrameCount;
                        var cells = json[D_Cells];
                        for ( int j=0; j<c;j++)
                        {
                            var cell = cells[j];
                            for (int i = 0; i < f; i++)
                            {
                                data.SetCellData(j, i, (int)cell[i]);
                            }

                        }
                    }
                }

            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        //------------------------------------------------------------------------------------------
    }
}
