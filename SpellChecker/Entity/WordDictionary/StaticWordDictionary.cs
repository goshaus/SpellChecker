using System.Collections.Generic;
using System.Linq;
using SpellChecker.Entity.VantagePointTree;


namespace SpellChecker.Entity.WordDictionary
{
    public class StaticWordDictionary : IWordDictionary
    {
        private VPTree<Word> wordTree;
        private INodeAnalyzer<Word> analyzer;


        public StaticWordDictionary(VPTree<Word> tree, INodeAnalyzer<Word> analyzer)
        {
            this.wordTree = tree;
            this.analyzer = analyzer;
        }

        /// <summary>
        /// Return the nearest word neighbors with minimal distance
        /// </summary>
        public Word[] FindNearestСoincidence(Word target, int distance)
        {
            List<Word> result = new List<Word>();
            this.wordTree.Search(target, out Word[] words);
            List<int> distances = this.analyzer.GetSearchResult().ToList<int>();
            int minDistance = int.MaxValue;

            for(int i = 0; i != words.Length; i++)
            {
                if (distances[i] >= 0 && distances[i] <= minDistance)
                {
                    if (distances[i] < minDistance)
                    {
                        minDistance = distances[i];
                        result.Clear();
                    }
                    result.Add(words[i]);
                }
            }

            return result.ToArray();
        }
    }
}
