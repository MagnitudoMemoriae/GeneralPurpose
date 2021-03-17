using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralPurposeLibrary.Algorithms.Sorting
{
    public class ClimbSortAlgorithm
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

        public ClimbSortAlgorithm(List<int> elements, int algo)
        {
            Inputs = elements;


            switch(algo)
            {
                case 0:
                    Outputs = RunOnlyMax();
                    break;
                case 1:
                    Outputs = RunBoth();
                    break;
                case 2:
                    //Outputs = RunBothRecursion(Inputs);
                    break;
            }

           
        }

        private List<int> RunOnlyMax()
        {
            List<int> ReturnValue = new List<int>();
            List<int> Locals = new List<int>(Inputs);

            int Diagnostic1 = 0;
            int Diagnostic2 = 0;

            int consecutiveRun = 0;
            for (int k = 0; k < Inputs.Count; k++)
            {
                List<int> chunkValues = new List<int>();
                List<int> chunkPositions = new List<int>();
                int consecutiveLoop = 0;
                int MinValue = int.MinValue;
                for (int i = 0; i < Locals.Count - k - consecutiveRun; i++)
                {
                    int CurrentValue = Locals[i];
                    if (CurrentValue >= MinValue)
                    {
                        chunkValues.Add(CurrentValue);
                        chunkPositions.Add(i);
                        MinValue = CurrentValue;
                        consecutiveLoop++;
                    }
                    else
                    {
                        consecutiveLoop = 0;
                    }

                    Diagnostic1++;
                }

                if(chunkValues.Count == Locals.Count) 
                {
                    ReturnValue = new List<int>(chunkValues);
                    break;
                }

                if (chunkValues.Count == 0)
                {
                    ReturnValue = new List<int>(Locals);
                    break;
                }

                for (int i = 0; i < chunkValues.Count; i++)
                {
                    int positionToReplace = Locals.Count - 1 - i - k - consecutiveRun;
                    int positionInChunkValues = chunkValues.Count - 1 - i;
                    int localValue = Locals[positionToReplace];
                    int chunkValue = chunkValues[positionInChunkValues];
                    int valueInChunkPositions = chunkPositions[positionInChunkValues];

                    if (Locals[positionToReplace] < chunkValue)
                    {
                        Locals[positionToReplace] = chunkValue;
                        Locals[valueInChunkPositions] = localValue;
                    }

                    Diagnostic2++;
                }

                consecutiveRun += consecutiveLoop;


            }

           
            return ReturnValue;
        }

        private List<int> RunBoth()
        {
            List<int> ReturnValue = new List<int>();
            List<int> Locals = new List<int>(Inputs);

            int Diagnostic1 = 0;
            int Diagnostic2 = 0;
            int Diagnostic3 = 0;
            int Diagnostic4 = 0;
            int Diagnostic5 = 0;
            int Diagnostic6 = 0;

            int consecutiveMaxRun = 0;
            int consecutiveMinRun = 0;
            for (int k = 0; k < Inputs.Count; k++)
            {
                List<int> chunkMaxValues = new List<int>();
                List<int> chunkMaxPositions = new List<int>();
                List<int> chunkMinValues = new List<int>();
                List<int> chunkMinPositions = new List<int>();
                int consecutiveMaxLoop = 0;
                int consecutiveMinLoop = 0;
                int MinValue = int.MinValue;
                int MaxValue = int.MaxValue;

                int forStart = k + consecutiveMinRun;
                int forStop = Locals.Count - k - consecutiveMaxRun;

                for (int i = forStart; i < forStop; i++)
                {
                    int CurrentValue = Locals[i];
                    if (CurrentValue >= MinValue)
                    {
                        chunkMaxValues.Add(CurrentValue);
                        chunkMaxPositions.Add(i);
                        MinValue = CurrentValue;
                        consecutiveMaxLoop++;
                        consecutiveMinLoop = 0;

                        Diagnostic1 += 1;
                    }
                    else if (CurrentValue <= MaxValue)
                    {
                        chunkMinValues.Add(CurrentValue);
                        chunkMinPositions.Add(i);
                        MaxValue = CurrentValue;
                        consecutiveMinLoop++;
                        consecutiveMaxLoop = 0;

                        Diagnostic2 += 1;
                    }
                    else
                    {
                        consecutiveMaxLoop = 0;
                        consecutiveMinLoop = 0;

                        Diagnostic3 += 1;
                    }

                   
                }

                if (chunkMaxValues.Count == Locals.Count)
                {
                    ReturnValue = new List<int>(chunkMaxValues);
                    break;
                }

                if (chunkMaxValues.Count == 0)
                {
                    ReturnValue = new List<int>(Locals);
                    break;
                }

                if (chunkMinValues.Count == Locals.Count)
                {
                    ReturnValue = new List<int>(chunkMinValues);
                    break;
                }

                if (chunkMinValues.Count == 0)
                {
                    ReturnValue = new List<int>(Locals);
                    break;
                }

                for (int i = 0; i < chunkMaxValues.Count; i++)
                {
                    int positionToReplace = Locals.Count - 1 - i - k - consecutiveMaxRun;
                    int positionInChunkValues = chunkMaxValues.Count - 1 - i;

                    int localValue = Locals[positionToReplace];
                    int chunkValue = chunkMaxValues[positionInChunkValues];
                    int valueInChunkPositions = chunkMaxPositions[positionInChunkValues];

                    if (Locals[positionToReplace] < chunkValue)
                    {
                        Locals[positionToReplace] = chunkValue;
                        Locals[valueInChunkPositions] = localValue;
                    }

                    Diagnostic4++;
                }

                for (int i = 0; i < chunkMinValues.Count; i++)
                {
                    int positionToReplace = i + k + consecutiveMinRun;
                    int positionInChunkValues = chunkMinValues.Count - 1 - i;

                    int localValue = Locals[positionToReplace];
                    int chunkValue = chunkMinValues[positionInChunkValues];
                    int valueInChunkPositions = chunkMinPositions[positionInChunkValues];

                    if (Locals[positionToReplace] > chunkValue)
                    {
                        Locals[positionToReplace] = chunkValue;
                        Locals[valueInChunkPositions] = localValue;
                    }

                    Diagnostic5++;
                }

                consecutiveMaxRun += consecutiveMaxLoop;
                consecutiveMinRun += consecutiveMinLoop;

            }


            this._BigO = Diagnostic1 + Diagnostic2 + Diagnostic3 + Diagnostic4 + Diagnostic5;


            return ReturnValue;
        }
#if false
        private List<int> RunBothRecursion(List<int> inputs)
        {
            List<int> ReturnValue = new List<int>();

            if(inputs.Count == 1)
            {
                ReturnValue = new List<int>(inputs);
                return ReturnValue;
            }

            if (inputs.Count == 2)
            {
                if(inputs[0] <= inputs[1])
                {
                    ReturnValue = new List<int>(inputs);
                    return ReturnValue;
                }
                else
                {
                    ReturnValue = new List<int>() {inputs[1], inputs[0] };
                    return ReturnValue;
                }

            }

            int StartNumber = inputs.Count;

            List<int> Locals = new List<int>(inputs);

            int Diagnostic1 = 0;
            int Diagnostic2 = 0;
            int Diagnostic3 = 0;
            int Diagnostic4 = 0;
            int Diagnostic5 = 0;
            int Diagnostic6 = 0;

            int consecutiveMaxRun = 0;
            int consecutiveMinRun = 0;
            for (int k = 0; k < StartNumber; k++)
            {
                List<int> chunkMaxValues = new List<int>();
                List<int> chunkMaxPositions = new List<int>();

                List<int> chunkMinValues = new List<int>();
                List<int> chunkMinPositions = new List<int>();

                List<int> chunkMiddleValues = new List<int>();
                List<int> chunkMiddlePositions = new List<int>();

                int consecutiveMaxLoop = 0;
                int consecutiveMinLoop = 0;
                int MinValue = int.MinValue;
                int MaxValue = int.MaxValue;

                int forStart = k + consecutiveMinRun;
                int forStop = StartNumber - k - consecutiveMaxRun;

                for (int i = forStart; i < forStop; i++)
                {
                    int CurrentValue = Locals[i];
                    if (CurrentValue >= MinValue)
                    {
                        chunkMaxValues.Add(CurrentValue);
                        chunkMaxPositions.Add(i);
                        MinValue = CurrentValue;
                        consecutiveMaxLoop++;
                        consecutiveMinLoop = 0;

                        Diagnostic1 += 1;
                    }
                    else if (CurrentValue <= MaxValue)
                    {
                        chunkMinValues.Add(CurrentValue);
                        chunkMinPositions.Add(i);
                        MaxValue = CurrentValue;
                        consecutiveMinLoop++;
                        consecutiveMaxLoop = 0;

                        Diagnostic2 += 1;
                    }
                    else
                    {

                        chunkMiddleValues.Add(CurrentValue);
                        chunkMiddlePositions.Add(i);

                        consecutiveMaxLoop = 0;
                        consecutiveMinLoop = 0;

                        Diagnostic3 += 1;
                    }
                }

                if (chunkMaxValues.Count == Locals.Count)
                {
                    ReturnValue = new List<int>(chunkMaxValues);
                    break;
                }

                if (chunkMaxValues.Count == 0)
                {
                    ReturnValue = new List<int>(Locals);
                    break;
                }

                if (chunkMinValues.Count == Locals.Count)
                {
                    ReturnValue = new List<int>(chunkMinValues);
                    break;
                }

                if (chunkMinValues.Count == 0)
                {
                    //ReturnValue = new List<int>(Locals);
                    //break;
                }

                for (int i = 0; i < chunkMaxValues.Count; i++)
                {
                    int positionToReplace = Locals.Count - 1 - i - k - consecutiveMaxRun;
                    int positionInChunkValues = chunkMaxValues.Count - 1 - i;

                    int localValue = Locals[positionToReplace];
                    int chunkValue = chunkMaxValues[positionInChunkValues];
                    int valueInChunkPositions = chunkMaxPositions[positionInChunkValues];

                    if (localValue < chunkValue)
                    {
                        Locals[positionToReplace] = chunkValue;
                        Locals[valueInChunkPositions] = localValue;
                    }

                    Diagnostic4++;
                }


                for (int i = 0; i < chunkMinValues.Count; i++)
                {
                    int positionToReplace = i + k + consecutiveMinRun;
                    int positionInChunkValues = chunkMinValues.Count - 1 - i;

                    int localValue = Locals[positionToReplace];
                    int chunkValue = chunkMinValues[positionInChunkValues];
                    int valueInChunkPositions = chunkMinPositions[positionInChunkValues];

                    if (localValue > chunkValue)
                    {
                        Locals[positionToReplace] = chunkValue;
                        Locals[valueInChunkPositions] = localValue;
                    }

                    Diagnostic5++;
                }


                if (chunkMiddleValues.Count > 0)
                {
                    List<int> sortedMiddlesValues = RunBothRecursion(chunkMiddleValues);

                    for (int i = 0; i < sortedMiddlesValues.Count; i++)
                    {
                        int positionToReplaceInLocal = forStart + i;
                        int positionInChunkValues = i;

                        //int localValue = Locals[positionToReplaceInLocal];
                        int chunkValue = sortedMiddlesValues[positionInChunkValues];
                        //int valueInChunkPositions = sortedMiddlesValues[positionInChunkValues];

                        Locals[positionToReplaceInLocal] = chunkValue;

                        //if (localValue > chunkValue)
                        //{
                        //    Locals[positionToReplaceInLocal] = chunkValue;
                        //    Locals[valueInChunkPositions] = localValue;
                        //}

                        Diagnostic5++;
                    }
                }





                if (consecutiveMaxLoop > 1)
                {
                    consecutiveMaxRun += consecutiveMaxLoop - 1;
                }

                if (consecutiveMinLoop > 1)
                {
                    consecutiveMinRun += consecutiveMinLoop - 1;
                }

            }

            if(StartNumber != ReturnValue.Count)
            {
                Console.WriteLine("Ops!");
            }


            return ReturnValue;
        }
#endif
    }
}
