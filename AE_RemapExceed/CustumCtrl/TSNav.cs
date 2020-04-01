using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace AE_RemapExceed
{
	public class TSNav : Control
	{
		public const int TopH = 20;
		public const int EndH = 20;

		private TSGrid tsg;
		private TSData tsd;
		private TSColors cols;
		private StringFormat format = new StringFormat();
		private int m_OffsetY = 0;
		private int m_OffsetYMax = 0;
		private int m_FrameCount = 0;
		private int m_FrameRate = 24;
		private int m_Sec = 0;
		private int m_range = 0;
		private int m_EndT = 0;

		private int m_downY = -1;
		public const string TopS = "Top";
		public const string EndS = "End";

		public Color Base = Color.FromArgb(0x80, 0x80, 0x80);
		public Color Active = Color.FromArgb(0xD0, 0xD0, 0xD0);
		public Color ActiveL = Color.FromArgb(0x30, 0x30, 0x30);
		public Color ActiveT = Color.FromArgb(0x00, 0x00, 0x00);
		
		public Color ActiveHi = Color.FromArgb(0xFF, 0xFF, 0xFF);
		public Color ActiveSdw = Color.FromArgb(0x50, 0x50, 0x50);

		public Color ActiveNone = Color.FromArgb(0x50, 0x50, 0x50);
		public Color ActiveNoneL = Color.FromArgb(0x30, 0x30, 0x30);
		public Color ActiveNoneT = Color.FromArgb(0x80, 0x80, 0x80);

		private int CurrentH = 24;

		//---------------------------------------------------------------------
		public TSNav()
		{
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;

			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}
		//---------------------------------------------------------------------
		protected override void OnPaint(PaintEventArgs pe)
		{
			// TODO: カスタム ペイント コードをここに追加します

			// 基本クラス OnPaint を呼び出しています
			if (GetPrm() == true)
			{
				SolidBrush b = new SolidBrush(Base);
				Pen p = new Pen(Active, 3);
				Graphics g = pe.Graphics;
				if (m_OffsetYMax < 0)
					return;
				try
				{
					//塗りつぶす
					Rectangle rct = new Rectangle(0, 0, this.Width, this.Height);
					g.FillRectangle(b, rct);
					
					//top
					DrawIcon(g, TopH, 0, TopS, (m_OffsetY != 0));
					//Bottom
					DrawIcon(g, EndH, this.Height - EndH, EndS, (m_OffsetY != m_OffsetYMax));


					if (m_OffsetYMax > 0)
					{
						int t = TopH + m_range * m_OffsetY / m_OffsetYMax;
						rct = new Rectangle(0, t, this.Width - 1, CurrentH - 1);
						b.Color = Active;
						g.FillRectangle(b, rct);
						p.Width = 1;
						int x0 = 0;
						int x1 = this.Width - 1;
						int y0 = t;
						int y1 = t + CurrentH - 1;
						p.Color = ActiveHi;
						g.DrawLine(p, x0, y0, x0, y1);
						g.DrawLine(p, x0, y0, x1, y0);
						p.Color = ActiveSdw;
						g.DrawLine(p, x0, y1, x1, y1);
						g.DrawLine(p, x1, y0, x1, y1);
					}

				}
				finally
				{
					b.Dispose();
					p.Dispose();
				}
			}
			else
			{
				base.OnPaint(pe);
			}
		}
		//---------------------------------------------------------------------
		private void DrawIcon(Graphics g, int h, int t, string cap, bool act)
		{
			Rectangle rct = new Rectangle(0, t, this.Width-1, h-1);
			SolidBrush b = new SolidBrush(Base);
			SolidBrush bs = new SolidBrush(Base);
			Pen p = new Pen(Color.Black, 1);
			try
			{
				if (act)
				{
					b.Color = Active;
					bs.Color = ActiveT;
					p.Color = ActiveL;
				}
				else
				{
					b.Color = ActiveNone;
					bs.Color = ActiveNoneT;
					p.Color = ActiveNoneL;
				}
				g.FillRectangle(b, rct);
				g.DrawString(cap, this.Font, bs, rct, format);
				g.DrawRectangle(p, rct);
			}
			finally
			{
				b.Dispose();
				bs.Dispose();
				p.Dispose();
			}
		}
		//---------------------------------------------------------------------
		private bool GetPrm()
		{
			if (tsg != null)
			{
				tsd = tsg.tsd;
				cols = tsg.cols;
				if (tsg.OffsetYMax <= 0) { tsg.GetStatus(); }

				m_OffsetYMax = tsg.OffsetYMax;
				m_OffsetY = tsg.OffsetY;
				m_FrameCount = tsg.tsd.FrameCount;
				m_FrameRate = (int)tsd.FrameRate;
				m_Sec = m_FrameCount / m_FrameRate;
				return true;
			}
			else
			{
				return false;
			}
		}
		//---------------------------------------------------------------------
		public TSGrid TSGrid
		{
			get { return tsg; }
			set { tsg = value; GetPrm(); }
		}
		//-------------------------------------------------------------------------
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (tsg != null) tsg.Focus();
		}
		//-------------------------------------------------------------------------
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			m_downY = -1;
			if (tsg == null) return;
			if (e.Y < TopH)
			{
				if (m_OffsetY != 0)
				{
					tsg.OffsetY = 0;
				}
			}
			else if (e.Y >= m_EndT)
			{
				if (m_OffsetY != m_OffsetYMax)
				{
					tsg.OffsetY = m_OffsetYMax;
				}
			}
			else
			{
				m_downY = e.Y - TopH;
			}


		}
		//-------------------------------------------------------------------------
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if ( (m_downY >= 0)&&( tsg != null))
			{
				int v = m_OffsetYMax * (e.Y - TopH) / m_range;
				if (v < 0) { v = 0; }
				else if (v > m_OffsetYMax) { v = m_OffsetYMax; }
				if (v != m_OffsetY)
				{
					tsg.OffsetY = v;
				}
			}
		}
		//-------------------------------------------------------------------------
		protected override void  OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if ((m_downY >= 0) && (tsg != null))
			{
				int v = m_OffsetYMax * (e.Y - TopH) / m_range;
				if (v < 0) { v = 0; }
				else if (v > m_OffsetYMax) { v = m_OffsetYMax; }
				if (v != m_OffsetY)
				{
					tsg.OffsetY = v;
				}

				m_downY = -1;
			}
		}
		//-------------------------------------------------------------------------
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (tsg != null)
			{
				if (tsg.SyncFlag == false) { return; }
				m_range = this.Height - (TopH + EndH + CurrentH);
				m_EndT = this.Height - EndH;
			}
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
		//-------------------------------------------------------------------------
		protected override bool IsInputKey(Keys keyData)
		{
			return true;
		}
        //-------------------------------------------------------------------------
        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
        }
    }
}
