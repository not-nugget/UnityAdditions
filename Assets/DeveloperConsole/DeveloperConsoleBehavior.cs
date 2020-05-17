using ObjectPool.Basic;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityAdditions.Singleton;
using UnityEngine;

namespace DeveloperConsole
{
    /// <summary>
    /// Monobehavior singleton providing the developer console functionality
    /// </summary>
    public class DeveloperConsoleBehavior : Singleton<DeveloperConsoleBehavior>
    {
        [BoxGroup("Command"), SerializeField, Tooltip("The current command prefix")]
        private string prefix = string.Empty;
        [BoxGroup("Command"), SerializeField, Tooltip("The current subscribed commands that can be invoked")]
        private Dictionary<string, IConsoleCommand> commands = null;

        [BoxGroup("UI"), SerializeField, Tooltip("The canvas representing the console UI")]
        private GameObject canvas = null;
        [BoxGroup("UI"), SerializeField, Tooltip("The user input field used to receive command requests")]
        private TMP_InputField userInputField = null;
        [BoxGroup("UI"), SerializeField, Tooltip("The object pool that stores all the prebuilt text prefabs which are used for the command output")]
        private SimpleObjectPool outputLogTextPool = null;

        private DeveloperConsole developerConsole;

        private void Awake()
        {
            developerConsole = new DeveloperConsole(prefix, ref commands);

            developerConsole.SubscribeToPrefixUpdate(UpdateStringPrefixExternally);
            developerConsole.SubscribeToCommandUpdate(UpdateCommandListExternally);
        }

        public void ToggleConsole()
        {
            if (canvas is null) throw new NullReferenceException("developer console does not have an instance of canvas to toggle on or off");
            canvas.SetActive(!canvas.activeSelf);
        }

        public void ProcessCommand(string commandInput)
        {
            string output = string.Empty;
            developerConsole.ProcessCommand(commandInput, ref output);

            TextMeshProUGUI outputTextElement = outputLogTextPool.Get().GetComponent<TextMeshProUGUI>();
            outputTextElement.text = $"({DateTime.Now}) - {output}";

            userInputField.text = string.Empty;
        }

        void UpdateStringPrefixExternally(string newValue) => prefix = newValue; //The stored prefix in the developerConsole instance was altered somewhere different than this class, so update the stored value here to the new one
        void UpdateCommandListExternally(IConsoleCommand newCommand)
        {
            if (commands.ContainsKey(newCommand.InvocationKeyword)) return;
            else
                commands[newCommand.InvocationKeyword] = newCommand;
        } //The stored commands in the developerConsole instance were altered somewhere different than this class, so update the stored value here to the new one
    }
}