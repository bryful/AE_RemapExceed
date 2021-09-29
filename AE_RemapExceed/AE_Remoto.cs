using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;


namespace AE_Remote
{
	public class AE_RemoteInfo : MarshalByRefObject
	{

		public class AE_RemoteInfoEventArg : EventArgs            //情報を引き渡すイベント引数クラス
		{
			private int m_mode = 0;                    //モード
			private string m_FileName = "";            //文字列
			public int Mode { get { return m_mode; } set { m_mode = value; } }
			public string FileName { get { return m_FileName; } set { m_FileName = value; } }
			public AE_RemoteInfoEventArg(int tmpMode, string tmpfName)
			{
				m_mode = tmpMode;
				m_FileName = tmpfName;
			}
		}

		public delegate void CallEventHandler(AE_RemoteInfoEventArg e);
		public event CallEventHandler OnTrance;
		public void DataTrance(int tmpmode, string tmpfname)
		{
			if (OnTrance != null)
			{
				OnTrance(new AE_RemoteInfoEventArg(tmpmode, tmpfname));
			}
		}
		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
