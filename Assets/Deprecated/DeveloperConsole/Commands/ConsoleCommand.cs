using UnityEngine;

/// <summary>
/// Console command base abstract class. Extend this for other custom commands
/// </summary>
namespace DeveloperConsole
{
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField, Tooltip("Keyword that will trigger this command when called")]
        private string invocationKeyword = null;
        [SerializeField, Tooltip("A short block of text that describes the function of this command"), TextArea]
        private string commandDescription = string.Empty;

        /// <summary>
        /// Command description. Keep it short and sweet
        /// </summary>
        public string CommandDescription => commandDescription;
        /// <summary>
        /// List of applicable invoation keywords. If any one of these strings match the input text, this command will be invoked
        /// </summary>
        public string InvocationKeyword => invocationKeyword;

        /// <summary>
        /// Called when the command is invoked from the console
        /// </summary>
        /// <param name="output">Text that is received from the command</param>
        /// <param name="parameters">Parameters acquired from the command invocation</param>
        /// <returns>True if the provided command and parameters are valid and the command is processed successfully</returns>
        public abstract bool Invoke(ref string output, params string[] parameters);
    }
}