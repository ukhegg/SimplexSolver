namespace SimplexSolver
{
    public class Statement
    {
        private Expression _lhs;
        private Relations _relations;
        private Expression _rhs;

        public Statement(Expression lhs, Relations relations, Expression rhs)
        {
            _lhs = lhs;
            _relations = relations;
            _rhs = rhs;
        }


    }
}