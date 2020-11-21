using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using static SimplexSolver.Expression;

namespace SimplexSolver
{
    //represents linear combination of variables
    public class Expression
    {
        private readonly Dictionary<Variable, double> _variables;

        public Expression()
        {
            _variables = new Dictionary<Variable, double>();
        }

        public Expression(Variable var, double coef)
        {
            _variables = new Dictionary<Variable, double>();
            if (coef != 0)
            {
                _variables[var] = coef;
            }
        }


        public double FreeMember { get; private set; } = 0;

        public IReadOnlyDictionary<Variable, double> Variables => _variables;

        public static Expression FromVariable(Variable var)
        {
            return new Expression(var, 1.0);
        }

        public static Expression FromValue(double val)
        {
            return new Expression() {FreeMember = val};
        }

        #region Unary -

        public static Expression operator -(Expression expr)
        {
            var result = new Expression() {FreeMember = -expr.FreeMember};
            foreach (var (variable, coef) in expr.Variables)
            {
                result._variables[variable] = -coef;
            }

            return result;
        }

        #endregion

        #region Binary +

        public static Expression operator +(Expression lhs, Expression rhs)
        {
            var result = new Expression
            {
                FreeMember = lhs.FreeMember + rhs.FreeMember
            };

            foreach (var (variable, value) in lhs.Variables)
            {
                result._variables.Add(variable, value);
            }

            foreach (var (variable, value) in rhs.Variables)
            {
                if (result._variables.ContainsKey(variable))
                {
                    var coef = result._variables[variable] + value;
                    if (coef != 0)
                    {
                        result._variables[variable] = coef;
                    }
                    else
                    {
                        result._variables.Remove(variable);
                    }
                }
                else
                {
                    result._variables[variable] = value;
                }
            }

            return result;
        }

        public static Expression operator +(Expression lhs, double rhs)
        {
            return lhs + FromValue(rhs);
        }

        public static Expression operator +(double lhs, Expression rhs)
        {
            return FromValue(lhs) + rhs;
        }

        public static Expression operator +(Expression lhs, Variable rhs)
        {
            return lhs + FromVariable(rhs);
        }

        public static Expression operator +(Variable lhs, Expression rhs)
        {
            return FromVariable(lhs) + rhs;
        }

        #endregion

        #region Binary -

        public static Expression operator -(Expression lhs, Expression rhs)
        {
            var result = new Expression
            {
                FreeMember = lhs.FreeMember - rhs.FreeMember
            };

            foreach (var (variable, value) in lhs.Variables)
            {
                result._variables.Add(variable, value);
            }

            foreach (var (variable, value) in rhs.Variables)
            {
                if (result._variables.ContainsKey(variable))
                {
                    var coef = result._variables[variable] - value;
                    if (coef != 0)
                    {
                        result._variables[variable] = coef;
                    }
                    else
                    {
                        result._variables.Remove(variable);
                    }
                }
                else
                {
                    result._variables[variable] = -value;
                }
            }

            return result;
        }

        public static Expression operator -(Expression lhs, double rhs)
        {
            return lhs - FromValue(rhs);
        }

        public static Expression operator -(double lhs, Expression rhs)
        {
            return FromValue(lhs) - rhs;
        }

        public static Expression operator -(Expression lhs, Variable rhs)
        {
            return lhs - FromVariable(rhs);
        }

        public static Expression operator -(Variable lhs, Expression rhs)
        {
            return FromVariable(lhs) - rhs;
        }

        #endregion

        #region Binary *

        public static Expression operator *(Expression lhs, double value)
        {
            var result = new Expression() {FreeMember = lhs.FreeMember * value};
            foreach (var (variable, coef) in lhs.Variables)
            {
                result._variables[variable] = coef * value;
            }

            return result;
        }

        public static Expression operator *(double value, Expression lhs)
        {
            return lhs * value;
        }

        #endregion

        #region Binary /

        public static Expression operator /(Expression lhs, double value)
        {
            return lhs * (1 / value);
        }

        #endregion

        #region Expression == Expression

        public static Statement operator ==(Expression lhs, Expression rhs)
        {
            return new Statement(lhs, Relations.Equal, rhs);
        }

        public static Statement operator !=(Expression lhs, Expression rhs)
        {
            return new Statement(lhs, Relations.NotEqual, rhs);
        }

        #endregion

        #region Expression == double

        public static Statement operator ==(Expression lhs, double rhs)
        {
            return new Statement(lhs, Relations.Equal, Expression.FromValue(rhs));
        }

        public static Statement operator !=(Expression lhs, double rhs)
        {
            return new Statement(lhs, Relations.NotEqual, Expression.FromValue(rhs));
        }

        public static Statement operator ==(double lhs, Expression rhs)
        {
            return new Statement(Expression.FromValue(lhs), Relations.Equal, rhs);
        }

        public static Statement operator !=(double lhs, Expression rhs)
        {
            return new Statement(Expression.FromValue(lhs), Relations.NotEqual, rhs);
        }

        #endregion

        #region Expression == Variable

        public static Statement operator ==(Expression lhs, Variable rhs)
        {
            return new Statement(lhs, Relations.Equal, Expression.FromVariable(rhs));
        }

        public static Statement operator !=(Expression lhs, Variable rhs)
        {
            return new Statement(lhs, Relations.NotEqual, Expression.FromVariable(rhs));
        }

        public static Statement operator ==(Variable lhs, Expression rhs)
        {
            return new Statement(Expression.FromVariable(lhs), Relations.Equal, rhs);
        }

        public static Statement operator !=(Variable lhs, Expression rhs)
        {
            return new Statement(Expression.FromVariable(lhs), Relations.NotEqual, rhs);
        }

        #endregion

        #region Expression <> Expression

        public static Statement operator >(Expression lhs, Expression rhs)
        {
            return new Statement(lhs, Relations.Greater, rhs);
        }

        public static Statement operator <(Expression lhs, Expression rhs)
        {
            return new Statement(lhs, Relations.Less, rhs);
        }

        #endregion

        #region Expression <> double

        public static Statement operator >(Expression lhs, double rhs)
        {
            return lhs > Expression.FromValue(rhs);
        }

        public static Statement operator <(Expression lhs, double rhs)
        {
            return lhs < Expression.FromValue(rhs);
        }

        public static Statement operator >(double lhs, Expression rhs)
        {
            return Expression.FromValue(lhs) > lhs;
        }

        public static Statement operator <(double lhs, Expression rhs)
        {
            return Expression.FromValue(lhs) < lhs;
        }

        #endregion

        #region Expression <>  Variable

        public static Statement operator >(Expression lhs, Variable rhs)
        {
            return lhs > Expression.FromVariable(rhs);
        }

        public static Statement operator <(Expression lhs, Variable rhs)
        {
            return lhs < Expression.FromVariable(rhs);
        }

        public static Statement operator >(Variable lhs, Expression rhs)
        {
            return Expression.FromVariable(lhs) > rhs;
        }

        public static Statement operator <(Variable lhs, Expression rhs)
        {
            return Expression.FromVariable(lhs) < rhs;
        }

        #endregion

        #region Expression <=> Expression

        public static Statement operator >=(Expression lhs, Expression rhs)
        {
            return new Statement(lhs, Relations.GreaterOrEqual, rhs);
        }

        public static Statement operator <=(Expression lhs, Expression rhs)
        {
            return new Statement(lhs, Relations.LessOrEqual, rhs);
        }

        #endregion

        #region Expression <=> double

        public static Statement operator >=(Expression lhs, double rhs)
        {
            return lhs >= Expression.FromValue(rhs);
        }

        public static Statement operator <=(Expression lhs, double rhs)
        {
            return lhs <= Expression.FromValue(rhs);
        }

        public static Statement operator >=(double lhs, Expression rhs)
        {
            return Expression.FromValue(lhs) >= lhs;
        }

        public static Statement operator <=(double lhs, Expression rhs)
        {
            return Expression.FromValue(lhs) <= lhs;
        }

        #endregion

        #region Expression <=> Variable

        public static Statement operator >=(Expression lhs, Variable rhs)
        {
            return lhs >= Expression.FromVariable(rhs);
        }

        public static Statement operator <=(Expression lhs, Variable rhs)
        {
            return lhs <= Expression.FromVariable(rhs);
        }

        public static Statement operator >=(Variable lhs, Expression rhs)
        {
            return Expression.FromVariable(lhs) >= rhs;
        }

        public static Statement operator <=(Variable lhs, Expression rhs)
        {
            return Expression.FromVariable(lhs) <= rhs;
        }

        #endregion
    }
}