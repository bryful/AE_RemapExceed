using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_RemapExceed
{
    //**************************************************************
    public class TSParams
    {
        public enum N
        {
            Left = 0,
            Top,
            Height,
            CellCount,
            FrameCount,
            CellWidth,
            CellHeight,
            FrameWidth,
            MemoWidth,
            CaptionHeight,
            FrameOffset,
            ZeroStart,
            FrameDisp,
            PageSec,
            FrameRate,
            AE_Vaersion,
            EmptyCell,
            remaping,
            AutoInputStart,
            AutoInputLast,
            AutoInputKoma,
            SecInputMode,
            LastFrame,
            IsPrintTITLE,
            IsPrintSUB_TITLE,
            IsPrintOPUS,
            IsPrintSCECNE,
            IsPrintCUT,
            IsPrintCREATE_USER,
            IsPrintUPDATE_USER,
            IsPrintCAMPANY_NAME,
            IsPrintComment,
            CommentAlign,
            Count
        }
        //----------------------------------------------
        public TSParams()
        {
        }
        //----------------------------------------------
        public int Count
        {
            get { return (int)N.Count; }
        }
        //----------------------------------------------
        public string[] Tags = new string[(int)N.Count]
        {
        "Left",
        "Top",
        "Height",
        "CellCount",
        "FrameCount",
        "CellWidth",
        "CellHeight",
        "FrameWidth",
        "MemoWidth",
        "CaptionHeight",
        "FrameOffset",
        "ZeroStart",
        "FrameDisp",
        "PageSec",
        "FrameRate",
        "AE_Version",
        "EmptyCell",
        "remaping",
        "AutoInputStart",
        "AutoInputLast",
        "AutoInputKoma",
        "SecInputMode",
        "LastFrame",
        "IsPrintTITLE",
        "IsPrintSUB_TITLE",
        "IsPrintOPUS",
        "IsPrintSCECNE",
        "IsPrintCUT",
        "IsPrintCREATE_USER",
        "IsPrintUPDATE_USER",
        "IsPrintCAMPANY_NAME",
        "IsPrintComment",
        "CommentAlign"

        };
        public string Tag(N tt)
        {
            return Tags[(int)tt];
        }
    }

}
