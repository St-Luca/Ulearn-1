using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        public char[] CharsToSplit = { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
        public Dictionary<string, Dictionary<int, List<int>>>
        Words = new Dictionary<string, Dictionary<int, List<int>>>();
        //Словарь внутри словаря, где внешний ключ - слово, 
        //а значение - внутренний словарь с айди документа как ключ и списком позиций слова в документе

        public void Add(int id, string documentText)
        {
            int index = 0;
            string[] text = documentText.Split(CharsToSplit);

            foreach (string word in text)
            {
                if (!Words.ContainsKey(word))   //Если уникальное слово
                {
                    Words.Add(word, new Dictionary<int, List<int>>());
                }
                if (!Words[word].ContainsKey(id))   //Если уникальное айди
                {
                    Words[word].Add(id, new List<int>());
                }

                Words[word][id].Add(index);
                index = index + word.Length + 1; //С пробелом
            }
        }

        public List<int> GetIds(string word)	//Ключ внутреннего словаря
        {
            if (Words.ContainsKey(word))
            {
                return Words[word].Keys.ToList();
            }
            return new List<int>();
        }

        public List<int> GetPositions(int id, string word)	//Значение внутреннего словаря
        {
            if (Words.ContainsKey(word))
            {
                if (Words[word].ContainsKey(id))
                {
                    return Words[word][id];
                }
            }
            return new List<int>();
        }

        public void Remove(int id)//Удалить по ключу внутреннего словаря
        {
            foreach (string key in Words.Keys)
            {
                Words[key].Remove(id);
            }
        }
    }
}