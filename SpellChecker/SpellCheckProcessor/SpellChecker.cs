using SpellChecker.Entity.WordDictionary;
using SpellChecker.Entity.Text;
using SpellChecker.Entity;
using SpellChecker.IOHandle;


namespace SpellChecker.SpellCheckProcessor
{
    public class SpellChecker
    {
        private IStructuredDataBuilder dictionaryBuilder;
        private IStructuredDataBuilder textBuilder;
        private ITextWriter writer;
        private INeighborsFormater formater;
        private string dataSplitter;


        public SpellChecker(IStructuredDataBuilder dictionaryBuilder, IStructuredDataBuilder textBuilder, string dataSplitter)
        {
            this.dictionaryBuilder = dictionaryBuilder;
            this.textBuilder = textBuilder;
            this.dataSplitter = dataSplitter;
        }

        /// <summary>
        /// Input dictionary and text matching with result writting to Output in accordance with the formatting
        /// </summary>
        public void Check(ITextWriter writer, INeighborsFormater formater, int maxDistance)
        {
            this.writer = writer;
            this.formater = formater;
            CreateComponent(this.dictionaryBuilder);
            CreateComponent(this.textBuilder);

            IText text = (IText)this.textBuilder.GetResult();
            IWordDictionary dictionary = (IWordDictionary)this.dictionaryBuilder.GetResult();

            for (int i = 0; i != text.LinesCount; i++)
            {
                var textLine = text.GetLine(i, out int[] whitespaces);
                for(int j = 0; j != textLine.Length; j++)
                {
                    Word word = textLine[j];
                    var neighbors = dictionary.FindNearestСoincidence(word, maxDistance);
                    var formatedNeighbors = this.formater.FormatNeighbors(word, neighbors);
                    Word editedWord = new Word(formatedNeighbors);
                    textLine[j] = editedWord;
                }

                this.writer.WriteLine(textLine, whitespaces);
            }
        }

        /// <summary>
        /// IStructuredDataBuilder management
        /// </summary>
        private void CreateComponent(IStructuredDataBuilder builder)
        {
            builder.ReadData(this.dataSplitter);
            builder.StructureData();
        }
    }
}
