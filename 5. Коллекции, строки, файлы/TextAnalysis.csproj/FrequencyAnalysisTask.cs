using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            Dictionary<string, string> resDictionary = new Dictionary<string, string>();
            //Начало N-граммы : (продолжение N-граммы : количество повторов)
            Dictionary<string, Dictionary<string, int>> dictionaryOfVariants = new
                                                    Dictionary<string, Dictionary<string, int>>();
            SortNgram(text, dictionaryOfVariants);

            foreach (var e in dictionaryOfVariants)
            {//Добавление первого значения из отсортированного по убыванию 
             //сначала по кол-ву повторов, а потом по лексикографическому значению, словаря
                resDictionary.Add(e.Key, e.Value.OrderByDescending(x => x.Value).
                                           ThenBy(y => y.Key, StringComparer.Ordinal).First().Key);
            }

            return resDictionary;
        }

        public static void SortNgram(List<List<string>> text, Dictionary<string,
                                                 Dictionary<string, int>> dictionaryOfVariants)
        {
            foreach (List<string> sentence in text)
            {
                for (int i = 0; i < sentence.Count - 1; i++)
                {
                    string wordKey = sentence[i];
                    //Где i - индекс в предложении, а 1 - для указания правильной позиции в SearchNgram
                    SearchNgram(dictionaryOfVariants, sentence, wordKey, i, 1);
                    if (i < sentence.Count - 2) //Триграммы
                    {
                        wordKey = sentence[i] + " " + sentence[i + 1];
                        SearchNgram(dictionaryOfVariants, sentence, wordKey, i, 2);
                    }
                }
            }
        }

        public static void SearchNgram(Dictionary<string, Dictionary<string, int>>
                dictionaryOfVariants, List<string> sentence, string wordKey, int index, int pos)
        {
            if (!dictionaryOfVariants.ContainsKey(wordKey))
            { //Если это первое начало нграммы(уникальное)
                dictionaryOfVariants[wordKey] = new Dictionary<string, int> { { sentence[index + pos], 0 } };
            }
            else if (!dictionaryOfVariants[wordKey].ContainsKey(sentence[index + pos]))
            {//Если это первое продолжение нграммы(уникальное)
                dictionaryOfVariants[wordKey][sentence[index + pos]] = 0;
            }
            dictionaryOfVariants[wordKey][sentence[index + pos]]++; //Если обычный случай. Увеличиваем кол-во повторов
        }
    }
}