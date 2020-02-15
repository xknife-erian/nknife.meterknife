using System.Collections.Generic;
using System.Linq;
using NKnife.Interface;

namespace NKnife.MeterKnife.Common.Scpi
{
    public class ScpiCommandPool : List<ScpiCommand>, IJobPool
    {
        public PoolCategory Category { get; set; }

        #region Implementation of IEnumerable<out IJobPoolItem>

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator<IJobPoolItem> IEnumerable<IJobPoolItem>.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<IJobPoolItem>

        /// <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1"></see> is
        ///     read-only.
        /// </exception>
        void ICollection<IJobPoolItem>.Add(IJobPoolItem item)
        {
            Add(item as CareCommand);
        }

        /// <summary>
        ///     Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific
        ///     value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        ///     true if <paramref name="item">item</paramref> is found in the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        /// </returns>
        bool ICollection<IJobPoolItem>.Contains(IJobPoolItem item)
        {
            return this.Contains(item);
        }

        /// <summary>
        ///     Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an
        ///     <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements
        ///     copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see>
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array">array</paramref> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="arrayIndex">arrayIndex</paramref> is less than
        ///     0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The number of elements in the source
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see> is greater than the available space from
        ///     <paramref name="arrayIndex">arrayIndex</paramref> to the end of the destination
        ///     <paramref name="array">array</paramref>.
        /// </exception>
        void ICollection<IJobPoolItem>.CopyTo(IJobPoolItem[] array, int arrayIndex)
        {
            ((ICollection<ScpiCommand>) this).CopyTo(array.Cast<ScpiCommand>().ToArray(), arrayIndex);
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        ///     true if <paramref name="item">item</paramref> was successfully removed from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if
        ///     <paramref name="item">item</paramref> is not found in the original
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1"></see> is
        ///     read-only.
        /// </exception>
        bool ICollection<IJobPoolItem>.Remove(IJobPoolItem item)
        {
            return Remove(item as ScpiCommand);
        }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is
        ///     read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.</returns>
        bool ICollection<IJobPoolItem>.IsReadOnly => ((ICollection<ScpiCommand>) this).IsReadOnly;

        #endregion

        #region Implementation of IJobPoolItem

        public bool IsPool { get; } = true;

        void IJobPool.AddRange(IEnumerable<IJobPoolItem> jobs)
        {
            AddRange(jobs.Cast<ScpiCommand>());
        }

        public bool IsOverall { get; set; } = true;

        #endregion
    }
}