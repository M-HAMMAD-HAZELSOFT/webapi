namespace webapi.Repositories
{
    /// <summary>
    /// The generic query result class.
    /// </summary>
    public class QueryResult<T>
    {
        /// <summary>
        /// Gets or sets the count of rows in query result.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the list of generic entity.
        /// </summary>
        public List<T> List { get; set; }
    }
}
