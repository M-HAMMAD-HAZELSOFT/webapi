using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using webapi.Data;

namespace webapi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // The following variable is going to hold the DBContext instance.
        private readonly DataContext _dataContext;

        // The following Variable is going to hold the DbSet Entity.
        private readonly DbSet<T> _dbSet;

        // Using the Parameterless Constructor, 
        // We are initializing the context object and table variable.
        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();

        }

        // This method will return all the Records from the dbSet.
        public async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> items = await _dbSet.ToListAsync();
            return items;
        }

        // This method will return the specified record from the dbSet.
        public IQueryable<T> GetById(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        // This method will Insert one object into the dbSet.
        // It will receive the object as an argument which needs to be inserted into the database.
        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            Save();
        }

        // This method is going to update the record in the dbSet.
        // It will receive the object as an argument.
        public void Update(T entity)
        {

            if (_dataContext.Entry(entity).State == EntityState.Detached)
            {
                // First attach the object to the dbSet.
                _dbSet.Attach(entity);
            }
            // Then set the state of the Entity as Modified.
            _dataContext.Entry(entity).State = EntityState.Modified;
            Save();
        }

        // This method is going to remove the record from the dbSet.
        // It will receive the primary key value as an argument,
        // Whose information needs to be removed from the dbSet.
        public bool Delete(T entity)
        {
            //This will mark the Entity State as Deleted.
            _dbSet.Remove(entity);
            Save();
            return true;
        }

        //This method will make the changes permanent in the database.
        //That means once we call Insert, Update, and Delete Methods, 
        //Then we need to call the Save method to make the changes permanent in the database.
        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}
