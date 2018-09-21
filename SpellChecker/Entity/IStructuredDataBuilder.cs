namespace SpellChecker.Entity
{
    public interface IStructuredDataBuilder
    {
        /// <summary>
        /// Read all data to the delimiter from Input
        /// </summary>
        void ReadData(string endData);

        /// <summary>
        /// Set string-data
        /// </summary>
        void SetData(string[] data);

        /// <summary>
        /// Structure all data
        /// </summary>
        void StructureData();

        /// <summary>
        /// Get builder result
        /// </summary>
        object GetResult();
    }
}
