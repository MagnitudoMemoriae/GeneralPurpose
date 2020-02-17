using System;
using System.Collections.Generic;

namespace GeneralPurposeLibrary.Programming
{
    public class GearArgument
    {
        private Dictionary<String, Object> _TheDict = new Dictionary<String, Object>();

        public GearArgument()
        {
        }

        public Boolean Add(String key, Object value)
        {
            Boolean ReturnValue = false;
            if (this._TheDict.ContainsKey(key) == false)
            {
                this._TheDict.Add(key, value);
            }
            return ReturnValue;
        }

        public Object GetArgument(String key)
        {
            Object ReturnValue = null;
            if (this._TheDict.ContainsKey(key) == true)
            {
                ReturnValue = this._TheDict[key];
            }
            return ReturnValue;
        }

        public IEnumerable<String> GetKeys()
        {
            foreach (KeyValuePair<String, Object> item in this._TheDict)
            {
                yield return item.Key;
            }
        }

        public int Count
        {
            get
            {
                return this._TheDict.Count;
            }
        }
    }
}