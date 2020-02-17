using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GeneralPurposeLibrary.Programming
{
    public class NPC
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class BaseNotifyItem
    {
        private Type _TypeItem;

        public Type TypeItem
        {
            get
            {
                return _TypeItem;
            }
        }

        private Object _Value;

        public Object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                this._IsSetted = true;
            }
        }

        private Boolean _IsSetted;

        public Boolean IsSetted
        {
            get
            {
                return _IsSetted;
            }
        }

        public BaseNotifyItem(Type typeItem)
        {
            this._TypeItem = typeItem;
            this._IsSetted = false;
            this._Value = GetDefault(typeItem);
        }

        private object GetDefault(Type t)
        {
            Type thisType = this.GetType();
            MethodInfo miGetDefaultGeneric = null;
            foreach (MethodInfo item in thisType.GetMethods())
            {
                if (item.Name == "GetDefaultGeneric")
                {
                    miGetDefaultGeneric = item;
                    break;
                }
            }

            if (miGetDefaultGeneric == null)
            {
                throw new MissingMethodException();
            }

            MethodInfo miMakeGenericMethod = miGetDefaultGeneric.MakeGenericMethod(t);

            //return this.GetType().GetMethod("GetDefaultGeneric").MakeGenericMethod(t).Invoke(this, null);
            return miMakeGenericMethod.Invoke(this, null);
        }

        public T GetDefaultGeneric<T>()
        {
            return default(T);
        }
    }

    public class BaseNotify : NPC
    {
        private Dictionary<String, BaseNotifyItem> _Elements = new Dictionary<String, BaseNotifyItem>();

        public BaseNotify(Type caller)
        {
            foreach (var prop in caller.GetProperties())
            {
                _Elements.Add(prop.Name, new BaseNotifyItem(prop.PropertyType));
            }
        }

        public void Set(Object value, [CallerMemberName] String field = "")
        {
            if (_Elements.ContainsKey(field) == true)
            {
                this._Elements[field].Value = value;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Object Get([CallerMemberName] String field = "")
        {
            Object Returnvalue = null;

            if (this._Elements.TryGetValue(field, out BaseNotifyItem value) == true)
            {
                Returnvalue = value.Value;
            }
            else
            {
                throw new ArgumentException();
            }

            return Returnvalue;
        }
    }


#if false
          public class NotifyUserControl : UserControl, INotifyPropertyChanged
    {
        protected BaseNotify _BaseNotify;

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NotifyUserControl()
        {
        }

        protected void Init(Type caller)
        {
            this._BaseNotify = new BaseNotify(caller);
        }

        protected Object Get([CallerMemberName] String field = "")
        {
            return this._BaseNotify.Get(field);
        }

        protected void Set(Object value, [CallerMemberName] String field = "")
        {
            this._BaseNotify.Set(value, field);
        }

        private Dictionary<String, Boolean> _OriginalFlippedValueProperty = new Dictionary<String, Boolean>();

        public void Flip(List<String> elements, Boolean value)
        {
            this._OriginalFlippedValueProperty = new Dictionary<String, Boolean>();
            for (int iElement = 0; iElement < elements.Count; iElement++)
            {
                String PropertyName = elements[iElement];
                this._OriginalFlippedValueProperty.Add(PropertyName, (Boolean)this.GetType().GetProperty(PropertyName).GetValue(this));
                this.Set(value, PropertyName);
            }
        }

        public void DeFlip()
        {
            foreach (KeyValuePair<String, Boolean> item in this._OriginalFlippedValueProperty)
            {
                //this.GetType().GetProperty(item.Key).SetValue(this, item.Value);
                this.Set(item.Value, item.Key);
            }
            this._OriginalFlippedValueProperty = new Dictionary<String, Boolean>();
        }
    }
#endif

}