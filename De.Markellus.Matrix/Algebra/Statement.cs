using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using De.Markellus.Matrix.Exceptions;

namespace De.Markellus.Matrix.Algebra
{
    public abstract class Statement
    {
        protected const string PATTERN_VARIABLE = "([a-zA-Z])";
        protected const string PATTERN_NUMBER   = "^([0-9]+([.,]*[0-9]+)*)$";

        private static List<Statement> _listStatements = new List<Statement>(GetEnumerableOfType<Statement>()).OrderBy(s => s.Weight).ToList();

        private static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            return Assembly.GetAssembly(typeof(T))
                .GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)))
                .Select(type => (T)Activator.CreateInstance(type, constructorArgs))
                .ToList();
        }

        public static Statement FindMatch(string strStatement)
        {
            foreach (Statement stat in _listStatements)
            {
                if (stat.Match(strStatement))
                {
                    try
                    {
                        return (Statement) Activator.CreateInstance(stat.GetType(), strStatement);
                    }
                    catch
                    {
                        throw new WrongImplementationException(
                            stat.GetType() + " has no constructor with string argument!");
                    }
                }

            }

            return null;
        }

        public abstract uint Weight { get; }

        protected Statement(string strStatement)
        {
            Sanitize(ref strStatement);
            Parse(strStatement);
        }

        protected Statement()
        { }

        private void Sanitize(ref string strStatement)
        {
            strStatement = strStatement.Replace(" ", "");

            if (!Match(strStatement))
            {
                throw new WrongImplementationException("Use Statement.Match() first");
            }
        }

        protected abstract void Parse(string strStatement);

        public abstract double Resolve();

        public abstract bool Match(string strStatement);

        public abstract bool IsNumber();
    }
}
