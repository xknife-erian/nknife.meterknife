using System.Collections.Generic;
using MeterKnife.Util.Interface.Patterns;

namespace MeterKnife.Util.Entities
{
    /// <summary>
    ///     命令模式中命令接口的集合类
    /// </summary>
    public class CommandList
    {
        #region 属性成员定义

        public LinkedList<ICommandPattern> Commands { get; set; }

        /// <summary>
        ///     当前命令,待撤销之操作
        /// </summary>
        public LinkedListNode<ICommandPattern> CurrentCommand { get; set; }

        #endregion

        #region 构造函数

        public CommandList()
        {
            Commands = new LinkedList<ICommandPattern>();
        }

        #endregion

        #region 公共函数成员接口

        /// <summary>
        ///     添加新命令并执行之
        /// </summary>
        /// <param name="command"></param>
        public ICommandPattern AddCommand(ICommandPattern command)
        {
            var commandNode = new LinkedListNode<ICommandPattern>(command);
            if (CurrentCommand != null)
            {
                Commands.AddAfter(CurrentCommand, commandNode);
            }
            else
            {
                Commands.AddFirst(commandNode);
            }
            CurrentCommand = commandNode;
            return command;
        }

        /// <summary>
        ///     重做命令
        /// </summary>
        public void Execute(object parameter)
        {
            if (CanExecute())
            {
                LinkedListNode<ICommandPattern> reDoCommandNode = CurrentCommand == null
                    ? Commands.First
                    : CurrentCommand.Next;
                if (reDoCommandNode != null) //执行重做命令
                {
                    reDoCommandNode.Value.Execute(parameter);
                    CurrentCommand = reDoCommandNode;
                }
            }
        }

        /// <summary>
        ///     撤销命令
        /// </summary>
        public void Cancel(object parameter)
        {
            if (CanCancel())
            {
                CurrentCommand.Value.Cancel(parameter);
                CurrentCommand = CurrentCommand.Previous;
            }
        }

        /// <summary>
        ///     是否可以撤销
        /// </summary>
        public bool CanCancel()
        {
            return CurrentCommand != null;
        }

        /// <summary>
        ///     是否可以重做
        /// </summary>
        public bool CanExecute()
        {
            return CurrentCommand != Commands.Last;
        }

        #endregion
    }
}