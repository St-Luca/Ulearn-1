using System.Collections.Generic;
using NUnit.Framework;
using System.Text;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("''", new[] { "" })]
        [TestCase("", new string[0])]
        [TestCase("' '", new[] { " " })]
        [TestCase("\"\\\\", new[] { "\\" })] //Слэш внутри и перед кавычками
        [TestCase("Слово", new[] { "Слово" })]
        [TestCase("Слово с 'кавычками'", new[] { "Слово", "с", "кавычками" })]
        [TestCase(" Пробел в конце или начале ", new[] { "Пробел", "в", "конце", "или", "начале" })]
        [TestCase("Большой     пробел", new[] { "Большой", "пробел" })]
        [TestCase("\"Пробел с незакрытой кавычкой ", new[] { "Пробел с незакрытой кавычкой " })]
        [TestCase("\"'Одинарные кавычки в двойных'\"", new[] { "'Одинарные кавычки в двойных'" })]
        [TestCase("'\"Двойные кавычки в одинарных\"'", new[] { "\"Двойные кавычки в одинарных\"" })]
        [TestCase("\"Незакрытая кавычка", new[] { "Незакрытая кавычка" })]
        [TestCase("Разделитель'без'пробелов", new[] { "Разделитель", "без", "пробелов" })]
        [TestCase("'После' поля в кавычках", new[] { "После", "поля", "в", "кавычках", })]
        [TestCase("\'\\\'Экраниров одинарн в одинарн\\\'\'", new[] { "\'Экраниров одинарн в одинарн\'" })]
        [TestCase("\"\\\"Экранированные двойные в двойных\\\"\"", new[] { "\"Экранированные двойные в двойных\"" })]

        public static void RunTests(string input, string[] expectedOutput)
        {   // Тело метода изменять не нужно
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            List<Token> res = new List<Token>();

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ')
                {
                    continue;
                }
                Token token = new Token("", 0, 0);

                if (line[i] == '\"' || line[i] == '\'')
                {
                    token = ReadQuotedField(line, i);
                }
                else
                {
                    token = ReadField(line, i);
                }
                res.Add(token);
                i = token.GetIndexNextToToken() - 1; //Переход сразу к следующему потенциальному токену
            }
            return res;
        }

        private static Token ReadField(string line, int startIndex)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = startIndex; i < line.Length; i++)
            {
                if (line[i] == ' ' || line[i] == '\"' || line[i] == '\'')
                {
                    break;
                }
                sb.Append(line[i]);
            }
            return new Token(sb.ToString(), startIndex, sb.Length);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}