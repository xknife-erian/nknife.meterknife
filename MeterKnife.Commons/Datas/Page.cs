using System;
using System.Collections.Generic;
using MeterKnife.Interfaces.Datas;

namespace MeterKnife.Datas
{
    /// <summary>
    /// A page is a sublist of a list of objects. It allows gain information about the position of it in the containing entire list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T> : IPage<T>
    {
        protected readonly IPageable _Pageable;
        protected readonly ulong _Total;

        /// <summary>Constructor</summary>
        /// <param name="content">the content of this page, must not be null.</param>
        /// <param name="pageable">the paging information, can be null.</param>
        /// <param name="total">
        ///     the total amount of items available. The total might be adapted considering the length of the
        ///     content given, if it is going to be the content of the last page.This is in place to mitigate inconsistencies
        /// </param>
        public Page(ICollection<T> content, IPageable pageable, ulong total)
        {
            Content = content;
            _Pageable = pageable;
            _Total = !(content.Count <= 0) && pageable != null && pageable.Offset + pageable.PageSize > total
                ? (ulong) (pageable.Offset + content.Count)
                : total;
        }

        #region Implementation of IPage<T>

        /// <summary>
        ///     Returns the number of the current <see cref="IPage{T}" />. Is always non-negative.
        /// </summary>
        public uint Number => _Pageable?.PageNumber ?? 0;

        /// <summary>
        ///     Returns the size of the <see cref="IPage{T}" />.
        /// </summary>
        public uint Size => _Pageable?.PageSize ?? 0;

        /// <summary>
        ///     Returns the number of elements currently on this <see cref="IPage{T}" />.
        /// </summary>
        public uint NumberOfElements => (uint) Content.Count;

        /// <summary>
        ///     Returns the page content as {@link List}.
        /// </summary>
        public ICollection<T> Content { get; }

        /// <summary>
        ///     Returns whether the <see cref="IPage{T}" /> has content at all.
        /// </summary>
        public bool HasContent => Content?.Count > 0;

        /// <summary>
        ///     Returns whether the current <see cref="IPage{T}" /> is the first one.
        /// </summary>
        public bool IsFirst => !HasPrevious;

        /// <summary>
        ///     Returns whether the current <see cref="IPage{T}" /> is the last one.
        /// </summary>
        public bool IsLast => !HasNext;

        /// <summary>
        ///     Returns if there is a next <see cref="IPage{T}" />.
        /// </summary>
        public bool HasNext => Number + 1 < TotalPages;

        /// <summary>
        ///     Returns if there is a previous <see cref="IPage{T}" />.
        /// </summary>
        public bool HasPrevious => Number > 0;

        /// <summary>
        ///     Returns the <see cref="IPageable" /> to request the next <see cref="IPage{T}" />. Can be not null in case the
        ///     current <see cref="IPage{T}" /> is already the last one. Clients should check HasNext before calling this method to
        ///     make sure they receive a not null value.
        /// </summary>
        public IPageable NextPageable => HasNext ? _Pageable.Next() : null;

        /// <summary>
        ///     Returns the <see cref="IPageable" /> to request the previous <see cref="IPage{T}" />. Can be null in case
        ///     the current <see cref="IPage{T}" /> is already the first one. Clients should check HasPrevious before calling this
        ///     method make sure receive a not null value.
        /// </summary>
        public IPageable PreviousPageable
        {
            get
            {
                if (HasPrevious)
                    return _Pageable.PreviousOrFirst();
                return null;
            }
        }

        /// <summary>
        ///     Returns the sorting parameters for the <see cref="IPage{T}" />.
        /// </summary>
        public IComparer<T> GetComparer()
        {
            return (IComparer<T>) _Pageable.GetComparer();
        }

        public uint TotalPages => Size == 0 ? 1 : (uint) Math.Ceiling(_Total / (double) Size);

        public ulong TotalElements => _Total;

        #endregion
    }
}