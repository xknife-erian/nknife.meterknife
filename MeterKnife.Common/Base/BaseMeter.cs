using System;
using System.Xml;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Properties;
using MeterKnife.Common.Util;

namespace MeterKnife.Common.Base
{
    public abstract class BaseMeter : IMeter
    {
        public int GpibAddress { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public GpibLanguage Language { get; set; }
        public string SimpleName { get { return MeterUtil.SimplifyName(Name).Second; } }

        public GpibCommandList GetGpibCommands()
        {
            return ParamPanel.GpibCommands;
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
    }
}