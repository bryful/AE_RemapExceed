using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace AE_RemapExceed
{
	public class TSInfo : Control
	{
		private StringFormat format = new StringFormat();

		//private int m_FrameCount = TSdef.FrameCount;
		//private TSFps m_FrameRate = TSdef.FrameRate;
		private TSGrid tsg;

		private bool FrameSecFlag = false;
		public TSInfo( )
		{
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;

			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}
        /*
		//---------------------------------------------------------------------
		public int FrameCount
		{
			get { return m_FrameCount; }
			set { m_FrameCount = value; }
		}
		//---------------------------------------------------------------------
		public TSFps FrameRate
		{
			get { return m_FrameRate; }
			set { m_FrameRate = value; }
		}
		//---------------------------------------------------------------------
         */ 
		protected override void OnPaint(PaintEventArgs pe)
		{
			// TODO: カスタム ペイント コードをここに追加します

			// 基本クラス OnPaint を呼び出しています
			SolidBrush b = new SolidBrush(Color.Transparent);
			Pen p = new Pen(Color.Black, 1);

			int x0 = 1;
			int w0 = this.Height;
			int y0 = 1;
			int h = this.Height-2;
			int x1 = x0 + w0;
			int w1 = this.Width - x1;

			try
			{
                bool dFlag = false;
                if (tsg != null)
                {
                    dFlag = tsg.DirectInput;
                }
                int fr = (int)tsg.tsd.FrameRate;
                int fc = tsg.tsd.FrameCountTrue;

				Graphics g = pe.Graphics;
				b.Color = Color.SkyBlue;
				g.FillRectangle(b, new Rectangle(x0, y0, w0, h));
				
				string fps = fr.ToString() ;
				b.Color = Color.White;
				format.Alignment = StringAlignment.Center;
				g.DrawString(fps, this.Font, b, new Rectangle(x0 + 1, y0 + 1, w0 - 2, h), format);
				
				g.DrawRectangle(p, new Rectangle(x0, y0, w0, h));

				b.Color = Color.Transparent;
				g.FillRectangle(b, new Rectangle(x1 , y0,  w1, h));
				string sec;
				if (FrameSecFlag == false)
				{
					sec = (fc / fr).ToString();
					sec += "+" + (fc % fr).ToString();
				}
				else
				{
					sec = fc.ToString()+"F";
				}
                if (dFlag)
                {
                    sec += " D";
                }
				b.Color = Color.Black;
				format.Alignment = StringAlignment.Near;
				g.DrawString(sec, this.Font, b, new Rectangle(x1 + 1, y0 + 1, w1 - 2, h), format);
			}
			finally
			{
				b.Dispose();
				p.Dispose();
			}
			base.OnPaint(pe);
		}
	//---------------------------------------------------------------------
		protected override void OnMouseClick(MouseEventArgs e)
		{
			FrameSecFlag = !FrameSecFlag;
			this.Invalidate();
			base.OnMouseClick(e);
		}
		//---------------------------------------------------------------------
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (tsg != null)
				tsg.Focus();
		}
		//-------------------------------------------------------------------------
		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			if (tsg != null)
			{
				tsg.funcs.exec(e.KeyData);
				tsg.Focus();
				e.IsInputKey = true;
			}
			base.OnPreviewKeyDown(e);
		}
		//----------------------------------------------------------------------------------------
		protected override bool IsInputKey(Keys keyData)
		{
			return true;
		}
		//-------------------------------------------------------------------------
		public TSGrid TSGrid
		{
			get { return tsg; }
			set { tsg = value; }
		}
		//-------------------------------------------------------------------------
	}
}
