using SpellChecker.Entity;


namespace SpellChecker.SpellCheckProcessor
{
    public class WordNeighborsFormater : INeighborsFormater
    {
        public string FormatNeighbors(Word word, Word[] neighbors)
        {
            string result;
            if (neighbors.Length == 0)
            {
                result = FormatNoNeighborsWord(word);
            }
            else if (neighbors.Length == 1)
            {
                result = FormatOneNeighborWord(neighbors[0]);
            }
            else
            {
                result = FormatNeighborsWord(neighbors);
            }

            return result;
        }


        public string FormatNoNeighborsWord(Word word)
        {
            string result = "{" + word.Data + "?}";

            return result;
        }


        public string FormatOneNeighborWord(Word neighbor)
        {
            return neighbor.Data;
        }


        public string FormatNeighborsWord(Word[] neighbors)
        {
            string result = "{";
            int neighborsCount = neighbors.Length;

            for(int i = 0; i != neighborsCount - 1; i++)
            {
                result += neighbors[i].Data + " ";
            }

            result += neighbors[neighborsCount - 1].Data + "}";

            return result;
        }
    }
}
