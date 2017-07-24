using System.Collections;
using MeterKnife.Interfaces.Datas;

namespace MeterKnife.Datas
{
    public class Pageable : IPageable
    {
        protected readonly IComparer _Comparer;

        protected Pageable(uint number, uint size, IComparer comparer)
        {
            PageNumber = number;
            PageSize = size;
            _Comparer = comparer;
        }

        #region Implementation of IPageable

        /// <summary>
        ///     Returns the page to be returned.
        /// </summary>
        public uint PageNumber { get; protected set; }

        /// <summary>
        ///     Returns the number of items to be returned.
        /// </summary>
        public uint PageSize { get; protected set; }

        /// <summary>
        ///     Returns the offset to be taken according to the underlying page and page size.
        /// </summary>
        public uint Offset => PageNumber * PageSize;

        /// <summary>
        ///     Returns the sorting parameters.
        /// </summary>
        public IComparer GetComparer()
        {
            return _Comparer;
        }

        /// <summary>
        ///     Returns the <see cref="IPageable" /> requesting the first page.
        /// </summary>
        public IPageable First()
        {
            return new Pageable(0, PageSize, _Comparer);
        }

        /// <summary>
        ///     Returns the <see cref="IPageable" /> requesting the next <see cref="IPage{T}" />.
        /// </summary>
        public IPageable Next()
        {
            return new Pageable(PageNumber + 1, PageSize, _Comparer);
        }

        /// <summary>
        ///     Returns the <see cref="IPageable" /> requesting the previous <see cref="IPage{T}" />.
        /// </summary>
        public IPageable Previous()
        {
            return PageNumber == 0 ? this : new Pageable(PageNumber + 1, PageSize, _Comparer);
        }

        /// <summary>
        ///     Returns the previous <see cref="IPageable" /> or the first <see cref="IPageable" /> if the current one already is
        ///     the first one.
        /// </summary>
        public IPageable PreviousOrFirst()
        {
            return HasPrevious() ? Previous() : First();
        }

        /// <summary>
        ///     Returns whether there's a previous <see cref="IPageable" /> we can access from the current one. Will return
        ///     false in case the current <see cref="IPageable" /> already refers to the first page.
        /// </summary>
        public bool HasPrevious()
        {
            return PageNumber > 0;
        }

        #endregion
    }
}