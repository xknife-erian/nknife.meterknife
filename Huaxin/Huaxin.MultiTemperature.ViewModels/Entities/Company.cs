using System.Collections.Generic;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Wrapper;

namespace Huaxin.MultiTemperature.ViewModels.Entities
{
    /// <summary>
    /// 项目所属机构
    /// </summary>
    public class Company
    {
        public Company()
        {
            Number = DI.Get<IIdGenerator>().Generate();
        }
        /// <summary>
        /// 数据库的ID。自动生成。
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 公司编号。系统自动生成。
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 联系人。
        /// </summary>
        public string Charger { get; set; }
        /// <summary>
        /// 公司名称。
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 联系电话。
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 联系地址。
        /// </summary>
        public string Address { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 备注。
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 机构所属仪器列表。
        /// </summary>
        public List<MeterInfo> MeterInfos { get; set; } = new List<MeterInfo>(0);
    }
}