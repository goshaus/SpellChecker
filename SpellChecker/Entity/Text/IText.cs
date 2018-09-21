namespace SpellChecker.Entity.Text
{
    public interface IText
    {
        /// <summary>
        /// Return text lines count
        /// </summary>
        int LinesCount { get; }

        /// <summary>
        /// Add new text line
        /// </summary>
        void AddNewLine(Word[] words, int[] whitespaces);

        /// <summary>
        /// Get text line
        /// </summary>
        Word[] GetLine(int index, out int[] whitespaces);
    }
}
