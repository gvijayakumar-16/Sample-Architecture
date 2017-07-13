using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogging.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Insert the <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">Entity to be inserted</param>
        /// <param name="saveChanges">Persist to DB</param>
        void Insert(TEntity entity, bool saveChanges = true);

        /// <summary>
        /// Insert the <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">Added entities</param>
        /// <param name="saveChanges">Persist to DB</param>
        void Insert(IEnumerable<TEntity> entities, bool saveChanges = true);

        /// <summary>
        /// Update the <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">Element to be updated</param>
        /// <param name="saveChanges">Persist to DB</param>
        void Update(TEntity entity, bool saveChanges = true);

        /// <summary>
        /// Update the elements in <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">Elements to be updated</param>
        /// <param name="saveChanges">Persist to DB</param>
        void Update(IEnumerable<TEntity> entities, bool saveChanges = true);

        /// <summary>
        /// Delete the <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">Element to be deleted</param>
        /// <param name="saveChanges">Persist to DB</param>
        void Delete(TEntity entity, bool saveChanges = true);

        /// <summary>
        /// Delete an elements passed in <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">Elements to be deleted</param>
        /// <param name="saveChanges">Persist to DB</param>
        void Delete(IEnumerable<TEntity> entities, bool saveChanges = true);

        /// <summary>
        /// Delete an element by <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the element</param>
        /// <param name="saveChanges">Persist to DB</param>
        void Delete(object id, bool saveChanges = true);

        /// <summary>
        /// Get the element by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        /// <returns></returns>
        TEntity GetById(object id);

        /// <summary>
        /// Get the element by <paramref name="id"/> asychronously
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(object id);

        /// <summary>
        /// Save changes to DB
        /// </summary>
        /// <param name="saveChanges">(Optional) Pass false if changes are not needed to be persisted to DB</param>
        void SaveChanges(bool saveChanges = true);

        /// <summary>
        /// Undo the changes
        /// </summary>
        void UndoChanges();

        /// <summary>
        /// Get all the elements
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetAll();

        /// <summary>
        /// Get all the elements asynchronously
        /// </summary>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync();

        /// <summary>
        /// Gets all element satisfying the <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">Condition to be satisfied</param>
        /// <returns></returns>
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets all element satisfying the <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Check if any element exist with the given <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">Condition to be satisfied</param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets the entities with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IList<TEntity> GetAllNoTracking();

        /// <summary>
        /// Gets the entities with "no tracking" enabled (EF feature) asynchronously. Use it only when you load record(s) only for read-only operations
        /// </summary>
        Task<IList<TEntity>> GetAllNoTrackingAsync();

        /// <summary>
        /// Gets the entities with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IList<TEntity> GetAllNoTracking(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets the entities with "no tracking" enabled (EF feature) asynchronously. Use it only when you load record(s) only for read-only operations
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);
    }
}