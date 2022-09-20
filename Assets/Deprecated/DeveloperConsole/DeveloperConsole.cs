using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DeveloperConsole
{
    /// <summary>
    /// Developer console class containing all functionality for a customizable text-based prefix-delimited console system. Commands are case-sensitive at the moment
    /// </summary>
    public class DeveloperConsole
    {
        //need an event here or elsewhere that will be called when the command or prefix is modified here
        //so that the new prefix and commands can be updated where else it is stored
        private event Action<string> PrefixUpdateEvent;
        private event Action<IConsoleCommand> CommandUpdateEvent;

        private string prefix;
        private Dictionary<string, IConsoleCommand> commands;

        private Regex regex;

        /// <summary>
        /// Create an instance of DeveloperConsole using the provided information
        /// </summary>
        /// <param name="prefix">Specify a prefix to use to indicate when a command is being requested</param>
        /// <param name="commands">Specify a prebuilt list of commads to listen for</param>
        public DeveloperConsole(string prefix, ref Dictionary<string, IConsoleCommand> commands)
        {
            UpdatePrefix(prefix);
            this.commands = commands;
        }

        public void ProcessCommand(string commandInput, ref string commandOutput)
        {
            if (string.IsNullOrWhiteSpace(commandInput)) return;
            if (regex.IsMatch(commandInput))
            {
                string input = regex.Replace(commandInput, string.Empty);
                string[] splitInput = input.Split(' ');

                string commandKey = splitInput[0];
                string[] commandArgs = splitInput.Skip(1).ToArray();

                ProcessCommand(commandKey, ref commandOutput, commandArgs);
            }
        }
        void ProcessCommand(string commandInput, ref string commandOutput, params string[] args)
        {
            if (commands.ContainsKey(commandInput))
            {
                if (commands[commandInput].Invoke(ref commandOutput, args))
                {
                    //successful invocation
                    return;
                }
                else
                {
                    //unsuccessful invocation... print some sort of error based on the output CommandStatus
                }
            }
        }

        /// <summary>
        /// Update the current command prefix when provided a valid, non-null string containing one character that is not whitespace
        /// </summary>
        /// <param name="newPrefix">Value of the new prefix</param>
        /// <returns>True when the provided trimmed prefix string is not null, empty, whitespace or greater than one character</returns>
        public bool UpdatePrefix(string newPrefix)
        {
            if (string.IsNullOrWhiteSpace(newPrefix)) throw new ArgumentException("provided prefix cannot be null, empty or whitespace", nameof(newPrefix));
            if (newPrefix.Trim().Length > 1) return false;

            prefix = newPrefix.Trim();
            regex = new Regex(prefix);
            PrefixUpdateEvent?.Invoke(prefix);
            return true;
        }

        /// <summary>
        /// Atempt to add a command to the dictionary. Will not add commands with duplicate invoation keywords
        /// </summary>
        /// <param name="commandToAdd">Command to add to the dictionary of available commands</param>
        /// <returns>True if the command is successfully added to the dictionary</returns>
        public bool TryAddCommand(IConsoleCommand commandToAdd)
        {
            if (commandToAdd is null) throw new ArgumentException("cannot add a null command instance", nameof(commandToAdd));
            if (string.IsNullOrWhiteSpace(commandToAdd.InvocationKeyword)) return false;
            if (commands.ContainsKey(commandToAdd.InvocationKeyword)) return false;

            commands[commandToAdd.InvocationKeyword.ToLowerInvariant()] = commandToAdd;
            CommandUpdateEvent?.Invoke(commandToAdd);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        public void SubscribeToPrefixUpdate(Action<string> method)
        {
            if (method is null) throw new ArgumentException("provided method cannot be null", nameof(method));

            PrefixUpdateEvent += method;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        public void UnsubscribeToPrefixUpdate(Action<string> method)
        {
            if (method is null) throw new ArgumentException("provided method cannot be null", nameof(method));

            PrefixUpdateEvent -= method;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        public void SubscribeToCommandUpdate(Action<IConsoleCommand> method)
        {
            if (method is null) throw new ArgumentException("provided method cannot be null", nameof(method));

            CommandUpdateEvent += method;
        }
        public void UnsubscribetoCommandUpdate(Action<IConsoleCommand> method)
        {
            if (method is null) throw new ArgumentException("provided method cannot be null", nameof(method));

            CommandUpdateEvent -= method;
        }
    }
}