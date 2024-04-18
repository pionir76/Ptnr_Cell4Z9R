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
        public short hpv;
        public short hsp;
        public short sts;

        public bool bOnLine;

        public ChamberSts()
        {
            tpv = 0;
            tsp = 0;
            hpv = 0;
            hsp = 0;
            sts = 0;
            bOnLine = false;
        }
    }
}
