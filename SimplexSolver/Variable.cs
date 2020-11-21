namespace SimplexSolver
{
    public class Variable
    {
        public Variable(string name)
        {
            Name = name;
        }

        public string Name { get; }

        protected bool Equals(Variable other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Variable) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return Name;
        }

        public static Expression operator +(Variable lhs, Variable rhs)
        {
            return Expression.FromVariable(lhs) + Expression.FromVariable(rhs);
        }

        public static Expression operator -(Variable lhs, Variable rhs)
        {
            return Expression.FromVariable(lhs) - Expression.FromVariable(rhs);
        }

        public static Expression operator +(Variable var, double value)
        {
            return Expression.FromVariable(var) + Expression.FromValue(value);
        }

        public static Expression operator +(double value, Variable var)
        {
            return Expression.FromVariable(var) + Expression.FromValue(value);
        }

        public static Expression operator *(Variable var, double coef)
        {
            return new Expression(var, coef);
        }

        public static Expression operator *(double coef, Variable var)
        {
            return new Expression(var, coef);
        }

        public static Expression operator -(Variable var)
        {
            return new Expression(var, -1);
        }

        #region <>

        public static Statement operator <(Variable var1, Variable var2)
        {
            return Expression.FromVariable(var1) < Expression.FromVariable(var2);
        }

        public static Statement operator >(Variable var1, Variable var2)
        {
            return Expression.FromVariable(var1) > Expression.FromVariable(var2);
        }

        public static Statement operator <(Variable var1, double val)
        {
            return Expression.FromVariable(var1) < Expression.FromValue(val);
        }

        public static Statement operator >(Variable var1, double val)
        {
            return Expression.FromVariable(var1) > Expression.FromValue(val);
        }

        public static Statement operator <(double val, Variable var)
        {
            return Expression.FromValue(val) < Expression.FromVariable(var);
        }

        public static Statement operator >(double val, Variable var)
        {
            return Expression.FromValue(val) > Expression.FromVariable(var);
        }

        #endregion


        #region <=>

        public static Statement operator <=(Variable lhs, Variable rhs)
        {
            return Expression.FromVariable(lhs) <= Expression.FromVariable(rhs);
        }

        public static Statement operator >=(Variable lhs, Variable rhs)
        {
            return Expression.FromVariable(lhs) >= Expression.FromVariable(rhs);
        }

        public static Statement operator <=(Variable lhs, double rhs)
        {
            return Expression.FromVariable(lhs) <= Expression.FromValue(rhs);
        }

        public static Statement operator >=(Variable lhs, double rhs)
        {
            return Expression.FromVariable(lhs) >= Expression.FromValue(rhs);
        }

        public static Statement operator <=(double a1, Variable a2)
        {
            return Expression.FromValue(a1) <= Expression.FromVariable(a2);
        }

        public static Statement operator >=(double rhs, Variable lhs)
        {
            return Expression.FromValue(rhs) >= Expression.FromVariable(lhs);
        }

        #endregion

        #region ==

        public static Statement operator ==(Variable lhs, Variable rhs)
        {
            return Expression.FromVariable(lhs) == Expression.FromVariable(rhs);
        }

        public static Statement operator !=(Variable lhs, Variable rhs)
        {
            return Expression.FromVariable(lhs) != Expression.FromVariable(rhs);
        }

        public static Statement operator ==(Variable lhs, double rhs)
        {
            return Expression.FromVariable(lhs) == Expression.FromValue(rhs);
        }

        public static Statement operator !=(Variable lhs, double rhs)
        {
            return Expression.FromVariable(lhs) != Expression.FromValue(rhs);
        }

        public static Statement operator ==(double rhs, Variable lhs)
        {
            return Expression.FromVariable(lhs) == Expression.FromValue(rhs);
        }

        public static Statement operator !=(double rhs, Variable lhs)
        {
            return Expression.FromVariable(lhs) != Expression.FromValue(rhs);
        }

        #endregion
    }
}