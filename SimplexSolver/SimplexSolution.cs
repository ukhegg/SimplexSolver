using System.Collections.Generic;

namespace SimplexSolver
{
    public class SimplexSolution
    {
        public SimplexSolution(IReadOnlyDictionary<Variable, double> variables,
            double expressionValue)
        {
            Variables = variables;
            ExpressionValue = expressionValue;
        }

        public IReadOnlyDictionary<Variable, double> Variables { get; }

        public double ExpressionValue { get; }
    }
}