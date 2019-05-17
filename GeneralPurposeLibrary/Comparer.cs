using System;

namespace GeneralPurposeLibrary
{
    public static class Comparer
    {
        public static int Compare(int first, int second)
        {
            int ReturnValue = 0;

            if (first == second)
            {
                ReturnValue = 0;
            }
            else
            {
                if (first < second)
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

        public static int Compare(String first, String second)
        {
            return String.Compare(first, second);
        }
    }
}