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
    }
}