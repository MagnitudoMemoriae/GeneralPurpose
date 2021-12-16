using GeneralPurposeLibrary.OR;
using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Google.OrTools.LinearSolver.Solver;

namespace GeneralPurposeGUITest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTest001_Click(object sender, RoutedEventArgs e)
        {
            Test001();
        }

        public void Test001()
        {

            ProblemMatrix pm1 = new ProblemMatrix(new Double[,] { { -1, 0 }, { 0, -1 }, { 1, 3 } }, new Double[] { -1, -1, 14 }, new Double[] { 1, 4 });
            LinearProgramming clp = new LinearProgramming(pm1, OPTIMIZATION.MAXIMIZATION, LINEAR_PROGRAMMING_PROBLEM.R, true);
            Solver.ResultStatus resultStatus = clp.Solver.Solve();

            // Check that the problem has an optimal solution.
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Debug.WriteLine("The problem does not have an optimal solution!");
            }
            Print(clp);
        }

        private void btnTest002_Click(object sender, RoutedEventArgs e)
        {
            Test002();
        }

        public void Test002()
        {

            ProblemMatrix pm1 = new ProblemMatrix(new Double[,] { { -1, 0 }, { 0, -1 }, { 1, 3 } }, new Double[] { -1, -1, 14 }, new Double[] { 1, 4 });
            LinearProgramming clp = new LinearProgramming(pm1, OPTIMIZATION.MAXIMIZATION, LINEAR_PROGRAMMING_PROBLEM.INTEGER, true);
            Solver.ResultStatus resultStatus = clp.Solver.Solve();

            // Check that the problem has an optimal solution.
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Debug.WriteLine("The problem does not have an optimal solution!");
            }
            Print(clp);
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


    }
}
