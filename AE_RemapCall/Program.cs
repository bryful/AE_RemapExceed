using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

using AE_Remote;

using System.IO;
namespace AE_RemapExceed
{
    class Program
    {
		public enum EXEC_MODE
		{
			NONE = 0,
			EXPORT,
			EXPORT_LAYER,
			IMPORT_LAYER,
			LOAD,
			ACTIVE,
			CALL,
			EXENOW,         //AEが起動しているか確認する。True/Falseの文字が戻る
            SCREEN_CENTER,
			HELP            //実装していない
		}
		//Path文字をJavScript形式へ
		static string ToJSP(string p)
        {
            p = p.Replace('\\', '/');
            if (p.Length > 2)
            {
                if ((p[1] == ':') && (p[2] == '/'))
                {
                    //c:\aaa
                    //012345
                    p = "/" + p[0] + "/" + p.Substring(3);
                }
            }
            return p;
        }
        //ちょっと待つ
        static private async void SleepAsync()
        {
            await Task.Delay(1000);
        }
        //
        static void Main(string[] args)
        {
			EXEC_MODE mode = EXEC_MODE.NONE;
            string filename = "";


			//最初に起動しているか調べる
			bool IsExecAE = (Process.GetProcessesByName("AE_RemapExceed").Length > 0);


			if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    char c = args[i][0];
                    if ((c == '-') || (c == '/'))
                    {
                        string s = args[i].Substring(1).ToUpper();
                        switch (s)
                        {
                            case "EXENOW":
                                if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.EXENOW;
                                break;
							case "CALL":
								if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.CALL;
								break;
							case "ACTIVE":
								if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.ACTIVE;
								break;
							case "EXPORT":
                                if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.EXPORT;
                                break;
                            case "EXPORT_LAYER":
                                if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.EXPORT_LAYER;
                                break;
                            case "IMPORT_LAYER":
                                if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.IMPORT_LAYER;
                                break;
                            case "SCREEN_CENTER":
                                if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.SCREEN_CENTER;
                                break;
                            default:
                                if (mode == EXEC_MODE.NONE) mode = EXEC_MODE.HELP;
                                break;
                        }
                    }
                    else
                    {
                        if (filename == "") {
                            if (File.Exists(args[i])==true)
                            {
                                filename = args[i];
                            }
                        }
                    }
                }
            }
			//何も引数がないのなら終わる
			if (mode == EXEC_MODE.NONE)
			{
				return;
			}


			if (mode == EXEC_MODE.HELP)
            {
                return;
            }
            else if (mode == EXEC_MODE.EXENOW)
            {
                Console.Write(String.Format("{0}", IsExecAE).ToLower());
                return;
            }
            else if (mode == EXEC_MODE.CALL)
			{
				if (IsExecAE == false)
				{
					string p = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AE_RemapExceed.exe");
					if (File.Exists(p) == false)
					{
						Console.Write("errer no AE_RemapExceed.exe");
						return;
					}
					else
					{
						Process.Start(p);
					}
				}
				return;
			}
			else
			{
				if (IsExecAE == false)
				{
					Console.Write("errer AE_remap execute");
					return;
				}

				//プロセスで制御
				AE_RemoteInfo m_msg = null;
                try
                {
                    IpcClientChannel clientChannel = new IpcClientChannel();
                    ChannelServices.RegisterChannel(clientChannel, true);
                    m_msg = (AE_RemoteInfo)Activator.GetObject(typeof(AE_RemoteInfo), "ipc://processtrancetest/message");
				}
				catch
				{
                    Console.Write("errer process");
                    return;
                }


				if ((mode == EXEC_MODE.EXPORT) || (mode == EXEC_MODE.EXPORT_LAYER))
                {
                    //予め書き出すファイルがあったら消しておく
                    filename = System.IO.Path.GetTempPath();
                    filename = Path.Combine(filename, "ae_remap_temp.json");
                    if (File.Exists(filename) == true) File.Delete(filename);
                }else if  (mode == EXEC_MODE.IMPORT_LAYER)
                {
                    //読み込むファイルがなければエラー
                    if (File.Exists(filename) == false)
                    {
                        Console.Write("errer none import file");
                        return;
                    }
                }
                //オプションを選ぶ
                switch (mode)
                {
					case EXEC_MODE.ACTIVE:
						m_msg.DataTrance((int)mode, filename);
						break;
                    case EXEC_MODE.SCREEN_CENTER:
                        m_msg.DataTrance((int)mode, filename);
                        break;
                    case EXEC_MODE.EXPORT:
						m_msg.DataTrance((int)mode, filename); 
                        break;
                    case EXEC_MODE.EXPORT_LAYER:
						m_msg.DataTrance((int)mode, filename);
						break;
                    case EXEC_MODE.IMPORT_LAYER:
						m_msg.DataTrance((int)mode, filename);
						break;
                }
                //エキスポート時はファイル作成されるまで待つ
                if ((mode == EXEC_MODE.EXPORT) || (mode == EXEC_MODE.EXPORT_LAYER))
                {
                    int idx = 0;
                    do
                    {
                        SleepAsync();
                        idx++;
                        if (idx > 500)
                        {
                            Console.Write("errer export");
                            return;
                        }
                    } while (File.Exists(filename) == false);
					//成功したら
                    Console.Write(ToJSP(filename));
                }
                return;
            }
        }
    }
}
