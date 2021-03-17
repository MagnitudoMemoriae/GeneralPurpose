using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeneralPurposeLibrary.Algorithms.Sorting
{
    public class FlipSortAlgorithm
    {
        public readonly List<int> Inputs;

        public readonly List<int> Outputs;


        private int _BigO = 0;

        public int BigO
        {
            get
            {
                return _BigO;
            }
        }

        private class SplitStucture
        {
            public readonly List<int> Lows;

            public readonly List<int> Highs;
        }

        public FlipSortAlgorithm(List<int> elements, int algo)
        {
            Inputs = elements;

            switch (algo)
            {
                case 0:
                    Outputs = Run0(Inputs);
                    break;

                case 1:
                    //Outputs = Run1(Inputs);
                    break;

                case 2:
                    Outputs = Run2(Inputs);
                    break;

                case 3:
                    Outputs = Run3(Inputs);
                    break;
            }
        }

        private List<int> Run0(List<int> inputs)
        {
            List<int> ReturnValue = new List<int>();

            if (inputs.Count == 0) return ReturnValue;
            if (inputs.Count == 1) return inputs;

            List<int> Locals = new List<int>(inputs);
            List<int> Lows = new List<int>();
            List<int> Highs = new List<int>();
            int Diagnostic1 = 0;

            do
            {
                Lows = new List<int>();
                Highs = new List<int>();

                for (int i = 0; i < Locals.Count - 1; i++)
                {
                    if (Locals[i] <= Locals[i + 1])
                    {
                        Highs.Add(Locals[i + 1]);
                    }
                    else
                    {
                        Lows.Add(Locals[i + 1]);
                    }
                    Diagnostic1++;
                }

                if (Locals[Locals.Count - 1] <= Locals[0])
                {
                    Highs.Add(Locals[0]);
                }
                else
                {
                    Lows.Add(Locals[0]);
                }

                Diagnostic1++;

                Locals = new List<int>();
                Locals.AddRange(Lows);
                Locals.AddRange(Highs);
            }
            while (!(Lows.Count <= 1));

            this._BigO = Diagnostic1;

            ReturnValue = new List<int>(Locals);
            return ReturnValue;
        }

        private List<int> Run1(List<int> inputs)
        {
            List<int> ReturnValue = new List<int>();

            if (inputs.Count == 0) return ReturnValue;
            if (inputs.Count == 1) return inputs;

            List<int> Locals = new List<int>(inputs);
            List<int> Lows = new List<int>();
            List<int> Highs = new List<int>();
            int Diagnostic1 = 0;

            do
            {
                Lows = new List<int>();
                Highs = new List<int>();


                if (Locals[Locals.Count - 1] <= Locals[0])
                {
                    Highs.Add(Locals[0]);
                }
                else
                {
                    Lows.Add(Locals[0]);
                }

                for (int i = 0; i < Locals.Count - 1; i++)
                {
                    if (Locals[i] <= Locals[i + 1])
                    {
                        Highs.Add(Locals[i + 1]);
                    }
                    else
                    {
                        Lows.Add(Locals[i + 1]);
                    }
                    Diagnostic1++;
                }

                Diagnostic1++;

                Locals = new List<int>();
                Locals.AddRange(Lows);
                Locals.AddRange(Highs);
            }
            while (!(Lows.Count <= 1));

            ReturnValue = new List<int>(Locals);
            return ReturnValue;
        }

        private List<int> Run2(List<int> inputs)
        {
            List<int> ReturnValue = new List<int>();

            if (inputs.Count == 0) return ReturnValue;
            if (inputs.Count == 1) return inputs;

            Queue<int> Locals = new Queue<int>(inputs);
            Queue<int> Lows = new Queue<int>();
            Queue<int> Highs = new Queue<int>();
            int MaxValue = int.MinValue;
            int MinPosition = -1;
            int Diagnostic1 = 0;
            Queue<int> coda = new Queue<int>();
            //coda.
            Print(inputs);

            do
            {
                Lows = new Queue<int>();
                Highs = new Queue<int>();

                int First = Locals.Peek();
                int CurrentValue = 0;
                int NextValue = 0;
                while (Locals.Count > 1)
                {
                    CurrentValue = Locals.Dequeue();
                    NextValue = Locals.Peek();
                    if ( CurrentValue <= NextValue )
                    {
                        Lows.Enqueue(CurrentValue);                       
                    }
                    else
                    {
                        Highs.Enqueue(CurrentValue);
                    }
                    Diagnostic1++;
                }

                int LastValue = Locals.Dequeue();

                

                if (LastValue >= CurrentValue)
                {
                    Highs.Enqueue(LastValue);
                }
                else
                {
                    Lows.Enqueue(LastValue);
                }

                Diagnostic1++;

                foreach (int item in Lows)
                {
                    Locals.Enqueue(item);
                }
                foreach (int item in Highs)
                {
                    Locals.Enqueue(item);
                }

                Print(Lows, Highs);

            }
            while (!(Highs.Count <= 1));

            this._BigO = Diagnostic1;

            ReturnValue = new List<int>(Locals);
            return ReturnValue;
        }

        private List<int> Run3(List<int> inputs)
        {
            List<int> ReturnValue = new List<int>();

            if (inputs.Count == 0) return ReturnValue;
            if (inputs.Count == 1) return inputs;

            Queue<int> Locals = new Queue<int>(inputs);
            Queue<int> Lows = new Queue<int>();
            Queue<int> Highs = new Queue<int>();
            int MaxValue = int.MinValue;
            int MinPosition = -1;
            int Diagnostic1 = 0;
            Queue<int> coda = new Queue<int>();
            Boolean ContinueLoop = true;
            //coda.
            Print(inputs);

            do
            {
                Lows = new Queue<int>();
                Highs = new Queue<int>();

                //int First = Locals.Peek();
                int CurrentValue = 0;
                int NextValue = 0;
                while (Locals.Count > 1)
                {
                    CurrentValue = Locals.Dequeue();
                    NextValue = Locals.Peek();
                    if (CurrentValue <= NextValue)
                    {
                        Lows.Enqueue(CurrentValue);
                    }
                    else
                    {
                        Highs.Enqueue(CurrentValue);
                    }
                    Diagnostic1++;
                }

                int LastValue = Locals.Dequeue();

                if ( CurrentValue <= LastValue)
                {
                    Highs.Enqueue(LastValue);
                }
                else
                {
                    Lows.Enqueue(LastValue);
                }

                

                Print(Lows, Highs);

                Diagnostic1++;

                ContinueLoop = false;

                while (Lows.Count > 0)
                {
                    int low = Lows.Peek();
                    if (Highs.Count > 0)
                    {
                        Diagnostic1++;
                        if (low <= Highs.Peek())
                        {
                            Locals.Enqueue(Lows.Dequeue());
                            Diagnostic1++;
                        }
                        else
                        {
                            Locals.Enqueue(Highs.Dequeue());
                            ContinueLoop = true;
                            Diagnostic1++;
                        }
                    }
                    else
                    {
                        Locals.Enqueue(Lows.Dequeue());
                        Diagnostic1++;
                    }
                }

                if(Highs.Count > 1)
                {
                    ContinueLoop = true;
                }

                while (Highs.Count > 0)
                {
                    Locals.Enqueue(Highs.Dequeue());
                }                            

            }
            while (ContinueLoop);

            this._BigO = Diagnostic1;

            ReturnValue = new List<int>(Locals);
            return ReturnValue;
        }

        private void Print(List<int> elements)
        {
            Debug.WriteLine("");
            for (int i = 0; i < elements.Count; i++)
            {
                Debug.Write(elements[i] + ",");
            }
        }

        private void Print(List<int> lows, List<int> highs)
        {
            Debug.WriteLine("");
            for (int i = 0; i < lows.Count; i++)
            {
                Debug.Write(lows[i] + ",");
            }
            Debug.Write(" - ");
            for (int i = 0; i < highs.Count; i++)
            {
                Debug.Write(highs[i] + ",");
            }
        }

        private void Print(Queue<int> lows, Queue<int> highs)
        {
            Print(new List<int>(lows) , new List<int>(highs));
        }
    }
}