using Blogging.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogging.Data.Repository
{
    public class EFBaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly BloggingContext _dbContext;
        private DbSet<TEntity> _entities;

        public EFBaseRepository(BloggingContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Entities
        /// </summary>
        private DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null) _entities = _dbContext.Set<TEntity>();
                return _entities;
            }
        }

        /// <summary>
        /// Get the error text in a readable format
        /// </summary>
        /// <param name="exc"></param>
        /// <returns></returns>
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
        }

        /// <summary>
        /// Get the element by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        /// <returns></returns>
        public virtual TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        /// <summary>
        /// Get the element by <paramref name="id"/> asychronously
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// Insert the <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">Entity to be inserted</param>
        /// <param name="saveChanges">Persist to DB</param>
        public virtual void Insert(TEntity entity, bool saveChanges = true)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Add(entity);
                SaveChanges(saveChanges);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Insert the <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">Added entities</param>
        /// <param name="saveChanges">Persist to DB</param>
        public virtual void Insert(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                Entities.AddRange(entities);
                SaveChanges(saveChanges);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Update the <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">Element to be updated</param>
        /// <param name="saveChanges">Persist to DB</param>
        public virtual void Update(TEntity entity, bool saveChanges = true)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                SaveChanges(saveChanges);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Update the elements in <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">Elements to be updated</param>
        /// <param name="saveChanges">Persist to DB</param>
        public virtual void Update(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                SaveChanges(saveChanges);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Delete the <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">Element to be deleted</param>
        /// <param name="saveChanges">Persist to DB</param>
        public virtual void Delete(TEntity entity, bool saveChanges = true)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Remove(entity);
                SaveChanges(saveChanges);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Delete an elements passed in <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">Elements to be deleted</param>
        /// <param name="saveChanges">Persist to DB</param>
        public virtual void Delete(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    this.Entities.Remove(entity);

                SaveChanges(saveChanges);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Delete an element by <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the element</param>
        /// <param name="saveChanges">Persist to DB</param>
        public virtual void Delete(object id, bool saveChanges = true)
        {
            TEntity entity = GetById(id);
            Delete(entity, saveChanges);
        }

        /// <summary>
        /// Save changes to DB
        /// </summary>
        /// <param name="saveChanges">(Optional) Pass false if changes are not needed to be persisted to DB</param>
        public virtual void SaveChanges(bool saveChanges = true)
        {
            if (saveChanges) _dbContext.SaveChanges();
        }

        /// <summary>
        /// Undo the changes
        /// </summary>
        public virtual void UndoChanges()
        {
            var changedEntries = _dbContext.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.State = EntityState.Detached;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Unchanged;
                }
            }
        }

        /// <summary>
        /// Get all the elements
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Get all the elements asynchronously
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Gets all element satisfying the <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        /// <summary>
        /// Gets all element satisfying the <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns></returns>
        public virtual async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Check if any element exist with the given <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Any(predicate);
        }

        /// <summary>
        /// Gets the entities with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IList<TEntity> GetAllNoTracking()
        {
            return Entities.AsNoTracking().ToList();
        }

        /// <summary>
        /// Gets the entities with "no tracking" enabled (EF feature) asynchronously. Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual async Task<IList<TEntity>> GetAllNoTrackingAsync()
        {
            return await Entities.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Gets the entities with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IList<TEntity> GetAllNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.AsNoTracking().Where(predicate).ToList();
        }

        /// <summary>
        /// Gets the entities with "no tracking" enabled (EF feature) asynchronously. Use it only when you load record(s) only for read-only operations
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IList<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.AsNoTracking().Where(predicate).ToListAsync();
        }

    }
}
