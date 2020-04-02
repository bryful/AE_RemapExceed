using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public class TSMemo : Control
	{
		private TSGrid tsg;
		private TSData tsd;
		private TSColors cols;

		StringFormat format = new StringFormat();
		private int mdY = 0;
		private int offsetY = 0;
		//-----------------------------------------------------------------------
		public TSMemo()
		{
			//表示フォーマット
			format.Alignment = StringAlignment.Near;
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

			if ((tsg != null) && (mdY != 0))
			{
				int v = offsetY + mdY - e.Y;
				if (v < 0)
				{
					v = 0;
				}
				else if (v > tsg.OffsetYMax)
				{
					v = tsg.OffsetYMax;
				}
				if (v != tsg.OffsetY) tsg.OffsetY = v;

			}
			else
			{
				mdY = 0;
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
		//---------------------------------------------------------------------
		protected override void OnPaint(PaintEventArgs pe)
		{
			if (tsg != null)
			{
				Graphics g = pe.Graphics;
				for (int i = tsg.GridStart; i <= tsg.GridEnd; i++)
				{
					DrawMemo(g, i);
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
		//----------------------------------------------------------------------------------------
		private void DrawMemo(Graphics g, int frame)
		{
			int x0 = 0;
			int y0 = frame * tsd.CellHeight - tsg.OffsetY;
			int w = tsd.MemoWidth;
			int h = tsd.CellHeight;
			int x1 = x0 + w;
			int y1 = y0 + h;
			//画面外なら何もしない
			if ((x0 > this.Width) || (x1 < 0) || (y0 > this.Height) || (y1 < 0))
			{
				return;
			}
			SolidBrush b = new SolidBrush(cols.MemoBase);
			Pen p = new Pen(cols.MemoLine, 1);
			Rectangle rct = new Rectangle(x0, y0, w, h);
			int frm = frame + tsd.FrameOffset;
			try
			{
				bool n = (tsd.FrameEnabeld(frame) < 0);
				if (n)
				{
					b.Color = cols.None;
				}
				//セルを背景色で塗る
				g.FillRectangle(b, x0, y0, tsd.MemoWidth, tsd.CellHeight);

				string s = tsd.Memo(frame);
				if (s != "")
				{

					b = new SolidBrush(cols.Text);
					if (n) b.Color = cols.NoneText;
					Rectangle rct1 = new Rectangle(x0+6, y0, w-4, h);
					g.DrawString(s, this.Font, b, rct1, format);
				}
				//横線
				g.DrawLine(p, x0, y0, x1, y0);
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
			finally
			{
				b.Dispose();
				p.Dispose();
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
		//----------------------------------------------------------------------------------------
		protected override bool IsInputKey(Keys keyData)
		{
			return true;
		}
        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
        }
    }
}
