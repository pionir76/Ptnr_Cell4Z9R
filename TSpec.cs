using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ptnr.TSpec;

namespace Ptnr
{
    public interface ICloneable<T>
    {
        T Clone();
    }

    public class TSpec : ICloneable<TSpec>
    {
        public enum Slop
        {
            Up,
            Dn,
        };

        public enum WorkingSts
        {
            Begin,
            TouchSpCheck,
            Waiting,
            Testing,
            End,
        };

        public enum WorkingRes
        {
            Fail,
            Success,
            NotDef,
        };

        public DateTime workStartTm;
        public DateTime workEndTm;
        public DateTime pvPassingTm;

        public WorkingSts workingSts;
        public WorkingRes result;

        public short startPv;
        public int waitTm;
        public int testTm;
        public int waitTimeOutTm;
        public bool bReport;

        public TSpec() {
            workingSts = WorkingSts.Begin;
            startPv = 0;
            bReport = true;

            waitTm =0;
            testTm=0;
            waitTimeOutTm=70;

            result = WorkingRes.NotDef;
        }

        public TSpec Clone()
        {
            return new TSpec();
        }
    }

    public class TSpecTpChamber : TSpec
    {
        public Slop tempSlop;
        public bool bTouchTemp;
        public short tsp;

        public short resCtrTMin;
        public short resCtrTMax;
        public short resCtrRamp;
        public short resUnifMin;
        public short resUnifMax;
        public short resTOver;
        public short resROver;
        public int resStableTm;

        // Judge
        public short jugTDisp;
        public short jugRamp;
        public short jugUnif;
        public short jugTOver;
        public int jugStableTm;

        public bool bUseTDisp;
        public bool bUseRamp;
        public bool bUseUnif;
        public bool bUseTOver;
        public bool bUseStableTm;

        public List<short>[] resRec;

        public TSpecTpChamber()
        {
            tsp = 0;
            bTouchTemp = false;

            resCtrTMin = SysDefs.NOT_DEFVAL;
            resCtrTMax = SysDefs.NOT_DEFVAL;

            resTOver = SysDefs.NOT_DEFVAL;
            resROver = SysDefs.NOT_DEFVAL;
            resStableTm = SysDefs.NOT_DEFVAL;
            resCtrRamp = 0;
            resUnifMin = SysDefs.NOT_DEFVAL;
            resUnifMax = SysDefs.NOT_DEFVAL;

            jugTDisp = 0;
            jugRamp = 0;
            jugUnif = 0;
            jugTOver = 0;
            jugStableTm = 0;

            bUseTDisp = false;
            bUseRamp = false;
            bUseUnif = false;
            bUseTOver = false;
            bUseStableTm = false;

            resRec = new List<short>[SysDefs.MAX_REC_CHCNT];

            resRec[SysDefs.CH1] = new List<short>();
            resRec[SysDefs.CH2] = new List<short>();
            resRec[SysDefs.CH3] = new List<short>();
            resRec[SysDefs.CH4] = new List<short>();
            
            resRec[SysDefs.CH1].Capacity = 600;
            resRec[SysDefs.CH2].Capacity = 600;
            resRec[SysDefs.CH3].Capacity = 600;
            resRec[SysDefs.CH4].Capacity = 600;
        }

        public void Reset()
        {
            resCtrTMin = SysDefs.NOT_DEFVAL;
            resCtrTMax = SysDefs.NOT_DEFVAL;
            resCtrRamp = 0;

            resTOver = SysDefs.NOT_DEFVAL;
            resROver = SysDefs.NOT_DEFVAL;

            resUnifMin = SysDefs.NOT_DEFVAL;
            resUnifMax = SysDefs.NOT_DEFVAL;

            resStableTm = SysDefs.NOT_DEFVAL;

            workingSts = WorkingSts.Begin;
            result = WorkingRes.NotDef;

            bTouchTemp = false;

            resRec[SysDefs.CH1].Clear();
            resRec[SysDefs.CH2].Clear();
            resRec[SysDefs.CH3].Clear();
            resRec[SysDefs.CH4].Clear();
        }

        public void AddRecData(int ch, short val)
        {
            if (resRec[ch].Count >= resRec[ch].Capacity)
            {
                resRec[ch].RemoveAt(0);
            }

            resRec[ch].Add(val);
        }

        public short GetMaxDiffRecData(int ch)
        {
            short maxDiff = 0;
            short val = 0;

            for (int i = 0; i < resRec[ch].Count; i++)
            {
                short diff = (short)Math.Abs(tsp - resRec[ch][i]);
                if (diff >= maxDiff)
                {
                    val = resRec[ch][i];
                }
            }
            return val;
        }
    }
}
