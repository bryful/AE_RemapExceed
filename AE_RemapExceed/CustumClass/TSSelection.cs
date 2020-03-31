/*
 * 選択範囲を管理するクラス
 */ 
using System;
using System.Collections.Generic;
using System.Text;

namespace AE_RemapExceed
{
	public enum TSMove
	{
		up=0,
		right,
		down,
		left
	}
   //選択範囲の管理をするクラス
  public class TSSelection
  {
	private int index = 0;
	private int start = 0;
	private int last = 0;

	public int CellTarget = -1;
	public int FrameTarget = -1;
	public int FrameTargetMove = -1;

	public int CellCount = 0;
	public int FrameCount = 0;
	//public bool Move = true;

	//----------------------------------------
	private void clear()
	{
		index = 0;
		start = 0;
		last = 0;
	}
	  //----------------------------------------
	//選択範囲の長さを設定
	private void setLength(int v)
    {
      if (v<=0){
		  v = 1;
      }
      last = start + v -1;
    }
	//----------------------------------------
	//選択範囲の長さを設定
	private void setLengthLast(int v)
	{
		if (v <= 0)
		{
			v = 1;
		}
		start = last - (v - 1); 
	}
	//------------------------------
    public int Length {
      set { setLength(value); }
      get {return (last - start) + 1; }
    }
	//------------------------------
	public int Index
	{
      set { this.index = value; }
      get { return this.index; }
    }
	//------------------------------
	public int Start
	{
		set { this.start = value; chk(); }
      get { return this.start; }
    }
	//------------------------------
	public int Last
	{
		set { this.last = value; chk(); }
      get { return this.last; }
    }
	public int StartLast
	{
		set { this.Start = this.Last = value; }
	}
	public void set(int s, int l)
	{
		this.start = s;
		this.last = l;
		chk();
	}
	public void shift(int v)
	{
		this.start += v;
		this.last += v;
	}
	//------------------------------
	public TSSelection()
    {
		clear();
    }
    //------------------------------
	public void Assign(TSSelection t)
	{
      this.index = t.index;
      this.start = t.start;
      this.last = t.last;
    }
    //------------------------------
    public void chk() {
      if (this.start > this.last) {
        int tmp = this.start;
        this.start = this.last;
        this.last = tmp;
      }
    }
    //------------------------------
    public bool IsIn(int idx, int v)
    {
      if (this.index != idx) {
        return false;
      }
	  return ((v >= this.start) && (v <= this.last));
    }
    //------------------------------
    public bool IsIn(int v)
		{
			return ((v >= this.start) && (v <= this.last));
		}
	  //------------------------------

  }
}
