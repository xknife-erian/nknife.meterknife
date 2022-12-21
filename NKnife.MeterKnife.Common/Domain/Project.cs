using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NKnife.Db.Base;
using NKnife.Interface;
using NKnife.Jobs;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Util.Tunnel;

namespace NKnife.MeterKnife.Common.Domain
{
    /// <summary>
    /// 一个测量工程
    /// </summary>
    public class Project : IId, ICloneable
    {
        public Project()
        {
            Id = SequentialGuid.Create().ToString("N").ToUpper();
        }

        /// <summary>
        /// 工程编号
        /// </summary>
        [Key]
        [Index]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// 工程的简称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工程的详细描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建工程的时间（一般是工程创建完成，保存工程的时间）
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 路径。当Sqlite时是文件路径及全名；当Mysql时是数据库与表名。
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 工程的命令集合
        /// </summary>
        public List<ScpiCommandPool> CommandPools { get; set; } = new List<ScpiCommandPool>();

        /// <summary>
        /// 获取本工程内部包含被测物
        /// </summary>
        public List<DUT> GetIncludedDUTArray()
        {
            List<DUT> duts = new List<DUT>();
            foreach (var cmdPool in CommandPools)
            {
                foreach (ScpiCommand cmd in cmdPool)
                {
                    if(cmd.DUT!=null)
                        duts.Add(cmd.DUT);
                }
            }
            return duts;
        }

        public List<Slot> GetIncludedSlots()
        {
            List<Slot> slots = new List<Slot>();
            foreach (var cmdPool in CommandPools)
            {
                foreach (ScpiCommand cmd in cmdPool)
                {
                    if (cmd.Slot != null && !slots.Contains(cmd.Slot))
                        slots.Add(cmd.Slot);
                }
            }
            return slots;
        }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Id}/{CreateTime}";
        }

        #endregion

        #region Implementation of ICloneable

        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion


    }

}
