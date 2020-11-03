using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralPurposeLibrary.Arrays
{
    public static class ArrayHelpers
    {

        /// <summary>
        /// https://www.csharpstar.com/csharp-program-to-rotate-array-to-the-right-given-a-pivot/
        /// </summary>
        /// <param name="x"></param>
        /// <param name="pivot"></param>
        /// <returns></returns>
        public static int[] Rotate(int[] x, int pivot)
        {
            if (pivot < 0 || x == null)
                throw new Exception("Invalid argument");

            pivot = pivot % x.Length;

            //Rotate first half
            x = RotateSub(x, 0, pivot - 1);

            //Rotate second half
            x = RotateSub(x, pivot, x.Length - 1);

            //Rotate all
            x = RotateSub(x, 0, x.Length - 1);

            return x;
        }


        /// <summary>
        /// https://www.csharpstar.com/csharp-program-to-rotate-array-to-the-right-given-a-pivot/
        /// </summary>
        /// <param name="x"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int[] RotateSub(int[] x, int start, int end)
        {
            while (start < end)
            {
                int temp = x[start];
                x[start] = x[end];
                x[end] = temp;
                start++;
                end--;
            }
            return x;
        }

        /// <summary>
        /// https://www.csharpstar.com/csharp-program-to-determine-if-any-two-integers-in-array-sum-to-given-integer/
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool TwoIntegersSumToTarget(int[] arr, int target)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int k = 0; k < arr.Length; k++)
                {
                    if (i != k)
                    {
                        int sum = arr[i] + arr[k];
                        if (sum == target)
                            return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// https://www.csharpstar.com/csharp-program-to-find-majority-element-in-an-unsorted-array/
        /// Find majority element in an unsorted array
        /// Ex. {1,2,3,4,5,2,2,2,2}, 2 is the majority element because it accounts for more than 50% of the array     
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int GetMajorityElement(params int[] x)
        {
            Dictionary<int, int> d = new Dictionary<int, int>();
            int majority = x.Length / 2;

            //Stores the number of occcurences of each item in the passed array in a dictionary
            foreach (int i in x)
            {
           
                if (d.ContainsKey(i))
                {
                    d[i]++;
                    //Checks if element just added is the majority element
                    if (d[i] > majority)
                        return i;
                }
                else
                {
                    d.Add(i, 1);
                }
            }    
            //No majority element
            throw new Exception("No majority element in array");
        }
    }
}
