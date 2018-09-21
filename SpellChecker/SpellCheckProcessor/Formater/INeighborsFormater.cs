using SpellChecker.Entity;


namespace SpellChecker.SpellCheckProcessor
{
    public interface INeighborsFormater
    {
        /// <summary>
        /// Format word neighbors by a given rule
        /// </summary>
        string FormatNeighbors(Word word, Word[] neighbors);
        
        /// <summary>
        /// Format word without neighbors
        /// </summary>
        string FormatNoNeighborsWord(Word word);

        /// <summary>
        /// Format word with last neighbor
        /// </summary>
        string FormatOneNeighborWord(Word neighbor);

        /// <summary>
        /// Format word with neighbors
        /// </summary>
        string FormatNeighborsWord(Word[] neighbors);
    }
}
