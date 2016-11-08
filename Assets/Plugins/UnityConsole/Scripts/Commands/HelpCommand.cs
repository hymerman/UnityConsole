using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityConsole;

namespace UnityConsole.Commands
{ 
    /// <summary>
    /// HELP command. Display the list of available commands or details about a specific command.
    /// </summary>
    public static class HelpCommand
    {
        public static readonly string name = "HELP";
        public static readonly string description = "Display the list of available commands or details about a specific command.";
        public static readonly string usage = "HELP [command]";

        private static StringBuilder commandList = new StringBuilder();

        public static ConsoleCommandResult Execute(params string[] args)
        {
            if (args.Length == 0)
            {
                return DisplayAvailableCommands();
            }
            else
            {
                return DisplayCommandDetails(args[0]);
            }
        }

        private static ConsoleCommandResult DisplayAvailableCommands()
        {
            commandList.Length = 0; // clear the command list before rebuilding it
            commandList.Append("<b>Available Commands</b>\n");

            foreach (ConsoleCommand command in ConsoleCommandsDatabase.commands)
            {
                commandList.Append(string.Format("    <b>{0}</b> - {1}\n", command.name, command.description));
            }

            commandList.Append("To display details about a specific command, type 'HELP' followed by the command name.");
            return ConsoleCommandResult.Succeeded(commandList.ToString());
        }

        private static ConsoleCommandResult DisplayCommandDetails(string commandName)
        {
            string formatting =
@"<b>{0} Command</b>
    <b>Description:</b> {1}
    <b>Usage:</b> {2}";

            try
            {
                ConsoleCommand command = ConsoleCommandsDatabase.GetCommand(commandName);
                return ConsoleCommandResult.Succeeded(string.Format(formatting, command.name, command.description, command.usage));
            }
            catch (NoSuchCommandException exception)
            {
                return ConsoleCommandResult.Failed(string.Format("Cannot find help information about {0}. Are you sure it is a valid command?", exception.command));
            }
        }
    }
}
