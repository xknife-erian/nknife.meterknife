using System.Collections;

namespace MeterKnife.Interfaces.Datas
{
    /// <summary>
    ///     Abstract interface for pagination information.
    /// </summary>
    public interface IPageable
    {
        /// <summary>
        ///     Returns the page to be returned.
        /// </summary>
        uint PageNumber { get; }

        /// <summary>
        ///     Returns the number of items to be returned.
        /// </summary>
        uint PageSize { get; }


        /// <summary>
        ///     Returns the offset to be taken according to the underlying page and page size.
        /// </summary>
        uint Offset { get; }

        /// <summary>
        ///     Returns the sorting parameters.
        /// </summary>
        IComparer GetComparer();

        /// <summary>
        ///     Returns the {@link Pageable} requesting the first page.
        /// </summary>
        IPageable First();

        /// <summary>
        ///     Returns the {@link Pageable} requesting the next {@link Page}.
        /// </summary>
        IPageable Next();

        /// <summary>
        /// Returns the {@link Pageable} requesting the previous {@link Page}.
        /// </summary>
        IPageable Previous();

        /// <summary>
        ///     Returns the previous {@link Pageable} or the first {@link Pageable} if the current one already is the first one.
        /// </summary>
        IPageable PreviousOrFirst();


        /// <summary>
        ///     Returns whether there's a previous {@link Pageable} we can access from the current one. Will return {@literal
        ///     false} in case the current {@link Pageable} already refers to the first page.
        /// </summary>
        bool HasPrevious();
    }
}