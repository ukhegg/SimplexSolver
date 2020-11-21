using NUnit.Framework;
using SimplexSolver;

namespace Tests
{
    public class VariableTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestToString()
        {
            var variable = new Variable("Name");
            Assert.AreEqual("Name", variable.ToString());
        }

        [Test]
        public void VariablesAreEqualIfNamesAreTheSame()
        {
            var var1 = new Variable("NameOne");
            var var2 = new Variable("NameOne");
            var var3 = new Variable("NameTwo");
            Assert.IsTrue(Equals(var1, var2));
            Assert.IsTrue(!Equals(var1, var3));
        }
    }
}