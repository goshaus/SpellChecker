namespace SpellChecker.Entity.WordDictionary
{
    public interface IWordDictionary
    {
        /// <summary>
        /// Return the nearest word neighbors
        /// </summary>
        Word[] FindNearestСoincidence(Word target, int distance);
    }
}
