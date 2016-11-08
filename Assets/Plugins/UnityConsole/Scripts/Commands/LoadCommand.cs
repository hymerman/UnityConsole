using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// LOAD command. Load the specified scene by name.
/// </summary>

namespace Wenzil.Console.Commands
{
    public static class LoadSceneCommand
    {
        public static readonly string name = "LoadScene";
        public static readonly string description = "Load the specified scene by name. Before you can load a scene you have to add it to the list of levels used in the game. Use File->Build Settings... in Unity and add the levels you need to the level list there.";
        public static readonly string usage = "LoadScene scene";

        public static ConsoleCommandResult Execute(params string[] args)
        {
            if(args.Length == 0)
            {
                return HelpCommand.Execute(LoadSceneCommand.name);
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

    public static class LoadSceneAdditiveCommand
    {
        public static readonly string name = "LoadSceneAdditive";
        public static readonly string description = "Load the specified scene by name. Before you can load a scene you have to add it to the list of levels used in the game. Use File->Build Settings... in Unity and add the levels you need to the level list there.";
        public static readonly string usage = "LoadSceneAdditive sceneName";

        public static ConsoleCommandResult Execute( params string[] args )
        {
            if( args.Length == 0 )
            {
                return HelpCommand.Execute( LoadSceneAdditiveCommand.name );
            }
            else
            {
                return LoadSceneAdditive( args[ 0 ] );
            }
        }

        private static ConsoleCommandResult LoadSceneAdditive( string sceneName )
        {
            try
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene( sceneName, LoadSceneMode.Additive );
            }
            catch
            {
                return ConsoleCommandResult.Failed( string.Format( "Failed to load {0}.", sceneName ) );
            }

            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName( sceneName );

            if( scene.IsValid() && scene.isLoaded )
                return ConsoleCommandResult.Succeeded( string.Format( "Success loading {0}.", sceneName ) );
            else
                return ConsoleCommandResult.Failed( string.Format( "Failed to load {0}. Are you sure it's in the list of levels in Build Settings?", sceneName ) );
        }
    }

    public static class UnloadSceneCommand
    {
        public static readonly string name = "UnloadScene";
        public static readonly string description = "Unloads the named scene";
        public static readonly string usage = "UnloadScene sceneName";

        public static ConsoleCommandResult Execute( params string[] args )
        {
            if( args.Length == 0 )
            {
                return HelpCommand.Execute( LoadSceneAdditiveCommand.name );
            }
            else
            {
                return UnloadScene( args[ 0 ] );
            }
        }

        private static ConsoleCommandResult UnloadScene( string sceneName )
        {
            try
            {
                UnityEngine.SceneManagement.SceneManager.UnloadScene( sceneName );
            }
            catch
            {
                return ConsoleCommandResult.Failed( string.Format( "Failed to load {0}.", sceneName ) );
            }

            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName( sceneName );

            if( scene.IsValid() && !scene.isLoaded )
                return ConsoleCommandResult.Succeeded( string.Format( "Success unloading {0}.", sceneName ) );
            else
                return ConsoleCommandResult.Failed( string.Format( "Failed to unload {0}. Are you sure it's in the list of levels in Build Settings?", sceneName ) );
        }
    }

    public static class SetActiveSceneCommand
    {
        public static readonly string name = "SetActiveScene";
        public static readonly string description = "Sets the named scene as the active scene";
        public static readonly string usage = "SetActiveScene sceneName";

        public static ConsoleCommandResult Execute( params string[] args )
        {
            if( args.Length == 0 )
            {
                return HelpCommand.Execute( SetActiveSceneCommand.name );
            }
            else
            {
                return SetActiveScene( args[ 0 ] );
            }
        }

        private static ConsoleCommandResult SetActiveScene( string sceneName )
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName( sceneName );

            try
            {
                UnityEngine.SceneManagement.SceneManager.SetActiveScene( scene );
            }
            catch
            {
                return ConsoleCommandResult.Failed( string.Format( "Failed to set {0} as active.", sceneName ) );
            }

            if( scene.IsValid() && UnityEngine.SceneManagement.SceneManager.GetActiveScene() == scene )
                return ConsoleCommandResult.Succeeded( string.Format( "Success setting {0} as active.", sceneName ) );
            else
                return ConsoleCommandResult.Failed( string.Format( "Failed to set {0} as active. Are you sure it's in the list of levels in Build Settings?", sceneName ) );
        }
    }
}