using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

using System.IO;
namespace CallAE_Remap
{
    class Program
    {
        enum MODE
        {
            NONE = 0,
            CALL,           //AEを起動させる
            EXENOW,         //AEが起動しているか確認する。True/Falseの文字が戻る
            EXPORT,         //現在のシートをjsonで保存させる。ファイルのパスが戻る
            IMPORT,         //指定したファイルを読み込む
            EXPORT_LAYER,   //1レイヤー分のEXPORT
            IMPORT_LAYER,   //1レイヤー分のIMPORT
            HELP            //実装していない

        }
        //
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
        //
        static private async void SleepAsync()
        {
            await Task.Delay(1000);
        }
        //
        static void Main(string[] args)
        {
            MODE mode = MODE.NONE;
            string filename = "";
            string exeName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "AE_RemapExceed.exe");

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
                            case "CALL":
                                if (mode == MODE.NONE) mode = MODE.CALL;
                                break;
                            case "EXENOW":
                                if (mode == MODE.NONE) mode = MODE.EXENOW;
                                break;
                            case "EXPORT":
                                if (mode == MODE.NONE) mode = MODE.EXPORT;
                                break;
                            case "IMPORT":
                                if (mode == MODE.NONE) mode = MODE.IMPORT;
                                break;
                            case "EXPORT_LAYER":
                                if (mode == MODE.NONE) mode = MODE.EXPORT_LAYER;
                                break;
                            case "IMPORT_LAYER":
                                if (mode == MODE.NONE) mode = MODE.IMPORT_LAYER;
                                break;
                            default:
                                if (mode == MODE.NONE) mode = MODE.HELP;
                                break;
                        }
                        if (mode != MODE.NONE) break;
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
            //何も引数がないのなら起動モードへ
            if (mode == MODE.NONE) mode = MODE.CALL;

            //ファイルがあるのにIMPORTでないのならエラー
            if (filename != "")
            {
                if((mode == MODE.IMPORT)|| (mode == MODE.IMPORT_LAYER))
                {
                    Console.Write("errer");
                    return;
                }
            }

            if (mode == MODE.HELP)
            {
                return;
            }
            else if (mode == MODE.CALL)
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = exeName;
                Process.Start(psi);
                return;
            }
            else if (mode == MODE.EXENOW)
            {
                Console.Write(String.Format("{0}", (Process.GetProcessesByName("AE_RemapExceed").Length > 0)).ToLower());
                return;
            }
            else
            {

                if ((mode == MODE.EXPORT) || (mode == MODE.EXPORT_LAYER))
                {
                    //予め書き出すファイルがあったら消しておく
                    filename = System.IO.Path.GetTempPath();
                    filename = Path.Combine(filename, "ae_remap_temp.json");
                    if (File.Exists(filename) == true) File.Delete(filename);
                }else if ((mode == MODE.IMPORT) || (mode == MODE.IMPORT_LAYER))
                {
                    //読み込むファイルがなければエラー
                    if (File.Exists(filename) == false)
                    {
                        Console.Write("errer");
                        return;
                    }

                }
                //オプションを選ぶ
                string opt = "";
                switch (mode)
                {
                    case MODE.EXPORT:
                        opt = "/export";
                        break;
                    case MODE.EXPORT_LAYER:
                        opt = "/export /layer";
                        break;
                    case MODE.IMPORT:
                        opt = "/import";
                        break;
                    case MODE.IMPORT_LAYER:
                        opt = "/import /layer";
                        break;
                }
                //起動させる
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = exeName;
                psi.Arguments = opt +  " \"" + filename + "\"";
                Process.Start(psi);

                //エキスポート時はファイル作成されるまで待つ
                if ((mode == MODE.EXPORT) || (mode == MODE.EXPORT_LAYER))
                {
                    int idx = 0;
                    do
                    {
                        SleepAsync();
                        idx++;
                        if (idx > 100)
                        {
                            Console.Write("errer");
                            return;
                        }
                    } while (File.Exists(filename) == false);
                    Console.Write(ToJSP(filename));
                }
                return;
                
            }
        }
    }
}
