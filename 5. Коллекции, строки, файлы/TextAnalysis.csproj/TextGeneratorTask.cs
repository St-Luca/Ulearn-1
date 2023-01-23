using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var parsedBeginningPhrase = SentencesParserTask.ParseSentences(phraseBeginning);
            var lastWord = parsedBeginningPhrase[parsedBeginningPhrase.Count - 1]; //Последнее слово начальной фразы
            StringBuilder result = new StringBuilder(phraseBeginning);

            for (int i = 0; i < wordsCount; i++)
            {
                if (nextWords.ContainsKey(GetLastWords(lastWord))) //Случай А
                {
                    result.Append(" " + nextWords[GetLastWords(lastWord)]);
                    lastWord.Add(nextWords[GetLastWords(lastWord)]);
                }
                else
                {
                    if (nextWords.ContainsKey(lastWord[lastWord.Count - 1])) //Случай В
                    {
                        result.Append(" " + nextWords[lastWord[lastWord.Count - 1]]);
                        lastWord.Add(nextWords[lastWord[lastWord.Count - 1]]);
                    }
                    else //Случай С
                    {
                        break;
                    }
                }
            }
            return result.ToString();
        }

        public static string GetLastWords(List<string> words) //Метод для получения последних слов для ключа
        {
            if (words.ToArray().Length > 1)
            {
                return words[words.ToArray().Length - 2] + " " + words[words.ToArray().Length - 1];
            }
            else
            {
                return words[0];
            }
        }
    }
}