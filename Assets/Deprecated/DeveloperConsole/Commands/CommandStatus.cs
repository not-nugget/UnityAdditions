using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeveloperConsole
{
    /// <summary>
    /// Command status base output class. Allow for easy debugging and verifying of values
    /// </summary>
    public abstract class CommandStatus
    {
        /// <summary>
        /// A status code that allows for easy lookup
        /// </summary>
        public int StatusCode { get; }
        /// <summary>
        /// A quick message displaying the status of the command
        /// </summary>
        public string StatusMessage { get; }

        /// <summary>
        /// The source command which contributed to this status
        /// </summary>
        public string SourceCommand => sourceCommand;
        private string sourceCommand = string.Empty;

        protected CommandStatus(string sourceCommand)
        {
            this.sourceCommand = sourceCommand;
        }
    }
}