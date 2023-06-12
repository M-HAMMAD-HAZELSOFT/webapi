using System.Linq.Expressions;

namespace webapi.Repositories
{
    //Here, we are creating the IGenericRepository interface as a Generic Interface
    //Here, we are applying the Generic Constraint 
    //The constraint is, T is going to be a class
    public interface IGenericRepository<T>
    {
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
