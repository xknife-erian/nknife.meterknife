using System.Collections.Generic;

namespace MeterKnife.Interfaces.Datas
{
    /// <summary>
    ///     A page is a sublist of a list of objects. It allows gain information about the position of it in the containing
    ///     entire list.
    /// </summary>
    public interface IPage<T>
    {
        /// <summary>
        ///     Returns the number of the current {@link Slice}. Is always non-negative.
        /// </summary>
        uint Number { get; }

        /// <summary>
        ///     Returns the size of the {@link Slice}.
        /// </summary>
        uint Size { get; }

        /// <summary>
        ///     Returns the number of elements currently on this {@link Slice}.
        /// </summary>
        uint NumberOfElements { get; }

        /// <summary>
        ///     Returns the page content as {@link List}.
        /// </summary>
        ICollection<T> Content { get; }

        /// <summary>
        ///     Returns whether the {@link Slice} has content at all.
        /// </summary>
        bool HasContent { get; }

        /// <summary>
        ///     Returns whether the current {@link Slice} is the first one.
        /// </summary>
        bool IsFirst { get; }

        /// <summary>
        ///     Returns whether the current {@link Slice} is the last one.
        /// </summary>
        bool IsLast { get; }

        /// <summary>
        ///     Returns if there is a next {@link Slice}.
        /// </summary>
        bool HasNext { get; }

        /// <summary>
        ///     Returns if there is a previous {@link Slice}.
        /// </summary>
        bool HasPrevious { get; }

        /// <summary>
        ///     Returns the {@link Pageable} to request the next {@link Slice}. Can be {@literal null} in case the current {@link
        ///     Slice} is already the last one. Clients should check {@link #hasNext()} before calling this method to make sure
        ///     they receive a non-{@literal null} value.
        /// </summary>
        IPageable NextPageable { get; }

        /// <summary>
        ///     Returns the {@link Pageable} to request the previous {@link Slice}. Can be {@literal null} in case the current
        ///     {@link Slice} is already the first one. Clients should check {@link #hasPrevious()} before calling this method make
        ///     sure receive a non-{@literal null} value.
        /// </summary>
        IPageable PreviousPageable { get; }

        /// <summary>
        ///     Returns the sorting parameters for the {@link Slice}.
        /// </summary>
        IComparer<T> GetComparer();

        uint TotalPages { get; }

        ulong TotalElements { get; }
    }
}