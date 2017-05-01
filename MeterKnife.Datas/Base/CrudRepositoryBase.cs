using System.Collections.Generic;
using System.Linq;
using LiteDB;
using MeterKnife.Interfaces.Datas;

namespace MeterKnife.Datas.Base
{
    public abstract class CrudRepositoryBase<T, ID> : RepositoryBase<T>, ICrudRepository<T, ID>
    {
        protected CrudRepositoryBase(string repositoryPath)
            : base(repositoryPath)
        {
        }

        #region Implementation of ICrudRepository<T,ID>

        /// <summary>
        ///     Saves a given entity. Use the returned instance for further operations as the save operation might have changed the
        ///     entity instance completely.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return the saved entity</returns>
        public T Save(T entity)
        {
            {
                var i = _Collection.Insert(entity);
                if (i == 1)
                    return entity;
                return default(T);
            }
        }

        /// <summary>
        ///     Saves all given entities.
        /// </summary>
        /// <returns>return the saved entities</returns>
        public IEnumerable<T> Save(IEnumerable<T> entities)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            foreach (var entity in enumerable)
            {
                _Collection.Insert(entity);
            }
            return enumerable;
        }

        /// <summary>
        ///     Retrieves an entity by its id.
        /// </summary>
        public T FindOne(ID id)
        {
            return _Collection.FindById(new BsonValue(id));
        }

        /// <summary>
        ///     Returns whether an entity with the given id exists.
        /// </summary>
        public bool Exists(ID id)
        {
            return _Collection.Exists(Query.EQ("_id", new BsonValue(id)));
        }

        /// <summary>
        ///     Returns all instances of the type.
        /// </summary>
        public IEnumerable<T> FindAll()
        {
            return _Collection.FindAll();
        }

        /// <summary>
        ///     Returns all instances of the type with the given IDs.
        /// </summary>
        public IEnumerable<T> FindAll(IEnumerable<ID> ids)
        {
            var enumerable = ids as ID[] ?? ids.ToArray();
            var list = new List<T>(enumerable.Count());
            list.AddRange(enumerable.Select(id => _Collection.FindById(new BsonValue(id))));
            return list;
        }

        /// <summary>
        ///     Returns the number of entities available.
        /// </summary>
        public long Count => _Collection.Count();

        /// <summary>
        ///     Deletes the entity with the given id.
        /// </summary>
        /// <param name="id">id must not be null</param>
        public void Delete(ID id)
        {
            _Collection.Delete(new BsonValue(id));
        }

        /// <summary>
        ///     Deletes a given entity.
        /// </summary>
        public void Delete(T entity)
        {
            _Collection.Delete(new BsonValue(entity));
        }

        /// <summary>
        ///     deletes the given entities.
        /// </summary>
        /// <param name="entities">entities</param>
        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        /// <summary>
        ///     Deletes all entities managed by the repository.
        /// </summary>
        public void DeleteAll()
        {
            _Database.DropCollection(nameof(T));
        }

        #endregion
    }
}