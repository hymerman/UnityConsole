namespace UnityConsole 
{
    public struct ConsoleCommandResult
    {
        public bool succeeded;
        public string Output;

        public static ConsoleCommandResult Failed(string output = null)
        {
            return new ConsoleCommandResult { succeeded = false, Output = output };
        }

        public static ConsoleCommandResult Succeeded(string output = null)
        {
            return new ConsoleCommandResult { succeeded = true, Output = output };
        }
    }

    public delegate ConsoleCommandResult ConsoleCommandCallback(params string[] args);

    public struct ConsoleCommand 
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Usage { get; private set; }
        public ConsoleCommandCallback Callback { get; private set; }

        public ConsoleCommand(string name, string description, string usage, ConsoleCommandCallback callback) : this()
        {
            Name = name;
            Description = (string.IsNullOrEmpty(description.Trim()) ? "No description provided" : description);
            Usage = (string.IsNullOrEmpty(usage.Trim()) ? "No usage information provided" : usage);
            Callback = callback;
        }
    }
}