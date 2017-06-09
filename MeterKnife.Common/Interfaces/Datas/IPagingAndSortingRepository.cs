using System.Collections.Generic;

namespace MeterKnife.Interfaces.Datas
{
    /// <summary>
    /// Extension of {@link CrudRepository} to provide additional methods to retrieve entities using the pagination and sorting abstraction.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    public interface IPagingAndSortingRepository<T, ID> : ICrudRepository<T, ID>
    {

        /// <summary>
        /// Returns all entities sorted by the given options.
        /// </summary>
        IEnumerable<T> FindAll(IComparer<T> comparer);

        /// <summary>
        /// Returns a {@link Page} of entities meeting the paging restriction provided in the {@code Pageable} object.
        /// </summary>
        IPage<T> FindAll(IPageable pageable);
    }
}