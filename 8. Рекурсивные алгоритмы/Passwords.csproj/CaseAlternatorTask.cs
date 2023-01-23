using System;
using System.Collections.Generic;
using System.Linq;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        //Тесты будут вызывать этот метод
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            return result;
        }

        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            if (startIndex < word.Length)
            {
                if (!char.IsLetter(word[startIndex])) //Пропуск не-букв
                {
                    AlternateCharCases(word, startIndex + 1, result);
                }
                else
                {
                    word[startIndex] = char.ToLower(word[startIndex]); //Ветка перебора по нижнему регистру
                    AlternateCharCases(word, startIndex + 1, result);

                    word[startIndex] = char.ToUpper(word[startIndex]); //Ветка перебора по верхнему регистру
                    AlternateCharCases(word, startIndex + 1, result);
                }
            }

            string resWord = new string(word);
            //Проверка на уникальность получившегося слова и его добавление в список
            if (!result.Contains(resWord))
            {
                result.Add(resWord);
            }
        }
    }
}