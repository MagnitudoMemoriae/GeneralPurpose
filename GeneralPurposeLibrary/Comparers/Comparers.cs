using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GeneralPurposeLibrary.Comparers
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                          System.AttributeTargets.Struct)]
    public class ComparerAttribute : Attribute
    {
        private Type _ComparerType;
        public Type ComparerType
        {
            get
            {
                return this._ComparerType;
            }
        }

        public String Name
        {
            get
            {
                return this._ComparerType.Name;
            }
        }
        public ComparerAttribute(Type comparerType)
        {
            this._ComparerType = comparerType;
        }
    }


    public static class ComparerFactory
    {
        public static IComparer<T> Create<T>()
        {
            IComparer<T> ReturnValue = null;

            List<Type> types = (from t in Assembly.GetExecutingAssembly().GetTypes()
                                where (t.IsClass == true) &&
                                       (t.Namespace == "GeneralPurposeLibrary.Comparers") &&
                                       (t.GetCustomAttributes(typeof(ComparerAttribute), true).Length > 0)
                                select t).ToList();

            Boolean Exist = false;
            for (int iType = 0; iType < types.Count; iType++)
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(types[iType]);  // Reflection.  

                foreach (System.Attribute attr in attrs)
                {
                    if (attr is ComparerAttribute)
                    {
                        ComparerAttribute a = (ComparerAttribute)attr;

                        if (a.ComparerType.Name == typeof(T).Name)
                        {
                            ReturnValue = (IComparer<T>)Activator.CreateInstance(types[iType]);
                            Exist = true;
                            break;
                        }
                    }

                }
                if (Exist == true)
                {
                    break;
                }
            }

            return ReturnValue;
        }
    }

    [ComparerAttribute(typeof(String))]
    public class StringComparer : IComparer<String>
    {
        public int Compare(string x, string y)
        {
            return String.Compare(x, y);
        }
    }

    [ComparerAttribute(typeof(DateTime))]
    public class DateTimeComparer : IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {
            return DateTime.Compare(x, y);
        }
    }

    [ComparerAttribute(typeof(TimeSpan))]
    public class TimeSpanComparer : IComparer<TimeSpan>
    {
        public int Compare(TimeSpan x, TimeSpan y)
        {
            return TimeSpan.Compare(x, y);
        }
    }
}
