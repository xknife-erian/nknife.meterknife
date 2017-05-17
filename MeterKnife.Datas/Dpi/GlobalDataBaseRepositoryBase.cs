using LiteDB;
using NKnife.DataLite;
using NKnife.IoC;

namespace MeterKnife.Datas.Dpi
{
    public abstract class GlobalDataBaseRepositoryBase<T, TId> : PagingAndSortingRepositoryBase<T, TId>
    {
        protected GlobalDataBaseRepositoryBase()
        {
        }

        #region Overrides of RepositoryBase<ExhibitData<T>>

        protected override LiteDatabase Database => DI.Get<DatabaseService>().DataBase;

        #endregion
    }
}