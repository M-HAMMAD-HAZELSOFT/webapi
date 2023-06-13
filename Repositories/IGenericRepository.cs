using System.Linq.Expressions;

namespace webapi.Repositories
{
    //Here, we are creating the IGenericRepository interface as a Generic Interface
    //Here, we are applying the Generic Constraint 
    //The constraint is, T is going to be a class
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Gets the records of the entity.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns>A list of entites.</returns>
        QueryResult<T> GetPaginatedByQuery(
            string search = null,
            string orderBy = "Id desc",
            int pageSize = 10,
            int currentPage = 1);

        /// <summary>
        /// Gets the list of all entities.
        /// </summary>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Gets the entity w.r.t. expression.
        /// </summary>
        /// <param name="expression">The expression to be applied.</param>
        IQueryable<T> GetById(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Inserts the entity.
        /// </summary>
        /// <param name="entity">The entity to be inserted.</param>
        void Insert(T entity);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        bool Delete(T entity);

        /// <summary>
        /// Saves the changes in database.
        /// </summary>
        void Save();
    }
}
