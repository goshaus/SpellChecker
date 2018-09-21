using System.IO;
using SpellChecker.IOHandle;


namespace SpellChecker.Entity.Text
{
    class RichTextBuilder : IStructuredDataBuilder
    {
        private ITextParser parser;
        public RichText text;
        private string[] data;


        public RichTextBuilder(ITextParser parser)
        {
            this.parser = parser;
            this.text = new RichText();
        }


        public void ReadData(string endData)
        {
            this.data = this.parser.ReadLines(endData);
        }


        public void SetData(string[] data)
        {
            this.data = data;
        }


        public void StructureData()
        {
            foreach(var line in this.data)
            {
                var words = parser.FindWords(line, out int[] whitespaces);
                this.text.AddNewLine(words, whitespaces);
            }
        }


        public object GetResult()
        {
            return this.text;
        }
    }
}
