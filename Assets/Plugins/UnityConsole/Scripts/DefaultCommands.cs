using UnityEngine;
using UnityConsole.Commands;

namespace UnityConsole
{ 
    public class DefaultCommands : MonoBehaviour
    {
        private void Start() 
        {
            ConsoleCommandsDatabase.RegisterCommand(QuitCommand.Name, QuitCommand.Description, QuitCommand.Usage, QuitCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(HelpCommand.Name, HelpCommand.Description, HelpCommand.Usage, HelpCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(LoadSceneCommand.Name, LoadSceneCommand.Description, LoadSceneCommand.Usage, LoadSceneCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(LoadSceneAdditiveCommand.Name, LoadSceneAdditiveCommand.Description, LoadSceneAdditiveCommand.Usage, LoadSceneAdditiveCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(UnloadSceneCommand.Name, UnloadSceneCommand.Description, UnloadSceneCommand.Usage, UnloadSceneCommand.Execute);
            ConsoleCommandsDatabase.RegisterCommand(SetActiveSceneCommand.Name, SetActiveSceneCommand.Description, SetActiveSceneCommand.Usage, SetActiveSceneCommand.Execute);
        }
    }
}
