using SpellChecker.IOHandle;
using SpellChecker.Entity.VantagePointTree;


namespace SpellChecker.Entity.WordDictionary
{
    class StaticWordDictionaryBuilder : IStructuredDataBuilder
    {
        private ITextParser parser;
        INodeAnalyzer<Word> analyzer;
        private StaticWordDictionary dictionary;
        private string data;


        public StaticWordDictionaryBuilder(ITextParser parser, INodeAnalyzer<Word> analyzer)
        {
            this.parser = parser;
            this.analyzer = analyzer;
        }


        public void ReadData(string endData)
        {
            var lines = this.parser.ReadLines(endData);
            this.data = string.Join(" ", lines);
        }


        public void SetData(string[] data)
        {
            this.data = string.Join(" ", data);
        }


        public void StructureData()
        {
            var words = parser.FindWords(this.data, out int[] whitespaces);
            VPTree<Word> tree = new VPTree<Word>();
            tree.Create(words, analyzer);
            this.dictionary = new StaticWordDictionary(tree, this.analyzer);
        }


        public object GetResult()
        {
            return this.dictionary;
        }
    }
}
