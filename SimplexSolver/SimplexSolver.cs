using System.Collections.Generic;

namespace SimplexSolver
{
    public class SimplexSolver
    {
        private IList<Statement> _statements;

        public SimplexSolver()
        {
            _statements = new List<Statement>();
        }

        public void AddStatement(Statement st)
        {
            _statements.Add(st);
        }

        public SimplexSolution Solve(ExpressionMinimum target)
        {
            return new SimplexSolution(new Dictionary<Variable, double>(), 0.0);
        }

        public SimplexSolution Solve(ExpressionMaximum target)
        {
            return new SimplexSolution(new Dictionary<Variable, double>(), 0.0);
        }
    }
}