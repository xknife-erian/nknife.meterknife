namespace NKnife.MeterKnife.Common.Base
{
    /// <summary>
    /// 这个配合<see cref="IDomain"/>的充血型实体，它的内容更加的完整易读，而不全是以ID为主要内容，通常这一类实体主要面向显示。
    /// 这一类实体不会直接存储在数据库里，通常它会是通过SQL语句的Join方法或者数据库中的视图读出数据。
    /// </summary>
    public interface IComplexDomain
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        string Id { get; set; }
    }
}