using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// LOAD command. Load the specified scene by name.
/// </summary>

namespace Wenzil.Console.Commands
{
    public static class LoadCommand
    {
        public static readonly string name = "LOAD";
        public static readonly string description = "Load the specified scene by name. Before you can load a scene you have to add it to the list of levels used in the game. Use File->Build Settings... in Unity and add the levels you need to the level list there.";
        public static readonly string usage = "LOAD scene";

        public static ConsoleCommandResult Execute(params string[] args)
        {
            if(args.Length == 0)
            {
                return HelpCommand.Execute(LoadCommand.name);
            }
            else
            {
                return LoadLevel(args[0]);
            }
        }

        private static ConsoleCommandResult LoadLevel(string sceneName)
        {
            try
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
            catch
            {
                return ConsoleCommandResult.Failed(string.Format("Failed to load {0}.", sceneName));
            }

            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);

            if(scene.IsValid() && scene.isLoaded)
                return ConsoleCommandResult.Succeeded(string.Format("Success loading {0}.", sceneName));
            else
                return ConsoleCommandResult.Failed(string.Format("Failed to load {0}. Are you sure it's in the list of levels in Build Settings?", sceneName));
        }
    }
}