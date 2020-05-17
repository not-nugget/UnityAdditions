namespace DeveloperConsole
{
    /// <summary>
    /// Console command interface. To specify a command, implement this interface on a scriptable object, or extend the abstract class <c>ConsoleCommand</c>
    /// </summary>
    public interface IConsoleCommand
    {
        /// <summary>
        /// Command invocation keyword. If input text matches this keyword, the command will be invoked
        /// </summary>
        string InvocationKeyword { get; }
        /// <summary>
        /// Command description. Keep it short and sweet
        /// </summary>
        string CommandDescription { get; }

        /// <summary>
        /// Called when the command is invoked from the console
        /// </summary>
        /// <param name="output">Text that is received from the command</param>
        /// <param name="parameters">Parameters acquired from the command invocation</param>
        /// <returns>True if the provided command and parameters are valid and the command is processed successfully</returns>
        bool Invoke(ref string output, params string[] parameters);
    }
}