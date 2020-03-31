using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AE_RemapExceed
{
    public class NavBtn : Control
    {
        private Color PushCol = SystemColors.ControlDark;
        private bool IsPush = false;
        private StringFormat format = new StringFormat();

        public NavBtn()
        {
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

        }
        //****************************************************************************************
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush b = new SolidBrush(BackColor);
            Pen p =  new Pen(ForeColor);
            try
            {
                Rectangle rct = new Rectangle(0, 0, this.Width, this.Height);
                if (IsPush) { b = new SolidBrush(PushCol); } 
                g.FillRectangle(b, rct);

                if (Text != "")
                {
                    b.Color = ForeColor;
                    g.DrawString(Text, this.Font, b, rct,format);
                }
                g.DrawRectangle(p, new Rectangle(0, 0, this.Width-1, this.Height-1));
            }
            finally
            {
                b.Dispose();
                p.Dispose();
            }

            base.OnPaint(e);
        }
        //****************************************************************************************
        protected override void OnMouseDown(MouseEventArgs e)
        {
            IsPush = true;
            this.Invalidate();
            base.OnMouseDown(e);
        }
        //****************************************************************************************
        protected override void OnMouseUp(MouseEventArgs e)
        {
            IsPush = false;
            this.Invalidate();
            base.OnMouseUp(e);
        }
        //****************************************************************************************
        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }
        //****************************************************************************************
        public Color PushColor
        {
            get { return PushCol; }
            set { PushCol = value; }
        }
        //****************************************************************************************
    }
}
