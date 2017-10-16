using LiteDB;
using MeterKnife.Interfaces;
using NKnife.DataLite;
using NKnife.IoC;

namespace MeterKnife.Datas.Dpi
{
    public abstract class GlobalDataBaseRepositoryBase<T, TId> : PagingAndSortingRepositoryBase<T, TId>
    {
        #region Overrides of RepositoryBase<ExhibitData<T>>

        protected override LiteDatabase Database => ((DatasService)DI.Get<IDatasService>()).GlobalDataBase;

        #endregion
    }
}