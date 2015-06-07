using System;
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
        public GpibLanguage Language { get; set; }
        public string AbbrName { get { return MeterUtil.SimplifyName(Name).Second; } }

        public ScpiCommandList GetScpiCommands()
        {
            return ParamPanel.ScpiCommands;
        }

        public abstract BaseParamPanel ParamPanel { get; }

        protected XmlDocument _TempDocument;
        protected XmlElement GetTempElement()
        {
            if (_TempDocument == null)
            {
                var xml = GlobalResources.DemoMeterParamElement;
                _TempDocument = new XmlDocument();
                _TempDocument.LoadXml(xml);
            }
            return _TempDocument.DocumentElement;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseMeter) obj);
        }

        protected bool Equals(BaseMeter other)
        {
            return GpibAddress == other.GpibAddress && string.Equals(Brand, other.Brand) && string.Equals(Name, other.Name) && Language == other.Language;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = GpibAddress;
                hashCode = (hashCode * 397) ^ (Brand != null ? Brand.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Language;
                return hashCode;
            }
        }
    }
}