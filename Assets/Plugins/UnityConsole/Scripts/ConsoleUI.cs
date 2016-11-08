using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace UnityConsole
{

    /// <summary>
    /// The interactive front-end of the console.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ConsoleController))]
    public class ConsoleUI : MonoBehaviour, IScrollHandler
    {
        public event Action<bool> OnToggleConsole;
        public event Action<string> OnSubmitCommand;
        public event Action OnClearConsole;

        public Scrollbar Scrollbar;
        public Text OutputText;
        public ScrollRect OutputArea;
        public InputField InputField;

        /// <summary>
        /// Indicates whether the console is currently open or close.
        /// </summary>
        public bool IsConsoleOpen { get { return enabled; } }

        private void Awake()
        {
            Show(false);
        }

        /// <summary>
        /// Opens or closes the console.
        /// </summary>
        public void ToggleConsole()
        {
            // Also clear the output whilst closing, as Unity's text boxes (oddly) submit their contents on becoming disabled.
            ClearInput();
            enabled = !enabled;
        }

        /// <summary>
        /// Opens the console.
        /// </summary>
        public void OpenConsole()
        {
            enabled = true;
        }

        /// <summary>
        /// Closes the console.
        /// </summary>
        public void CloseConsole()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            OnToggle(true);
        }

        private void OnDisable()
        {
            OnToggle(false);
        }

        private void OnToggle(bool open)
        {
            Show(open);

            if (open)
                InputField.ActivateInputField();
            else
                ClearInput();

            if (OnToggleConsole != null)
                OnToggleConsole(open);
        }

        private void Show(bool show)
        {
            InputField.gameObject.SetActive(show);
            OutputArea.gameObject.SetActive(show);
            Scrollbar.gameObject.SetActive(show);
        }

        /// <summary>
        /// What to do when the user wants to submit a command.
        /// </summary>
        public void OnSubmit(string input)
        {
            if (EventSystem.current.alreadySelecting) // if user selected something else, don't treat as a submit
                return;

            if (input.Length > 0)
            {
                if (OnSubmitCommand != null)
                    OnSubmitCommand(input);
                Scrollbar.value = 0;
                ClearInput();
            }

            InputField.ActivateInputField();
        }

        /// <summary>
        /// What to do when the user uses the scrollwheel while hovering the console input.
        /// </summary>
        public void OnScroll(PointerEventData eventData)
        {
            Scrollbar.value += 0.08f * eventData.scrollDelta.y;
        }

        /// <summary>
        /// Displays the given message as a new entry in the console output.
        /// </summary>
        public void AddNewOutputLine(string line)
        {
            OutputText.text += Environment.NewLine + line;
        }

        /// <summary>
        /// Clears the console output.
        /// </summary>
        public void ClearOutput()
        {
            OutputText.text = "";
            OutputText.SetLayoutDirty();
            if(OnClearConsole != null)
                OnClearConsole();
        }

        /// <summary>
        /// Clears the console input.
        /// </summary>
        public void ClearInput()
        {
            SetInputText("");
        }

        /// <summary>
        /// Writes the given string into the console input, ready to be user submitted.
        /// </summary>
        public void SetInputText(string input) 
        {
            InputField.MoveTextStart(false);
            InputField.text = input;
            InputField.MoveTextEnd(false);
        }
    }
}