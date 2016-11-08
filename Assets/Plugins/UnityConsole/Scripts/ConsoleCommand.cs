namespace Wenzil.Console 
{
    public struct ConsoleCommandResult
    {
        public bool succeeded;
        public string output;

        public static ConsoleCommandResult Failed(string output = null)
        {
            return new ConsoleCommandResult { succeeded = false, output = output };
        }

        public static ConsoleCommandResult Succeeded(string output = null)
        {
            return new ConsoleCommandResult { succeeded = true, output = output };
        }
    }

    public delegate ConsoleCommandResult ConsoleCommandCallback(params string[] args);

    public struct ConsoleCommand 
    {
        public string name { get; private set; }
        public string description { get; private set; }
        public string usage { get; private set; }
        public ConsoleCommandCallback callback { get; private set; }

        public ConsoleCommand(string name, string description, string usage, ConsoleCommandCallback callback) : this()
        {
            this.name = name;
            this.description = (string.IsNullOrEmpty(description.Trim()) ? "No description provided" : description);
            this.usage = (string.IsNullOrEmpty(usage.Trim()) ? "No usage information provided" : usage);
            this.callback = callback;
        }
    }
}