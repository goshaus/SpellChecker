using SpellChecker.Entity;
using System.IO;
using System.Collections.Generic;


namespace SpellChecker.IOHandle
{
    public class WordParser : ITextParser
    {
        private StreamReader reader;


        public WordParser(Stream inStream)
        {
            this.reader = new StreamReader(inStream);
        }

        
        public string[] ReadLines(string delimiter)
        {
            List<string> lines = new List<string>();
            var line = "";

            while (line != null)
            {
                line = reader.ReadLine();
                if (line == delimiter)
                {
                    break;
                }

                lines.Add(line);
            }

            return lines.ToArray();
        }

        
        public Word[] FindWords(string textLine, out int[] whitespaces)
        {
            List<Word> words = new List<Word>();
            List<int> tmpWhitespaces = new List<int>();
            string tmpWord = "";
            int whitespaceCount = 0;

            foreach(var character in textLine)
            {
                if (character != ' ')
                {
                    tmpWord += character;
                }
                else
                {
                    if (tmpWord.Length != 0)
                    {
                        var word = new Word(tmpWord);
                        tmpWhitespaces.Add(whitespaceCount);
                        words.Add(word);
                        tmpWord = "";
                        whitespaceCount = 0;
                    }
                    if (character == ' ')
                    {
                        whitespaceCount++;
                    }
                }
            }

            if (tmpWord.Length != 0)
            {
                var word = new Word(tmpWord);
                words.Add(word);
                tmpWhitespaces.Add(whitespaceCount);
            }

            whitespaces = tmpWhitespaces.ToArray();

            return words.ToArray();
        }
    }
}
