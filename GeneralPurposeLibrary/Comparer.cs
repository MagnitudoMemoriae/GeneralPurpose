using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralPurposeLibrary
{
    public class Comparer
    {
        public static int Compare(int first , int second)
        {
            int ReturnValue = 0;

            if(first == second)
            {
                ReturnValue = 0;
            }
            else
            {
                if(first < second)
                {
                    ReturnValue = -1;
                }
                else
                {
                    ReturnValue = 1;
                }
            }

            return ReturnValue;
        }
    }
}
