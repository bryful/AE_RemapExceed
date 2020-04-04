using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

using System.IO;

using Codeplex.Data;

namespace AE_RemapExceed
{
	public class Ardj
	{
		public string header { get; set; }
		public int cellCount { get; set; }
		public int frameCount { get; set; }
		public int pageSec { get; set; }
		public int frameRate { get; set; }
		public string sheetName { get; set; }
		public string CREATE_USER { get; set; }
		public string UPDATE_USER { get; set; }
		public DateTime CREATE_TIME { get; set; }
		public DateTime UPDATE_TIME { get; set; }

		public string TITLE { get; set; }
		public string SUB_TITLE { get; set; }
		public string OPUS { get; set; }
		public string SCECNE { get; set; }
		public string CUT { get; set; }
		public string CAMPANY_NAME { get; set; }

		public string[] caption { get; set; }
		public int[][][] cell { get; set; }
	}
	public class Ardj_layer
	{
		public string header { get; set; }
		public int frameCount { get; set; }
		public int frameRate { get; set; }
		public int[][] cell { get; set; }
	}

	public class TSJson
	{
		//ardに使う文字定数
		public const string D_Header = "ardjV2";

		//データクラス
		private TSData data = null;
		public TSJson(TSData d = null)
		{
			data = d;
		}
		//
		public string ToJson()
		{
			dynamic ardj = new DynamicJson();
			ardj.header = D_Header;
			ardj.cellCount = data.CellCount;
			ardj.frameCount = data.FrameCount;
			ardj.pageSec = (int)data.PageSec;
			ardj.frameRate = (int)data.FrameRate;
			ardj.sheetName = data.SheetName;

			ardj.CREATE_USER = data.CREATE_USER;
			ardj.UPDATE_USER = data.UPDATE_USER;
			ardj.CREATE_TIME = data.CREATE_TIME;
			ardj.UPDATE_TIME = data.UPDATE_TIME;
			ardj.TITLE = data.TITLE;
			ardj.SUB_TITLE = data.SUB_TITLE;
			ardj.OPUS = data.OPUS;
			ardj.SCECNE = data.SCECNE;
			ardj.CUT = data.CUT;
			ardj.CAMPANY_NAME = data.CAMPANY_NAME;
			ardj.caption = data.GetCellCaption();

			int[][][] cc = new int[data.CellCount][][];

			for (int i = 0; i < data.CellCount; i++)
			{
				List<List<int>> a = data.CellLayerT(i);
				cc[i] = new int[a.Count][];
				for (int j = 0; j < a.Count; j++)
				{
					cc[i][j] = new int[2];
					cc[i][j][0] = a[j][0];
					cc[i][j][1] = a[j][1];

				}
			}
			ardj.cell = cc;
			//return DynamicJson.Serialize(ardj);
			return ardj.ToString();

		}
		public string ToJson2()
		{
			Ardj ardj = new Ardj();
			ardj.header = D_Header;
			ardj.cellCount = data.CellCount;
			ardj.frameCount = data.FrameCount;
			ardj.pageSec = (int)data.PageSec;
			ardj.frameRate = (int)data.FrameRate;
			ardj.sheetName = data.SheetName;

			ardj.CREATE_USER = data.CREATE_USER;
			ardj.UPDATE_USER = data.UPDATE_USER;
			ardj.CREATE_TIME = data.CREATE_TIME;
			ardj.UPDATE_TIME = data.UPDATE_TIME;
			ardj.TITLE = data.TITLE;
			ardj.SUB_TITLE = data.SUB_TITLE;
			ardj.OPUS = data.OPUS;
			ardj.SCECNE = data.SCECNE;
			ardj.CUT = data.CUT;
			ardj.CAMPANY_NAME = data.CAMPANY_NAME;
			ardj.caption = data.GetCellCaption();


			int[][][] cc = new int[data.CellCount][][];

			for (int i = 0; i < data.CellCount; i++)
			{
				List<List<int>> a = data.CellLayerT(i);
				cc[i] = new int[a.Count][];
				for (int j = 0; j < a.Count; j++)
				{
					cc[i][j] = new int[2];
					cc[i][j][0] = a[j][0];
					cc[i][j][1] = a[j][1];

				}
			}
			ardj.cell = cc;

			return JsonSerializer.Serialize(ardj);


		}
		//------------------------------------------------------------------------------------------
		public string ToJson_Layer(int idx)
		{
			dynamic ardj = new DynamicJson();
			ardj.header = D_Header;
			ardj.frameCount = data.FrameCount;
			ardj.frameRate = (int)data.FrameRate;

			int[][] cc = new int[data.FrameCount][];

			List<List<int>> a = data.CellLayerT(idx);
			cc = new int[a.Count][];
			for (int j = 0; j < a.Count; j++)
			{
				cc[j] = new int[2];
				cc[j][0] = a[j][0];
				cc[j][1] = a[j][1];

			}
			ardj.cell = cc;

			return ardj.ToSting();


		}       //------------------------------------------------------------------------------------------
		public string ToJson_Layer2(int idx)
		{
			Ardj_layer ardj = new Ardj_layer();
			ardj.header = D_Header;
			ardj.frameCount = data.FrameCount;
			ardj.frameRate = (int)data.FrameRate;

			int[][] cc = new int[data.FrameCount][];

			List<List<int>> a = data.CellLayerT(idx);
			cc = new int[a.Count][];
			for (int j = 0; j < a.Count; j++)
			{
				cc[j] = new int[2];
				cc[j][0] = a[j][0];
				cc[j][1] = a[j][1];

			}
			ardj.cell = cc;

			return JsonSerializer.Serialize(ardj);


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
		public bool LayerSaveToFile(string p,int idx)
		{
			bool ret = false;
			try
			{
				if (File.Exists(p)) File.Delete(p);
				File.WriteAllText(p, ToJson_Layer(idx), Encoding.GetEncoding("utf-8"));
				ret = File.Exists(p);
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
			if (data == null) { return false; }
			try
			{
				if (File.Exists(p) == true)
				{
					string str = File.ReadAllText(p, Encoding.GetEncoding("utf-8"));


					Ardj ardj = JsonSerializer.Deserialize<Ardj>(str);

					if (ardj.header != D_Header) return ret;
					if (ardj.cellCount < 6) return ret;
					if (ardj.frameCount < 6) return ret;
					if (ardj.caption.Length != ardj.cellCount) return ret;
					if (ardj.cell.Length != ardj.cellCount) return ret;

					data.ClearDellData();

					int c = ardj.cellCount;
					int f = ardj.frameCount;
					data.SetSize(c, f);
					data.PageSec = (TSPageSec)ardj.pageSec;
					data.FrameRate = (TSFps)ardj.frameRate;
					data.SheetName = ardj.sheetName;

					data.CREATE_USER = ardj.CREATE_USER;
					data.UPDATE_USER = ardj.UPDATE_USER;
					data.CREATE_TIME = ardj.CREATE_TIME;
					data.UPDATE_TIME = ardj.UPDATE_TIME;
					data.TITLE = ardj.TITLE;
					data.SUB_TITLE = ardj.SUB_TITLE;
					data.OPUS = ardj.OPUS;
					data.SCECNE = ardj.SCECNE;
					data.CUT = ardj.CUT;
					data.CAMPANY_NAME = ardj.CAMPANY_NAME;

					data.SetCellCaption(ardj.caption);

					for (int i = 0; i < c; i++)
					{
						int[] ary = new int[f];
						for (int j = 0; j < f; j++) ary[j] = -100;
						int ll = ardj.cell[i].Length;
						for (int j = 0; j < ll; j++)
						{
							ary[ardj.cell[i][j][0]] = ardj.cell[i][j][1];
						}

						if (ary[0] <= -100) ary[0] = 0;
						for (int j = 1; j < f; j++)
						{
							if (ary[j] <= -100)
							{
								ary[j] = ary[j - 1];
							}
						}
						data.SetCellLayer(i, ary);

					}

					ret = true;
				}

			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		//------------------------------------------------------------------------------------------
		//------------------------------------------------------------------------------------------
		public bool LayerLoadFromFile(string p, int idx)
		{
			bool ret = false;
			if (data == null) { return false; }
			try
			{
				if (File.Exists(p) == true)
				{
					string str = File.ReadAllText(p, Encoding.GetEncoding("utf-8"));


					Ardj_layer ardj = JsonSerializer.Deserialize<Ardj_layer>(str);

					if (ardj.header != D_Header) return ret;
					if (ardj.frameCount < 6) return ret;

					int f = ardj.frameCount;


					if (data.FrameCount < f)
					{
						data.SetSize(data.CellCount, f);
					}

					int[] ary = new int[f];
					for (int j = 0; j < f; j++) ary[j] = -100;
					int ll = ardj.cell.Length;
					for (int j = 0; j < ll; j++)
					{
						ary[ardj.cell[j][0]] = ardj.cell[j][1];
					}

					if (ary[0] <= -100) ary[0] = 0;
					for (int j = 1; j < f; j++)
					{
						if (ary[j] <= -100)
						{
							ary[j] = ary[j - 1];
						}
					}
					data.SetCellLayer(idx, ary);


					ret = true;
				}

			}
			catch
			{
				ret = false;
			}
			return ret;
		}
		//------------------------------------------------------------------------------------------   }
	}
}