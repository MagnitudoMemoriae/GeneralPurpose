using System;
using System.Collections.Generic;

namespace GeneralPurposeLibrary.Comparers
{
    public enum Order
    {
        ERROR = 0,
        LESS = 2,
        LESS_OR_EQUAL = 3,
        EQUAL = 1,
        EQUAL_OF_GREATER = 5,
        GREATER = 4
    }

    public static class Comparer
    {

        /// <summary>
        /// If first is less then    second = -1
        /// If first equal then      second = 0
        /// if first is graater then second = 1
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
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

        public static int CompareInt(int first, int second)
        {
            return Compare(first, second);
        }

        public static Order GetOrder(int first, int second)
        {
            return GetOrder(Compare(first, second));
        }



        /// <summary>
        /// If first is less then    second = -1
        /// If first equal then      second = 0
        /// if first is graater then second = 1
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int Compare(long first, long second)
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

        public static int CompareLong(long first, long second)
        {
            return Compare(first, second);
        }

        public static Order GetOrder(long first, long second)
        {
            return GetOrder(Compare(first, second));
        }

        /// <summary>
        /// If first is less then    second = -1
        /// If first equal then      second = 0
        /// if first is graater then second = 1
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int Compare(float first, float second)
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

        public static int CompareFloat(float first, float second)
        {
            return Compare(first, second);
        }

        public static Order GetOrder(float first, float second)
        {
            return GetOrder(Compare(first, second));
        }

        /// <summary>
        /// If first is less then    second = -1
        /// If first equal then      second = 0
        /// if first is graater then second = 1
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int Compare(double first, double second)
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

        public static int CompareDouble(double first, double second)
        {
            return Compare(first, second);
        }

        public static Order GetOrder(double first, double second)
        {
            return GetOrder(Compare(first, second));
        }


        public static Order GetOrder(int intOrder)
        {
            Order ReturnValue = Order.ERROR;

            switch(intOrder)
            {
                case -1:
                    ReturnValue = Order.LESS;
                    break;

                case 0:
                    ReturnValue = Order.EQUAL;
                    break;

                case 1:
                    ReturnValue = Order.GREATER;
                    break;
            }

            return ReturnValue;

        }

        public static int Compare(String first, String second)
        {
            return String.Compare(first, second);
        }
    }
}