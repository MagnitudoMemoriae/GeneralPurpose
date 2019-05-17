using System;
using System.Reflection;

namespace GeneralPurposeLibrary.Reflection
{
    public static class ReflectionHelper
    {
        public static Object GetFieldValue(object src, string name)
        {
            return src.GetType().GetField(name).GetValue(src);
        }

        public static Boolean ExistFieldName(object element, String name)
        {
            Boolean ReturnValue = false;

            if (element != null)
            {
                foreach (FieldInfo info in element.GetType().GetFields())
                {
                    if (String.Compare(info.Name.ToLower(), name.ToLower()) == 0)
                    {
                        ReturnValue = true;
                        break;
                    }
                }
            }
            return ReturnValue;
        }

        public static Object GetValueFromFieldName(Object element, String name)
        {
            Object ReturnValue = null;

            if (element != null)
            {
                foreach (FieldInfo info in element.GetType().GetFields())
                {
                    if (String.Compare(info.Name.ToLower(), name.ToLower()) == 0)
                    {
                        ReturnValue = info.GetValue(element);
                        break;
                    }
                }
            }
            return ReturnValue;
        }

        public static Boolean SetValueFromFieldName(Object element, String name, Object value)
        {
            Boolean ReturnValue = false;

            if (element != null)
            {
                foreach (FieldInfo info in element.GetType().GetFields())
                {
                    if (String.Compare(info.Name.ToLower(), name.ToLower()) == 0)
                    {
                        info.SetValue(element, value);
                        ReturnValue = true;
                        break;
                    }
                }
            }
            return ReturnValue;
        }

        public static Object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static Boolean ExistPropertyName(object element, String name)
        {
            Boolean ReturnValue = false;

            if (element != null)
            {
                foreach (PropertyInfo info in element.GetType().GetProperties())
                {
                    if (String.Compare(info.Name.ToLower(), name.ToLower()) == 0)
                    {
                        ReturnValue = true;
                        break;
                    }
                }
            }
            return ReturnValue;
        }

        public static Object GetValueFromPropertyName(Object element, String name)
        {
            Object ReturnValue = null;

            if (element != null)
            {
                foreach (PropertyInfo info in element.GetType().GetProperties())
                {
                    if (String.Compare(info.Name.ToLower(), name.ToLower()) == 0)
                    {
                        ReturnValue = info.GetValue(element);
                        break;
                    }
                }
            }
            return ReturnValue;
        }

        public static Boolean SetValueFromPropertyName(Object element, String name, Object value)
        {
            Boolean ReturnValue = false;

            if (element != null)
            {
                foreach (PropertyInfo info in element.GetType().GetProperties())
                {
                    if (String.Compare(info.Name.ToLower(), name.ToLower()) == 0)
                    {
                        info.SetValue(element, value);
                        ReturnValue = true;
                        break;
                    }
                }
            }
            return ReturnValue;
        }
    }
}