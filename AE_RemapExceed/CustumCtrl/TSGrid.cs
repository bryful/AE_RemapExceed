/*
 * AE_Remap Exceedのグリッド表示を行うカスタムコントロール
 * シート操作の根底となるもの。
 */ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AE_RemapExceed
{
	public class TSGrid : Control
	{
		//--------------------
		//クリップボードのヘッダー AE_Remap準拠
		public const string ClipHeader = "#AF_Remap.exe clip";
		public const string CRLF = "\r\n";
		
		//根底クラスの定義
		public TSData tsd = new TSData();
		public TSColors cols = new TSColors();
		public TSSelection sel = new TSSelection();
		public TSFunctions funcs = new TSFunctions();
        public TSPrint tsp = new TSPrint();


		//文字表示フォーマット
		private  StringFormat format = new StringFormat();

		//表示関係
		private int m_OffsetY = 0;
		private int m_OffsetYMax = 0;

		private int m_GridStart = 0;
		private int m_GridEnd = 0;
		//
		private int m_Value = 0;
		
		//同期するコントロール
		private TSCellCaption tsc;
		private TSFrame tsf;
		private TSInput tsi;
		private TSNav tsn;
        private TSForm tsfm;
 
		
		//新規イベント
		public event EventHandler SelectionChanged;
		public event EventHandler KeyBindChanged;
		public event EventHandler FileLoaded;
		private string m_SelInfo = "";

		public bool SyncFlag = true;
		private bool m_SaveFlag = false;
		//-----------------------------------------------------------------------
		public TSGrid()
		{
			//表示フォーマット
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;

            //Printの準備
            //tsp.Font = this.Font;
            tsp.TSData = tsd;


			//ダブルバッファー表示
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			GetStatus();

			//キーバインド実行コードを割り当てる
			funcs.setNumFunc(ValueAdd);
			funcs.setSelMoveFunc(SelMoveFunc);

			/*00*/funcs.setFunc(funcCmd.New, SheetSetting);
			/*01*/funcs.setFunc(funcCmd.Open, Load);
			/*02*/funcs.setFunc(funcCmd.Save, Save);
			/*03*/funcs.setFunc(funcCmd.SaveAs, SaveAs);
            /*04*/funcs.setFunc(funcCmd.Quit, Quit);

			/*05*/funcs.setFunc(funcCmd.Copy, Copy);
			/*06*/funcs.setFunc(funcCmd.Cut, Cut);
			/*07*/funcs.setFunc(funcCmd.Paste, Paste);
			/*08*/funcs.setFunc(funcCmd.ColorSetting, ColorSetting);
			/*09*/funcs.setFunc(funcCmd.LayoutSetting, LayoutSetting);
			/*10*/funcs.setFunc(funcCmd.KeySetting, KeySetting);

			/*11*/funcs.setFunc(funcCmd.ValueInput, ValueEnter);
			/*12*/funcs.setFunc(funcCmd.ValueAutoInc, ValueAutoInc);
			/*13*/funcs.setFunc(funcCmd.ValueAutoDec, ValueAutoDec);
			/*14*/funcs.setFunc(funcCmd.ValueAutoSame, ValueAutoSame);
			/*15*/funcs.setFunc(funcCmd.ValueBack, ValueBackSpace);
			/*16*/funcs.setFunc(funcCmd.ValueDelete, ValueDelete);

			funcs.setFunc(funcCmd.SelectionALL, SelectedAll);
			funcs.setFunc(funcCmd.SelectionToEND, SelectedToEND);

			funcs.setFunc(funcCmd.LayerMoveToLeft, LayerMoveToLeft);
			funcs.setFunc(funcCmd.LayerMoveToRight, LayerMoveToRight);

			funcs.setFunc(funcCmd.PageUp, PageUp);
			funcs.setFunc(funcCmd.PageDown, PageDown);
			funcs.setFunc(funcCmd.JumpTop, JumpTop);
			funcs.setFunc(funcCmd.JumpEnd, JumpEnd);

			funcs.setFunc(funcCmd.SelTailInc, SelTailInc);
			funcs.setFunc(funcCmd.SelTailDec, SelTailDec);
			funcs.setFunc(funcCmd.SelHeadInc, SelHeadInc);
			funcs.setFunc(funcCmd.SelHeadDec, SelHeadDec);

			funcs.setFunc(funcCmd.LayerRemove, LayerRemove);
			funcs.setFunc(funcCmd.LayerInsert, LayerInsert);
			funcs.setFunc(funcCmd.LayerRename, LayerRename);

			funcs.setFunc(funcCmd.FrameInsert, FrameInsert);
			funcs.setFunc(funcCmd.FrameDelete, FrameDelete);
			funcs.setFunc(funcCmd.AutoInput, AutoInput);
            funcs.setFunc(funcCmd.ValueEdit, ValueEdit);

            funcs.setFunc(funcCmd.Selecton1, Selected1);
            funcs.setFunc(funcCmd.Selecton2, Selected2);
            funcs.setFunc(funcCmd.Selecton3, Selected3);
            funcs.setFunc(funcCmd.Selecton4, Selected4);
            funcs.setFunc(funcCmd.Selecton5, Selected5);
            funcs.setFunc(funcCmd.Selecton6, Selected6);
            funcs.setFunc(funcCmd.Selecton7, Selected7);
            funcs.setFunc(funcCmd.Selecton8, Selected8);
            funcs.setFunc(funcCmd.Selecton9, Selected9);
            funcs.setFunc(funcCmd.Selecton10, Selected10);
            funcs.setFunc(funcCmd.Selecton11, Selected11);
            funcs.setFunc(funcCmd.Selecton12, Selected12);

            funcs.setFunc(funcCmd.SelectionUp, SelMoveUp);
            funcs.setFunc(funcCmd.SelectionRight, SelMoveRight);
            funcs.setFunc(funcCmd.SelectionDown, SelMoveDown);
            funcs.setFunc(funcCmd.SelectionLeft, SelMoveLeft);

            funcs.setFunc(funcCmd.Print, Print);
            funcs.setFunc(funcCmd.PrintPreview, PrintPreview);
            funcs.setFunc(funcCmd.PageSetup, PageSetup);
			funcs.setFunc(funcCmd.PrintSetting, PrintSetting);

            funcs.setFunc(funcCmd.About, About);
			funcs.setFunc(funcCmd.ClearAll, ClearAll);
			funcs.setFunc(funcCmd.ClearLayer, ClearLayer);

		}
		//***********************************************************************
		//新規イベント定義
		//***********************************************************************
		//-----------------------------------------------------------------------
		protected virtual void OnSelectionChanged(EventArgs e) 
		{
			if (SelectionChanged != null)
			{
				m_SelInfo = tsd.CellCaption(sel.Index) + " satrt:" + (sel.Start+1).ToString() + " end:" + (sel.Last+1).ToString() + " Length:"+sel.Length.ToString();
				SelectionChanged(this, e);
			}
		}
		//-----------------------------------------------------------------------
		protected virtual void OnKeyBindChanged(EventArgs e)
		{
			if (KeyBindChanged != null)
			{
				KeyBindChanged(this, e);
			}
		}
		//-----------------------------------------------------------------------
		protected virtual void OnFileLoaded(EventArgs e)
		{
			if (FileLoaded != null)
			{
				FileLoaded(this, e);
			}
		}
        //***********************************************************************
        //各種プロパティ
        //***********************************************************************
  
		//選択範囲の情報
		public string SelInfo
		{
			get { return m_SelInfo; }
		}
		//-----------------------------------------------------------------------
		//現在表示されているGridのフレーム数。上。
		public int GridStart
		{
			get { return m_GridStart; }
		}
		//-----------------------------------------------------------------------
		//現在表示されているGridのフレーム数。下。
		public int GridEnd
		{
			get { return m_GridEnd; }
		}
		//-----------------------------------------------------------------------
		//ターゲットとなるセルレイヤーのインデックス
		public int CellIndex
		{
			get { return sel.Index; }
			set
			{
				int v = value;
				if (v < 0)
					v = 0;
				if (v >= tsd.CellCount)
					v = tsd.CellCount - 1;
				if (sel.Index != v)
				{
					sel.Index = v;
					Sync();
				}
			}
		}
		//-----------------------------------------------------------------------
		//スクロールの数値
		public int OffsetY
		{
			set
			{
				int v = value;
				if (v < 0) v = 0;
				if (v > m_OffsetYMax) v = m_OffsetYMax;
				if (m_OffsetY != v)
				{
					m_OffsetY = v;
					GetStatus();
					Sync();
				}
			}
			get
			{
				return m_OffsetY;
			}
		}
		//-----------------------------------------------------------------------
		//現在のスクロール範囲の最大値
		public int OffsetYMax
		{
			get { return m_OffsetYMax; }
		}
		//-----------------------------------------------------------------------
		//入力数値
		public int Value
		{
			get { return m_Value; }
		}
		//コントロールのプロパティ
		//-----------------------------------------------------------------------
		public TSCellCaption TSCellCaption
		{
			get { return tsc; }
			set { tsc = value; }
		}
		//-----------------------------------------------------------------------
		public TSFrame TSFrame
		{
			get { return tsf; }
			set { tsf = value; }
		}
		//-----------------------------------------------------------------------
		public TSInput TSInput
		{
			get { return tsi; }
			set { tsi = value; }
		}
		//-----------------------------------------------------------------------
		public TSNav TSNav
		{
			get { return tsn; }
			set { tsn = value; }
		}
		//-----------------
		public string FileName
		{
			get { return tsd.FileName; }
		}
		//-----------------
		public bool SaveFlag
		{
			get { return m_SaveFlag; }
		}
        public TSForm TSForm
        {
            get { return tsfm; }
            set { tsfm = value; }
        }
		//***********************************************************************
		//***********************************************************************
		//-----------------------------------------------------------------------
		//リンクされているコントロールと同期する関数
		public void Sync()
		{
			if (SyncFlag == false) return;
			
			if (tsi != null)
			{
				tsi.Invalidate();
			}
			if (tsf != null)
			{
				tsf.Refresh();
			}
			if (tsc != null)
			{
				tsc.Invalidate();
			}

			if (tsn != null)
			{
				tsn.Refresh();
			}
			this.Refresh();
		}
		//***********************************************************************
		/*
		 * 描画のメイン
		 */ 
		//***********************************************************************
		//-----------------------------------------------------------------------
		protected override void OnPaint(PaintEventArgs pe)
		{
			this.SuspendLayout();
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

			Rectangle rct = new Rectangle(0, 0, this.Width, this.Height);
			Pen p = new Pen(cols.BaseLine, 1);
			try
			{
				//まずグリッドを描画
				for (int i = 0; i < this.tsd.CellCount; i++)
				{
					for (int j = m_GridStart; j <= m_GridEnd; j++)
					{
						DrawGrid(g, i, j);
					}
				}

				//外枠を描画
				rct.Width -= 1;
				rct.Height -= 1;
				g.DrawRectangle(p, rct);
			}
			finally
			{
				p.Dispose();
			}
			this.ResumeLayout();
		}
		//-----------------------------------------------------------------------
		//グリッドを表示
		private void DrawGrid(Graphics g, int cell, int frame)
		{
			int x0 = cell * tsd.CellWidth;
			int y0 = frame * tsd.CellHeight - m_OffsetY;
			int w = tsd.CellWidth;
			int h = tsd.CellHeight;
			int x1 = x0 + w;
			int y1 = y0 + h;
			//画面外なら何もしない
			if ((x0 > this.Width) || (x1 < 0) || (y0 > this.Height) || (y1 < 0))
			{
				return;
			}

			SolidBrush b = new SolidBrush(this.cols.CellBase1);
			Pen p = new Pen(cols.CellLine, 1);
			Rectangle rct = new Rectangle(x0, y0, w, h);
			int frm = frame + tsd.FrameOffset;
			try
			{
				bool n = (tsd.FrameEnabeld(frame) < 0);
				//セルを背景色で塗る
				//無効なフレーム
				if (sel.IsIn(cell, frame) == true)
				{
					b.Color = cols.Selection;
				}
				else if (n)
				{
					b.Color = cols.None;
				}
				else
				{
					if ((cell % 2) == 0)
					{
						b.Color = cols.CellBase1;
					}
					else
					{
						b.Color = cols.CellBase2;
					}
				}
				g.FillRectangle(b, x0, y0, tsd.CellWidth, tsd.CellHeight);

				string s;
				s = "";
				b.Color = cols.Text;

				int v_cur = tsd.CellData(cell, frame);
				int v_mae = -1;
				if (frm != 0)
				{
					v_mae = tsd.CellData(cell, frame - 1);
				}
				if (v_cur == v_mae)
				{
					if (v_cur != 0)
					{
						p.Width = 1;
						if (n) { p.Color = cols.NoneText; }
						else { p.Color = cols.CellLine; }
						int xx = x0 + (x1 - x0)/2;
						g.DrawLine(p,xx,y0,xx,y1);
					}
				}
				else if (v_cur <= 0)
				{
					p.Width = 1;
					if (n) { p.Color = cols.NoneText; }
					else { p.Color = cols.CellLine; }
					g.DrawLine(p, x0, y0, x1, y1);
					g.DrawLine(p, x0, y1, x1, y0);
				}
				else
				{
					s = v_cur.ToString();
					if (n) { b.Color = cols.NoneText; }
					else { b.Color = cols.Text; }
					g.DrawString(s, this.Font, b, rct, format);
				}

				//縦線
				p.Width = 1;
				p.Color = cols.CellLine;
				g.DrawLine(p, x0, y0, x0, y1);

				//横線
				g.DrawLine(p, x0, y0, x1, y0);
				if (( (frm+1) % (int)tsd.FrameRate) == 0)
				{
					p.Width = 2;
					g.DrawLine(p, x0, y1 - 1, x1, y1 - 1);
				}
				else if (((frm+1) % tsd.HorLine) == 0)
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
		//****************************************************************************************
		/*
		 * Value操作
		 * セル番号の入力用
		 */ 
		//****************************************************************************************
		public void ValueDelete( )
		{
            if (m_Value != 0)
            {
                m_Value = 0;
                Sync();
            }
		}
		//----------------------------------------------------------------------------------------
		public void ValueBackSpace( )
		{
            if (m_Value > 0)
            {
                m_Value /= 10;
                Sync();
            }
            
		}
		//----------------------------------------------------------------------------------------
		//Valueに数値を追加
		public void ValueAdd(int v)
		{
            //if (DirectInput == false)
            {
                int vv = m_Value * 10 + v;
                if (vv < 10000)
                {
                    m_Value = vv;
                    if (tsi != null)
                        tsi.Invalidate();
                }
            }
            /*
            else
            {
                m_Value = 0;
                tsd.SetCellData(sel, v);
                SelMove(TSMove.down, sel.Length);
                m_SaveFlag = true;
                this.Invalidate();
            }*/
			
		}
		//----------------------------------------------------------------------------------------
		public void ValueEnter( )
		{
			tsd.SetCellData(sel, m_Value);
			m_Value = 0;
			SelMove(TSMove.down,sel.Length);
			m_SaveFlag = true;
			this.Invalidate();

		}
		//----------------------------------------------------------------------------------------
		public void ValueAutoInc( )
		{
			int st = sel.Start;
			if (st < 0)
				st = 0;
			int v = 1;
			if (st > 0)
			{
				v = tsd.GetCellData(sel.Index, st - 1);
				v++;
			}

			tsd.SetCellData(sel, v);
			m_Value = 0;
			SelMove(TSMove.down, sel.Length);
			m_SaveFlag = true;
			this.Invalidate();

		}
		//----------------------------------------------------------------------------------------
		public void ValueAutoDec( )
		{
			int st = sel.Start;
			if (st < 0)
				st = 0;
			int v = 0;
			if (st > 0)
			{
				v = tsd.GetCellData(sel.Index, st - 1) -1;
				if (v < 0)
					v = 0;
			}
			tsd.SetCellData(sel, v);
			m_Value = 0;
			SelMove(TSMove.down, sel.Length);
			m_SaveFlag = true;
			this.Invalidate();

		}
		//----------------------------------------------------------------------------------------
		public void ValueAutoSame( )
		{
			int st = sel.Start;
			if (st < 0)
				st = 0;
			int v = 0;
			if (st > 0)
			{
				v = tsd.GetCellData(sel.Index, st - 1);
			}
			tsd.SetCellData(sel, v);
			m_Value = 0;
			SelMove(TSMove.down, sel.Length);
			m_SaveFlag = true;
			this.Invalidate();

		}
		//****************************************************************************************
		/*
		 * キーイベント操作
		 */ 
		//****************************************************************************************
		public void KeyExec(Keys k)
		{
			//MessageBox.Show("KeyExec");
            funcs.exec(k);
		}
		/*
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			MessageBox.Show("KeyPress");
			base.OnKeyPress(e);
		}
		 */ 
		//----------------------------------------------------------------------------------------
		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			//MessageBox.Show("OnPreviewKeyDown");
            e.IsInputKey = true;
			funcs.exec(e.KeyData);
            //m_DirectInput = (Control.IsKeyLocked(Keys.CapsLock));
            

			//base.OnPreviewKeyDown(e);
		}
        //----------------------------------------------------------------------------------------
		protected override bool IsInputKey(Keys keyData)
		{
				return true;
		}
		//****************************************************************************************
		//マウス関係
		//****************************************************************************************
        //----------------------------------------------------------------------------------------
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            int v = tsd.GetCellData(sel.Index,sel.Start);
            if (m_Value != v)
            {
                
                m_Value = v;
                if (tsi != null)
                {
                    tsi.Invalidate();
                }
            }
        }
        //----------------------------------------------------------------------------------------
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				//Shiftキーが押されていたら選択範囲の追加
				if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					int y = (e.Y + m_OffsetY) / tsd.CellHeight;
					if (y < sel.Start) sel.Start = y;
					else sel.Last = y;
				}
				else
				{
					sel.CellTarget = e.X / tsd.CellWidth;

					sel.Index = sel.CellTarget;
					sel.FrameTarget = (e.Y + m_OffsetY) / tsd.CellHeight;
					sel.FrameTargetMove = sel.FrameTarget;
					sel.StartLast = sel.FrameTarget;
				}
				OnSelectionChanged(new EventArgs());
				Sync();
			}
		}
		//----------------------------------------------------------------------------------------
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (sel.CellTarget >= 0)
			{
				if (e.Y < 0)
				{
					m_OffsetY += e.Y;
					GetStatus();
					sel.FrameTargetMove = m_GridStart;
				}
				else if (e.Y >= this.Height)
				{
					m_OffsetY += (e.Y- this.Height);
					GetStatus();
					sel.FrameTargetMove = m_GridEnd;
				}
				else
				{
					sel.FrameTargetMove = (e.Y + m_OffsetY) / tsd.CellHeight;
				}
				if (sel.Last != sel.FrameTargetMove)
				{
					sel.Start = sel.FrameTarget;
					sel.Last = sel.FrameTargetMove;
					Sync();
					OnSelectionChanged(new EventArgs());
				}
			}
		}
		//----------------------------------------------------------------------------------------
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (sel.CellTarget >= 0)
			{
				sel.FrameTargetMove = (e.Y + m_OffsetY) / tsd.CellHeight;
				if (sel.FrameTargetMove < 0) { sel.FrameTargetMove = 0; }
				else if (sel.FrameTargetMove > m_GridEnd) { sel.FrameTargetMove = m_GridEnd; }
				sel.Start = sel.FrameTarget;
				sel.Last = sel.FrameTargetMove;
				sel.CellTarget = -1;
				sel.FrameTarget = -1;
				sel.FrameTargetMove = -1;
				Sync();
				OnSelectionChanged(new EventArgs());

			}
		}
		//****************************************************************************************
		//選択範囲
		//****************************************************************************************
		public void SelectedAll()
		{
            sel.Start = 0;
			sel.Last = tsd.FrameCount - 1;
			if (SyncFlag)
			{
				Sync();
				OnSelectionChanged(new EventArgs());
			}

		}
        //-----------------------------------
        public void SelectedS(int v)
        {
            if (sel.Start < 0)
            {
                sel.Start = 0;
            }
            sel.Length = v;
            
            if (SyncFlag)
            {
                Sync();
                OnSelectionChanged(new EventArgs());
            }

        }
        //-----------------------------------
        public void Selected1()
        {
            SelectedS(1);
        }
        //-----------------------------------
        public void Selected2()
        {
            SelectedS(2);
        }
        //-----------------------------------
        public void Selected3()
        {
            SelectedS(3);
        }
        //-----------------------------------
        public void Selected4()
        {
            SelectedS(4);
        }
        //-----------------------------------
        public void Selected5()
        {
            SelectedS(5);
        }
         //-----------------------------------
        public void Selected6()
        {
            SelectedS(6);
        }
        //-----------------------------------
        public void Selected7()
        {
            SelectedS(7);
        }
        //-----------------------------------
        public void Selected8()
        {
            SelectedS(8);
        }
        //-----------------------------------
        public void Selected9()
        {
            SelectedS(9);
        }
        //-----------------------------------
        public void Selected10()
        {
            SelectedS(10);
        }
        //-----------------------------------
        public void Selected11()
        {
            SelectedS(11);
        }
        //-----------------------------------
        public void Selected12()
        {
            SelectedS(12);
        }
        //------------------------------
		public void SelectedToEND()
		{
            sel.Last = tsd.FrameCount - 1;
			if (SyncFlag)
			{
				Sync();
				OnSelectionChanged(new EventArgs());
			}

		}
		//------------------------------
		public void SelMoveFunc(Keys k)
		{
			Keys kl = (Keys)((int)k & 0xFF);
			Keys kh = (Keys)((int)k & 0xFFFF0000);
			if (kh == Keys.None)
			{
				switch (kl)
				{
					case Keys.Up:
						SelMove(TSMove.up, sel.Length);
						break;
					case Keys.Right:
						SelMove(TSMove.right, 1);
						break;
					case Keys.Down:
						SelMove(TSMove.down, sel.Length);
						break;
					case Keys.Left:
						SelMove(TSMove.left, 1);
						break;
				}
			}
			else if (kh == Keys.Alt)
			{
				switch (kl)
				{
					case Keys.Up:
						SelHeadInc();
						break;
					case Keys.Down:
						SelHeadDec();
						break;
				}
			}
			else if (kh == Keys.Control)
			{
				switch (kl)
				{
					case Keys.Up:
						SelTailDec();
						break;
					case Keys.Down:
						SelTailInc();
						break;
				}
			}
			else if (kh == Keys.Shift)
			{
				switch (kl)
				{
					case Keys.Up:
						RowUp();
						break;
					case Keys.Down:
						RowDown();
						break;
				}
			}

		}
		//------------------------------
        public void SelMoveUp()
        {
            SelMove(TSMove.up, sel.Length);
        }
        //------------------------------
        public void SelMoveRight()
        {
            SelMove(TSMove.right, 1);
        }
        //------------------------------
        public void SelMoveDown()
        {
            SelMove(TSMove.down, sel.Length);
        }
        //------------------------------
        public void SelMoveLeft()
        {
            SelMove(TSMove.left, 1);
        }
        //------------------------------
        public void SelMove(TSMove mv, int l)
		{
			//sel.Move = false;
			if (l == 0)
			{
				return;
			}
			switch (mv)
			{
				case TSMove.up:
					if ((sel.Last - l) >= 0)
					{
						sel.shift(-l);
						if (m_GridStart >= sel.Last)
						{
							m_OffsetY = sel.Start * tsd.CellHeight;
							GetStatus();
						}
					}
					break;
				case TSMove.right:
					if (sel.Index < tsd.CellCount - 1)
					{
						sel.Index += 1;
					}
					break;
				case TSMove.down:
					if ((sel.Start + l) < tsd.FrameCount)
					{
						sel.shift(l);
						if (m_GridEnd <= sel.Start)
						{
							m_OffsetY = sel.Start * tsd.CellHeight;
							GetStatus();
						}
					}
					break;

				case TSMove.left:
					if (sel.Index > 0)
					{
						sel.Index -= 1;
					}
					break;
			}
			if (SyncFlag == true)
			{
				Sync();
				OnSelectionChanged(new EventArgs());
			}
		}
		//----------------------------------------------------------------------------------------
		public void SelTailInc( )
		{
			if (sel.Last < tsd.FrameCount-1)
			{
				sel.Last += 1;
				if (m_GridEnd < sel.Last)
				{
					RowUpDown(TSMove.down);
				}
				if (SyncFlag)
				{
					Sync();
					OnSelectionChanged(new EventArgs());
				}
			}
		}
		//----------------------------------------------------------------------------------------
		public void SelTailDec( )
		{
			if (sel.Start < sel.Last)
			{
				sel.Last -= 1;
				if (SyncFlag)
				{
					Sync();
					OnSelectionChanged(new EventArgs());
				}
			}
		}
		//----------------------------------------------------------------------------------------
		public void SelHeadInc( )
		{
			if (sel.Start > 0)
			{
				sel.Start -= 1;
				if (m_GridStart > sel.Start)
				{
					RowUpDown(TSMove.up);
				}
				if (SyncFlag)
				{
					Sync();
					OnSelectionChanged(new EventArgs());
				}
			}
		}
		//----------------------------------------------------------------------------------------
		public void SelHeadDec( )
		{
			if (sel.Start < sel.Last)
			{
				sel.Start += 1;
				if (SyncFlag)
				{
					Sync();
					OnSelectionChanged(new EventArgs());
				}
			}
		}
		//****************************************************************************************
		//カット＆ペースト
		//****************************************************************************************
		public void Copy()
		{
            int[] data = tsd.GetCellData(sel);
			if (data.Length > 0)
			{
				string s = ClipHeader+CRLF;
				for (int i = 0; i < data.Length; i++)
				{
					s += data[i].ToString() + CRLF;
				}
				Clipboard.SetText(s);
			}
		}
		//----------------------------------------------------------------------------------------
		public void Cut()
		{
            Copy();
			tsd.SetCellData(sel, 0);
			m_SaveFlag = true;
			Sync();
		}
		//----------------------------------------------------------------------------------------
		public void Paste()
		{
            if (Clipboard.ContainsText())
			{
				string [] sa = Clipboard.GetText().Split('\r');
				if (sa.Length <= 1) return;
				if (sa[0].Replace("\n","") == ClipHeader){
					List<int> data = new List<int>();
					for (int i = 1; i < sa.Length; i++)
					{
						string s = sa[i].Replace("\n","");
						if (s != "")
						{
							try
							{
								int v = int.Parse(sa[i]);
								data.Add(v);
							}
							catch (FormatException)
							{
							}

						}
					}
					//
					tsd.SetCellData(sel, data);
					Sync();
				}

			}

		}
		//----------------------------------------------------------------------------------------
		public bool ClipEnabled
		{
			get
			{
				bool ret = false;
				if (Clipboard.ContainsText())
				{
					string[] sa = Clipboard.GetText().Split('\r');
					if (sa.Length > 2)
					{
						ret = (sa[0].Replace("\n", "") == ClipHeader);
					}
				}
				return ret;
			}
		}
		//****************************************************************************************
		//
		//****************************************************************************************
		public void ClearAll()
		{
			tsd.ClearAll();
			Sync();

		}
		public void ClearLayer()
		{
			tsd.ClearLayer(sel.Index);
			Sync();
		}
		//****************************************************************************************
		//レイヤ
		//****************************************************************************************
		public void LayerMoveToLeft()
		{
			if (tsd.MoveToLeft(sel.Index))
			{
				sel.Index -= 1;
				Sync();
			}
		}
		//----------------------------------------------------------------------------------------
		public void LayerMoveToRight()
		{
			if (tsd.MoveToRight(sel.Index))
			{
				sel.Index += 1;
				Sync();
			}
		}
		//****************************************************************************************
		//各種イベントをオーバーライド
		//****************************************************************************************
		//----------------------------------------------------------------------------------------
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			GetStatus();
			
		}
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }
        //****************************************************************************************
        //機能追加
        //****************************************************************************************
        //----------------------------------------------------------------------------------------
        public void GetStatus()
		{
			//OffsetYMaxの値を計算
			this.m_OffsetYMax = tsd.heightMax - this.Height;
			if (this.m_OffsetYMax < 0) { this.m_OffsetYMax = 0; }
			
			//OffsetYMaxに合わせてOffsetYの数値を補正
			if (this.m_OffsetY > this.m_OffsetYMax) this.m_OffsetY = this.m_OffsetYMax;
			if (this.m_OffsetY < 0) { this.m_OffsetY = 0; }

			//表示グリッド範囲を計算
			m_GridStart = m_OffsetY / tsd.CellHeight;
			if (m_GridStart < 0) m_GridStart = 0;
			int ls = (m_OffsetY + this.Height);
			m_GridEnd =  ls / tsd.CellHeight;
			if ((ls % tsd.CellHeight) > 0) m_GridEnd++;
			if (m_GridEnd >= tsd.FrameCount - 1) m_GridEnd = tsd.FrameCount - 1;

			sel.CellCount = tsd.CellCount;
			sel.FrameCount = tsd.FrameCount;

		}
		//****************************************************************************************
		//****************************************************************************************
		//----------------------------------------------------------------------------------------
		public void PageUp()
		{
			PageUpDown(TSMove.up);
		}
		//----------------------------------------------------------------------------------------
		public void PageDown()
		{
			PageUpDown(TSMove.down);
		}
		//----------------------------------------------------------------------------------------
		public void PageUpDown(TSMove dir)
		{
            int idx = m_OffsetY / tsd.CellHeight;
			if ((m_OffsetY % tsd.CellHeight) > 0)
				idx++;
			idx *= tsd.CellHeight;
			int upV = (this.Height / 2) / tsd.CellHeight;
			upV *= tsd.CellHeight;
			switch (dir)
			{
				case TSMove.up:
				case TSMove.left:
					idx -= upV;
					if (idx < 0)
						idx = 0;
					break;
				case TSMove.down:
				case TSMove.right:
					idx += upV;
					if (idx > m_OffsetYMax)
						idx = m_OffsetYMax;
					break;

			}
			if (m_OffsetY != idx)
			{
				m_OffsetY = idx;
				GetStatus();
				Sync();
			}
		}
		//----------------------------------------------------------------------------------------
		public void RowUp()
		{
			RowUpDown(TSMove.up);
		}
		//----------------------------------------------------------------------------------------
		public void RowDown()
		{
			RowUpDown(TSMove.down);
		}
		//----------------------------------------------------------------------------------------
		public void RowUpDown(TSMove dir)
		{
            int idx = m_OffsetY / tsd.CellHeight;
			if ((m_OffsetY % tsd.CellHeight) > 0)
				idx++;
			idx *= tsd.CellHeight;
			
			int upV = tsd.CellHeight;
			switch (dir)
			{
				case TSMove.up:
				case TSMove.left:
					idx -= upV;
					if (idx < 0)
						idx = 0;
					break;
				case TSMove.down:
				case TSMove.right:
					idx += upV;
					if (idx > m_OffsetYMax)
						idx = m_OffsetYMax;
					break;

			}

			if (m_OffsetY != idx)
			{
				m_OffsetY = idx;
				GetStatus();
				Sync();
			}
		}
		public void JumpTop()
		{
			m_OffsetY = 0;
			GetStatus();
			Sync();
		}
		public void JumpEnd()
		{
			m_OffsetY = m_OffsetYMax;
			GetStatus();
			Sync();
		}
        //****************************************************************************************
        /*
		 * ファイル入出力
		 */
        //****************************************************************************************
        //----------------------------------------------------------------------------------------
        public bool SaveToJsonFile(string path,bool IsName = true)
        {
            TSJson sv = new TSJson(tsd);
            if (sv.SaveToFile(path,true) == true)
            {
                m_SaveFlag = false;
				if(IsName) OnFileLoaded(new EventArgs());
                return true;
            }
            else
            {
                return false;
            }
        }
		//----------------------------------------------------------------------------------------
		public bool ExportJson(string path)
		{
			TSJson sv = new TSJson(tsd);
			if (sv.SaveToFile(path,false) == true)
			{
				m_SaveFlag = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		//----------------------------------------------------------------------------------------
		public bool ExportJsonLayer(string path)
		{
			TSJson sv = new TSJson(tsd);
			if (sv.LayerSaveToFile(path,sel.Index) == true)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		//----------------------------------------------------------------------------------------
		public bool SaveToArdFile(string path, bool IsName = true)
		{
            TSSaveFile sv = new TSSaveFile(tsd);
			if (sv.SaveToFile(path) == true)
			{
                m_SaveFlag = false;
				if (IsName) OnFileLoaded(new EventArgs());
				return true;
			}
			else
			{
				return false;
			}
		}
 

        //----------------------------------------------------------------------------------------
        public void Load()
		{
			int fileType = 1;

			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "file load";
            if (FileName != "")
            {
                dlg.FileName = Path.GetFileName(FileName);
                dlg.InitialDirectory = Path.GetDirectoryName(FileName);

				string e = Path.GetExtension(FileName).ToLower();
				if (e == ".ardj") fileType = 1;
				else if (e == ".ard") fileType = 2;
				else if (e == ".json") fileType = 3;
			}
			else
            {
                dlg.FileName = "untite";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            dlg.Filter = "ardj files(*.ardj)|*.ardj|ard files(*.ard)|*.ard|json files(*.json)|*.json|all files(*.*)|*.*";

            if ((fileType==1)|| (fileType > 3))

			{
                dlg.FilterIndex = 1;
                dlg.DefaultExt = "ardj";
            }
            else if (fileType == 2)
			{
                dlg.FilterIndex = 2;
                dlg.DefaultExt = "ard";
            }
			else if (fileType == 3)
			{
				dlg.FilterIndex = 3;
				dlg.DefaultExt = "json";
			}

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string e = Path.GetExtension(dlg.FileName).ToLower();
				if (e == ".ard")
                {
					LoadFromArdFile(dlg.FileName);
				}
                else
                {
					LoadFromJsonFile(dlg.FileName);
                }
            }
		}
        //----------------------------------------------------------------------------------------
        public void Save()
		{
			if (m_SaveFlag == false)
				return;
			if (System.IO.File.Exists(FileName) == true)
			{
                bool ard = (Path.GetExtension(FileName).ToLower() == ".ard");
                if (ard)
                {
                    SaveToArdFile(FileName);
                }
                else
                {
                    SaveToJsonFile(FileName);
                }
            }
			else
			{
				SaveAs();
			}
		}

		//----------------------------------------------------------------------------------------
		public void SaveToPath(string p)
		{
			tsd.FileName = p;
			//SaveAs();

			bool ard = (Path.GetExtension(FileName).ToLower() == ".ard");
			if (ard)
			{
				SaveToArdFile(FileName,false);
			}
			else
			{
				SaveToJsonFile(FileName, false);
			}

		}
		//----------------------------------------------------------------------------------------
		public void SaveAs()
		{
			int fileType = 1;

			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Title = "file load";
            if (FileName != "")
            {
                dlg.FileName = Path.GetFileName(FileName);
                dlg.InitialDirectory = Path.GetDirectoryName(FileName);
				string e = Path.GetExtension(FileName).ToLower();
				if (e == ".ardj") fileType = 1;
				else if (e == ".ard") fileType = 2;
				else if (e == ".json") fileType = 3;
			}
            else
            {
                dlg.FileName = "untite";
                dlg.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }

			dlg.Filter = "ardj files(*.ardj)|*.ardj|ard files(*.ard)|*.ard|json files(*.json)|*.json|all files(*.*)|*.*";

			if ((fileType == 1) || (fileType > 3))
			{
				dlg.FilterIndex = 1;
				dlg.DefaultExt = "ardj";
			}
			else if (fileType == 2)
			{
				dlg.FilterIndex = 2;
				dlg.DefaultExt = "ard";
			}
			else if (fileType == 3)
			{
				dlg.FilterIndex = 3;
				dlg.DefaultExt = "json";
			}
			if (dlg.ShowDialog() == DialogResult.OK)
			{
                bool ard = (Path.GetExtension(dlg.FileName).ToLower() == ".ard");
                if (ard)
                {
					SaveToArdFile(dlg.FileName);
				}
				else
                {
					SaveToJsonFile(dlg.FileName);
                }
            }
		}
        //----------------------------------------------------------------------------------------
        public void SaveToClip( )
		{
			if (m_SaveFlag == false)
				return;
			TSSaveFile sv = new TSSaveFile(tsd);
			sv.SaveToClipboard();
		}
		//----------------------------------------------------------------------------------------
		public bool LoadFromFile(string path, bool IsName = true)
		{
			bool ret = false;

			string e = Path.GetExtension(path).ToLower();
			bool isJson = ((e == ".ardj") || (e == ".json"));

			if (isJson)
			{
				ret = LoadFromJsonFile(path,IsName);
			}
			else
			{
				ret = LoadFromArdFile(path, IsName);
			}


			return ret;
		}
		//----------------------------------------------------------------------------------------
		public bool LoadFromArdFile(string path, bool IsName = true)
		{
			TSSaveFile sv = new TSSaveFile(tsd);
			int w = tsd.widthMax;
			if (sv.LoadFromFile(path))
			{
				if(IsName)tsd.FileName = path;
				m_SaveFlag = false;
				if (w != tsd.widthMax) { base.OnSizeChanged(new EventArgs()); }
				if (IsName) OnFileLoaded(new EventArgs());
				if (IsName) tsd.SheetName = System.IO.Path.GetFileNameWithoutExtension(FileName);
				Sync();
				return true;
			}
			else
			{
				return false;
			}
		}
        //----------------------------------------------------------------------------------------
        public bool LoadFromJsonFile(string path, bool IsName = true)
        {
            TSJson sv = new TSJson(tsd);
            int w = tsd.widthMax;
            if (sv.LoadFromFile(path))
            {
				if (IsName) tsd.FileName = path;
                m_SaveFlag = false;
                if (w != tsd.widthMax) { base.OnSizeChanged(new EventArgs()); }
				if (IsName) OnFileLoaded(new EventArgs());
				if (IsName) tsd.SheetName = System.IO.Path.GetFileNameWithoutExtension(FileName);
                Sync();
                return true;
            }
            else
            {
                return false;
            }
        }
		//----------------------------------------------------------------------------------------
		public bool ImportLayerJson(string path)
		{
			TSJson sv = new TSJson(tsd);

			if (sv.LayerLoadFromFile(path,sel.Index))
			{
				SetLoadAfter();
				return true;
			}
			else
			{
				return false;
			}
		}
		//----------------------------------------------------------------------------------------
		//--------------------------------------------------------------------------------------
		public delegate void DelegateSetLoadAftere();
		public void SetLoadAfterSub()
		{
			Sync();

			tsfm.BringToFront();
			if (tsfm.TopMost == false)
			{
				tsfm.TopMost = true;
				tsfm.TopMost = false;
			}

		}
		public void SetLoadAfter()
		{
			if (tsfm.InvokeRequired)
			{
				tsfm.Invoke(new DelegateSetLoadAftere(SetLoadAfterSub));
			}
			else
			{
				SetLoadAfterSub();
			}
		}       //----------------------------------------------------------------------------------------
		public void Quit( )
		{
			Application.Exit();
		}
		//****************************************************************************************
		//フレームの有効・無効の設定
		//****************************************************************************************
		//----------------------------------------------------------------------------------------
		public void SetFrameEnabled(bool sw)
		{
			tsd.FrameEnabeld(sel, sw);
			Sync();
			OnSizeChanged(new EventArgs());
		}
		//----------------------------------------------------------------------------------------
		public void SetFrameEnabledON()
		{
			tsd.FrameEnabeld(sel, true);
			Sync();
			OnSizeChanged(new EventArgs());
		}
		//----------------------------------------------------------------------------------------
		public void SetFrameEnabledOFF()
		{
			tsd.FrameEnabeld(sel, false);
			Sync();
			OnSizeChanged(new EventArgs());
		}
		//----------------------------------------------------------------------------------------
		//****************************************************************************************
		/*
		 * 設定ダイアログ
		 */
		//****************************************************************************************
		//----------------------------------------------------------------------------------------
		//タイムシート設定ダイアログ
		public void SheetSetting()
		{
            TimeSheetSetting tss = new TimeSheetSetting();
			try
			{
				tss.SecInputMode = tsd.SecInputMode;

				tss.FrameCount = tsd.FrameCount;
				tss.CellCount = tsd.CellCount;
				tss.FrameRate = tsd.FrameRate;
				tss.PageSec = tsd.PageSec;
				tss.ZeroStart = tsd.ZeroStart;
				tss.FrameOffset = tsd.FrameOffset;

				tss.Comment = tsd.CommentLines;


				tss.TSDataToComb(tsd);

				if (tss.ShowDialog() == DialogResult.OK)
				{

					tsd.SecInputMode = tss.SecInputMode;
					tsd.PageSec = tss.PageSec;
					tsd.FrameRate = tss.FrameRate;
					tsd.SetSize(tss.CellCount, tss.FrameCount);
					tsd.ZeroStart = tss.ZeroStart;
					tsd.FrameOffset = tss.FrameOffset;

					tsd.CommentLines = tss.Comment;
					tss.CombToTSData(tsd);
					GetStatus();


					tsfm.PrefSave();
					OnSizeChanged(new EventArgs());
					Sync();
				}
			}
			finally
			{
				tss.Dispose();

			}
		}
		//----------------------------------------------------------------------------------------
		public void PrintSetting()
		{
			PrintSettingDlg psd = new PrintSettingDlg();
			try
			{
                psd.MainForm = tsfm;
				psd.Comment = tsd.CommentLines;
				psd.IsPrintComment = tsd.IsPrintComment;
				psd.TSDataToComb(tsd);
                psd.CommentAlign = tsd.CommentAlign;
				if (psd.ShowDialog() == DialogResult.OK)
				{
					tsd.CommentLines = psd.Comment;
					psd.CombToTSData(tsd);
                    tsd.CommentAlign = psd.CommentAlign;
	
				}
			}
			finally
			{
				psd.Dispose();
			}

		}
		//----------------------------------------------------------------------------------------
		//セルグリッド設定ダイアログ
		public void LayoutSetting()
		{
            LayoutSetteings ls = new LayoutSetteings();
			try
			{
				ls.CellWidth = tsd.CellWidth;
				ls.CellHeight = tsd.CellHeight;
				ls.CaptionHeight = tsd.CaptionHeight;
				ls.FrameWidth = tsd.FrameWidth;
				if (ls.ShowDialog() == DialogResult.OK)
				{
					if (
						(tsd.CellWidth != ls.CellWidth)
					|| (tsd.CellHeight != ls.CellHeight)
					|| (tsd.CaptionHeight != ls.CaptionHeight)
					|| (tsd.FrameWidth != ls.FrameWidth))
					{

						tsd.CellWidth = ls.CellWidth;
						tsd.CellHeight = ls.CellHeight;
						tsd.CaptionHeight = ls.CaptionHeight;
						tsd.FrameWidth = ls.FrameWidth;
						OnSizeChanged(new EventArgs());
						Sync();
					}
				}
			}
			finally
			{
				ls.Dispose();
			}

		}
		//----------------------------------------------------------------------------------------
		//キーボード設定ダイアログ
		public void KeySetting()
		{
            KeySettings ks = new KeySettings(funcs);
			try
			{
                //if (mf != null) mf.ShortCutClear();
				if (ks.ShowDialog() == DialogResult.OK)
				{
					for (int i = 0; i < (int)funcCmd.Count; i++)
					{
						funcs.setKeyTable((funcCmd)i, ks.getKeyData((funcCmd)i));
						funcs.setKeyTableSub((funcCmd)i, ks.getKeyDataSub((funcCmd)i));
					}
					OnKeyBindChanged(new EventArgs());
				}
                //if (mf != null) mf.ShortCutPre();
			}
			finally
			{
				ks.Dispose();
			}

		}
		//----------------------------------------------------------------------------------------
		//カラー設定ダイアログ
		public void ColorSetting()
		{
            ColorSetting d = new ColorSetting(cols);
			try
			{
				if (d.ShowDialog() == DialogResult.OK)
				{
					cols.Assign(d.Cols);
					Sync();
				}
			}
			finally
			{
				d.Dispose();
			}
		}
		
		//****************************************************************************************
		public void LayerRemove()
		{
			string s = tsd.CellCaption(sel.Index);
			if (MessageBox.Show("セルレイヤ[ "+s+" ]を削除しますか？", "AE_Remap Exceed", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				tsd.RemoveLayer(sel.Index);
				if (sel.Index >= tsd.CellCount) { sel.Index = tsd.CellCount - 1; }
				OnSizeChanged(new EventArgs());
				if (tsc != null) { tsc.Invalidate(); }
				this.Invalidate();
			}
		}
		//****************************************************************************************
		public void LayerInsert()
		{
            LayerInsertDlg d = new LayerInsertDlg();
			try
			{
				if (d.ShowDialog() == DialogResult.OK)
				{
					tsd.InsertLayer(sel.Index, d.Caption);
					OnSizeChanged(new EventArgs());
					if (tsc != null) { tsc.Invalidate(); }
					this.Invalidate();
				}
			}
			finally
			{
				d.Dispose();
			}
		}
		//****************************************************************************************
		public void LayerRename()
		{
            LayerRenameDlg d = new LayerRenameDlg();
			try
			{
				d.CellName = tsd.CellCaption(sel.Index);
				if (d.ShowDialog() == DialogResult.OK)
				{
					tsd.CellCaption(sel.Index, d.CellName);
					if (tsc != null)
					{
						tsc.Invalidate();
					}
				}
			}
			finally
			{
				d.Dispose();
			}
		}
		//****************************************************************************************
		public void FrameInsert()
		{
			tsd.InsertFrameWithDuration(sel);
			GetStatus();
			OnSizeChanged(new EventArgs());
			Sync();
		}
		//****************************************************************************************
		public void FrameDelete()
		{
            string s = "";
			s += tsd.FrameStr2(sel.Start) + "から";
			s += tsd.FrameStr2(sel.Last) + "まで、";
			s += sel.Length.ToString();
			if (MessageBox.Show(s + "フレームを削除しますか？", "AE_Remap Exceed", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				tsd.DeleteFrameWithDuration(sel);
				GetStatus();
				if (m_GridEnd <= sel.Start) { sel.shift(-sel.Length); }
				OnSizeChanged(new EventArgs());
				Sync();
			}
		}
		//****************************************************************************************
		public void AutoInput()
		{
            AutoInputDlg d = new AutoInputDlg();
			try
			{
				d.Start = tsd.AutoInputStart;
				d.Last = tsd.AutoInputLast;
				d.Koma = tsd.AutoInputKoma;
				if (d.ShowDialog() == DialogResult.OK)
				{
					tsd.AutoInputStart = d.Start;
					if (tsd.AutoInputStart < 0)
						tsd.AutoInputStart = 0;
					tsd.AutoInputLast = d.Last;
					if (tsd.AutoInputLast < 0)
						tsd.AutoInputLast = 0;
					tsd.AutoInputKoma = d.Koma;
					if (tsd.AutoInputKoma < 1)
						tsd.AutoInputKoma = 1;
					tsd.AutoInput(sel, tsd.AutoInputStart, tsd.AutoInputLast, tsd.AutoInputKoma);
					Sync();
				}
			}
			finally
			{
				d.Dispose();
			}
		}
        //****************************************************************************************
        public void showOK(string cap, int mm)
        {
            OKDialog ok = new OKDialog(cap,mm);
            try
            {
                int w = ok.Width;
                int h = ok.Height;
                int x = 0 + sel.Index * tsd.CellWidth - (w/2);
                int y = 0;
                Point p = this.PointToScreen(new Point(x,y));

                ok.Left = p.X;
                ok.Top = p.Y;

                ok.execute();
            }
            finally
            {
                ok.Dispose();
            }
        }
 

       
        //****************************************************************************************
        public void ValueEdit()
        {
            ValueEditDlg ve = new ValueEditDlg();
            try
            {
                ve.Mode = tsd.ValueEditMode;
                ve.Value = tsd.ValueEditValue;
                if (ve.ShowDialog() == DialogResult.OK)
                {
                    tsd.CalcValue(sel,ve.Value,ve.Mode);
                    this.Invalidate();
                }
                tsd.ValueEditMode = ve.Mode;
                tsd.ValueEditValue = ve.Value;
            }
            finally
            {
                ve.Dispose();
            }
        }
        //****************************************************************************************
        public void Print()
        {
            tsp.Print();
        }
        //****************************************************************************************
        public void PrintPreview()
        {
            tsp.Preview();
        }
        //****************************************************************************************
        public void PageSetup()
        {
            tsp.PrintPageSetup();
        }
        //****************************************************************************************
		public void About( )
		{
            AboutDialog ad = new AboutDialog();
			try
			{
				ad.ShowDialog();
			}
			finally
			{
				ad.Dispose();
			}
		}
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
	}
	//----------------------------------------------------------------------------------------

}
