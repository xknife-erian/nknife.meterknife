﻿using System;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Keysights
{
    public class KeysightQuestion : QuestionBase<string>
    {
        private readonly Guid _Id;
        /// <summary>
        /// 描述设备向PC串口返回的交换数据
        /// </summary>
        public KeysightQuestion(IChannel<string> channel, IDevice device, IExhibit exhibit, bool isLoop, string data) 
            : base(channel, device, exhibit, isLoop, data)
        {
            _Id = Guid.NewGuid();
        }

        #region Overrides of Object

        /// <summary>确定指定的 <see cref="T:System.Object" /> 是否等于当前的 <see cref="T:System.Object" />。</summary>
        /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
        /// <param name="obj">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
        public override bool Equals(object obj)
        {
            if (!(obj is KeysightQuestion))
                return false;
            return Equals((KeysightQuestion)obj);
        }

        #region Equality members

        protected bool Equals(KeysightQuestion other)
        {
            return _Id.Equals(other._Id);
        }

        /// <summary>用作特定类型的哈希函数。</summary>
        /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
        public override int GetHashCode()
        {
            return _Id.GetHashCode();
        }

        #endregion

        #endregion
    }
}