using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.VisualBasic.ApplicationServices;

namespace AE_RemapExceed
{
    
    public class MyApp : WindowsFormsApplicationBase
    {
        public MyApp()
        : base()
        {
            this.EnableVisualStyles = true;
            this.IsSingleInstance = true;
            this.MainForm = new TSForm();//スタートアップフォームを設定
            //this.StartupNextInstance += new StartupNextInstanceEventHandler(MyApplication_StartupNextInstance);
        }
        public void MyApplication_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            //ここに二重起動されたときの処理を書く
            //e.CommandLineでコマンドライン引数を取得出来る
            //((TSForm)this.MainForm).GetCommand(e.CommandLine.ToArray<string>(), "Program.cs");
        }
    }
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //下の3行を復活させれば多重起動ができる
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            MyApp winAppBase = new MyApp();
            winAppBase.Run(args);
        }
    }
}
