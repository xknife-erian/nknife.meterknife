using System.Collections.Generic;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using NKnife.IoC;

namespace MeterKnife.Datas
{
    public class DataServices
    {
        private readonly Dictionary<int, ICollectSource> _CollectSources = new Dictionary<int, ICollectSource>();

        public void BindCollectDataSource(ICollectSource source)
        {
            _CollectSources.Add(source.GpibAddress, source);
            source.ReceviedCollectData += CollectSource_ReceviedCollectData;
            source.ReceviedTemperatureData += CollectSource_ReceviedTemperatureData;
        }

        public void UnbindCollectDataSource(ICollectSource source)
        {
            _CollectSources.Remove(source.GpibAddress);
            source.ReceviedCollectData -= CollectSource_ReceviedCollectData;
            source.ReceviedTemperatureData -= CollectSource_ReceviedTemperatureData;
        }

        protected virtual void CollectSource_ReceviedTemperatureData(object sender, CollectEventArgs e)
        {
        }

        protected virtual void CollectSource_ReceviedCollectData(object sender, CollectEventArgs e)
        {
        }

    }
}
