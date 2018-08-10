using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace De.Markellus.Matrix.Algebra
{
    public class StatementDecimal : Statement
    {
        private double _dNumber = 0;

        public override uint Weight => (uint)StatementWeight.Lowest;

        public StatementDecimal(string strStatement) : base(strStatement) { }

        public StatementDecimal() { }

        

        protected override void Parse(string strStatement)
        {
            if (!Double.TryParse(strStatement, NumberStyles.Any, CultureInfo.InvariantCulture, out _dNumber))
            {
                throw new ArithmeticException("Can not parse \"" + strStatement + "\" as a decimal");
            }
        }

        public override double Resolve() => _dNumber;

        public override bool Match(string strStatement)
        {
            Match match = Regex.Match(strStatement, PATTERN_NUMBER, RegexOptions.IgnorePatternWhitespace);
            return match.Success && match.Value == strStatement;
        }

        public override bool IsNumber() => true;

        public override string ToString() => _dNumber.ToString(CultureInfo.CurrentCulture);
    }
}