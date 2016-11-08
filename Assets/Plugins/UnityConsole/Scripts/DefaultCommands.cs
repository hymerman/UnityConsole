using UnityEngine;
using UnityConsole.Commands;

namespace UnityConsole
{ 
    public class DefaultCommands : MonoBehaviour
    {
        void Start() 
        {
            ConsoleCommandsDatabase.RegisterCommand(QuitCommand.name, QuitCommand.description, QuitCommand.usage, QuitCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(HelpCommand.name, HelpCommand.description, HelpCommand.usage, HelpCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(LoadSceneCommand.name, LoadSceneCommand.description, LoadSceneCommand.usage, LoadSceneCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(LoadSceneAdditiveCommand.name, LoadSceneAdditiveCommand.description, LoadSceneAdditiveCommand.usage, LoadSceneAdditiveCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(UnloadSceneCommand.name, UnloadSceneCommand.description, UnloadSceneCommand.usage, UnloadSceneCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(SetActiveSceneCommand.name, SetActiveSceneCommand.description, SetActiveSceneCommand.usage, SetActiveSceneCommand.Execute);
        }
    }
}
