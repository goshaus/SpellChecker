using System;
using System.IO;
using SpellChecker.Entity;


namespace SpellChecker.IOHandle
{
    class RichTextWriter : ITextWriter
    {
        private StreamWriter writer;


        public RichTextWriter(Stream outStream)
        {
            this.writer = new StreamWriter(outStream);
        }


        public void Flush()
        {
            this.writer.Flush();
        }


        public void WriteLine(Word[] words, int[] whitespaces)
        {
            for(int i = 0; i != words.Length; i++)
            {
                var whitespaceCount = whitespaces[i];
                var word = words[i].Data;

                this.writer.Write(word.PadLeft(word.Length + whitespaceCount));
            }

            this.writer.Write(Environment.NewLine);
        }


        public void WriteLine(string data)
        {
            this.writer.Write(data);
            this.writer.Write(Environment.NewLine);
        }
    }
}
