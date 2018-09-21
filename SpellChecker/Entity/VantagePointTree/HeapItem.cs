namespace SpellChecker.Entity.VantagePointTree
{
    public class HeapItem
    {
        public int index;
        public int distance;


        public HeapItem(int index, int distance)
        {
            this.index = index;
            this.distance = distance;
        }


        public static bool operator <(HeapItem h1, HeapItem h2)
        {
            return h1.distance < h2.distance;
        }


        public static bool operator >(HeapItem h1, HeapItem h2)
        {
            return h1.distance > h2.distance;
        }
    }
}
