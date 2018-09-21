using SpellChecker.Entity;


namespace SpellChecker.IOHandle
{
    public interface ITextParser
    {
        /// <summary>
        /// Return all lines to the delimiter
        /// </summary>
        string[] ReadLines(string endString);

        /// <summary>
        /// Find words with whitespaces before the word
        /// </summary>
        Word[] FindWords(string textLine, out int[] whitespaces);
    }
}
