using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Utils
{
    public class NumberFormatUtil
    {
        public static string AddZeroPrefix(int requiredLength, int val)
        {
            return val.ToString().PadLeft(requiredLength,'0') ;
        }
    }
}
