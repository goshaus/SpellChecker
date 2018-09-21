namespace SpellChecker.Entity.VantagePointTree
{
    public interface INodeAnalyzer<T>
        where T : class
    {
        int MaxDistance { get; }
        int CalculateDistance(T firstObject, T secondObject);
        int SearchComparator(T firstObject, T secondObject);
        int[] GetSearchResult();
    }
}
