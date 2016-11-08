using UnityEngine;
using System.Linq;

namespace UnityConsole
{
    /// <summary>
    /// The behavior of the console.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ConsoleController))]
    public class ConsoleController : MonoBehaviour
    {
        private const int InputHistoryCapacity = 20;
 
        public ConsoleUI UI;
        public KeyCode ToggleKey = KeyCode.BackQuote;
        public bool CloseOnEscape;

        private readonly ConsoleInputHistory _inputHistory = new ConsoleInputHistory(InputHistoryCapacity);

        public void ExecuteCommandDropResult( string input )
        {
            ExecuteCommand( input );
        }

        public ConsoleCommandResult ExecuteCommand(string input)
        {
            string[] parts = input.Split(' ');
            string command = parts[0];
            string[] args = parts.Skip(1).ToArray();

            Console.Log("> " + input);
            var result = ConsoleCommandsDatabase.ExecuteCommand(command, args);

            Console.Log(result.succeeded ? "Done" : "Failed");

            if(!string.IsNullOrEmpty(result.Output))
            {
                Console.Log(result.Output);
            }

            _inputHistory.AddNewInputEntry(input);

            return result;
        }

        private void Awake()
        {
            /* This instantiation causes a bug when Unity rebuilds the project while in play mode
               Solution: move it to class level initialization, and make inputHistoryCapacity a const */
            // inputHistory = new ConsoleInputHistory(inputHistoryCapacity); 
        }

        private void OnEnable()
        {
            Console.OnConsoleLog += UI.AddNewOutputLine;
            UI.OnSubmitCommand += ExecuteCommandDropResult;
            UI.OnClearConsole += _inputHistory.Clear;
        }

        private void OnDisable()
        {
            Console.OnConsoleLog -= UI.AddNewOutputLine;
            UI.OnSubmitCommand -= ExecuteCommandDropResult;
            UI.OnClearConsole -= _inputHistory.Clear;
        }

        private void Update()
        {
            if (Input.GetKeyDown(ToggleKey))
                UI.ToggleConsole();
            else if (Input.GetKeyDown(KeyCode.Escape) && CloseOnEscape)
                UI.CloseConsole();
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                NavigateInputHistory(true);
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                NavigateInputHistory(false);
        }

        private void NavigateInputHistory(bool up)
        {
            string navigatedToInput = _inputHistory.Navigate(up);
            UI.SetInputText(navigatedToInput);
        }
    }
}