using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapExceed
{
    //**************************************************************
    public enum SheetInfo
    {
        TITLE = 0,
        SUB_TITLE,
        OPUS,
        SCECNE,
        CUT,
        CREATE_USER,
        UPDATE_USER,
        CAMPANY_NAME,
        Count,
    }
    //**************************************************************
    public enum CmtAligns
    {
        LeftTop = 0,
        CenterTop,
        RightTop,
        LeftCenter,
        CenterCenter,
        RightCenter,
        LeftBottom,
        CenterBottom,
        RightBottom,
        Count
    }
    //**************************************************************
    public enum ValueEditMode
    {
        direct = 0,
        add,
        dec,
        Count
    }   //**************************************************************
    //各パラメータの初期値
    public class TSdef
    {
        public const int none = 0;
        public const int CellCount = 10;
        public const int FrameCount = 144;
        public const int CellWidth = 28;
        public const int CellHeight = 16;
        public const int FrameWidth = 80;
        //public const int MemoWidth = 80;
        public const int CaptionHeight = 20;
        public const int FrameOffset = 0;
        public const bool ZeroStart = false;
        public const TSPageSec PageSec = TSPageSec.sec6;
        public const TSFps FrameRate = TSFps.fps24;
        public const int HorLine = 6;
        public const TSFrameDisp FrameDisp = TSFrameDisp.pageFrame;

        public const int AutoInputStart = 1;
        public const int AutoInputLast = 10;
        public const int AutoInputKoma = 3;
        public const bool SecInputMode = false;
        public const int LastFrame = 1440;

    }
    //**************************************************************
    //FrameRateの定数
    public enum TSFps
    {
        fps12 = 12,
        fps15 = 15,
        fps24 = 24,
        fps30 = 30
    }
    //**************************************************************
    //１ページのフレーム数
    public enum TSPageSec
    {
        sec3 = 3,
        sec6 = 6
    }
    public enum TSFrameDisp
    {
        frame = 0,
        pageFrame,
        paseSecFrame,
        SecFrame,
        Count
    }
}
