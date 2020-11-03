using System;
using System.Collections.Generic;

namespace GeneralPurposeLibrary.Strings
{
    public class ReplaceItem
    {
        private String _OldValue = String.Empty;
        private String _NewValue = String.Empty;

        public String OldValue
        {
            get { return this._OldValue; }
        }

        public String NewValue
        {
            get { return this._NewValue; }
        }

        public ReplaceItem(String oldValue, String newValue)
        {
            this._OldValue = oldValue;
            this._NewValue = newValue;
        }
    }

    public static class StringHelper
    {
        public static String Replace(String value, List<ReplaceItem> items)
        {
            String ReturnValue = String.Empty;

            String ReplacedString = value;
            value = null;

            for (int iItem = 0; iItem < items.Count; iItem++)
            {
                ReplaceItem ri = items[iItem];

                if (ReplacedString.Contains(ri.OldValue) == true)
                {
                    String NewReplacedString = ReplacedString.Replace(ri.OldValue, ri.NewValue);
                    if (String.Compare(ReplacedString, NewReplacedString) == 0)
                    {
                        Console.WriteLine("Ops!!");
                    }
                    else
                    {
                        ReplacedString = NewReplacedString;
                    }
                    NewReplacedString = null;
                }
            }
            ReturnValue = ReplacedString;
            ReplacedString = null;

            return ReturnValue;
        }

        public static String IfNullGoEmpty(String element)
        {
            String ReturnValue = String.Empty;

            if (String.IsNullOrEmpty(element) == false)
            {
                ReturnValue = element;
            }

            return ReturnValue;
        }

        public static String IfObjectNullGoEmpty(object element)
        {
            String ReturnValue = String.Empty;

            try
            {
                if (element != null)
                {
                    if (element is String)
                    {
                        ReturnValue = IfNullGoEmpty((String)element);
                    }
                    else
                    {
                        ReturnValue = IfNullGoEmpty(element.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ReturnValue = String.Empty;
            }

            return ReturnValue;
        }

        public static String IfNullOrEmptyGoDefault(String element, String defaultValue)
        {
            String ReturnValue = String.Empty;

            if (String.IsNullOrEmpty(element) == false)
            {
                ReturnValue = defaultValue;
            }
            else
            {
                ReturnValue = element;
            }

            return ReturnValue;
        }

        public static String RemoveLast(String element, int number)
        {
            String ReturnValue = String.Empty;

            if (String.IsNullOrEmpty(element) == false)
            {
                int Length = element.Length;
                if (Length >= number)
                {
                    ReturnValue = element.Substring(0, Length - number);
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// https://www.csharpstar.com/remove-duplicate-characters-from-string-in-csharp/
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RemoveDuplicateChars(string key)
        {
            // --- Removes duplicate chars using string concats. ---
            // Store encountered letters in this string.
            string table = "";

            // Store the result in this string.
            string result = "";

            // Loop over each character.
            foreach (char value in key)
            {
                // See if character is in the table.
                if (table.IndexOf(value) == -1)
                {
                    // Append to the table and the result.
                    table += value;
                    result += value;
                }
            }
            return result;
        }

        /// <summary>
        /// https://www.csharpstar.com/csharp-program-to-determine-if-two-words-are-anagrams-of-each-other/
        /// Modified   
        /// </summary>
        /// <param name="sentenceA"></param>
        /// <param name="sentenceB"></param>
        /// <returns></returns>
        public static Boolean AreAnagram(String sentenceA,String sentenceB)
        {

            Boolean ReturnValue = false;
            //step 1  
            char[] char1 = sentenceA.ToLower().ToCharArray();
            char[] char2 = sentenceB.ToLower().ToCharArray();

            //Step 2  
            Array.Sort(char1);
            Array.Sort(char2);

            //Step 3  
            string NewWord1 = new string(char1);
            string NewWord2 = new string(char2);

            //Step 4  
            //ToLower allows to compare the words in same case, in this case, lower case.  
            //ToUpper will also do exact same thing in this context  
            if (NewWord1 == NewWord2)
            {
                ReturnValue = true;
            }
            else
            {
                ReturnValue = false;
            }

            return ReturnValue;
        }


        /// <summary>
        /// https://www.csharpstar.com/c-program-to-reverse-a-string/
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string Reverse(string x)
        {
            string result = "";
            for (int i = x.Length - 1; i >= 0; i--)
                result += x[i];
            return result;
        }


        /// <summary>
        /// https://www.csharpstar.com/csharp-program-to-count-number-of-words-in-a-string/
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Count(string x)
        {
            int result = 0;

            //Trim whitespace from beginning and end of string
            x = x.Trim();

            //Necessary because foreach will execute once with empty string returning 1
            if (x == "")
                return 0;

            //Ensure there is only one space between each word in the passed string
            while (x.Contains("  "))
                x = x.Replace("  ", " ");

            //Count the words
            foreach (string y in x.Split(' '))
                result++;

            return result;
        }


        /// <summary>
        /// https://www.csharpstar.com/palindrome-in-csharp/
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool IsPalindrome(string word)
        {
            int min = 0;
            int max = word.Length - 1;
            while (true)
            {
                if (min > max)
                {
                    return true;
                }
                char a = word[min];
                char b = word[max];
                if (char.ToLower(a) != char.ToLower(b))
                {
                    return false;
                }
                min++;
                max--;
            }
        }

        /// <summary>
        ///  https://www.csharpstar.com/csharp-program-to-determine-if-a-string-has-all-unique-characters/
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsUnique(string s)
        {
            HashSet<char> d = new HashSet<char>();

            foreach (char c in s)
            {
                if (d.Contains(c))
                    return false;
                else
                    d.Add(c);
            }
            return true;
        }

        public static List<String> AllSubString(String element)
        {
            List<String> ReturnValue = new List<string>();

            for (int length = 1; length < element.Length; length++)
            {
                // End index is tricky.
                for (int start = 0; start <= element.Length - length; start++)
                {
                    string substring = element.Substring(start, length);
                    ReturnValue.Add(substring);
                }
            }

            return ReturnValue;
        }


    }
}