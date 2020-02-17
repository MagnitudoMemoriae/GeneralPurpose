using System;
using System.Collections.Generic;
using System.Reflection;

namespace GeneralPurposeLibrary.Attributes
{
    public class Enumerator : Attribute
    {
        private int _Index;

        public int Index
        {
            get
            {
                return this._Index;
            }
            set
            {
                this._Index = value;
            }
        }

        private List<int> _Indexes = new List<int>();

        public List<int> Indexes
        {
            get
            {
                return this._Indexes;
            }
            set
            {
                this._Indexes = value;
            }
        }

        private String _Tag = String.Empty;

        public String Tag
        {
            get { return this._Tag; }
        }

        public Enumerator(int index)
        {
            this._Index = index;
        }

        public Enumerator(int index, String tag)
        {
            this._Index = index;
            this._Tag = tag;
        }

        public Enumerator(List<int> indexes)
        {
            this._Indexes = indexes;
        }
    }

    /// <summary>
    /// Helper class to manage Enumerator
    /// </summary>
    public static class EnumeratorHelper
    {
        /// <summary>
        /// Get the dictionary of the properties
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static SortedDictionary<int, PropertyInfo> GetProperties(object element)
        {
            SortedDictionary<int, PropertyInfo> ReturnValue = new SortedDictionary<int, PropertyInfo>();

            if (element != null)
            {
                foreach (PropertyInfo prop in element.GetType().GetProperties())
                {
                    Attribute[] attrs = System.Attribute.GetCustomAttributes(prop);

                    for (int iAttribute = 0; iAttribute < attrs.Length; iAttribute++)
                    {
                        Attribute attribute = attrs[iAttribute];
                        if (attribute is Enumerator)
                        {
                            Enumerator enumerator = ((Enumerator)attribute);
                            int Index = enumerator.Index;
                            if (ReturnValue.ContainsKey(Index) == false)
                            {
                                ReturnValue.Add(Index, prop);
                            }
                        }
                    }
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// Get the dictionry of the properties filtered by index
        /// </summary>
        /// <param name="element"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static SortedDictionary<int, PropertyInfo> GetProperties(object element, int index)
        {
            SortedDictionary<int, PropertyInfo> ReturnValue = new SortedDictionary<int, PropertyInfo>();

            if (element != null)
            {
                foreach (PropertyInfo prop in element.GetType().GetProperties())
                {
                    Attribute[] attrs = System.Attribute.GetCustomAttributes(prop);

                    for (int iAttribute = 0; iAttribute < attrs.Length; iAttribute++)
                    {
                        Attribute attribute = attrs[iAttribute];
                        if (attribute is Enumerator)
                        {
                            Enumerator enumerator = ((Enumerator)attribute);
                            int Index = enumerator.Indexes[index];
                            if (ReturnValue.ContainsKey(Index) == false)
                            {
                                ReturnValue.Add(Index, prop);
                            }
                        }
                    }
                }
            }

            return ReturnValue;
        }
    }
}