using System;
using System.Text.RegularExpressions;

namespace De.Markellus.Matrix.Algebra
{
    public class StatementVariable : Statement
    {
        private char _cVariable;

        public override uint Weight => (uint) StatementWeight.Lowest;

        public StatementVariable(string strStatement) : base(strStatement) { }

        public StatementVariable() { }

        protected override void Parse(string strStatement) => _cVariable = strStatement[0];

        public override double Resolve() => Double.NaN;

        public override bool Match(string strStatement)
        {
            Match match = Regex.Match(strStatement, PATTERN_VARIABLE, RegexOptions.IgnorePatternWhitespace);
            return match.Success && match.Value == strStatement;
        }

        public override bool IsNumber() => false;

        public override string ToString() => _cVariable + "";
    }
}