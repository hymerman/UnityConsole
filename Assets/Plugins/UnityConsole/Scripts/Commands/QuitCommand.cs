using UnityEngine;

namespace UnityConsole.Commands
{
    /// <summary>
    /// QUIT command. Quit the application.
    /// </summary>
    public static class QuitCommand
    {
        public static readonly string Name = "QUIT";
        public static readonly string Description = "Quit the application.";
        public static readonly string Usage = "QUIT";

        public static ConsoleCommandResult Execute(params string[] args)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            return ConsoleCommandResult.Succeeded();
        }
    }
}