using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Google.OrTools.LinearSolver.Solver;

namespace GeneralPurposeLibrary.OR
{
    public enum OPTIMIZATION
    {
        MAXIMIZATION,
        MINIMIZATION
    };

    public enum LINEAR_PROGRAMMING_PROBLEM
    {
         R,
         INTEGER
    }

    public class ProblemMatrix
    {

        // Matrice dei vincoli
        public readonly Double[,] A;

        // Vettore dei Termini noti
        public readonly Double[] B;

        // Vettore dei cosi
        public readonly Double[] C;

        public ProblemMatrix(Double[,] a, Double[] b, Double[] c)
        {
            A = a;
            B = b;
            C = c;
        }
    }

    public static class Helper
    {
        public static String GetSolverId(LINEAR_PROGRAMMING_PROBLEM problem)
        {
            String ReturnValue = String.Empty; 

             switch(problem)
            {
                case LINEAR_PROGRAMMING_PROBLEM.R:
                    ReturnValue = "GLOP";
                    break;
                case LINEAR_PROGRAMMING_PROBLEM.INTEGER:
                    ReturnValue = "SCIP";
                    break;
            }

            return ReturnValue;
        }
    }

    public abstract class BaseLinearProgramming
    {
        protected ProblemMatrix _Problem;

        protected OPTIMIZATION _Optimization;
        protected LINEAR_PROGRAMMING_PROBLEM _ProgrammingProblem;
        protected Boolean _ActiveDebug;

        protected List<Variable> _Variables = new List<Variable>();
        protected Solver _Solver;
        protected Objective _Objective;
        protected int _NumConstrains = 0;
        protected int _NumVariables = 0;

        public BaseLinearProgramming(ProblemMatrix problem, OPTIMIZATION optimization, LINEAR_PROGRAMMING_PROBLEM programmingProblem, Boolean activeDebug)
        {
            _Problem = problem;

            _Optimization = optimization;
            _ProgrammingProblem = programmingProblem;
            _ActiveDebug = activeDebug;

            _NumConstrains = _Problem.A.GetLength(0);
            _NumVariables = _Problem.A.GetLength(1);

            String sType = Helper.GetSolverId(programmingProblem);
            _Solver = Solver.CreateSolver(sType);

            _Variables = this._GetVariables();
        }

        public BaseLinearProgramming(Double[,] a, Double[] b, Double[] c,
                                        OPTIMIZATION optimization,
                                        LINEAR_PROGRAMMING_PROBLEM programmingProblem ,
                                        Boolean activeDebug) : this(new ProblemMatrix(a, b, c), optimization, programmingProblem, activeDebug)
        {
        }

        protected List<Variable> _GetVariables()
        {
            return GetVariables(this._Solver);
        }

        protected List<Variable> GetVariables(Solver solver)
        {
            List<Variable> ReturnValue = new List<Variable>();

            for (int i = 0; i < GetNumberOfVariables; i++)
            {
                int iVar = i + 1;
                String sVar = this.ToStringVariableName(iVar);
                Variable x; ;
                switch (this._ProgrammingProblem)
                {
                    case LINEAR_PROGRAMMING_PROBLEM.R:
                        x = solver.MakeNumVar(0.0, double.PositiveInfinity, sVar);
                        break;

                    case LINEAR_PROGRAMMING_PROBLEM.INTEGER:
                        x = solver.MakeIntVar(0.0, double.PositiveInfinity, sVar);
                        break;

                    default:
                        x = solver.MakeNumVar(0.0, double.PositiveInfinity, sVar);
                        break;
                }

                ReturnValue.Add(x);
            }

            return ReturnValue;
        }

        protected String ToStringVariableName(int index)
        {
            return String.Format($"x{index}");
        }

        protected String ToStringConstrainName(int index)
        {
            return String.Format($"Cs{index}");
        }

        protected void Print(String log)
        {
            if (this._ActiveDebug == true) { Debug.WriteLine(log); }
        }

        public Solver Solver
        {
            get { return this._Solver; }
        }

        public List<Variable> Variables
        {
            get { return this._Variables; }
        }

        public Objective Objective
        {
            get { return this._Objective; }
        }

        public int GetNumberOfConstraints
        {
            get { return _NumConstrains; }
        }

        public int GetNumberOfVariables
        {
            get { return _NumVariables; }
        }
    }

    public class LinearProgramming : BaseLinearProgramming
    {

        public LinearProgramming(ProblemMatrix problem, OPTIMIZATION optimization, LINEAR_PROGRAMMING_PROBLEM programmingProblem, Boolean activeDebug) : base(problem, optimization, programmingProblem, activeDebug)
        {
            for (int iCt = 0; iCt < this.GetNumberOfConstraints; iCt++)
            {
                double lb = double.NegativeInfinity;
                double ub = this._Problem.B[iCt];
                String name = this.ToStringConstrainName(iCt + 1);

                Constraint cs = _Solver.MakeConstraint(lb, ub, name);

                Print($"Constraint = {name} , lb = {lb} , ub = {ub} ");
                for (int iVar = 0; iVar < this.GetNumberOfVariables; iVar++)
                {
                    double coeff = this._Problem.A[iCt, iVar];
                    Print($"Variables = {this._Variables[iVar].Name()} , coeff = {coeff}");
                    cs.SetCoefficient(this._Variables[iVar], coeff);
                }
            }

            this._Objective = _Solver.Objective();
            for (int iVar = 0; iVar < this.GetNumberOfVariables; iVar++)
            {
                double coeff = this._Problem.C[iVar];
                Print($"objective : Variables = {this._Variables[iVar].Name()} , coeff = {coeff}");
                this._Objective.SetCoefficient(this._Variables[iVar], coeff);
            }

            if (optimization == OPTIMIZATION.MINIMIZATION)
            {
                this._Objective.SetMinimization();
            }
            else
            {
                this._Objective.SetMaximization();
            }
        }

        public LinearProgramming(Double[,] a, Double[] b, Double[] c, OPTIMIZATION optimization, LINEAR_PROGRAMMING_PROBLEM programmingProblem, Boolean activeDebug) : this(new ProblemMatrix(a, b, c), optimization, programmingProblem, activeDebug)
        {

        }
    }
#if false
    internal class PlayGround
    {
#if false
        public class DoubleLinearProgrammingSupport : BaseLinearProgramming
        {
            public DoubleLinearProgrammingSupport(int[,] a, int[] b, int[] c, OPTIMIZATION optimization)
            {
                _A = a;
                _B = b;
                _C = c;

                _Variables = this._GetVariables();

                for (int iCt = 0; iCt < this.GetNumberOfConstraints; iCt++)
                {
                    double lb = double.NegativeInfinity;
                    double ub = this._B[iCt];
                    String name = String.Format($"Cs{iCt + 1}");

                    Constraint cs = _Solver.MakeConstraint(lb, ub, name);
                    Debug.WriteLine($"Constraint = {name} , lb = {lb} , ub = {ub} ");
                    for (int iVar = 0; iVar < this.GetNumberOfVariables; iVar++)
                    {
                        double coeff = this._A[iCt, iVar];
                        Debug.WriteLine($"Variables = {this._Variables[iVar].Name()} , coeff = {coeff}");
                        cs.SetCoefficient(this._Variables[iVar], coeff);
                    }
                }

                this._Objective = _Solver.Objective();
                for (int iVar = 0; iVar < this.GetNumberOfVariables; iVar++)
                {
                    double coeff = this._C[iVar];
                    Debug.WriteLine($"objective : Variables = {this._Variables[iVar].Name()} , coeff = {coeff}");
                    this._Objective.SetCoefficient(this._Variables[iVar], coeff);
                }

                if (optimization == OPTIMIZATION.MINIMIZATION)
                {
                    this._Objective.SetMinimization();
                }
                else
                {
                    this._Objective.SetMaximization();
                }
            }
        }
#endif

        //public enum OptimizationProblemType
        //{
        //    CLP_LINEAR_PROGRAMMING = 0,
        //    GLPK_LINEAR_PROGRAMMING = 1,
        //    GLOP_LINEAR_PROGRAMMING = 2,
        //    SCIP_MIXED_INTEGER_PROGRAMMING = 3,
        //    GLPK_MIXED_INTEGER_PROGRAMMING = 4,
        //    CBC_MIXED_INTEGER_PROGRAMMING = 5,
        //    GUROBI_LINEAR_PROGRAMMING = 6,
        //    GUROBI_MIXED_INTEGER_PROGRAMMING = 7,
        //    CPLEX_LINEAR_PROGRAMMING = 10,
        //    CPLEX_MIXED_INTEGER_PROGRAMMING = 11,
        //    BOP_INTEGER_PROGRAMMING = 12,
        //    SAT_INTEGER_PROGRAMMING = 14,
        //    XPRESS_LINEAR_PROGRAMMING = 101,
        //    XPRESS_MIXED_INTEGER_PROGRAMMING = 102
        //}

        public static void Test001()
        {
            Debug.WriteLine("Test001");

           
            //Double[,] A = new Double[,] { { 1, 2 }, { -3, 1 }, { 1, -1 } };
            //Double[] B = new Double[] { 14, 0, 2 };
            //Double[] C = new Double[] { 3, 4 };

            //Double[,] A = new Double[,] { { 1, 7 }, { 1, 0 } };
            //Double[]  B = new Double[] { 17.5 , 3.5 };
            //Double[]  C = new Double[] { 1, 10 };

            // I vincoli sono nella forma  NegativeInfinity <= A * xi <= Bi
            ProblemMatrix pm1 = new ProblemMatrix(new Double[,] { { 1, 2 }, { -3, 1 }, { 1, -1 } }, new Double[] { 14, 0, 2 }, new Double[] { 3, 4 });
            ProblemMatrix pm2 = new ProblemMatrix(new Double[,] { { 1, 7 }, { 1, 0 } }, new Double[] { 17.5, 3.5 }, new Double[] { 1, 10 });

            LinearProgramming clp = new LinearProgramming(pm1, OPTIMIZATION.MAXIMIZATION, OptimizationProblemType.GLOP_LINEAR_PROGRAMMING, true);
            Solver.ResultStatus resultStatus = clp.Solver.Solve();

            // Check that the problem has an optimal solution.
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Debug.WriteLine("The problem does not have an optimal solution!");
            }
            Print(clp);

            LinearProgramming ilp = new LinearProgramming(pm1, OPTIMIZATION.MAXIMIZATION, OptimizationProblemType.SCIP_MIXED_INTEGER_PROGRAMMING, true);
            resultStatus = ilp.Solver.Solve();
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Debug.WriteLine("The problem does not have an optimal solution!");
            }
            Print(ilp);
        }

        private static void Print(BaseLinearProgramming lp)
        {
            Debug.WriteLine("Number of variables = " + lp.Solver.NumVariables());

            Debug.WriteLine("Number of constraints = " + lp.Solver.NumConstraints());

            Debug.WriteLine("Solution:");
            Debug.WriteLine("Objective value = " + lp.Solver.Objective().Value());
            for (int iVar = 0; iVar < lp.GetNumberOfVariables; iVar++)
            {
                Debug.WriteLine($"{lp.Variables[iVar].Name()} = " + lp.Variables[iVar].SolutionValue());
            }
            Debug.WriteLine("\nAdvanced usage:");
            Debug.WriteLine("Problem solved in " + lp.Solver.WallTime() + " milliseconds");
            Debug.WriteLine("Problem solved in " + lp.Solver.Iterations() + " iterations");
            Debug.WriteLine("Problem solved in " + lp.Solver.Nodes() + " branch-and-bound nodes");
        }

        public static void Test002()
        {
            Debug.WriteLine("Test002");

            Solver solver = Solver.CreateSolver("GLOP");
            // x and y are continuous non-negative variables.
            Variable x = solver.MakeNumVar(0.0, double.PositiveInfinity, "x");
            Variable y = solver.MakeNumVar(0.0, double.PositiveInfinity, "y");

            Debug.WriteLine("Number of variables = " + solver.NumVariables());

            // x + 2y <= 14.
            solver.Add(x + 2 * y <= 14.0);

            // 3x - y >= 0.
            solver.Add(-3 * x + y <= 0.0);

            // x - y <= 2.
            solver.Add(x - y <= 2.0);

            Debug.WriteLine("Number of constraints = " + solver.NumConstraints());

            // Objective function: 3x + 4y.
            solver.Maximize(3 * x + 4 * y);

            Solver.ResultStatus resultStatus = solver.Solve();

            // Check that the problem has an optimal solution.
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Debug.WriteLine("The problem does not have an optimal solution!");
                return;
            }
            Debug.WriteLine("Solution:");
            Debug.WriteLine("Objective value = " + solver.Objective().Value());
            Debug.WriteLine("x = " + x.SolutionValue());
            Debug.WriteLine("y = " + y.SolutionValue());

            Debug.WriteLine("\nAdvanced usage:");
            Debug.WriteLine("Problem solved in " + solver.WallTime() + " milliseconds");
            Debug.WriteLine("Problem solved in " + solver.Iterations() + " iterations");
        }

        public static void Test002_1()
        {
            Debug.WriteLine("Test002");

            Solver solver = Solver.CreateSolver("GLOP");
            // x and y are continuous non-negative variables.
            Variable x = solver.MakeNumVar(0.0, double.PositiveInfinity, "x");
            Variable y = solver.MakeNumVar(0.0, double.PositiveInfinity, "y");

            Debug.WriteLine("Number of variables = " + solver.NumVariables());

            // x + 2y <= 14.
            LinearExpr linearExpr = new LinearExpr();

            solver.Add(x + 2 * y <= 14.0);

            // 3x - y >= 0.
            solver.Add(-3 * x + y <= 0.0);

            // x - y <= 2.
            solver.Add(x - y <= 2.0);

            Debug.WriteLine("Number of constraints = " + solver.NumConstraints());

            // Objective function: 3x + 4y.
            solver.Maximize(3 * x + 4 * y);

            Solver.ResultStatus resultStatus = solver.Solve();

            // Check that the problem has an optimal solution.
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Debug.WriteLine("The problem does not have an optimal solution!");
                return;
            }
            Debug.WriteLine("Solution:");
            Debug.WriteLine("Objective value = " + solver.Objective().Value());
            Debug.WriteLine("x = " + x.SolutionValue());
            Debug.WriteLine("y = " + y.SolutionValue());

            Debug.WriteLine("\nAdvanced usage:");
            Debug.WriteLine("Problem solved in " + solver.WallTime() + " milliseconds");
            Debug.WriteLine("Problem solved in " + solver.Iterations() + " iterations");
        }

        public static void Test003()
        {
            Debug.WriteLine("Test003");
            // Create the linear solver with the GLOP backend.
            Solver solver = Solver.CreateSolver("GLOP");

            // Create the variables x and y.
            Variable x = solver.MakeNumVar(0.0, 1.0, "x");
            Variable y = solver.MakeNumVar(0.0, 2.0, "y");

            Debug.WriteLine("Number of variables = " + solver.NumVariables());

            // Create a linear constraint, 0 <= x + y <= 2.
            Constraint ct = solver.MakeConstraint(0.0, 2.0, "ct");
            ct.SetCoefficient(x, 1);
            ct.SetCoefficient(y, 1);

            Debug.WriteLine("Number of constraints = " + solver.NumConstraints());

            // Create the objective function, 3 * x + y.
            Objective objective = solver.Objective();
            objective.SetCoefficient(x, 3);
            objective.SetCoefficient(y, 1);
            objective.SetMaximization();

            solver.Solve();

            Debug.WriteLine("Solution:");
            Debug.WriteLine("Objective value = " + solver.Objective().Value());
            Debug.WriteLine("x = " + x.SolutionValue());
            Debug.WriteLine("y = " + y.SolutionValue());
        }

        public static void Test004()
        {
            Debug.WriteLine("Test004");

            Double[,] A = new Double[,] { { 1, 0 }, { 0, 1 }, { 1, 1 } };
            Double[] B = new Double[] { 1, 2, 2 };
            Double[] C = new Double[] { 3, 1 };

            // Create the linear solver with the GLOP backend.
            //Solver solver = Solver.CreateSolver("GLOP");

            LinearProgramming lp = new LinearProgramming(A, B, C, OPTIMIZATION.MAXIMIZATION, OptimizationProblemType.GLOP_LINEAR_PROGRAMMING, true);

            // Create the variables x and y.
            //Variable x = solver.MakeNumVar(0.0, 1.0, "x");
            //Variable y = solver.MakeNumVar(0.0, 2.0, "y");

            Debug.WriteLine("Number of variables = " + lp.Solver.NumVariables());

            // Create a linear constraint, 0 <= x + y <= 2.
            //Constraint ct = solver.MakeConstraint(0.0, 2.0, "ct");
            //ct.SetCoefficient(x, 1);
            //ct.SetCoefficient(y, 1);

            Debug.WriteLine("Number of constraints = " + lp.Solver.NumConstraints());

            // Create the objective function, 3 * x + y.

            lp.Solver.Solve();

            Debug.WriteLine("Solution:");
            Debug.WriteLine("Objective value = " + lp.Solver.Objective().Value());
            for (int iVar = 0; iVar < lp.GetNumberOfVariables; iVar++)
            {
                Debug.WriteLine($"x{iVar + 1} = " + lp.Variables[0].SolutionValue());
            }
        }

        public static void Test005()
        {
            ProblemMatrix pm1 = new ProblemMatrix(new Double[,] { { -1, 0 }, { 0, -1 }, { 1, 3 } }, new Double[] { -1, -1, 14 }, new Double[] { 1, 4 });
            LinearProgramming clp = new LinearProgramming(pm1, OPTIMIZATION.MAXIMIZATION, OptimizationProblemType.GLOP_LINEAR_PROGRAMMING, true);
            Solver.ResultStatus resultStatus = clp.Solver.Solve();

            // Check that the problem has an optimal solution.
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Debug.WriteLine("The problem does not have an optimal solution!");
            }
            Print(clp);
        }
     }
#endif
}