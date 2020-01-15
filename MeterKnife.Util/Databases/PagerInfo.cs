namespace NKnife.Databases
{
    /// <summary>分页参数
    /// </summary>
    public struct PagerInfo
    {
        /// <summary>当前页码
        /// </summary>
        public uint CurrentPage { get; set; }

        /// <summary>每页数据数量
        /// </summary>
        public uint Count { get; set; }

        public static PagerInfo Empty
        {
            get
            {
                var pi = new PagerInfo {CurrentPage = 0, Count = 0};
                return pi;
            }
        }

        public bool Equals(PagerInfo other)
        {
            return CurrentPage == other.CurrentPage && Count == other.Count;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) CurrentPage*397) ^ (int) Count;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PagerInfo && Equals((PagerInfo) obj);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(PagerInfo left, PagerInfo right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(PagerInfo left, PagerInfo right)
        {
            return !Equals(left, right);
        }
    }
}