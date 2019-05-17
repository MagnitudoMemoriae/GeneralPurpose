using System;
using System.Collections.Generic;

namespace GeneralPurposeLibrary
{
    public enum IntervalType
    {
        IN_IN,
        IN_OUT,
        OUT_OUT,
        OUT_IN
    }

    public class Range
    {
        private int _First = 0;
        private int _Second = 0;
        private IntervalType _Interval = IntervalType.IN_IN;

        public int First
        {
            get
            {
                return this._First;
            }
        }

        public int Second
        {
            get
            {
                return this._Second;
            }
        }

        public IntervalType Interval
        {
            get
            {
                return this._Interval;
            }
        }

        public Range(int first, int second, IntervalType interval)
        {
            this._First = first;
            this._Second = second;
            this._Interval = interval;

        }

        public Boolean Match(int value)
        {
            Boolean ReturnValue = false;

            switch (this._Interval)
            {
                case IntervalType.IN_IN:
                    ReturnValue = (this._First <= value) && (value <= this._Second);
                    break;

                case IntervalType.IN_OUT:
                    ReturnValue = (this._First <= value) && (value < this._Second);
                    break;

                case IntervalType.OUT_IN:
                    ReturnValue = (this._First < value) && (value <= this._Second);
                    break;

                case IntervalType.OUT_OUT:
                    ReturnValue = (this._First < value) && (value < this._Second);
                    break;
            }

            return ReturnValue;
        }
    }

    public class RangeEngine
    {
        private List<Range> _Ranges = new List<Range>();

        public RangeEngine(List<Range> ranges)
        {
            this._Ranges = ranges;
        }

        public List<Boolean> Match(int value)
        {
            List<Boolean> ReturnValue = new List<Boolean>();

            for (int iElement = 0; iElement < this._Ranges.Count; iElement++)
            {
                Range range = this._Ranges[iElement];
                Boolean IsMatched = range.Match(value);
                ReturnValue.Add(IsMatched);
            }

            return ReturnValue;
        }
    }

    public class Range<T>
    {
        private T _First = default(T);
        private T _Second = default(T);
        private IntervalType _Interval = IntervalType.IN_IN;
        private Func<T, T, int> _Comparer;

        public T First
        {
            get
            {
                return this._First;
            }
        }

        public T Second
        {
            get
            {
                return this._Second;
            }
        }

        public IntervalType Interval
        {
            get
            {
                return this._Interval;
            }
        }

        public Func<T, T, int> Comparer
        {
            get
            {
                return this._Comparer;
            }
        }

        public Range(T first, T second, IntervalType interval, Func<T, T, int> comparer)
        {
            this._First = first;
            this._Second = second;
            this._Interval = interval;
            this._Comparer = comparer;
        }

        public Boolean Match(T value)
        {
            Boolean ReturnValue = false;

            switch (this._Interval)
            {
                case IntervalType.IN_IN:
                    ReturnValue = (this._Comparer(this._First, value) <= 0) && (this._Comparer(value, this._Second) <= 0);
                    break;

                case IntervalType.IN_OUT:
                    ReturnValue = (this._Comparer(this._First, value) <= 0) && (this._Comparer(value, this._Second) < 0);
                    break;

                case IntervalType.OUT_IN:
                    ReturnValue = (this._Comparer(this._First, value) < 0) && (this._Comparer(value, this._Second) <= 0);
                    break;

                case IntervalType.OUT_OUT:
                    ReturnValue = (this._Comparer(this._First, value) < 0) && (this._Comparer(value, this._Second) < 0);
                    break;
            }

            return ReturnValue;
        }
    }

    public class RangeEngine<T>
    {
        private List<Range<T>> _Ranges = new List<Range<T>>();

        public RangeEngine(List<Range<T>> ranges)
        {
            this._Ranges = ranges;
        }

        public List<Boolean> Match(T value)
        {
            List<Boolean> ReturnValue = new List<Boolean>();

            for (int iElement = 0; iElement < this._Ranges.Count; iElement++)
            {
                Range<T> range = this._Ranges[iElement];
                Boolean IsMatched = range.Match(value);
                ReturnValue.Add(IsMatched);
            }

            return ReturnValue;
        }
    }
}