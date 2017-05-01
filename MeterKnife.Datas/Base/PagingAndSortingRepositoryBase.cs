using System;
using System.Collections.Generic;
using LiteDB;
using MeterKnife.Interfaces.Datas;

namespace MeterKnife.Datas.Base
{
    public abstract class PagingAndSortingRepositoryBase<T, ID> : CrudRepositoryBase<T, ID>, IPagingAndSortingRepository<T, ID>
    {
        protected PagingAndSortingRepositoryBase(string repositoryPath)
            : base(repositoryPath)
        {
        }

        #region Implementation of IPagingAndSortingRepository<T,ID>

        /// <summary>
        /// Returns all entities sorted by the given options.
        /// </summary>
        public IEnumerable<T> FindAll(IComparer<T> comparer)
        {
            var all = FindAll();
            var list = new List<T>(all);
            list.Sort(comparer);
            return list;
        }
        
        /// <summary>
        /// Returns a {@link Page} of entities meeting the paging restriction provided in the {@code Pageable} object.
        /// </summary>
        public IPage<T> FindAll(IPageable pageable)
        {
            long count = Count;
            //TODO: 就差这个复杂的页查询
            ICollection<T> list = null;//FindAll(Query.with(pageable));
            return new Page<T>(list, pageable, (ulong)Count);
        }

        #endregion
    }
}