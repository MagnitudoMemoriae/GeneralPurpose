using System;
using System.Collections.Generic;

namespace GeneralPurposeLibrary.SetTheory
{
    public class FullOuterJoinResponse<T>
    {
        internal List<T> _OnlyFirst;
        internal List<T> _OnlySecond;
        internal List<T> _Both;

        public List<T> OnlyFirst
        {
            get
            {
                return this._OnlyFirst;
            }
        }

        public List<T> OnlySecond
        {
            get
            {
                return this._OnlySecond;
            }
        }

        public List<T> Both
        {
            get
            {
                return this._Both;
            }
        }

        public FullOuterJoinResponse()
        {
            _OnlyFirst = new List<T>();
            _OnlySecond = new List<T>();
            _Both = new List<T>();
        }
    }

    public class FullOuterJoin<T>
    {
        private List<T> _Firsts;

        public List<T> Firsts
        {
            get
            {
                return this._Firsts;
            }
        }

        private List<T> _Seconds;

        public List<T> Seconds
        {
            get
            {
                return this._Seconds;
            }
        }

        private Func<T, T, int> _Comparer;

        public Func<T, T, int> Comparer
        {
            get
            {
                return this._Comparer;
            }
        }

        private Func<List<T>, List<T>> _Sorter;

        public Func<List<T>, List<T>> Sorter
        {
            get
            {
                return this._Sorter;
            }
        }

        private FullOuterJoinResponse<T> _Response = new FullOuterJoinResponse<T>();

        public FullOuterJoinResponse<T> Response
        {
            get
            {
                return this._Response;
            }
        }

        public FullOuterJoin(List<T> firsts, List<T> seconds, Func<T, T, int> comparer, Func<List<T>, List<T>> sorter)
        {
            this._Firsts = firsts;
            this._Seconds = seconds;
            this._Comparer = comparer;
            this._Sorter = sorter;
        }

        public FullOuterJoin(List<T> firsts, List<T> seconds, Func<T, T, int> comparer)
        {
            this._Firsts = firsts;
            this._Seconds = seconds;
            this._Comparer = comparer;
            this._Sorter = null;
        }

        public void Process()
        {
            _Response = new FullOuterJoinResponse<T>();

            List<T> FirstOrderd = new List<T>();
            List<T> SecondOrderd = new List<T>();
            if (this._Sorter != null)
            {
                FirstOrderd = _Sorter(this._Firsts);
                SecondOrderd = _Sorter(this._Seconds);
            }
            else
            {
                FirstOrderd.AddRange(this._Firsts);
                SecondOrderd.AddRange(this._Seconds);
            }

            int iFirst = 0;
            int iSecond = 0;
            // Only for debug
            int iCounter = 0;
            do
            {
                iCounter++;
                T First = FirstOrderd[iFirst];
                T Second = SecondOrderd[iSecond];

                int Comparation = this.Comparer(First, Second);
                if (Comparation == 0)
                {
                    _Response._Both.Add(First);
                    iFirst++;
                    iSecond++;
                }
                else
                {
                    if (Comparation < 0)
                    {
                        // first is less then second
                        _Response._OnlyFirst.Add(First);
                        iFirst++;
                    }
                    else
                    {
                        // first is more then second
                        _Response._OnlySecond.Add(Second);
                        iSecond++;
                    }
                }
            }
            while (iFirst < FirstOrderd.Count && iSecond < SecondOrderd.Count);

            for (int iFirstRemain = iFirst; iFirstRemain < FirstOrderd.Count; iFirstRemain++)
            {
                _Response._OnlyFirst.Add(FirstOrderd[iFirstRemain]);
            }

            for (int iSecondRemain = iSecond; iSecondRemain < SecondOrderd.Count; iSecondRemain++)
            {
                _Response._OnlySecond.Add(SecondOrderd[iSecondRemain]);
            }
        }
    }
}