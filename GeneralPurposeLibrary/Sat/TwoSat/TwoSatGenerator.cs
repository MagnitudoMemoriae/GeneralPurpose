using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace GeneralPurposeLibrary.Sat.TwoSat
{
    public class TwoSatGenerator
    {

        private int _SeedCounter = 0;
        private Random _Random;
        private Random GetRandom()
        {
            return _Random;
        }

        private int GetNextInt(int n_variables)
        {
            return _Random.Next(1,n_variables);
        }

        private Double GetNextDouble()
        {
            return _Random.NextDouble();
        }

        public Boolean IsPositive(double negative_percentage)
        {
            return _Random.NextDouble() > negative_percentage;
        }

        public int GetRandomVariable(int n_variables)
        {
            return GetNextInt(n_variables);
        }

        public int GetNotThisVariable(int n_variables, int variable)
        {
            int next = GetRandomVariable(n_variables);
            while (variable == next)
            {
                next = GetRandomVariable(n_variables);
            }
            return next;
        }

        public List<int> GetVariables(int n_variables, int seed)
        {
            List<int> variables = new List<int>();

            variables.Add(GetNextInt(n_variables));

            variables.Add(GetNotThisVariable(n_variables, variables[0]));

            return variables;
        }

        public String GetVariableString(int variable, double negative_percentage)
        {
            String ReturnValue = null;
            if (!IsPositive(negative_percentage))
            {
                ReturnValue = $"-{variable}";
            }
            else
            {
                ReturnValue = $"{variable}";
            }
            return ReturnValue;
        }


        public TwoSatGenerator(int n_variables,int max_n_clauses,double negative_percentage, int seed = 42, char separator = ' ') 
        {
           this._Random = new Random(seed);

            string FullPathOutputFile = @"D:\Data\Temp\TwoSat\problem.txt";

            List<String> list = new List<String>();
            int clause_counter = 0;
            for (int i = 1; i <= n_variables; i++)
            {
                StringBuilder sb = new StringBuilder();
                int first = i;
                int second = GetNotThisVariable(n_variables, first);
                sb.Append(GetVariableString(first,  negative_percentage));
                sb.Append(separator);
                sb.Append(GetVariableString(second, negative_percentage));
                sb.Append(" 0");

                if (list.Contains(sb.ToString()))
                {
                    //
                }
                else
                {
                    list.Add(sb.ToString());
                    clause_counter++;
                }

            }

            while ( clause_counter < max_n_clauses)
            {
                StringBuilder sb = new StringBuilder();
                List<int> variables = GetVariables(n_variables,seed);

                sb.Append(GetVariableString(variables[0], negative_percentage));
                sb.Append(separator);
                sb.Append(GetVariableString(variables[1], negative_percentage));
                sb.Append(" 0");

                if (list.Contains(sb.ToString()))
                {
                    //
                }
                else
                {
                    list.Add(sb.ToString());
                    clause_counter++;
                }

            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                stringBuilder.AppendLine(list[i]);
            }

            if(File.Exists(FullPathOutputFile) == true)
            {
                File.Delete(FullPathOutputFile);
            }

            File.WriteAllText(FullPathOutputFile, stringBuilder.ToString());
            
        
        }
    }
}
