using SpellChecker.Entity;


namespace SpellChecker.IOHandle
{
    public interface ITextWriter
    {
        /// <summary>
        /// Write all string to Output
        /// </summary>
        void WriteLine(Word[] words, int[] whitespaces);

        /// <summary>
        /// Write line with preceding whitespaces
        /// </summary>
        void WriteLine(string data);

        /// <summary>
        /// Clear all buffers for the Stream and causes any buffered data to be written to the underlying device
        /// </summary>
        void Flush();
    }
}
