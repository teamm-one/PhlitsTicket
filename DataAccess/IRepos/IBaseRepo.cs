using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepos
{
    public interface IBaseRepo<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities with optional filtering, including related entities, or applying additional query logic.
        /// </summary>
        /// <param name="includes">Array of navigation properties to include in the query.</param>
        /// <param name="expression">Filter expression to apply.</param>
        /// <param name="additionalIncludes">Additional query logic to apply.</param>
        /// <returns>IEnumerable of the requested entities.</returns>
        IEnumerable<T> GetAll(
            Expression<Func<T, object>>[]? includes = null,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IQueryable<T>>? additionalIncludes = null);

        /// <summary>
        /// Retrieves a single entity matching a filter, optionally including related entities.
        /// </summary>
        /// <param name="expression">Filter expression to apply.</param>
        /// <param name="includes">Array of navigation properties to include in the query.</param>
        /// <param name="additionalIncludes">Additional query logic to apply.</param>
        /// <returns>The first entity matching the filter, or null if no match is found.</returns>
        T? GetOne(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, object>>[]? includes = null,
            Func<IQueryable<T>, IQueryable<T>>? additionalIncludes = null);

        /// <summary>
        /// Creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        void Create(T entity);

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Edit(T entity);

        /// <summary>
        /// Deletes an entity by ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>True if the entity was found and deleted, otherwise false.</returns>
        bool Delete(int id);

        /// <summary>
        /// Commits changes made to the database.
        /// </summary>
        public void commit();
    }
}
