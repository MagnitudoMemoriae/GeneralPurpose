using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralPurposeLibrary.Sat.TwoSat
{
    public class Variable<T> 
    {
        public readonly T Name =default(T);
        public readonly Boolean Value = false;
        public readonly Boolean Sign = false;
        public readonly Boolean Outcome = false;

        public Variable(T name, Boolean value, Boolean sign) 
        {
            Name = name;
            Value = value;  
            Sign = sign;
            Outcome = value & sign;
        }

    }
    public class Clause<T>
    {
        public readonly Variable<T> V1 ;
        public readonly Variable<T> V2 ;
        public Clause(Variable<T> v1 , Variable<T> v2) 
        {
            V1 = v1;
            V2 = v2;
        }
    }

    public class DisjunctionClause<T> : Clause<T>
    {
        public Boolean Outcome = false ;
        public DisjunctionClause(Variable<T> v1, Variable<T> v2) : base(v1, v2)
        {
            Outcome = this.V1.Outcome || this.V2.Outcome;
        }
    }

    public class Variable
    {
        public readonly int Name = -1;
        public readonly Boolean Value = false;
        public readonly Boolean Sign = false;
        public readonly Boolean Outcome = false;

        public Variable(int name, Boolean value, Boolean sign)
        {
            Name = name;
            Value = value;
            Sign = sign;
            Outcome = value & sign;
        }

    }
    public class Clause
    {
        public readonly Variable V1;
        public readonly Variable V2;
        public Clause(Variable v1, Variable v2)
        {
            V1 = v1;
            V2 = v2;
        }
    }

    public class DisjunctionClause : Clause
    {
        public Boolean Outcome = false;
        public DisjunctionClause(Variable v1, Variable v2) : base(v1, v2)
        {
            Outcome = this.V1.Outcome || this.V2.Outcome;
        }
    }


    public class ImplicationGrapthNode
    {
        public readonly Variable Name; 
        public Boolean IsActive = false;    
        //public List<>
    }

    public class ImplicationGraph
    {

    }

    internal class TwoSatCore
    {
    }
}
