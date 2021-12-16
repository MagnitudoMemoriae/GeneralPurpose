using GeneralPurposeLibrary.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace GeneralPurposeLibrary.SetTheory
{
    public enum IntervalType
    {
        OUT_OUT,
        IN_OUT,
        OUT_IN,
        IN_IN,
           
    }

    public enum EdgeType
    {
        OUT,
        IN 
    }


    public class Edge<T>
    {
        public readonly T Value;
        public readonly EdgeType Type;
        public readonly int Index = 0;


        public Edge(T value, EdgeType type, int index )  : this(value, type)
        {
            Index = index;
        }

        public Edge(T value , EdgeType type)
        {
            Value = value;
            Type = type;
        }
    }

    public static class RangeHelpers
    {
        public static EdgeType GetBeginEdge(IntervalType interval)
        {
            EdgeType ReturnValue = EdgeType.IN;

            switch (interval)
            {
                case IntervalType.IN_IN:                    
                case IntervalType.IN_OUT:
                    ReturnValue = EdgeType.IN;
                    break;

                case IntervalType.OUT_IN:
                case IntervalType.OUT_OUT:
                    ReturnValue = EdgeType.OUT;
                    break;
            }

            return ReturnValue;
        }

        public static EdgeType GetFinishEdge(IntervalType interval)
        {
            EdgeType ReturnValue = EdgeType.IN;

            switch (interval)
            {
                case IntervalType.IN_IN:
                case IntervalType.OUT_IN:
                    ReturnValue = EdgeType.IN;
                    break;

                case IntervalType.IN_OUT:
                case IntervalType.OUT_OUT:
                    ReturnValue = EdgeType.OUT;
                    break;
            }

            return ReturnValue;
        }

        public static IntervalType GetInterval(EdgeType begin,EdgeType finish)
        {
            IntervalType ReturnValue = IntervalType.OUT_OUT;


            if(begin == EdgeType.IN)
            {
                if (finish == EdgeType.IN)
                {
                    ReturnValue = IntervalType.IN_IN;
                }
                else
                {
                    ReturnValue = IntervalType.IN_OUT;
                }
            }
            else
            {
                if (finish == EdgeType.IN)
                {
                    ReturnValue = IntervalType.OUT_IN;
                }
                else
                {
                    ReturnValue = IntervalType.OUT_OUT;
                }
            }

            return ReturnValue;
        }
    }

    public class RangeInt
    {
        private int _Begin = 0;
        private int _Finish = 0;
        public readonly Boolean IsValid;
        private IntervalType _Interval = IntervalType.IN_IN;

        public int Begin
        {
            get
            {
                return this._Begin;
            }
        }

        public EdgeType BeginEdge
        {
            get
            {
                return RangeHelpers.GetBeginEdge(this._Interval);
            }
        }

        public int Finish
        {
            get
            {
                return this._Finish;
            }
        }

        public EdgeType FinishEdge
        {
            get
            {
                return RangeHelpers.GetFinishEdge(this._Interval);
            }
        }

        public IntervalType Interval
        {
            get
            {
                return this._Interval;
            }
        }

        public RangeInt(int begin, int finish, IntervalType interval)
        {
            this._Begin = begin;
            this._Finish = finish;
            this._Interval = interval;

            IsValid = GetIsValid(begin, finish, interval);

        }

        public static Boolean GetIsValid(int begin, int finish, IntervalType interval)
        {
            Boolean ReturnValue = false;
            if(begin < finish)
            {
                ReturnValue = true;
            }
            else if( (begin == finish) && (interval == IntervalType.IN_IN))
            {
                ReturnValue = true;
            }

            return ReturnValue;
        }

        public Boolean Match(int value)
        {
            Boolean ReturnValue = false;

            switch (this._Interval)
            {
                case IntervalType.IN_IN:
                    ReturnValue = (this._Begin <= value) && (value <= this._Finish);
                    break;

                case IntervalType.IN_OUT:
                    ReturnValue = (this._Begin <= value) && (value < this._Finish);
                    break;

                case IntervalType.OUT_IN:
                    ReturnValue = (this._Begin < value) && (value <= this._Finish);
                    break;

                case IntervalType.OUT_OUT:
                    ReturnValue = (this._Begin < value) && (value < this._Finish);
                    break;
            }

            return ReturnValue;
        }
    }

    public class RangeIntEngine
    {
        private List<RangeInt> _Ranges = new List<RangeInt>();

        public RangeIntEngine(List<RangeInt> ranges)
        {
            this._Ranges = ranges;
        }

        public List<Boolean> Match(int value)
        {
            List<Boolean> ReturnValue = new List<Boolean>();

            for (int iElement = 0; iElement < this._Ranges.Count; iElement++)
            {
                RangeInt range = this._Ranges[iElement];
                Boolean IsMatched = range.Match(value);
                ReturnValue.Add(IsMatched);
            }

            return ReturnValue;
        }
    }

    public class Range<T> 
    {
        private String _Name = String.Empty;
        private T _Begin = default(T);
        private T _Finish = default(T);
        private IntervalType _Interval = IntervalType.IN_IN;
        private Func<T, T, int> _Comparer;
        private Boolean _IsValid = false;

        public T Begin
        {
            get
            {
                return this._Begin;
            }
        }

        public EdgeType BeginEdge
        {
            get
            {
                return RangeHelpers.GetBeginEdge(this._Interval);
            }
        }

        public T Finish
        {
            get
            {
                return this._Finish;
            }
        }

        public EdgeType FinishEdge
        {
            get
            {
                return RangeHelpers.GetFinishEdge(this._Interval);
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



        /// <summary>
        /// If first is equal or less then second = true else false
        /// </summary>
        public Boolean IsValid
        {
            get
            {
                return this._IsValid;
            }
        }

        private void Init(T begin, T finish, IntervalType interval, Func<T, T, int> comparer)
        {
            this._Begin = begin;
            this._Finish = finish;

            this._Interval = interval;
            this._Comparer = comparer;

            int iOrder = this._Comparer(this._Begin, this._Finish);

            Order order = Comparers.Comparer.GetOrder(iOrder);

            switch (order)
            {
                case Order.ERROR:
                    this._IsValid = false;
                    break;

                case Order.LESS:
                    this._IsValid = true;
                    break;

                case Order.EQUAL:

                    if(Interval == IntervalType.IN_IN)
                    {
                        this._IsValid = true;
                    }
                    else
                    {
                        this._IsValid = false;
                    }

                    break;

                case Order.GREATER:
                    this._IsValid = false;
                    break;

                default:
                    this._IsValid = false;
                    break;
            }
        }

        public Range(Edge<T> edgeBegin,Edge<T> edgeFinish , Func<T, T, int> comparer)
        {
            IntervalType interval = IntervalType.IN_IN;
            switch (edgeBegin.Type)
            {
                case EdgeType.IN:
                    switch (edgeFinish.Type)
                    {
                        case EdgeType.IN:
                            interval = IntervalType.IN_IN;
                            break;
                        case EdgeType.OUT:
                            interval = IntervalType.IN_OUT;
                            break;
                    }
                    break;
                case EdgeType.OUT:
                    switch (edgeFinish.Type)
                    {
                        case EdgeType.IN:
                            interval = IntervalType.OUT_IN;
                            break;
                        case EdgeType.OUT:
                            interval = IntervalType.OUT_OUT;
                            break;
                    }
                    break;
            }

            this.Init(edgeBegin.Value, edgeFinish.Value, interval, comparer);
        }

        public Range(T begin, T finish, IntervalType interval, Func<T, T, int> comparer)
        {
            this.Init(begin,finish,interval,comparer);
        }

        public Boolean Match(T value)
        {
            Boolean ReturnValue = false;

            switch (this._Interval)
            {
                case IntervalType.IN_IN:
                    ReturnValue = (this._Comparer(this._Begin, value) <= 0) && (this._Comparer(value, this._Finish) <= 0);
                    break;

                case IntervalType.IN_OUT:
                    ReturnValue = (this._Comparer(this._Begin, value) <= 0) && (this._Comparer(value, this._Finish) < 0);
                    break;

                case IntervalType.OUT_IN:
                    ReturnValue = (this._Comparer(this._Begin, value) < 0) && (this._Comparer(value, this._Finish) <= 0);
                    break;

                case IntervalType.OUT_OUT:
                    ReturnValue = (this._Comparer(this._Begin, value) < 0) && (this._Comparer(value, this._Finish) < 0);
                    break;
            }

            return ReturnValue;
        }
    
    
        public Order CompareToBegin(T value)
        {
            return Comparers.Comparer.GetOrder(Comparer(Begin, value));
        }

        public Order CompareToFinish(T value)
        {
            return Comparers.Comparer.GetOrder(Comparer(Finish, value));
        }
    }

    public class RangeSystem<T>
    {
        private List<Range<T>> _Ranges = new List<Range<T>>();
        public List<Boolean> _TruthList = new List<Boolean>();



    }

    public class RangeEngine<T>
    {
        private List<Range<T>> _Ranges = new List<Range<T>>();

        public RangeEngine(List<Range<T>> ranges)
        {
            this._Ranges = ranges;
        }

        public void Add(Range<T> value)
        {
            this._Ranges.Add(value);
        }

        public List<Boolean> Matches(T value)
        {
            return Matches(this._Ranges, value);
        }

        public static List<Boolean> Matches(List<Range<T>> ranges,T value)
        {
            List<Boolean> ReturnValue = new List<Boolean>();

            for (int iElement = 0; iElement < ranges.Count; iElement++)
            {
                Range<T> range = ranges[iElement];
                Boolean IsMatched = range.Match(value);
                ReturnValue.Add(IsMatched);
            }

            return ReturnValue;
        }

        public Boolean Match(T value, Boolean expected = true)
        {
            return Match(this._Ranges,value,expected);
        }

        public static Boolean Match(List<Range<T>> ranges,T value, Boolean expected = true)
        {
            return Matches(ranges, value).Any(x => x == expected);
        }

        public static Range<T> GetMinimumEdge(List<Range<T>> ranges)
        {
            Range<T> ReturnValue = null;

            //
            T MinValue = default(T);
            EdgeType MinEdge = EdgeType.IN;
            //
            T CurrentValue = default(T);
            EdgeType CurrentEdge = EdgeType.IN;
            //

            if (ranges != null)
            {
                if (ranges.Count > 0)
                {
                    CurrentValue = ranges[0].Begin;
                    CurrentEdge = RangeHelpers.GetBeginEdge(ranges[0].Interval);
                    MinValue = CurrentValue;
                    MinEdge = CurrentEdge;
                     

                    for (int iRange = 1; iRange < ranges.Count; iRange++)
                    {
                        Range<T> currentRange = ranges[iRange];
                        CurrentValue = currentRange.Begin;
                        CurrentEdge = RangeHelpers.GetBeginEdge(currentRange.Interval);
                        int iOrder = currentRange.Comparer(MinValue, CurrentValue);

                        Order order = Comparers.Comparer.GetOrder(iOrder);

                        switch (order)
                        {
                            case Order.ERROR:
                                //      Problema
                                break;

                            case Order.LESS:
                                //   Ok
                                break;

                            case Order.EQUAL:
                                // Gestire EdgeType
                                break;

                            case Order.GREATER:
                                // Aggiornare MinValue
                                break;

                            default:
                                //      Problema
                                break;
                        }



                    }
                }
            }



            return ReturnValue;
        }


        public static List<Range<T>> GetOrSystemRages(Range<T> firstRange, Range<T> secondRange)
        {
            List<Range<T>> ReturnValue = new List<Range<T>>();




            return ReturnValue;
        }

        public static Edge<T> GetOrEdge(T value , EdgeType first,EdgeType second)
        {
            Edge<T> ReturnValue = null;

            if ((first == EdgeType.IN) || (second == EdgeType.IN))
            {               
                // include
                ReturnValue = new Edge<T>(value, EdgeType.IN);
            }
            else
            {
                // exclude
                ReturnValue = new Edge<T>(value, EdgeType.OUT);
            }

            return ReturnValue;
        }

        public static List<Range<T>> GetOrRanges(Range<T> firstRange,Range<T> secondRange)
        {
            List<Range<T>> ReturnValue = new List<Range<T>>();

            if( (firstRange == null) || (secondRange == null))
            {
                return ReturnValue;
            }

            if ( (firstRange.IsValid == false) || (secondRange.IsValid == false))
            {
                return ReturnValue;
            }

            Order orderChange = firstRange.CompareToBegin(secondRange.Begin);
            if (orderChange == Order.GREATER)
            {
                // Reverse order

                ReturnValue = GetOrRanges(secondRange, firstRange);
                return ReturnValue;
            }
          
            Order orderBegin = firstRange.CompareToBegin(secondRange.Begin);
            Order orderFinish = firstRange.CompareToFinish(secondRange.Finish);

            if ((orderBegin == Order.EQUAL) && (orderFinish == Order.EQUAL))
            {
               
                // Perfect Overlap

                //    o-------o
                //    o-------o

                ReturnValue = new List<Range<T>>();

                Edge<T> beginEdge = GetOrEdge(firstRange.Begin, firstRange.BeginEdge, secondRange.BeginEdge);

                Edge<T> finishEdge = GetOrEdge(firstRange.Finish, firstRange.FinishEdge, secondRange.FinishEdge);

                Range<T>  range = new Range<T>(beginEdge, finishEdge, firstRange.Comparer);

                ReturnValue.Add(range); 

                return ReturnValue;
            }

            if ((orderBegin == Order.LESS) && (orderFinish == Order.EQUAL))
            {
                // Partial overlap  1/4

                //    o-------o
                //       o----o

                ReturnValue = new List<Range<T>>();

                Edge<T> beginEdge = new Edge<T>(firstRange.Begin, firstRange.BeginEdge);
                
                Edge<T> finishEdge = GetOrEdge(firstRange.Finish, firstRange.FinishEdge, secondRange.FinishEdge);
                
                Range<T> range = new Range<T>(beginEdge, finishEdge, firstRange.Comparer);

                ReturnValue.Add(range);

                return ReturnValue;
            }

            if ((orderBegin == Order.EQUAL) && (orderFinish == Order.LESS))
            {
                // Partial overlap  2/4

                //    o-------o
                //    o----o

                ReturnValue = new List<Range<T>>();

                Edge<T> beginEdge = GetOrEdge(firstRange.Begin, firstRange.BeginEdge, secondRange.BeginEdge);

                Edge<T> finishEdge = new Edge<T>(secondRange.Finish, secondRange.FinishEdge);

                Range<T> range = new Range<T>(beginEdge, finishEdge, firstRange.Comparer);

                ReturnValue.Add(range);

                return ReturnValue;

            }

            if ((orderBegin == Order.LESS) && (orderFinish == Order.LESS))
            {
                // Partial overlap  3/4

                //    o-------o
                //       o-------o

                ReturnValue = new List<Range<T>>();

                Edge<T> beginEdge = new Edge<T>(firstRange.Begin, firstRange.BeginEdge);

                Edge<T> finishEdge = new Edge<T>(secondRange.Finish, secondRange.FinishEdge);

                Range<T> range = new Range<T>(beginEdge, finishEdge, firstRange.Comparer);

                ReturnValue.Add(range);

                return ReturnValue;
            }

            if ((orderBegin == Order.LESS) && (orderFinish == Order.GREATER))
            {
                // Partial overlap  4/4  (firstRange overlap secondRange on begin and finish)

                //    o-------o
                //       o--o

                ReturnValue = new List<Range<T>>();

                Edge<T> beginEdge = new Edge<T>(firstRange.Begin, firstRange.BeginEdge);

                Edge<T> finishEdge = new Edge<T>(firstRange.Finish, firstRange.FinishEdge);

                Range<T> range = new Range<T>(beginEdge, finishEdge, firstRange.Comparer);

                ReturnValue.Add(range);

                return ReturnValue;
            }

            Order orderMixed = firstRange.CompareToFinish(secondRange.Begin);
            if (orderMixed == Order.LESS)
            {
                // Disjoint


                //    o-------o
                //                o-------o

                ReturnValue = new List<Range<T>>();

                ReturnValue.Add(firstRange);
                ReturnValue.Add(secondRange);

                return ReturnValue;
            }

            if (orderMixed == Order.EQUAL)
            {
                //

                //    o-------o
                //            o-------o

                Edge<T> middleEdge = GetOrEdge(firstRange.Finish, firstRange.FinishEdge, secondRange.BeginEdge);

                if(middleEdge.Type == EdgeType.IN)
                {
                    ReturnValue = new List<Range<T>>();

                    Edge<T> beginEdge = new Edge<T>(firstRange.Begin, firstRange.BeginEdge);

                    Edge<T> finishEdge = new Edge<T>(secondRange.Finish, secondRange.FinishEdge);

                    Range<T> range = new Range<T>(beginEdge, finishEdge, firstRange.Comparer);

                    ReturnValue.Add(range);

                    return ReturnValue;
                }
                else
                {

                    Range<T> beginRange = new Range<T>(new Edge<T>(firstRange.Begin, firstRange.BeginEdge),
                                                       new Edge<T>(firstRange.Finish, EdgeType.OUT), 
                                                       firstRange.Comparer);

                    Range<T> finishRange = new Range<T>(new Edge<T>(secondRange.Begin, EdgeType.OUT),
                                                        new Edge<T>(secondRange.Finish, secondRange.FinishEdge),
                                                        firstRange.Comparer);

                    ReturnValue.Add(beginRange);

                    ReturnValue.Add(finishRange);

                    return ReturnValue;

                }
            }

            return ReturnValue;
        }

        public static Range<T> GetRange(List<Range<T>> ranges)
        {
            Range<T> ReturnValue = null;

            List<Edge<T>> unorderedList = new List<Edge<T>>();

            Range<T> currentRange = ranges.First();

            for (int iRange = 0; iRange < ranges.Count; iRange++)
            {
                unorderedList.Add(new Edge<T>(ranges[iRange].Begin,  ranges[iRange].BeginEdge , iRange));
                unorderedList.Add(new Edge<T>(ranges[iRange].Finish, ranges[iRange].FinishEdge, iRange));
            }

            List<Edge<T>> orderedList = unorderedList.OrderBy(x => x.Value).ToList();

            T PreviousValue = orderedList[0].Value;
            EdgeType PreviousEdgeType = orderedList[0].Type;
            for (int iRange = 1; iRange < orderedList.Count; iRange++)
            {
                T CurrentValue = orderedList[iRange].Value;
                EdgeType CurrentEdgeType = orderedList[iRange].Type;

                int iOrder = currentRange.Comparer(PreviousValue, CurrentValue);

                Order order = Comparers.Comparer.GetOrder(iOrder);

                switch (order)
                {
                    case Order.ERROR:
                        //      Problema
                        break;

                    case Order.LESS:
                        //   Ok
                        break;

                    case Order.EQUAL:
                        //      Gestire EdgeType
                        break;

                    case Order.GREATER:
                        //      Problema
                        break;

                    default:
                        //      Problema
                        break;
                }
  
            }

            return ReturnValue;
        }

        public static List<Range<T>> GetRanges(List<Range<T>> ranges)
        {
            List<Range<T>> ReturnValue = null;
           
            return ReturnValue;
        }
    }
}