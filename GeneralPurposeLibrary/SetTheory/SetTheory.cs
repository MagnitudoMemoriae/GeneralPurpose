using GeneralPurposeLibrary.Comparers;
using System;
using System.Collections.Generic;

namespace GeneralPurposeLibrary.SetTheory
{
    public class SetTheory
    {
    }

    public enum RelationSearchResponse
    {
        FIRST,
        SECOND,
        BOTH,
        NOTEXIST,
        ERROR
    }

    public class Relation<T>
    {
        public readonly T First;
        public readonly T Second;
        public readonly RelationSearchResponse Response;

        public Relation(T first, T second)
        {
            First = first;
            Second = second;

            int Counter = 0;

            if (First != null)
            {
                Counter += 1;
            }

            if (Second != null)
            {
                Counter += 2;
            }

            switch (Counter)
            {
                case 0:
                    Response = RelationSearchResponse.NOTEXIST;
                    break;

                case 1:
                    Response = RelationSearchResponse.FIRST;
                    break;

                case 2:
                    Response = RelationSearchResponse.SECOND;
                    break;

                case 3:
                    Response = RelationSearchResponse.BOTH;
                    break;
            }
        }
    }

    public class SetRelation<T>
    {
        private SortedDictionary<T, List<int>> _First = new SortedDictionary<T, List<int>>();
        private SortedDictionary<T, List<int>> _Second = new SortedDictionary<T, List<int>>();
        private SortedDictionary<int, Relation<T>> _Relations = new SortedDictionary<int, Relation<T>>();

        private int _Counter = 0;
        private IComparer<T> _Comparer = null;

        public SetRelation(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new NullReferenceException();
            }

            this.Init(comparer);
        }

        public SetRelation() : this(ComparerFactory.Create<T>())
        {
        }

        private void Init(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new NullReferenceException();
            }

            this._Comparer = comparer;
            this._First = new SortedDictionary<T, List<int>>(_Comparer);
            this._Second = new SortedDictionary<T, List<int>>(_Comparer);
        }

        public void Add(Relation<T> relation)
        {
            if (relation != null)
            {
                this.Add(relation.First, relation.Second);
            }
            else
            {
                throw new NullReferenceException();
            }
            return;
        }

        public void Add(T first, T second)
        {
            if ((first == null) && (second == null))
            {
                throw new NullReferenceException();
            }
            else
            {
                int CurrentCounter = _Counter++;
                this._Relations.Add(CurrentCounter, new Relation<T>(first, second));

                if (first != null)
                {
                    if (this._First.TryGetValue(first, out List<int> edges))
                    {
                        edges.Add(CurrentCounter);
                    }
                    else
                    {
                        this._First.Add(first, new List<int>() { CurrentCounter });
                    }
                }

                if (second != null)
                {
                    if (this._Second.TryGetValue(second, out List<int> edges))
                    {
                        edges.Add(CurrentCounter);
                    }
                    else
                    {
                        this._Second.Add(second, new List<int>() { CurrentCounter });
                    }
                }
            }
        }

        public List<T> GetSeconds(T first)
        {
            List<T> ReturnValue = new List<T>();

            if (first != null)
            {
                if (this._First.TryGetValue(first, out List<int> edges) == true)
                {
                    for (int iEdge = 0; iEdge < edges.Count; iEdge++)
                    {
                        int iRelation = edges[iEdge];
                        ReturnValue.Add(this._Relations[iRelation].Second);
                    }
                }
            }
            else
            {
                throw new NullReferenceException();
            }

            return ReturnValue;
        }

        public List<T> GetFirsts(T second)
        {
            List<T> ReturnValue = new List<T>();

            if (second != null)
            {
                if (this._Second.TryGetValue(second, out List<int> edges) == true)
                {
                    for (int iEdge = 0; iEdge < edges.Count; iEdge++)
                    {
                        int iRelation = edges[iEdge];
                        ReturnValue.Add(this._Relations[iRelation].First);
                    }
                }
            }
            else
            {
                throw new NullReferenceException();
            }

            return ReturnValue;
        }

        public List<Relation<T>> GetRelationFirsts(T first)
        {
            List<Relation<T>> ReturnValue = new List<Relation<T>>();

            if (first == null)
            {
                throw new NullReferenceException();
            }

            List<int> relations = this._First[first];

            ReturnValue = GetRelations(relations);

            return ReturnValue;
        }

        public IEnumerable<Relation<T>> GetRelationFirsts(IEnumerable<T> firsts)
        {
            List<Relation<T>> ReturnValue = new List<Relation<T>>();

            if (firsts == null)
            {
                throw new NullReferenceException();
            }

            foreach (T node in firsts)
            {
                List<int> relations = this._First[node];

                ReturnValue.AddRange(GetRelations(relations));
            }

            return ReturnValue;
        }

        public List<Relation<T>> GetRelationSeconds(T second)
        {
            List<Relation<T>> ReturnValue = new List<Relation<T>>();

            if (second == null)
            {
                throw new NullReferenceException();
            }

            List<int> relations = this._Second[second];

            ReturnValue = GetRelations(relations);

            return ReturnValue;
        }

        public IEnumerable<Relation<T>> GetRelationSeconds(IEnumerable<T> seconds)
        {
            List<Relation<T>> ReturnValue = new List<Relation<T>>();

            if (seconds == null)
            {
                throw new NullReferenceException();
            }

            foreach (T node in seconds)
            {
                List<int> relations = this._Second[node];

                ReturnValue.AddRange(GetRelations(relations));
            }

            return ReturnValue;
        }

        private List<Relation<T>> GetRelations(List<int> relations)
        {
            List<Relation<T>> ReturnValue = new List<Relation<T>>();

            if (relations == null)
            {
                throw new NullReferenceException();
            }

            for (int iRelation = 0; iRelation < relations.Count; iRelation++)
            {
                int iRelationIndex = relations[iRelation];
                Relation<T> relation = _Relations[iRelationIndex];
                ReturnValue.Add(relation);
            }

            return ReturnValue;
        }

        public Boolean IsFirstTwoWay(T first)
        {
            Boolean ReturnValue = false;

            if (first == null)
            {
                throw new NullReferenceException();
            }

            if (this.IsFirstUnique(first) == true)
            {
                int iRelation = this._First[first][0];
                T second = this._Relations[iRelation].Second;

                if (this.IsSecondUnique(second) == true)
                {
                    ReturnValue = true;
                }
            }

            return ReturnValue;
        }

        public Boolean IsSecondTwoWay(T second)
        {
            Boolean ReturnValue = false;

            if (second != null)
            {
                if (this._Second.ContainsKey(second) == true)
                {
                    if (this._Second[second].Count == 1)
                    {
                        int iRelation = this._Second[second][0];
                        T first = this._Relations[iRelation].First;

                        if (this._First[first].Count == 1)
                        {
                            ReturnValue = true;
                        }
                    }
                }
            }
            else
            {
                throw new NullReferenceException();
            }

            return ReturnValue;
        }

        public Boolean IsFirstUnique(T first)
        {
            Boolean ReturnValue = false;

            if (first != null)
            {
                if (this._First.ContainsKey(first) == true)
                {
                    if (this._First[first].Count == 1)
                    {
                        ReturnValue = true;
                    }
                }
            }
            else
            {
                throw new NullReferenceException();
            }

            return ReturnValue;
        }

        public Boolean IsSecondUnique(T second)
        {
            Boolean ReturnValue = false;

            if (second != null)
            {
                if (this._Second.ContainsKey(second) == true)
                {
                    if (this._Second[second].Count == 1)
                    {
                        ReturnValue = true;
                    }
                }
            }
            else
            {
                throw new NullReferenceException();
            }

            return ReturnValue;
        }

        public RelationSearchResponse Search(T element)
        {
            RelationSearchResponse ReturnValue = RelationSearchResponse.ERROR;
            int Counter = 0;

            if (element == null)
            {
                return RelationSearchResponse.ERROR;
            }

            if (this._First.ContainsKey(element) == true)
            {
                Counter += 1;
            }

            if (this._Second.ContainsKey(element) == true)
            {
                Counter += 2;
            }

            switch (Counter)
            {
                case 0:
                    ReturnValue = RelationSearchResponse.NOTEXIST;
                    break;

                case 1:
                    ReturnValue = RelationSearchResponse.FIRST;
                    break;

                case 2:
                    ReturnValue = RelationSearchResponse.SECOND;
                    break;

                case 3:
                    ReturnValue = RelationSearchResponse.BOTH;
                    break;
            }

            return ReturnValue;
        }

        public Dictionary<int, List<Relation<T>>> GetConnected()
        {
            Dictionary<int, List<Relation<T>>> ReturnValue = new Dictionary<int, List<Relation<T>>>();

            int Counter = 0;

            HashSet<Relation<T>> alreadyProcessed = new HashSet<Relation<T>>();
            foreach (KeyValuePair<int, Relation<T>> item in this._Relations)
            {
                if (alreadyProcessed.Contains(item.Value) == true)
                {
                    continue;
                }

                switch (item.Value.Response)
                {
                    case RelationSearchResponse.NOTEXIST:
                        // Non deve esistere
                        break;

                    case RelationSearchResponse.FIRST:
                        ReturnValue.Add(Counter++, new List<Relation<T>>() { item.Value });
                        break;

                    case RelationSearchResponse.SECOND:
                        ReturnValue.Add(Counter++, new List<Relation<T>>() { item.Value });
                        break;

                    case RelationSearchResponse.BOTH:
                        if (this.IsFirstTwoWay(item.Value.First) == true)
                        {
                            ReturnValue.Add(Counter++, new List<Relation<T>>() { item.Value });
                        }
                        else
                        {
                            Boolean ContinueLoop = true;
                            HashSet<T> f = new HashSet<T>();
                            HashSet<T> s = new HashSet<T>();
                            Queue<T> tuboF = new Queue<T>();
                            Queue<T> tuboS = new Queue<T>();
                            tuboF.Enqueue(item.Value.First);
                            tuboS.Enqueue(item.Value.Second);
                            do
                            {
                                HashSet<T> newSeconds = new HashSet<T>();
                                HashSet<T> newFirsts = new HashSet<T>();

                                if (tuboF.Count > 0)
                                {
                                    newSeconds = new HashSet<T>(GetSeconds(tuboF.Dequeue()));
                                }

                                if (tuboS.Count > 0)
                                {
                                    newFirsts = new HashSet<T>(GetFirsts(tuboS.Dequeue()));
                                }

                                if (newFirsts.SetEquals(f) == false)
                                {
                                    HashSet<T> local = new HashSet<T>(f);
                                    newFirsts.ExceptWith(local);
                                    foreach (T itemT in newFirsts)
                                    {
                                        tuboF.Enqueue(itemT);
                                    }
                                    f.UnionWith(newFirsts);
                                }

                                if (newSeconds.SetEquals(s) == false)
                                {
                                    HashSet<T> local = new HashSet<T>(s);
                                    newSeconds.ExceptWith(local);
                                    foreach (T itemT in newSeconds)
                                    {
                                        tuboS.Enqueue(itemT);
                                    }
                                    s.UnionWith(newSeconds);
                                }

                                if ((tuboF.Count == 0) && (tuboS.Count == 0))
                                {
                                    ContinueLoop = false;
                                }
                            }
                            while (ContinueLoop == true);
                            Counter++;
                            List<Relation<T>> relations = new List<Relation<T>>();

                            HashSet<Relation<T>> hsF = new HashSet<Relation<T>>(GetRelationFirsts(f));
                            HashSet<Relation<T>> hsS = new HashSet<Relation<T>>(GetRelationSeconds(s));

                            hsF.UnionWith(hsS);
                            alreadyProcessed.UnionWith(hsF);

                            ReturnValue.Add(Counter++, new List<Relation<T>>(hsF));
                        }

                        break;
                }
            }

            return ReturnValue;
        }
    }

    public enum SetOperationResponse
    {
        DISJOINT,
        EQUAL,
        FIRSTISSUPERSET,
        SECONDISSUPERSET,
        INTERSECTION,
        ERROR
    }

    public class SetOperation<T>
    {
        private HashSet<T> _FirstSet = new HashSet<T>();
        private HashSet<T> _SecondSet = new HashSet<T>();
        private SetOperationResponse _Classification = SetOperationResponse.ERROR;

        public SetOperation(HashSet<T> firstSet, HashSet<T> secondSet)
        {
            if ((firstSet != null) && (secondSet != null))
            {
                if ((firstSet.Count > 0) && (secondSet.Count > 0))
                {
                    this._FirstSet = firstSet;
                    this._SecondSet = secondSet;
                    this._Process();
                }
            }
        }

        private void _Process()
        {
            if (_FirstSet.SetEquals(_SecondSet) == true)
            {
                this._Classification = SetOperationResponse.EQUAL;
            }
            else if (_FirstSet.Overlaps(_SecondSet) == true)
            {
                if (_FirstSet.IsProperSupersetOf(_SecondSet) == true)
                {
                    this._Classification = SetOperationResponse.FIRSTISSUPERSET;
                }
                else if (_SecondSet.IsProperSupersetOf(_FirstSet) == true)
                {
                    this._Classification = SetOperationResponse.SECONDISSUPERSET;
                }
                else
                {
                    this._Classification = SetOperationResponse.INTERSECTION;
                }
            }
            else
            {
                this._Classification = SetOperationResponse.DISJOINT;
            }
        }

        public SetOperationResponse Classification
        {
            get
            {
                return this._Classification;
            }
        }

        public HashSet<T> Union()
        {
            HashSet<T> ReturnValue = new HashSet<T>();

            switch (this.Classification)
            {
                case SetOperationResponse.DISJOINT:
                    ReturnValue.UnionWith(this._FirstSet);
                    ReturnValue.UnionWith(this._SecondSet);
                    break;

                case SetOperationResponse.EQUAL:
                    ReturnValue.UnionWith(this._FirstSet);
                    break;

                case SetOperationResponse.FIRSTISSUPERSET:
                    ReturnValue.UnionWith(this._FirstSet);
                    break;

                case SetOperationResponse.SECONDISSUPERSET:
                    ReturnValue.UnionWith(this._SecondSet);
                    break;

                case SetOperationResponse.INTERSECTION:
                    ReturnValue.UnionWith(this._FirstSet);
                    ReturnValue.UnionWith(this._SecondSet);
                    break;
            }

            return ReturnValue;
        }

        public HashSet<T> Intersection()
        {
            HashSet<T> ReturnValue = new HashSet<T>();

            switch (this.Classification)
            {
                case SetOperationResponse.DISJOINT:
                    break;

                case SetOperationResponse.EQUAL:
                    ReturnValue.UnionWith(this._FirstSet);
                    break;

                case SetOperationResponse.FIRSTISSUPERSET:
                    ReturnValue.UnionWith(this._SecondSet);
                    break;

                case SetOperationResponse.SECONDISSUPERSET:
                    ReturnValue.UnionWith(this._FirstSet);
                    break;

                case SetOperationResponse.INTERSECTION:
                    ReturnValue.UnionWith(this._FirstSet);
                    ReturnValue.IntersectWith(this._SecondSet);
                    break;
            }

            return ReturnValue;
        }

        public HashSet<T> Complement()
        {
            HashSet<T> ReturnValue = new HashSet<T>();

            switch (this.Classification)
            {
                case SetOperationResponse.DISJOINT:
                    ReturnValue.UnionWith(this._FirstSet);
                    break;

                case SetOperationResponse.EQUAL:
                    break;

                case SetOperationResponse.FIRSTISSUPERSET:
                    ReturnValue.UnionWith(this._FirstSet);
                    ReturnValue.ExceptWith(this._SecondSet);
                    break;

                case SetOperationResponse.SECONDISSUPERSET:

                    break;

                case SetOperationResponse.INTERSECTION:
                    ReturnValue.UnionWith(this._FirstSet);
                    ReturnValue.ExceptWith(this._SecondSet);
                    break;
            }

            return ReturnValue;
        }

        public HashSet<T> InverseComplement()
        {
            HashSet<T> ReturnValue = new HashSet<T>();

            switch (this.Classification)
            {
                case SetOperationResponse.DISJOINT:
                    ReturnValue.UnionWith(this._SecondSet);
                    break;

                case SetOperationResponse.EQUAL:
                    break;

                case SetOperationResponse.FIRSTISSUPERSET:
                    break;

                case SetOperationResponse.SECONDISSUPERSET:
                    ReturnValue.UnionWith(this._SecondSet);
                    ReturnValue.ExceptWith(this._FirstSet);
                    break;

                case SetOperationResponse.INTERSECTION:
                    ReturnValue.UnionWith(this._SecondSet);
                    ReturnValue.ExceptWith(this._FirstSet);
                    break;
            }

            return ReturnValue;
        }

        public HashSet<T> SymmetricDifference()
        {
            HashSet<T> ReturnValue = new HashSet<T>();

            switch (this.Classification)
            {
                case SetOperationResponse.DISJOINT:
                    ReturnValue.UnionWith(this._FirstSet);
                    ReturnValue.UnionWith(this._SecondSet);
                    break;

                case SetOperationResponse.EQUAL:
                    break;

                case SetOperationResponse.FIRSTISSUPERSET:
                    ReturnValue.UnionWith(this._FirstSet);
                    ReturnValue.ExceptWith(this._SecondSet);
                    break;

                case SetOperationResponse.SECONDISSUPERSET:
                    ReturnValue.UnionWith(this._SecondSet);
                    ReturnValue.ExceptWith(this._FirstSet);
                    break;

                case SetOperationResponse.INTERSECTION:
                    HashSet<T> union = this.Union();
                    union.ExceptWith(this.Intersection());
                    ReturnValue.UnionWith(union);
                    break;
            }

            return ReturnValue;
        }

        public IEnumerable<Tuple<T, T>> GetCartesian()
        {
            foreach (T first in this._FirstSet)
            {
                foreach (T second in this._SecondSet)
                {
                    yield return new Tuple<T, T>(first, second);
                }
            }
        }
    }
}