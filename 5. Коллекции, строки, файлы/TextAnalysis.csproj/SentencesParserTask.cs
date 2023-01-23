using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            text = text.ToLower(); //Сразу переводим весь текст в нижний регистр
            char[] seps = { '.', ':', ';', '?', '!', '(', ')' };
            var sentences = text.Split(seps, StringSplitOptions.RemoveEmptyEntries);

            foreach (var sentence in sentences)
            {
                List<string> listOfWords = new List<string>(); //Список для слов
                StringBuilder sb = new StringBuilder();       //Строка для составления слова из символов
                foreach (var symbol in sentence)
                {
                    if (char.IsLetter(symbol) || symbol == '\'')
                    {
                        sb.Append(symbol);
                    }
                    else
                    {
                        AddIfWordIsNotEmpty(sb, listOfWords);
                    }
                }

                AddIfWordIsNotEmpty(sb, listOfWords); //Дополнительная проверка 

                if (listOfWords.Count > 0)   //Добавление слов в итоговый список 
                {
                    sentencesList.Add(listOfWords);
                }
            }
            return sentencesList;
        }

        //Метод для проверки длины строки слова
        public static void AddIfWordIsNotEmpty(StringBuilder sb, List<string> listOfWords)
        {
            if (sb.Length > 0)
            {
                listOfWords.Add(sb.ToString());
                sb.Clear();
            }
        }
    }
}
