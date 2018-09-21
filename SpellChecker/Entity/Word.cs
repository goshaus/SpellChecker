namespace SpellChecker.Entity
{
    public class Word
    {
        /// <summary>
        /// Return word string-data
        /// </summary>
        public string Data { get; private set; }


        public Word()
        {
            this.Data = "";
        }

        /// <summary>
        /// Set word string-data
        /// </summary>
        public Word(string data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Get word length
        /// </summary>
        public int Length
        {
            get
            {
                return Data.Length;
            }
        }

        /// <summary>
        /// Indicates whether the specified string is null or an Empty word.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(this.Data);
            }
        }


        public char this[int index]
        {
            get
            {
                return this.Data[index];
            }
            set
            {
                var tmp = this.Data.ToCharArray();
                tmp[index] = value;
                this.Data = tmp.ToString();
            }
        }
    }
}
