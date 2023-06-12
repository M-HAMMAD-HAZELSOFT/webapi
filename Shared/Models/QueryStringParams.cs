namespace webapi.Shared.Models
{
    /// <summary>
    /// The paging request params. 
    /// These will be used for Get Requests to return paged results.
    /// </summary>
    public class QueryStringParams
    {
        public QueryStringParams()
        {
            OrderBy = "Id desc";
        }
        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        public string Search { get; set; }
    }
}
