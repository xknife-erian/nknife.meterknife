using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Base.Channels;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Keysights
{
    public class KeysightQuestionGroup : MeasureQuestionGroup<string>
    {
        /// <summary>
        /// 获取当前Group中需要处理的Question所在的索引
        /// </summary>
        public int CurrentIndex { get; set; } = 0;

        /// <summary>
        /// 获取当前Group中需要处理的Question
        /// </summary>
        public KeysightQuestion Current
        {
            get
            {
                if (Count > CurrentIndex)
                    return (KeysightQuestion) this[CurrentIndex];
                return null;
            }
        }

        /// <summary>
        /// 取出一条Question。当该Question需要Loop时，保留Question在Group中；当该Question不需要Loop时，弹出并从Group中移除。
        /// </summary>
        public KeysightQuestion PeekOrDequeue()
        {
            var question = Current;
            if (question == null)
            {
                return null;
            }
            if (!question.IsLoop)
            {
                Remove(question);
                if (CurrentIndex == Count)
                    SetCurrent();
            }
            else
            {
                SetCurrent();
            }
            return question;
        }

        private void SetCurrent()
        {
            if (CurrentIndex < Count - 1)
                CurrentIndex++;
            else
                CurrentIndex = 0;
        }

        public void Add(params KeysightQuestion[] questions)
        {
            AddRange(questions);
        }
    }
}
