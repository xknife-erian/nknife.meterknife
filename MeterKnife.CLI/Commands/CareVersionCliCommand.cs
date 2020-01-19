using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Services;

namespace NKnife.MeterKnife.CLI.Commands
{
    [Command("cv", Description = "连接Care，读取Care版本。")]
    public class CareVersionCliCommand : ICommand
    {
        #region Implementation of ICommand

        /// <summary>
        /// Executes command using specified implementation of <see cref="T:CliFx.Services.IConsole" />.
        /// This method is called when the command is invoked by a user through command line interface.
        /// </summary>
        public async Task ExecuteAsync(IConsole console)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
