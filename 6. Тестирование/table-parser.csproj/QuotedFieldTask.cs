using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("\"abc\"", 0, "abc", 5)]
        [TestCase("b \"a'\"", 2, "a'", 4)]
        [TestCase("\\", 0, "", 1)]
        [TestCase(@"'a\' b'", 0, "a' b", 7)]
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actual = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actual);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            StringBuilder result = new StringBuilder();
            int quoteBegin = line[startIndex];
            int i = startIndex + 1;

            while (i < line.Length && line[i] != quoteBegin)//Пока строка не нулевая или меньше длины
            {
                if (line[i] == '\\')
                {
                    i++;
                }

                result.Append(line[i]);
                i++;
            }
            if (i == line.Length)//Если нулевая
            {
                return new Token(result.ToString(), startIndex, i - startIndex);
            }
            return new Token(result.ToString(), startIndex, i - startIndex + 1);
        }
    }
}