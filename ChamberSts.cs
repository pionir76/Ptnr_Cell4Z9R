using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptnr
{
    public class ChamberSts
    {
        public short tpv;
        public short tsp;
        public short sts;

        public bool bOnLine;
        public bool bRun;

        public ChamberSts()
        {
            tpv = 0;
            tsp = 0;
            sts = 0;
            bOnLine = false;
            bRun =false;
        }
    }
}
