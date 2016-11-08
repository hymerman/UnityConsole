using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityConsole
{
    /// <summary>
    /// Use RegisterCommand() to register your own commands. Registered commands persist between scenes but don't persist between multiple application executions.
    /// </summary>
    public static class ConsoleCommandsDatabase 
    {
        private static readonly Dictionary<string, ConsoleCommand> Database = new Dictionary<string, ConsoleCommand>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Return all the commands in alphabetical order.
        /// </summary>
        public static IEnumerable<ConsoleCommand> Commands { get { return Database.OrderBy(kv => kv.Key).Select(kv => kv.Value); } }

        public static void RegisterCommand(string command, ConsoleCommandCallback callback, string description = "", string usage = "") 
        {
            RegisterCommand(command, description, usage, callback);
        }

        public static void RegisterCommand(string command, string description, string usage, ConsoleCommandCallback callback)
        {
            Database[command] = new ConsoleCommand(command, description, usage, callback);
        }

        public static void UnRegisterCommand(string command)
        {
            Database.Remove(command);
        }

        public static ConsoleCommandResult ExecuteCommand(string command, params string[] args)
        {
            try
            {
                ConsoleCommand retrievedCommand = GetCommand(command);
                return retrievedCommand.Callback(args);
            }
            catch (NoSuchCommandException e)
            {
                return new ConsoleCommandResult { succeeded = false, Output = e.Message };
            }
            catch (Exception e)
            {
                return new ConsoleCommandResult { succeeded = false, Output = e.Message };
            }
        }

        public static bool TryGetCommand(string command, out ConsoleCommand result)
        {
            try
            {
                result = GetCommand(command);
                return true;
            }
            catch (NoSuchCommandException)
            {
                result = default(ConsoleCommand);
                return false;
            }
        }

        public static ConsoleCommand GetCommand(string command)
        {
            if (HasCommand(command))
            {
                return Database[command];
            }
            else
            {
                command = command.ToUpper();
                throw new NoSuchCommandException("Command " + command + " not found.", command);
            }
        }

        public static bool HasCommand(string command)
        {
            return Database.ContainsKey(command);
        }
    }
}