using System;
using System.Collections.Generic;
using System.Xml;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Properties;
using MeterKnife.Common.Util;
using ScpiKnife;

namespace MeterKnife.Common.Base
{
    public abstract class BaseMeter : IMeter
    {
        public int GpibAddress { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 缩写名称
        /// </summary>
        public string AbbrName { get { return MeterUtil.SimplifyName(Name).Second; } }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseMeter) obj);
        }

        protected bool Equals(BaseMeter other)
        {
            return GpibAddress == other.GpibAddress && string.Equals(Brand, other.Brand) && string.Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = GpibAddress;
                hashCode = (hashCode * 397) ^ (Brand != null ? Brand.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}