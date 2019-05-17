using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneralPurposeLibrary.FiniteStateMachines
{
    public class FiniteStateMachine
    {
        public readonly HashSet<Transition> Table;
        private HashSet<Transition> _PossibleTransition = new HashSet<Transition>();
        private FSMState _CurrentState;

        public FiniteStateMachine(HashSet<Transition> table)
        {
            Table = table;
            this._CurrentState = Table.ElementAt(0).CurrentState;
            SetPossibleTransition();
        }

        private void SetPossibleTransition()
        {
            _PossibleTransition = new HashSet<Transition>();

            foreach (Transition transition in Table)
            {
                if (transition.CurrentState == this._CurrentState)
                {
                    this._PossibleTransition.Add(transition);
                }
            }
        }

        public FSMState Next(FSMEvent nextEvent)
        {
            foreach (Transition transition in this._PossibleTransition)
            {
                if (transition.CurrentState == this._CurrentState)
                {
                    this._CurrentState = transition.NextState;
                    break;
                }
            }
            SetPossibleTransition();

            return this._CurrentState;
        }

        public FSMState CurrentState
        {
            get
            {
                return this._CurrentState;
            }
        }
    }

    public abstract class FSMObject
    {
        private String _Name = String.Empty;
        public String Name { get { return this._Name; } }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }

    public class FSMState : FSMObject
    {
    }

    public class FSMEvent : FSMObject
    {
    }

    public class Transition : IComparable
    {
        public readonly FSMState CurrentState;

        public readonly FSMEvent Event;

        public readonly FSMState NextState;

        private readonly int _Hash;

        public Transition(FSMState currentState, FSMEvent currentEvent, FSMState nextState)
        {
            CurrentState = currentState;
            Event = currentEvent;
            NextState = nextState;

            String CompositeName = String.Format("{0}#{1}#{2}", CurrentState.Name, Event.Name, NextState.Name);
            this._Hash = CompositeName.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return -1;
            }

            if (obj is Transition)
            {
                Transition local = (Transition)obj;
                if (local._Hash == this._Hash)
                {
                    return 0;
                }
                else
                {
                    if (local._Hash < this._Hash)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            return (this.CompareTo(obj) == 0);
        }

        public override int GetHashCode()
        {
            return this._Hash;
        }
    }
}