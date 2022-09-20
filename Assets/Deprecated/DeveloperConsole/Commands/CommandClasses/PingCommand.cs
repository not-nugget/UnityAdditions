using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeveloperConsole
{
    [CreateAssetMenu(fileName = "New PingCommand", menuName = "DeveloperConsole/Commands/PingCommand")]
    public class PingCommand : ConsoleCommand
    {
        public override bool Invoke(ref string output, params string[] parameters)
        {
            if (parameters is null || parameters.Length == 0)
            {
                output = "Pong!";
                return true;
            }
            else return false;
        }
    }
}