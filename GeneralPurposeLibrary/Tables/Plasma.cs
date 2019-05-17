using System;
using System.Collections.Generic;

namespace GeneralPurposeLibrary.Tables
{
    public class PlasmaState
    {
    }

    public class PlasmaParticel
    {
        public readonly Dictionary<String, Object> Properties = new Dictionary<String, Object>();

        public PlasmaParticel(Dictionary<String, String> properties)
        {
            foreach (var item in properties)
            {
                Properties.Add(item.Key, item.Value);
            }
        }

        public void Add(String name, String content)
        {
            Properties.Add(name, content);
        }

        public void Add(String name, int content)
        {
            Properties.Add(name, content);
        }

        public PlasmaParticel()
        {
        }
    }

    public class PlasmaBulk
    {
        private HashSet<PlasmaParticel> _Particels = new HashSet<PlasmaParticel>();

        public HashSet<PlasmaParticel> Particels
        {
            get
            {
                return this._Particels;
            }
            set
            {
                this._Particels = value;
            }
        }
    }
}