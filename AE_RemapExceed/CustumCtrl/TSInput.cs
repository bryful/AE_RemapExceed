using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace AE_RemapExceed
{
	public class TSInput : Control
	{
		private TSGrid tsg;
		private TSData tsd;
		private TSColors cols;

		private StringFormat format = new StringFormat();
		//---------------------------------------------------------------------
		public TSInput()
		{
			format.Alignment = StringAlignment.Far;
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
			if (tsg != null)
			{
				SolidBrush b = new SolidBrush(cols.InputBase);
				Pen p = new Pen(cols.BaseLine, 1);
				Rectangle rct = new Rectangle(0, 0, this.Width, this.Height);
				try
				{
                    //if (tsg.DirectInput == true)
                    //{
                    //    b.Color = SystemColors.ControlLight;
                    //}
					Graphics g = pe.Graphics;
					g.FillRectangle(b, rct);
					string s = tsg.Value.ToString();
                    //if (tsg.DirectInput == true)
                    //{
                    //    b.Color = SystemColors.ControlLightLight;
                   // }
                    //else
                    //{
                        b.Color = cols.Text;
                    //}
					rct.X += 1;
					rct.Width -= 2;
					rct.Y += 1;
					rct.Height -= 2;

					g.DrawString(s, this.Font, b, rct, format);
					rct.X -= 1;
					rct.Width += 1;
					rct.Y -= 1;
					rct.Height += 1;
					g.DrawRectangle(p, rct);
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
		//----------------------------------------------------------------------------------------
		protected override bool IsInputKey(Keys keyData)
		{
			return true;
		}
	}
}
