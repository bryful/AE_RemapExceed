using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
namespace AE_RemapExceed
{
    public class TSPrint
    {
        //******************************************************
        public const string sOPUS = "話 数";
        public const string sSubTitle = "サ ブ タ イ ト ル";
        public const string sSecne = "シ ー ン";
        public const string sCut = "カ ッ ト";
        public const string sSec = "秒数";
        public const string sSheet = "シ ー ト";
        public const string sSheetSepa = " ／ ";

        //******************************************************
        public const float pSizeS = 0.12f;
        public const float pSizeM = 0.38f;
		public const float pSizeL = 0.5f;
        public const float pSizeLL = 0.8f;

        public const float FontSize = 10f;
        //public const float FontTitleSize = 20f;
	
		public const float TitleTop = 0f;
		public const float TitleLeft = 10f;
        public const float TitleWidth = CapWidthOpus + CapWidthSubTitle + CapWidthScecne  - TitleLeft;
		public const float TitleHeight = 7f;


        public const float UserTop = TitleTop;
        public const float UserLeft = TitleLeft + TitleWidth;
        public const float UserWidth = CapWidthSec + CapWidthCut;

        public const float CampanyLeft = UserLeft + UserWidth;
        public const float CampanyTop = TitleTop;
        public const float CampanyWidth = CapWidthR + 3;
        public const float CampanyHeight = TitleHeight;

		public const float CapTop = TitleHeight;
        public const float CapTopB = TitleHeight + CapHeightT;

        public const float CapHeight = CapHeightT + CapHeightB;
        public const float CapHeightT = 5f;
        public const float CapHeightB = 10f;


		public const float CapWidthOpus = 30f;
		public const float CapWidthSubTitle = 60f;
		public const float CapWidthScecne = 30f;
		public const float CapWidthCut = 40f;
		public const float CapWidthSec = 40f;

        public const float CapWidthL = CapWidthOpus + CapWidthSubTitle + CapWidthScecne + CapWidthCut + CapWidthSec;
        public const float CapWidthR = 40f;

        public const float CapLeftL = 0f;
        public const float CapLeftR = CapWidthL + 3f;


        public const float SheetWidth = CapLeftR + CapWidthR; //249
        
        public const float CapLeftOpus = CapLeftL;
		public const float CapLeftSubTitle = CapLeftOpus + CapWidthOpus;
		public const float CapLeftScecne = CapLeftSubTitle + CapWidthSubTitle;
		public const float CapLeftCut = CapLeftScecne + CapWidthScecne;
		public const float CapLeftSec = CapLeftCut + CapWidthCut;

		public const float CommentLeft = 0f;
        public const float CommentTop = CapTop + CapHeight + 2.5f;
		public const float CommentHeight = 30f;
		public const float CommentWidth = SheetWidth;


        public const float CellTop = CommentTop + CommentHeight + 2f;//15+15+2.5+30+2 = 64.5 / 294.5

        public const float CellRectWidth = (SheetWidth - 4f) / 2f;
        public const float CellLeftL = 0f;
        public const float CellLeftR = CellRectWidth + 4f;
         
		public const float CellWidth = 7f;
		public const float CellHeight = 3.9f;
		public const float CellCapHeight = 6f;
		public const float FrameWidth = 15f;

        public const float GridLeftL = CellLeftL + FrameWidth;
		public const float GridLeftR = CellLeftR + FrameWidth;
        public const float GridTop = CellTop + CellCapHeight;
        //public const float GridWidth = 

        public const float MemoTop = GridTop;
        public const float MemoLeftL = GridLeftL + CellWidth * CellCount;
        public const float MemoLeftR = GridLeftR + CellWidth * CellCount;
        public const float MemoWidth = CellRectWidth - (FrameWidth + CellWidth * CellCount);

        public const float MemoLeft3 = GridLeftL + CellWidth * CellCount3;
        public const float MemoWidth3 = SheetWidth - (FrameWidth + CellWidth * CellCount3);


        public const float CellRectHeight = CellCapHeight + ( CellHeight * 72 ); // 3.5 x 72 + 5

        public const float SheetHeight = CellTop + CellRectHeight;//359


        public const int CellCount = 12;
        public const int CellCount3 = 30;

        public const int FrameCount = 72;
        public const int FrameRate = 24;

        //-------------------------------
        private float _pSizeS = pSizeS;
        private float _pSizeM = pSizeM;
        private float _pSizeL = pSizeL;
        private float _pSizeLL = pSizeLL;
        private float _FontSize = FontSize;
        //private float _FontTitleSize = FontTitleSize;

        private float _TitleTop = TitleTop;
        private float _TitleLeft = TitleLeft;
        private float _TitleWidth = TitleWidth;
        private float _TitleHeight = TitleHeight;

		private float _CampanyLeft = CampanyLeft;
		private float _CampanyTop = CampanyTop;
		private float _CampanyWidth = CampanyWidth;
		private float _CampanyHeight = CampanyHeight;

        private float _UserTop = UserTop;
        private float _UserLeft = UserLeft;
        private float _UserWidth = UserWidth;

        private float _CapTop = CapTop;
        private float _CapTopB = CapTopB;
        private float _CapHeight = CapHeight;
        private float _CapHeightT = CapHeightT;
        private float _CapHeightB = CapHeightB;

        private float _CapWidthOpus = CapWidthOpus;
        private float _CapWidthSubTitle = CapWidthSubTitle;
        private float _CapWidthScecne = CapWidthScecne;
        private float _CapWidthCut = CapWidthCut;
        private float _CapWidthSec = CapWidthSec;

        private float _CapWidthL = CapWidthL;
        private float _CapWidthR = CapWidthR;

        private float _CapLeftL = CapLeftL;
        private float _CapLeftR = CapLeftR;

        private float _SheetWidth = SheetWidth;

        private float _CapLeftOpus = CapLeftOpus;
        private float _CapLeftSubTitle = CapLeftSubTitle;
        private float _CapLeftScecne = CapLeftScecne;
        private float _CapLeftCut = CapLeftCut;
        private float _CapLeftSec = CapLeftSec;

        private float _CommentLeft = CommentLeft;
        private float _CommentTop = CommentLeft;
        private float _CommentHeight = CommentHeight;
        private float _CommentWidth = CommentWidth;

        private float _CellTop = CellTop;

        private float _CellRectWidth = CellRectWidth;
        private float _CellLeftL = CellLeftL;
        private float _CellLeftR = CellLeftR;

        private float _CellWidth = CellWidth;
        private float _CellHeight = CellHeight;
        private float _CellCapHeight = CellCapHeight;
        private float _FrameWidth = FrameWidth;
        private float _GridLeftL = GridLeftL;
        private float _GridLeftR = GridLeftR;
        private float _GridTop = GridTop;

        private float _MemoTop = MemoTop;
        private float _MemoLeftL = MemoLeftL;
        private float _MemoLeftR = MemoLeftR;
        private float _MemoWidth = MemoWidth;

        private float _MemoLeft3 = MemoLeft3;
        private float _MemoWidth3 = MemoWidth3;


        private float _CellRectHeight = CellRectHeight;

        private float _SheetHeight = SheetHeight;

		//******************************************************
        private TSData tsd;
		private Font m_Font = new Font("MS UI Gothic", 8);
		//private Font m_Font = new Font("FixedSys", FontSize);
		//private Font m_Font = new Font("Terminal", FontSize);
		private int m_PageCount = 0;
        private int m_LastCount = 0;

        private bool m_IsFrameEnabled = true;
        private bool m_Sheet3 = false;
        private int m_PageFrame = 144;
        private int m_PageFrameH = 72;


        private float m_mX = 0.0f;
        private float m_mY = 0.0f;
        private float m_Width = 0.0f;
        private float m_Height = 0.0f;
        private float m_Sale = 1.0f;

        //文字表示フォーマット
        private StringFormat format = new StringFormat();
       
        public PrintDocument pd = new PrintDocument();

        private int m_FrameCount = 0;
        private int m_CellCount = 0;
        private float m_FontScale = 1.0f;

        private int[][] m_data = new int[1][];
        private string[] m_memo = new string[1];

        private int currentPage = 1;

 
        //******************************************************
        public TSPrint()
        {
            //表示フォーマット
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

			
            GetPrintSize();

        }
        //******************************************************
        //プロパティ
        //******************************************************
        
        public TSData TSData
        {
            //get { return tsd; }
            set 
            { 
                tsd = value;
                GetData();
       
            }
        }
        //******************************************************
        
		public Font Font
        {
            get { return m_Font; }
            set { m_Font = value; }
        }
		
        //******************************************************
        public bool IsFrameEnabled
        {
            get { return m_IsFrameEnabled; }
            set { m_IsFrameEnabled = value; }
        }
        //******************************************************
        private string GetCaption(int idx)
        {
            if (tsd == null) return string.Empty;
            return tsd.CellCaption(idx);
        }
        //******************************************************
        private bool GetData()
        {
            if (chkFps() == false) return false;
 
            m_FrameCount = 0;
            if (m_IsFrameEnabled == true)
            {
                m_FrameCount = tsd.FrameCountTrue;
            }
            else
            {
                m_FrameCount = tsd.FrameCount;
            }
            m_Sheet3 = ( tsd.PageSec == TSPageSec.sec3);
            
            m_PageFrame = ((int)tsd.PageSec * (int)tsd.FrameRate);
            m_PageFrameH = m_PageFrame / 2;

            int p = (m_FrameCount / m_PageFrame);
            if ((m_FrameCount % m_PageFrame) > 0) p++;
            m_PageCount = p;

            m_LastCount = m_FrameCount % (int)tsd.FrameRate;

            m_CellCount = tsd.CellCount;
            if (m_Sheet3)
            {
                if (m_CellCount > CellCount3)
                {
                    MessageBox.Show("すみません。3秒シートでは、セルレイヤは、" + CellCount3.ToString() + "枚までしか印刷されません。");
                    m_CellCount = CellCount3;
                }
            }
            else
            {

                if (m_CellCount > CellCount)
                {
                    MessageBox.Show("すみません。6秒シートでは、セルレイヤは、" + CellCount.ToString() + "枚までしか印刷されません。");
                    m_CellCount = CellCount;
                }
            }

            Array.Resize(ref m_data, m_CellCount);
            for (int i = 0; i < m_CellCount; i++)
            {
                Array.Resize(ref m_data[i], m_FrameCount);
                int[] cd;
                if (m_IsFrameEnabled == true)
                {
                    cd = tsd.GetCellDataTrue(i);
                }
                else
                {
                    cd = tsd.GetCellData(i);
                }
                for (int j = 0; j < m_FrameCount; j++)
                {
                    m_data[i][j] = cd[j];
                }
            }

            if (m_memo.Length != m_FrameCount)
            {
                Array.Resize(ref m_memo, m_FrameCount);
            }

            GetPrintSize();

            return true;
        }

        //******************************************************
        private void SetFontSize(float v)
        {
            if (m_FontScale == v) return;
            float sz = FontSize * m_Sale * v;
            if ( sz<=0) sz = 1.0f;
            FontFamily ff = m_Font.FontFamily;
            FontStyle fs = m_Font.Style;
            m_Font.Dispose();
            m_Font = new Font(ff, sz, fs | FontStyle.Bold);
            m_FontScale = v;
        }
        //******************************************************
        private void GetPrintSize()
        {
            m_Width = ( pd.DefaultPageSettings.PrintableArea.Width * 25.4f / 100f);
            m_Height =( pd.DefaultPageSettings.PrintableArea.Height * 25.4f / 100f);


			float oA = SheetHeight / SheetWidth;
			float pA = m_Height / m_Width;

			if (oA >= pA)
			{
				m_Sale = m_Height / (SheetHeight+ 5f);

			}
			else
			{
				m_Sale = m_Width / (SheetWidth + 5f);

			}
            if (m_Sale < 0) { m_Sale = 1.0f; }

			ScaleSet();

			m_mX = (m_Width - SheetWidth * m_Sale   ) / 2f;
            m_mY = (m_Height - SheetHeight * m_Sale) / 2f;
	
            SetFontSize(1.0f);
			/*
			string s =
				"m_width:" + m_Width.ToString() + " m_Height:" + m_Height.ToString() + "\n" +
				"SheetWidth:" + (SheetWidth * m_Sale).ToString() + " SheetHeight:" + (SheetHeight * m_Sale).ToString() +"\n"+
				"m_Scale:" + m_Sale.ToString() +"\n"+
				"m_mX:" + m_mX.ToString() + " m_mY:" + m_mY.ToString();
			
			string s =
				"Left:" + pd.DefaultPageSettings.Margins.Left.ToString() + "\n" +
				"Right:" + pd.DefaultPageSettings.Margins.Right.ToString() + "\n" +
				"Top:" + pd.DefaultPageSettings.Margins.Top.ToString() + "\n" +
				"Bottom:" + pd.DefaultPageSettings.Margins.Bottom.ToString() + "\n";
			
			MessageBox.Show(s);
			*/
		}
 
        //******************************************************
        private void ScaleSet()
        {
            if (m_Sale < 0) { m_Sale = 1.0f; }

            _pSizeLL = pSizeLL * m_Sale;
            _pSizeL = pSizeL * m_Sale;
            _pSizeM = pSizeM * m_Sale;
            _pSizeS = pSizeS * m_Sale;
            _FontSize = FontSize * m_Sale;
            //_FontTitleSize = FontTitleSize * m_Sale;

            _TitleTop = TitleTop * m_Sale;
            _TitleLeft = TitleLeft * m_Sale;
            _TitleWidth = TitleWidth * m_Sale;
            _TitleHeight = TitleHeight * m_Sale;

			_CampanyTop = CampanyTop * m_Sale;
			_CampanyLeft = CampanyLeft * m_Sale;
			_CampanyWidth = CampanyWidth * m_Sale;
			_CampanyHeight = CampanyHeight * m_Sale;

            _UserLeft = UserLeft * m_Sale;
            _UserTop = UserTop * m_Sale;
            _UserWidth = UserWidth * m_Sale;

            _CapTop = CapTop * m_Sale;
            _CapTopB = CapTopB * m_Sale;
            _CapHeight = CapHeight * m_Sale;
            _CapHeightT = CapHeightT * m_Sale;
            _CapHeightB = CapHeightB * m_Sale;

            _CapWidthOpus = CapWidthOpus * m_Sale;
            _CapWidthSubTitle = CapWidthSubTitle * m_Sale;
            _CapWidthScecne = CapWidthScecne * m_Sale;
            _CapWidthCut = CapWidthCut * m_Sale;
            _CapWidthSec = CapWidthSec * m_Sale;

            _CapWidthL = CapWidthL * m_Sale;
            _CapWidthR = CapWidthR * m_Sale;

            _CapLeftL = CapLeftL * m_Sale;
            _CapLeftR = CapLeftR * m_Sale;

            _SheetWidth = SheetWidth * m_Sale;

            _CapLeftOpus = CapLeftOpus * m_Sale;
            _CapLeftSubTitle = CapLeftSubTitle * m_Sale;
            _CapLeftScecne = CapLeftScecne * m_Sale;
            _CapLeftCut = CapLeftCut * m_Sale;
            _CapLeftSec = CapLeftSec * m_Sale;

            _CommentLeft = CommentLeft * m_Sale;
            _CommentTop = CommentTop * m_Sale;
            _CommentHeight = CommentHeight * m_Sale;
            _CommentWidth = CommentWidth * m_Sale;

            _CellTop = CellTop * m_Sale;

            _CellRectWidth = CellRectWidth * m_Sale;
            _CellLeftL = CellLeftL * m_Sale;
            _CellLeftR = CellLeftR * m_Sale;

            _CellWidth = CellWidth * m_Sale;
            _CellHeight = CellHeight * m_Sale;
            _CellCapHeight = CellCapHeight * m_Sale;
            _FrameWidth = FrameWidth * m_Sale;
            _GridLeftL = GridLeftL * m_Sale;
            _GridLeftR = GridLeftR * m_Sale;
            _GridTop = GridTop * m_Sale;

            _MemoTop = MemoTop * m_Sale;
            _MemoLeftL = MemoLeftL * m_Sale;
            _MemoLeftR = MemoLeftR * m_Sale;
            _MemoWidth = MemoWidth * m_Sale;

            _MemoLeft3 = MemoLeft3 * m_Sale;
            _MemoWidth3 = MemoWidth3 * m_Sale;

            _CellRectHeight = CellRectHeight * m_Sale;

            _SheetHeight = SheetHeight * m_Sale;
        }
        //******************************************************
        //イベント
        //******************************************************
        //******************************************************
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;


            if (e.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages && currentPage == 1)
            {
                currentPage = e.PageSettings.PrinterSettings.FromPage;
            }

            //カット尻を描画
            DrawLast(e.Graphics);

            //カット番号の描画
            int startF = (currentPage - 1) * m_PageFrame;
            int endF = startF + m_PageFrame;

            Pen p = new Pen(Color.Black);
            SolidBrush b = new SolidBrush(Color.Black);
            try
            {
                p.Width = _pSizeM;
                SetFontSize(1.0f);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                if (m_Sheet3)
                {
                    for (int c = 0; c < m_CellCount; c++)
                    {
                        for (int f = startF; f < endF; f++)
                        {
                            DrawCellOne(e.Graphics, b, p, c, f);
                        }
                    }
                }
                else
                {
                    for (int c = 0; c < m_CellCount; c++)
                    {
                        for (int f = startF; f < endF; f++)
                        {
                            DrawCellTwo(e.Graphics, b, p, c, f);
                        }
                    }
                }
            }
            finally
            {
                p.Dispose();
                b.Dispose();
            }

            //枠の描画
            DrawWaku(e.Graphics);
            //カット番号のグリッドを描画
            if (m_Sheet3)
            {
                DrawGridOne(e.Graphics);
            }
            else
            {
                DrawGridTwo(e.Graphics);
            }
            
            //コメントの描画
            format.Alignment = StringAlignment.Near;
            SetFontSize(0.8f);
            if (currentPage == 1)
            {
                DrawComment(e.Graphics);
            }


            //次のページがあるか調べる
            if (currentPage >= m_PageCount ||
                (e.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages &&
                e.PageSettings.PrinterSettings.ToPage <= currentPage))
            {
                //次のページがないことを通知する
                e.HasMorePages = false;
                currentPage = 1;
            }
            else
            {
                e.HasMorePages = true;
                currentPage++;
            }


        }
        //******************************************************
        private bool chkFps()
        {
            if (tsd == null)
            {
                MessageBox.Show("TSDataが失われています。");
                return false;
            }
            if (tsd.FrameRate != TSFps.fps24)
            {
                MessageBox.Show("すみません。24fpsでしかプリントアウトできません。");
                return false;
            }
            return true;
        }
        //******************************************************
        public void Print()
        {
            if (GetData() == false) return;

            PrintDialog pdlg = new PrintDialog();
            pdlg.Document = pd;
            pdlg.AllowSomePages = true;
            pdlg.PrinterSettings.MinimumPage = 1;
            pdlg.PrinterSettings.MaximumPage = m_PageCount;
            pdlg.PrinterSettings.FromPage = pdlg.PrinterSettings.MinimumPage;
            pdlg.PrinterSettings.ToPage = pdlg.PrinterSettings.MaximumPage;
            if (pdlg.ShowDialog() == DialogResult.OK)
            {
                //OKがクリックされた時は印刷する
                pd.Print();
                    
            }
 
        }
        //******************************************************
        public void PrintPageSetup()
        {
            if (GetData() == false) return;
            PageSetupDialog psd = new PageSetupDialog();
            psd.Document = pd;
			
			psd.EnableMetric = false;

            if (psd.ShowDialog() == DialogResult.OK)
            {
            }

        }
        //******************************************************
        public void Preview()
        {
            if (GetData() == false) return;
            PrintPreviewDialog ppd = new PrintPreviewDialog();

            //はじめの表示位置を指定する
            ppd.StartPosition = FormStartPosition.WindowsDefaultLocation;

            ppd.Width = (int)Math.Round(m_Width * 3);
            ppd.Height = (int)Math.Round(m_Height * 3);
            //ppd.Width = 1024;
            //ppd.Height = 900;
            //ppd.PrintPreviewControl.Zoom = 1;

            ppd.Document = pd;
            ppd.ShowDialog();
        }
        //******************************************************
        private string SP3(int v)
        {
            string s = v.ToString();
            int l = s.Length;
            if (l == 0) { s = "   "; }
            else if (l == 1) { s = "  " + s; }
            else if (l == 2) { s = " " + s; }

            return s;
        }
        //******************************************************
        public void DrawComment(Graphics g)
        {
            int cnt = tsd.Comment.Count;
            if (cnt <= 0) return;
			if (tsd.IsPrintComment == false) return;
            Pen p = new Pen(Color.Black);
            SolidBrush b = new SolidBrush(Color.Black);
            try
            {
                int v = (int)tsd.CommentAlign;
                switch (v % 3)
                {
                    case 0: format.Alignment = StringAlignment.Near; break;
                    case 1: format.Alignment = StringAlignment.Center; break;
                    case 2: format.Alignment = StringAlignment.Far; break;
                }
                switch (v / 3)
                {
                    case 0: format.LineAlignment = StringAlignment.Near; break;
                    case 1: format.LineAlignment = StringAlignment.Center; break;
                    case 2: format.LineAlignment = StringAlignment.Far; break;
                }
                string s = "";
                for (int i = 0; i < cnt; i++)
                {
                    s += tsd.Comment[i];
                    if (i < cnt - 1) s += "\n";
                }
                RectangleF rct = new RectangleF(m_mX + _CommentLeft + 5f, m_mY + _CommentTop + 1f, _CommentWidth - 10f, _CommentHeight - 2f);
                SetFontSize(1.5f);
                g.DrawString(s, m_Font, b, rct, format);

            }
            finally
            {
                p.Dispose();
                b.Dispose();
            }

        }
        //******************************************************
        public void DrawGridTwo(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            SolidBrush b = new SolidBrush(Color.Black);
            try
            {
                float x0;
                float x1;
                float y0;
                float y1;
 
                //セル範囲
                p.Width = _pSizeL;
                g.DrawRectangle(p, m_mX + _CellLeftL, m_mY + _CellTop, _CellRectWidth, _CellRectHeight);
                g.DrawRectangle(p, m_mX + _CellLeftR, m_mY + _CellTop, _CellRectWidth, _CellRectHeight);
                
                //横線の描画
                x0 = m_mX + _CellLeftL;
                x1 = x0 + _CellRectWidth;
                y0 = m_mY + _CellTop + _CellCapHeight;

                float offset = _CellLeftR - _CellLeftL;

                for (int i = 0; i < FrameCount; i++)
                {
                    if ((i % 24) == 0)
                    {
                        p.Width = _pSizeL;
                    }
                    else if ((i % 6) == 0)
                    {
                        p.Width = _pSizeM;
                    }
                    else
                    {
                        p.Width = _pSizeS;
                    }
                    g.DrawLine(p, x0, y0, x1, y0);
                    g.DrawLine(p, x0 + offset, y0, x1 + offset, y0);
                    y0 += _CellHeight;
                }

                //縦線の描画
                x0 = m_mX + _GridLeftL;
                float xx0 = m_mX + _GridLeftR;
                y0 = m_mY + _CellTop;
                y1 = y0 + _CellRectHeight;
                for (int i = 0; i <= CellCount; i++)
                {
                    if ((i % 6) == 0)
                    {
                        p.Width = _pSizeM;
                    }
                    else
                    {
                        p.Width = _pSizeS;
                    }
                    g.DrawLine(p, x0, y0, x0, y1);
                    g.DrawLine(p, xx0, y0, xx0, y1);
                    x0 += _CellWidth;
                    xx0 += _CellWidth;
                }
                //フレーム番号の描画
                SetFontSize(1f);
                RectangleF rctL = new RectangleF(m_mX + _CellLeftL + 0.5f, m_mY + _CellTop + _CellCapHeight + 0.25f, _FrameWidth - 1f, _CellHeight - 0.25f);
                RectangleF rctR = new RectangleF(m_mX + _CellLeftR + 0.5f, m_mY + _CellTop + _CellCapHeight + 0.25f, _FrameWidth - 1f, _CellHeight - 0.25f);
                format.Alignment = StringAlignment.Far;
                format.LineAlignment = StringAlignment.Near;
                for (int i = 1; i <= FrameCount; i++)
                {
                    if ((i % 2) == 0)
                    {
                        format.Alignment = StringAlignment.Far;
                        g.DrawString(i.ToString(), m_Font, b, rctL, format);
                        g.DrawString((FrameCount + i).ToString(), m_Font, b, rctR, format);
                    }
                    if ((i % FrameRate) == 1)
                    {
                        format.Alignment = StringAlignment.Near;
                        int sec = (i / FrameRate) + (currentPage - 1) * 6;
                        string sL = SP3(sec) + "s";
                        string sR = SP3(sec + 3) + "s";
                        g.DrawString(sL, m_Font, b, rctL, format);
                        g.DrawString(sR, m_Font, b, rctR, format);

                    }
                    rctL.Y += _CellHeight;
                    rctR.Y += _CellHeight;
                }
                //セルキャプション
                SetFontSize(0.8f);
                rctL = new RectangleF(m_mX + _GridLeftL, m_mY + _CellTop, _CellWidth, _CellCapHeight);
                rctR = new RectangleF(m_mX + _GridLeftR, m_mY + _CellTop, _CellWidth, _CellCapHeight);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                for (int i = 0; i < m_CellCount; i++)
                {
                    string cap = GetCaption(i);
                    g.DrawString(cap, m_Font, b, rctL, format);
                    g.DrawString(cap, m_Font, b, rctR, format);
                    rctL.X += _CellWidth;
                    rctR.X += _CellWidth;
                }
            }
            finally
            {
                p.Dispose();
                b.Dispose();
            }
 
        }
        //******************************************************
        public void DrawGridOne(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            SolidBrush b = new SolidBrush(Color.Black);
            try
            {
                float x0;
                float x1;
                float y0;
                float y1;

                //セル範囲
                p.Width = _pSizeL;
                g.DrawRectangle(p, m_mX + _CellLeftL, m_mY + _CellTop, _SheetWidth, _CellRectHeight);

                //横線の描画
                x0 = m_mX + _CellLeftL;
                x1 = x0 + _SheetWidth;
                y0 = m_mY + _CellTop + _CellCapHeight;
                for (int i = 0; i < FrameCount; i++)
                {
                    if ((i % 24) == 0)
                    {
                        p.Width = _pSizeL;
                    }
                    else if ((i % 6) == 0)
                    {
                        p.Width = _pSizeM;
                    }
                    else
                    {
                        p.Width = _pSizeS;
                    }

                    g.DrawLine(p, x0, y0, x1, y0);
                    y0 += _CellHeight;
                }

                //縦線の描画
                x0 = m_mX + _GridLeftL;
                y0 = m_mY + _CellTop;
                y1 = y0 + _CellRectHeight;
                p.Width = _pSizeS;
                for (int i = 0; i <= CellCount3; i++)
                {
                    if ((i % 6) == 0)
                    {
                        p.Width = _pSizeM;
                    }
                    else
                    {
                        p.Width = _pSizeS;
                    }
                    g.DrawLine(p, x0, y0, x0, y1);
                    x0 += _CellWidth;
                }
                //フレーム番号の描画
                SetFontSize(1f);
                RectangleF rctL = new RectangleF(m_mX + _CellLeftL + 1f, m_mY + _CellTop + _CellCapHeight + 0.25f, _FrameWidth - 2f, _CellHeight - 0.25f);
                format.Alignment = StringAlignment.Far;
                format.LineAlignment = StringAlignment.Near;
                for (int i = 1; i <= FrameCount; i++)
                {
                    if ((i % 2) == 0)
                    {
                        format.Alignment = StringAlignment.Far;
                        g.DrawString(i.ToString(), m_Font, b, rctL, format);
                    }
                    if ((i % FrameRate) == 1)
                    {
                        format.Alignment = StringAlignment.Near;
                        int sec = (i / FrameRate) + (currentPage - 1) * 6;
                        string sL = SP3(sec) + "s";
                        g.DrawString(sL, m_Font, b, rctL, format);

                    }
                    rctL.Y += _CellHeight;
                }
                //セルキャプション
                SetFontSize(0.8f);
                rctL = new RectangleF(m_mX + _GridLeftL, m_mY + _CellTop, _CellWidth, _CellCapHeight);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                for (int i = 0; i < m_CellCount; i++)
                {
                    string cap = GetCaption(i);
                    g.DrawString(cap, m_Font, b, rctL, format);
                    rctL.X += _CellWidth;
                }
            }
            finally
            {
                p.Dispose();
                b.Dispose();
            }

        }
        //******************************************************
        public void DrawWaku(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            SolidBrush b = new SolidBrush(Color.Black);
			//g.PageUnit = GraphicsUnit.Millimeter;

            try
            {
                float x0;
                float x1;
                float y0;
                float y1;
                //上の左の周囲
                p.Width = _pSizeL;
                x0 = m_mX + _CapLeftL;
                y0 = m_mY + _CapTop;
				g.DrawRectangle(p, x0, y0, _CapWidthL, _CapHeight);
                
                //上の左。横線
				p.Width = _pSizeM;
				x0 = m_mX + _CapLeftOpus;
				y0 = m_mY + _CapTopB;
                x1 = x0 + _CapWidthL;
				g.DrawLine(p, x0, y0, x1, y0);

                //上の左。縦線
				x0 = m_mX + _CapLeftSubTitle;
				y0 = m_mY + _CapTop;
				y1 = m_mY + _CapTop + _CapHeight;
				g.DrawLine(p, x0, y0, x0, y1);
				x0 = m_mX + _CapLeftScecne;
				g.DrawLine(p, x0, y0, x0, y1);
				x0 = m_mX + _CapLeftCut;
				g.DrawLine(p, x0, y0, x0, y1);
				x0 = m_mX + _CapLeftSec;
				g.DrawLine(p, x0, y0, x0, y1);

                //上の右の周囲
				p.Width = _pSizeLL;
                x0 = m_mX + _CapLeftR;
                y0 = m_mY + _CapTop;
				g.DrawRectangle(p, x0, y0, _CapWidthR, _CapHeight);
				p.Width = _pSizeM;
                x0 = m_mX + _CapLeftR;
                y0 = m_mY + _CapTopB;
                x1 = x0 + _CapWidthR;
				g.DrawLine(p, x0, y0, x1, y0);

                //コメントの周囲
                p.Width = _pSizeS;
                x0 = m_mX + _CommentLeft;
                y0 = m_mY + _CommentTop;
				g.DrawRectangle(p, x0, y0, _CommentWidth, _CommentHeight);

                //---------------------------------
                //その他の描画
                RectangleF rct = new RectangleF(_CapLeftOpus + m_mX, _CapTop + m_mY, _CapWidthOpus, _CapHeightT);
                SetFontSize(1.0f);
                
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                g.DrawString(sOPUS, m_Font, b, rct, format);
                rct = new RectangleF(_CapLeftSubTitle + m_mX, _CapTop + m_mY, _CapWidthSubTitle, _CapHeightT); 
                g.DrawString(sSubTitle, m_Font, b, rct, format);
                rct = new RectangleF(_CapLeftScecne + m_mX, _CapTop + m_mY, _CapWidthScecne, _CapHeightT); 
                g.DrawString(sSecne, m_Font, b, rct, format);
                rct = new RectangleF(_CapLeftCut + m_mX, _CapTop + m_mY, _CapWidthCut, _CapHeightT);
                g.DrawString(sCut, m_Font, b, rct, format);
                rct = new RectangleF(_CapLeftSec + m_mX, _CapTop + m_mY, _CapWidthSec, _CapHeightT);
                g.DrawString(sSec, m_Font, b, rct, format);

                format.Alignment = StringAlignment.Near;
                rct = new RectangleF(_CapLeftR + m_mX + 5, _CapTop + m_mY, _CapWidthR - 5, _CapHeightT);
                g.DrawString(sSheet, m_Font, b, rct, format);


				if ((tsd.CAMPANY_NAME != "") && (tsd.GetIsPtintInfo(SheetInfo.CAMPANY_NAME)))
				{
					format.Alignment = StringAlignment.Far;
					SetFontSize(2f);
					rct = new RectangleF(_CampanyLeft + m_mX, _CampanyTop + m_mY, _CampanyWidth, _CampanyHeight);
					g.DrawString(tsd.CAMPANY_NAME, m_Font, b, rct, format);
				}

                if (tsd.GetIsPtintInfo(SheetInfo.CREATE_USER) || tsd.GetIsPtintInfo(SheetInfo.UPDATE_USER))
                {

                    string s = "";
                    if ((tsd.GetIsPtintInfo(SheetInfo.CREATE_USER) == true) && (tsd.CREATE_USER != ""))
                    {
                        s += "担当者:" + tsd.CREATE_USER;
                    }
                    if ((tsd.GetIsPtintInfo(SheetInfo.UPDATE_USER) == true) && (tsd.UPDATE_USER != ""))
                    {
                        s += "  修正者:" + tsd.UPDATE_USER;
                    }
                    if (s != "")
                    {
                        rct = new RectangleF(_UserLeft + m_mX, _UserTop + m_mY , _UserWidth, _TitleHeight);
                        format.Alignment = StringAlignment.Far;
                        format.LineAlignment = StringAlignment.Far;
                        SetFontSize(1.2f);
                        g.DrawString(s, m_Font, b, rct, format);
                        format.LineAlignment = StringAlignment.Center;
                    }
                }
				
				if ((tsd.TITLE != "") && (tsd.GetIsPtintInfo(SheetInfo.TITLE)))
                {
					format.Alignment = StringAlignment.Near;
					SetFontSize(2f);
                    rct = new RectangleF(_TitleLeft + m_mX, _TitleTop + m_mY, _TitleWidth, _TitleHeight);
                    g.DrawString(tsd.TITLE, m_Font, b, rct, format);
                }
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                SetFontSize(1.5f);
				if ((tsd.OPUS != "") && (tsd.GetIsPtintInfo(SheetInfo.OPUS)))
                {
                    rct = new RectangleF(_CapLeftOpus + m_mX, _CapTopB + m_mY, _CapWidthOpus, _CapHeightB);
                   g.DrawString(tsd.OPUS, m_Font, b, rct, format);
            
                }
                if ((tsd.SUB_TITLE != "")&&(tsd.GetIsPtintInfo(SheetInfo.SUB_TITLE)))
                {
                    rct = new RectangleF(_CapLeftSubTitle + m_mX, _CapTopB + m_mY, _CapWidthSubTitle, _CapHeightB);
                    g.DrawString(tsd.SUB_TITLE, m_Font, b, rct, format);

                }
                if ( (tsd.SCECNE != "")&&(tsd.GetIsPtintInfo(SheetInfo.SCECNE)))
                {
                    rct = new RectangleF(_CapLeftScecne + m_mX, _CapTopB + m_mY, _CapWidthScecne, _CapHeightB);
                    g.DrawString(tsd.SCECNE, m_Font, b, rct, format);

                }
                if ( (tsd.CUT != "")&&(tsd.GetIsPtintInfo(SheetInfo.CUT)))
                {
                    SetFontSize(1.7f);
                    rct = new RectangleF(_CapLeftCut + m_mX, _CapTopB + m_mY, _CapWidthCut, _CapHeightB);
                    g.DrawString(tsd.CUT, m_Font, b, rct, format);

                }
                SetFontSize(2.4f);
                int AA = m_FrameCount / FrameRate;
                int BB = m_FrameCount % FrameRate;
                string byou = AA.ToString() + " + " + BB.ToString();
                rct = new RectangleF(_CapLeftSec + m_mX, _CapTopB + m_mY, _CapWidthSec, _CapHeightB);
                g.DrawString(byou, m_Font, b, rct, format);
                
                rct = new RectangleF(_CapLeftR + m_mX, _CapTopB + m_mY, _CapWidthR, _CapHeightB);
                string sh = currentPage.ToString() + sSheetSepa + m_PageCount.ToString();
                g.DrawString(sh, m_Font, b, rct, format);
                SetFontSize(1.0f);
            }
            finally
            {
                p.Dispose();
                b.Dispose();
            }
        }
        //******************************************************
        private void DrawCellTwo(Graphics g, SolidBrush b,Pen p, int c, int f)
        {
            if ((f >= m_FrameCount) || (c >= m_CellCount)) return;
            float pw = p.Width;
            

            int ff = f % m_PageFrame;
 
            float x0;
            float y0;
            if (ff < m_PageFrameH)
            {
                x0 = m_mX + _GridLeftL + c * _CellWidth;
                y0 = m_mY + _CellTop + _CellCapHeight  + ff * _CellHeight;
            }
            else
            {
                x0 = m_mX + _GridLeftR + c * _CellWidth;
                y0 = m_mY + _CellTop + _CellCapHeight + (ff - 72) * _CellHeight;
            }

            RectangleF rct = new RectangleF(x0, y0 + 0.25f, _CellWidth, _CellHeight - 0.25f);

            int v_cur = m_data[c][f];
            int v_bef = 0;
            if (f > 0) { v_bef = m_data[c][f - 1]; }

            if ((v_cur != v_bef) || ((f % m_PageFrameH) == 0))
            {
                if (v_cur != 0)
                {
                    g.DrawString(v_cur.ToString(), m_Font, b, rct, format);
                }
                else
                {
					p.Width = _pSizeS;
                    g.DrawLine(p, rct.X, rct.Y, rct.Right, rct.Bottom);
                    g.DrawLine(p, rct.X, rct.Bottom, rct.Right, rct.Y);
                }
            }
            else if (v_cur != 0)
            {
				p.Width = _pSizeM;
				p.DashStyle = DashStyle.Dot;
				float x = rct.X + rct.Width / 2f;
                g.DrawLine(p, x, rct.Y - 0.25f, x, rct.Bottom + 0.25f);
				p.DashStyle = DashStyle.Solid;

			}
            p.Width = pw;
        }
        //******************************************************
        private void DrawCellOne(Graphics g, SolidBrush b, Pen p, int c, int f)
        {
            if ((f >= m_FrameCount) || (c >= m_CellCount)) return;
            float pw = p.Width;
            

            int ff = f % m_PageFrame;

            float x0;
            float y0;
            x0 = m_mX + _GridLeftL + c * _CellWidth;
            y0 = m_mY + _CellTop + _CellCapHeight + ff * _CellHeight;

            RectangleF rct = new RectangleF(x0, y0 + 0.25f, _CellWidth, _CellHeight - 0.25f);

            int v_cur = m_data[c][f];
            int v_bef = 0;
            if (f > 0) { v_bef = m_data[c][f - 1]; }

            if ((v_cur != v_bef) || ((f % m_PageFrame) == 0))
            {
                if (v_cur != 0)
                {
                    g.DrawString(v_cur.ToString(), m_Font, b, rct, format);
				}
                else
                {
					
					p.Width = _pSizeS;
                    g.DrawLine(p, rct.X, rct.Y, rct.Right, rct.Bottom);
                    g.DrawLine(p, rct.X, rct.Bottom, rct.Right, rct.Y);
                }
            }
            else if (v_cur != 0)
            {
				p.Width = _pSizeM;
				p.DashStyle = DashStyle.Dot;
				float x = rct.X + rct.Width / 2f;
                g.DrawLine(p, x, rct.Y-0.25f, x, rct.Bottom+0.25f);
				p.DashStyle = DashStyle.Solid;
				
            }
            
            p.Width = pw;
        }
        //******************************************************
        private void DrawMemoTwo(Graphics g, SolidBrush b, int f)
        {
            if ((f < 0) || (f >= m_FrameCount)) return;
            //if ( tsd.GetMemoDataTrue

            int ff = f % m_PageFrame;

            float x0;
            float y0;
            if (ff < 72)
            {
                x0 = m_mX + _MemoLeftL;
                y0 = m_mY + _MemoTop +  ff * _CellHeight;
            }
            else
            {
                x0 = m_mX + _MemoLeftR;
                y0 = m_mY + _MemoTop +  (ff - 72) * _CellHeight;
            }
            RectangleF rct = new RectangleF(x0 + 1f, y0 + 0.5f, _CellWidth - 2f, _CellHeight- 0.5f);
            g.DrawString(m_memo[f], m_Font, b, rct, format);
        }
        //******************************************************
        private void DrawMemoOne(Graphics g, SolidBrush b, int f)
        {
            if ((f < 0) || (f >= m_FrameCount)) return;

            int ff = f % m_PageFrame;

            float x0;
            float y0;
            x0 = m_mX + _MemoLeft3;
            y0 = m_mY + _MemoTop + ff * _CellHeight;
            RectangleF rct = new RectangleF(x0 + 1f, y0 + 0.5f, _CellWidth - 2f, _CellHeight - 0.5f);
            g.DrawString(m_memo[f], m_Font, b, rct, format);
        }
        //******************************************************
        private void DrawLast(Graphics g)
        {
            if (currentPage == m_PageCount)
            {
                float t, l, h;
                int ff = (m_FrameCount % m_PageFrame);

                HatchBrush hb = new HatchBrush(HatchStyle.Percent30, Color.Black, Color.White);
                try
                {
                    h = _CellHeight * 3;
                    if (m_Sheet3 == false)
                    {
                        if (ff == 0)
                        {
                            t = _GridTop + _CellHeight * m_PageFrame / 2;
                            l = _CellLeftR;

                        }
                        else if (ff <= m_PageFrameH)
                        {
                            t = _GridTop + ff * _CellHeight;
                            l = _CellLeftL;
                        }
                        else
                        {
                            ff = ff - m_PageFrameH;
                            t = _GridTop + ff * _CellHeight;
                            l = _CellLeftR;
                        }
                        g.FillRectangle(hb, new RectangleF(m_mX + l, m_mY + t, _CellRectWidth, h));
                    }
                    else
                    {
                        if (ff == 0)
                        {
                            t = _GridTop + _CellHeight * m_PageFrame;
                            l = _CellLeftL;

                        }
                        else
                        {
                            t = _GridTop + ff * _CellHeight;
                            l = _CellLeftL;
                        }
                        g.FillRectangle(hb, new RectangleF(m_mX + l, m_mY + t, _SheetWidth, h));

                    }
                }
                finally
                {
                    hb.Dispose();
                }
            }
        }
        //******************************************************
 
    }
    //----------------------------------------------------------
}
