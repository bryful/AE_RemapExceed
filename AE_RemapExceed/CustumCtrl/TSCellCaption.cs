using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public class TSCellCaption : Control
	{
		private TSGrid tsg;
		private TSData tsd;
		private TSColors cols;

		private StringFormat format = new StringFormat();
		//---------------------------------------------------------------------
		public TSCellCaption()
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
			if (tsg != null)
			{
				Graphics g = pe.Graphics;
				for (int i = 0; i < tsd.CellCount; i++)
				{
					DrawCaption(g, i);
				}
				Pen p = new Pen(cols.CapLine, 1);
				Rectangle rct = new Rectangle(0, 0, this.Width -1, this.Height -1);
				try
				{
					g.DrawRectangle(p, rct);
				}
				finally
				{
					p.Dispose();
				}
			}
			else
			{
				// 基本クラス OnPaint を呼び出しています
				base.OnPaint(pe);
			}
		}
		//---------------------------------------------------------------------
		private void DrawCaption(Graphics g, int cell)
		{
			if (tsg == null) return;
			int x0 = cell * tsd.CellWidth;
			int y0 = 0;
			int w = tsd.CellWidth;
			int h = this.Height;
			int x1 = x0 + w;
			int y1 = y0 + h;
			//画面外なら何もしない
			if ((x0 > this.Width) || (x1 < 0))
			{
				return;
			}

			SolidBrush b = new SolidBrush(cols.CaptionBase);
			Pen p = new Pen(cols.CapLine, 1);
			Rectangle rct = new Rectangle(x0, y0, w, h);
			try
			{
				//セルを背景色で塗る
				
				if (cell == tsg.CellIndex)
				{
					b.Color = cols.CaptionSelection;
				}
				 
				g.FillRectangle(b, x0, y0, w, h);

				string s;
				s = tsd.CellCaption(cell);
				
				b.Color = cols.Text;

				g.DrawString(s, this.Font, b, rct, format);

				g.DrawLine(p, x0, y0, x0, y1);
				//g.DrawLine(p, x0, y0, x1, y0);
			}
			finally
			{
				b.Dispose();
				p.Dispose();
			}
		}
		//---------------------------------------------------------------------
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
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (tsg != null) { tsg.Focus(); }
		}
		//---------------------------------------------------------------------
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (tsg != null)
			{
				int idx = e.X / tsd.CellWidth;
				tsg.CellIndex = idx;
			}
		}
		//---------------------------------------------------------------------
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			if (tsg != null)
			{
				tsg.sel.Index = e.X / tsd.CellWidth;
				tsg.SelectedAll();
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
		//----------------------------------------------------------------------------------------
		protected override bool IsInputKey(Keys keyData)
		{
			return true;
		}

	}
}
