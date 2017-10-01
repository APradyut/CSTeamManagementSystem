using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Team_Database
{
    class SupportLayer
    {
        /*For inputType()
         * 0   ->   alpha
         * 1   ->   number
         * -1  ->   anything else
         */
        public int inputType(string queryLine)
        {
            Regex rg1 = new Regex(@"^[a-zA-Z\s,]*$");
            Regex rg2 = new Regex(@"^[0-9\s,]*$");
            if (rg1.IsMatch(queryLine.Trim()))
            {
                return 0;
            }
            else if (rg2.IsMatch(queryLine.Trim()))
            {
                return 1;
            }
            return -1;
        }
    }
}
