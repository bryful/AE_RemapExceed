using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public class TSFrame : Control
	{
		private TSGrid tsg;
		private TSData tsd;
		private TSColors cols;

		StringFormat format = new StringFormat();

		public TSSelection sel = new TSSelection();

		private int mdY = 0;
		private int offsetY = 0;
		
		//-----------------------------------------------------------------------
		public TSFrame()
		{
			//表示フォーマット
			format.Alignment = StringAlignment.Far;
			format.LineAlignment = StringAlignment.Center;

			//ダブルバッファー表示
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}
		//---------------------------------------------------------------------
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (tsg != null)
			{
				mdY = e.Y;
				offsetY = tsg.OffsetY;
			}
			base.OnMouseDown(e);
		}
		//---------------------------------------------------------------------
		protected override void OnMouseMove(MouseEventArgs e)
		{

			if ((tsg != null)&&(mdY != 0))
			{
				int v = offsetY + mdY - e.Y;
				if ( v<0) 
				{
					v=0;
				}
				else if (v> tsg.OffsetYMax) 
				{
					v = tsg.OffsetYMax;
				}
				if ( v!= tsg.OffsetY) tsg.OffsetY = v;

			}
			base.OnMouseMove(e);
		}
		//---------------------------------------------------------------------
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if ((tsg != null) && (mdY != 0))
			{
				mdY = 0;
			}
			base.OnMouseUp(e);
		}
		//---------------------------------------------------------------------
		protected override void OnPaint(PaintEventArgs pe)
		{
			// TODO: カスタム ペイント コードをここに追加します

			if (tsg != null)
			{
				Graphics g = pe.Graphics;
				for (int i = tsg.GridStart; i <= tsg.GridEnd; i++)
				{
					DrawFrame(g, i);
				}
				//周囲の枠を描く
				Rectangle rct = new Rectangle(0, 0, this.Width, this.Height);
				Pen p = new Pen(cols.BaseLine, 1);
				try
				{
					rct.Width -= 1;
					rct.Height -= 1;
					g.DrawRectangle(p, rct);
				}
				finally
				{
					p.Dispose();
				}
			}
		}
	//---------------------------------------------------------------------
		private void DrawFrame(Graphics g, int frame)
		{
			if (tsg == null) return;
			int x0 = 0;
			int y0 = frame * tsd.CellHeight - tsg.OffsetY;
			int w = this.Width;
			int h = tsd.CellHeight;
			int x1 = x0 + w;
			int y1 = y0 + h;
			//画面外なら何もしない
			if ((y0 > this.Height) || (y1 < 0))
			{
				return;
			}

			SolidBrush b = new SolidBrush(cols.FrameBase);
			Pen p = new Pen(cols.FrameLine, 1);
			Rectangle rct = new Rectangle(x0, y0, w, h);
			int frm = frame +tsd.FrameOffset;
			try
			{
				bool n = (tsd.FrameEnabeld(frame) < 0); 
				//セルを背景色で塗る
				if (tsg.sel.IsIn(frame) == true)
				{
					b.Color = cols.FrameSelection;
				}
				else if (n)
				{
					b.Color = cols.None;
				}

				g.FillRectangle(b, x0, y0, w, h);


				if (frm >= 0)
				{
					if (n) { b.Color = cols.NoneText; } else { b.Color = cols.Text; } 

					rct.X += 2;
					rct.Width -= 4;

					g.DrawString(tsg.tsd.FrameStr(frm), this.Font, b, rct, this.format);
				}


				//横線
				g.DrawLine(p, x0, y0, x1, y0);
				if (n == false)
				{
					if (((frm + 1) % (int)tsd.FrameRate) == 0)
					{
						p.Width = 2;
						g.DrawLine(p, x0, y1 - 1, x1, y1 - 1);
					}
					else if (((frm + 1) % tsd.HorLine) == 0)
					{
						p.Width = 1;
						g.DrawLine(p, x0, y1 - 1, x1, y1 - 1);
					}
				}

			}

			finally
			{
				b.Dispose();
				p.Dispose();
			}
		}
		//-------------------------------------------------------------------------
		public TSGrid TSGrid
		{
			get { return tsg; }
			set 
			{ 
				tsg = value;
				tsd = tsg.tsd;
				cols = tsg.cols;
			}
		}
		//-------------------------------------------------------------------------
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (tsg != null) tsg.Focus();
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
        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
        }
        //----------------------------------------------------------------------------------------
        protected override bool IsInputKey(Keys keyData)
		{
			return true;
		}
		//-------------------------------------------------------------------------
	}
}
