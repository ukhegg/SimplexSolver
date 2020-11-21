using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using NUnit.Framework;
using SimplexSolver;

namespace Tests
{
    public class ExpressionTests
    {
        [Test]
        public void CanCreateExpressionFromVariable()
        {
            var expr = Expression.FromVariable(new Variable("X"));

            Assert.AreEqual(0, expr.FreeMember);
            Assert.True(expr.Variables.ContainsKey(new Variable("X")));
            Assert.AreEqual(1, expr.Variables[new Variable("X")]);
        }

        [Test]
        public void CanCreateExpressionFromDoubleValue()
        {
            var expr = Expression.FromValue(1.0);
            Assert.AreEqual(1.0, expr.FreeMember);
            Assert.IsFalse(expr.Variables.Any());
        }

        [Test]
        public void CanCreateExpressionFromVariableMultipliedByDouble()
        {
            var x = new Variable("X");
            var expr = x * 5;
            Assert.AreEqual(0, expr.FreeMember);
            Assert.IsTrue(expr.Variables.ContainsKey(x));
            Assert.AreEqual(5, expr.Variables[x]);
        }

        [Test]
        public void CanCreateExpressionFromDoubleMultipliedByVariable()
        {
            var x = new Variable("X");
            var expr = 5 * x;
            Assert.AreEqual(0, expr.FreeMember);
            Assert.IsTrue(expr.Variables.ContainsKey(x));
            Assert.AreEqual(5, expr.Variables[x]);
        }

        [Test]
        public void CanCreateExpressionFromVariableAddedToDouble()
        {
            var x = new Variable("X");
            var expr = 5 + x;

            Assert.AreEqual(5, expr.FreeMember);
            Assert.IsTrue(expr.Variables.ContainsKey(x));
            Assert.AreEqual(1, expr.Variables[x]);
        }

        [Test]
        public void CanCreateExpressionFromDoubleAddedToVariable()
        {
            var x = new Variable("X");
            var expr = x + 5;

            Assert.AreEqual(5, expr.FreeMember);
            Assert.IsTrue(expr.Variables.ContainsKey(x));
            Assert.AreEqual(1, expr.Variables[x]);
        }

        [Test]
        public void CanAddDoubleToExpression()
        {
            var x = new Variable("X");
            var expr = x * 11;

            var expr2 = expr + 7;

            Assert.AreEqual(7, expr2.FreeMember);
            Assert.IsTrue(expr2.Variables.ContainsKey(x));
            Assert.AreEqual(11, expr2.Variables[x]);
        }

        [Test]
        public void CanAddExpressionToDouble()
        {
            var x = new Variable("X");
            var expr = x * 11;

            var expr2 = 7 + expr;

            Assert.AreEqual(7, expr2.FreeMember);
            Assert.IsTrue(expr2.Variables.ContainsKey(x));
            Assert.AreEqual(11, expr2.Variables[x]);
        }

        [Test]
        public void CanAddVariableToExpression()
        {
            var x = new Variable("X");
            var expr = x * 11;

            var expr2 = expr + x;

            Assert.AreEqual(0, expr2.FreeMember);
            Assert.IsTrue(expr2.Variables.ContainsKey(x));
            Assert.AreEqual(12, expr2.Variables[x]);
        }

        [Test]
        public void CanAddExpressionToVariable()
        {
            var x = new Variable("X");
            var expr = x * 11;

            var expr2 = x + expr;

            Assert.AreEqual(0, expr2.FreeMember);
            Assert.IsTrue(expr2.Variables.ContainsKey(x));
            Assert.AreEqual(12, expr2.Variables[x]);
        }

        [Test]
        public void CanSubtractDoubleFromExpression()
        {
            var x = new Variable("X");
            var expr = x * 11;

            var expr2 = expr - 7;

            Assert.AreEqual(-7, expr2.FreeMember);
            Assert.IsTrue(expr2.Variables.ContainsKey(x));
            Assert.AreEqual(11, expr2.Variables[x]);
        }

        [Test]
        public void CanSubtractExpressionFromDouble()
        {
            var x = new Variable("X");
            var expr = x * 11;

            var expr2 = 7 - expr;

            Assert.AreEqual(7, expr2.FreeMember);
            Assert.IsTrue(expr2.Variables.ContainsKey(x));
            Assert.AreEqual(-11, expr2.Variables[x]);
        }

        [Test]
        public void CanSubtractVariableFromExpression()
        {
            var x = new Variable("X");
            var expr = x * 11;

            var expr2 = expr - x;

            Assert.AreEqual(0, expr2.FreeMember);
            Assert.IsTrue(expr2.Variables.ContainsKey(x));
            Assert.AreEqual(10, expr2.Variables[x]);
        }

        [Test]
        public void CanSubtractExpressionFromVariable()
        {
            var x = new Variable("X");
            var expr = x * 11;

            var expr2 = x - expr;

            Assert.AreEqual(0, expr2.FreeMember);
            Assert.IsTrue(expr2.Variables.ContainsKey(x));
            Assert.AreEqual(-10, expr2.Variables[x]);
        }

        [Test]
        public void CanSubtractExpressionFromExpression()
        {
            var x = new Variable("X");
            var y = new Variable("Y");
            var lhs = 5 * x + y + 20;
            var rhs = 10 * x - y + 5;

            var res = lhs - rhs;

            Assert.IsTrue(res.Variables.ContainsKey(x));
            Assert.IsTrue(res.Variables.ContainsKey(y));
            Assert.AreEqual(-5, res.Variables[x]);
            Assert.AreEqual(2, res.Variables[y]);
            Assert.AreEqual(15, res.FreeMember);
        }

        [Test]
        public void VariableIsRemovedFromVariablesListIfCoefIsZero()
        {
            var x = new Variable("X");

            var expr = x * 0;
            Assert.IsFalse(expr.Variables.Any());
        }

        [Test]
        public void RemovesVariableIfZeroCoefAfterAddition()
        {
            var x = new Variable("X");
            var y = new Variable("Y");

            var lhs = 5 * x;
            var rhs = y - 5 * x;
            var expr = lhs + rhs;

            Assert.IsFalse(expr.Variables.ContainsKey(x));
        }

        [Test]
        public void RemovesVariableIfZeroCoefAfterSubtraction()
        {
            var x = new Variable("X");
            var y = new Variable("Y");

            var lhs = 5 * x;
            var rhs = y + 5 * x;
            var expr = lhs - rhs;

            Assert.IsFalse(expr.Variables.ContainsKey(x));
        }

        [Test]
        public void CanMultiplyExpressionByDouble()
        {
            var x = new Variable("X");

            var expr = x + 10;

            expr = expr * 3;
            Assert.AreEqual(3, expr.Variables[x]);
            Assert.AreEqual(30, expr.FreeMember);
        }

        [Test]
        public void CanMultiplyDoubleByExpression()
        {
            var x = new Variable("X");

            var expr = x + 10;

            expr = 3 * expr;
            Assert.AreEqual(3, expr.Variables[x]);
            Assert.AreEqual(30, expr.FreeMember);
        }

        [Test]
        public void CanDivideExpressionByDouble()
        {
            var x = new Variable("X");

            var expr = 10 * x + 34;

            expr = expr / 5;

            Assert.AreEqual(34.0 / 5, expr.FreeMember, 0.1);
            Assert.AreEqual(2, expr.Variables[x]);
        }

        [Test]
        public void ExpressionToStringTest()
        {
        }
    }
}