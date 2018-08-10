using System;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using NUnit.Framework;

namespace De.Markellus.Matrix.Algebra.Tests
{
    [TestFixture]
    public class StatementTest
    {
        [Test]
        public void TestStatementDecimal()
        {
            StatementDecimal stat = new StatementDecimal();
            Assert.IsTrue(stat.Match("23"));
            Assert.IsTrue(stat.Match("14"));
            Assert.IsTrue(stat.Match("12,5"));
            Assert.IsTrue(stat.Match("12.543"));
            Assert.IsTrue(stat.Match("43243235432"));
            Assert.IsTrue(stat.Match("43243235432.53343"));
            Assert.IsFalse(stat.Match("2*20x"));
            Assert.IsFalse(stat.Match("x5"));
            Assert.IsFalse(stat.Match("100z"));
            Assert.IsFalse(stat.Match("x"));

            stat = new StatementDecimal("200.5");
            Assert.IsTrue(stat.IsNumber());
            Assert.AreEqual(200.5, stat.Resolve());
        }

        [Test]
        public void TestStatementVariable()
        {
            StatementVariable stat = new StatementVariable();
            Assert.IsTrue(stat.Match("a"));
            Assert.IsTrue(stat.Match("B"));
            Assert.IsTrue(stat.Match("t"));
            Assert.IsTrue(stat.Match("X"));
            Assert.IsFalse(stat.Match("2"));
            Assert.IsFalse(stat.Match("x5"));
            Assert.IsFalse(stat.Match("100z"));
            Assert.IsFalse(stat.Match("x="));
            Assert.IsFalse(stat.Match("ab"));
            Assert.IsFalse(stat.Match("Ls"));
            Assert.IsFalse(stat.Match("xD"));

            stat = new StatementVariable("s");
            Assert.IsFalse(stat.IsNumber());
            Assert.AreEqual(Double.NaN, stat.Resolve());
        }

        [Test]
        public void TestStatementMultiplication()
        {
            StatementMultiplication stat = new StatementMultiplication();
            Assert.IsTrue(stat.Match("23.0*1,94"));
            Assert.IsTrue(stat.Match("x*14"));
            Assert.IsTrue(stat.Match("14*x"));
            Assert.IsTrue(stat.Match("20x*2x"));
            Assert.IsTrue(stat.Match("20x*2"));
            Assert.IsTrue(stat.Match("2*20x"));
            Assert.IsFalse(stat.Match("100"));

            stat = new StatementMultiplication("5*10");
            Assert.AreEqual(50, stat.Resolve());

            stat = new StatementMultiplication(" 5 * 10 ");
            Assert.AreEqual(50, stat.Resolve());

            stat = new StatementMultiplication(" 2 *5 * 10 ");
            Assert.AreEqual(100, stat.Resolve());
        }
    }
}