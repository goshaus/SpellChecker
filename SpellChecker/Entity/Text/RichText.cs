using System.Collections.Generic;


namespace SpellChecker.Entity.Text
{
    public class RichText : IText
    {
        private List<int[]> whitespaces;
        private List<Word[]> words;


        public int LinesCount 
        {
            get
            {
                return words.Count;
            }
        }


        public RichText()
        {
            this.words = new List<Word[]>();
            this.whitespaces = new List<int[]>();
        }


        public void AddNewLine(Word[] words, int[] whitespaces)
        {
            this.words.Add(words);
            this.whitespaces.Add(whitespaces);
        }


        public Word[] GetLine(int index, out int[] whitespaces)
        {
            whitespaces = this.whitespaces[index];
            return this.words[index];
        }
    }
}
