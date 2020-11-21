using NUnit.Framework;
using SimplexSolver;

namespace Tests
{
    public class SimplexSolverTests
    {
        [Test]
        public void CanSolverSampleTask()
        {
            var solver = new SimplexSolver.SimplexSolver();
            var x1 = new Variable("x1");
            var x2 = new Variable("x2");

            solver.AddStatement(-x1 + 2 * x2 >= 2);
            solver.AddStatement(x1 + x2 >= 4);
            solver.AddStatement(x1 - x2 <= 2);
            solver.AddStatement(x2 <= 6);
            solver.AddStatement(x1 >= 0);
            solver.AddStatement(x2 <= 0);

            var sol = solver.Solve(new ExpressionMinimum(x1 + 2 * x2));
        }
    }
}