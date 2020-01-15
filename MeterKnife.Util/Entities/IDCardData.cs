using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NKnife.Entities
{
    /// <summary>中华人民共和国第2代身份证信息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
// ReSharper disable InconsistentNaming
    public struct IDCardData : IEquatable<IDCardData>, ICloneable
// ReSharper restore InconsistentNaming
    {
        /// <summary>
        /// 姓名 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Name;
        /// <summary>
        /// 性别
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Sex; 
        /// <summary>
        /// 民族
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
        public string Nation;
        /// <summary>
        /// 出生日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string Born;
        /// <summary>
        /// 住址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
        public string Address;
        /// <summary>
        /// 身份证号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
        public string IDCardNo;
        /// <summary>
        /// 发证机关
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string GrantDept;
        /// <summary>
        /// 有效开始日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string UserLifeBegin;
        /// <summary>
        /// 有效截止日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string UserLifeEnd;
        /// <summary>
        /// 保留
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
        public string Reserved;
        /// <summary>
        /// 照片路径
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string PhotoFileName;

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Name);
            sb.AppendLine(Sex);
            sb.AppendLine(Nation);
            sb.AppendLine(Born);
            sb.AppendLine(Address);
            sb.AppendLine(IDCardNo);
            sb.AppendLine(GrantDept);
            sb.AppendLine(UserLifeBegin);
            sb.AppendLine(UserLifeEnd);
            sb.AppendLine(Reserved);
            sb.AppendLine(PhotoFileName);
            return sb.ToString();
        }

        public object Clone()
        {
            var data = new IDCardData();
            data.Address = Address;
            data.Born = Born;
            data.GrantDept = GrantDept;
            data.IDCardNo = IDCardNo;
            data.Name = Name;
            data.Nation = Nation;
            data.PhotoFileName = PhotoFileName;
            data.Reserved = Reserved;
            data.Sex = Sex;
            data.UserLifeBegin = UserLifeBegin;
            data.UserLifeEnd = UserLifeEnd;
            return data;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(IDCardData other)
        {
            return Equals(other.Name, Name) 
                && Equals(other.Sex, Sex)
                && Equals(other.Nation, Nation)
                && Equals(other.Born, Born)
                && Equals(other.Address, Address)
                && Equals(other.IDCardNo, IDCardNo)
                && Equals(other.GrantDept, GrantDept)
                && Equals(other.UserLifeBegin, UserLifeBegin)
                && Equals(other.UserLifeEnd, UserLifeEnd)
                && Equals(other.Reserved, Reserved)
                && Equals(other.PhotoFileName, PhotoFileName);
        }
    }
}
