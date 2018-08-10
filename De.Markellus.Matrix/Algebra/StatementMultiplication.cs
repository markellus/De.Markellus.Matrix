using System;
using System.Text.RegularExpressions;

namespace De.Markellus.Matrix.Algebra
{
    public class StatementMultiplication : Statement
    {

        private const string PATTERN_MULTIPLY = "(^[^*]+)\\*(.+)$"; //"^(([0-9]+|[a-zA-Z])\\*([0-9]+|[a-zA-Z]))|(([0-9]+)[a-zA-Z])$";

        private Statement _statLeft  = null;
        private Statement _statRight = null;

        public override uint Weight => (uint) StatementWeight.Point;

        public StatementMultiplication(string strStatement) : base(strStatement) { }

        public StatementMultiplication() { }

        protected override void Parse(string strStatement)
        {
            Match match = Regex.Match(strStatement, PATTERN_MULTIPLY, RegexOptions.IgnorePatternWhitespace);

            _statLeft   = Statement.FindMatch(match.Groups[1].Value);
            _statRight  = Statement.FindMatch(match.Groups[2].Value);
        }

        public override double Resolve()
        {
            if (!IsNumber())
            {
                return Double.NaN;
            }

            return _statLeft.Resolve() * _statRight.Resolve();
        }

        public override bool Match(string strStatement)
        {
            Match match = Regex.Match(strStatement, PATTERN_MULTIPLY, RegexOptions.IgnorePatternWhitespace);
            return match.Success && match.Value == strStatement;
        }

        public override bool IsNumber() => _statLeft.IsNumber() && _statRight.IsNumber();

        public override string ToString() => _statLeft + " * " + _statRight;
    }
}
