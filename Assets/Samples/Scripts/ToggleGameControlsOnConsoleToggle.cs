using UnityEngine;
using UnityConsole;

/// <summary>
/// A special utility class that revokes user controls whenever the console is open. Very game-specific.
/// </summary>
public class ToggleGameControlsOnConsoleToggle : MonoBehaviour
{
    public ConsoleUI Console;
    public MouseLook MouseLook;
    public WASDMovement WASDMovement;

    private void OnEnable()
    {
        Console.OnToggleConsole += ToggleMouseLook;
        ToggleMouseLook(Console.IsConsoleOpen);
    }

    private void OnDisable()
    {
        Console.OnToggleConsole -= ToggleMouseLook;
        ToggleMouseLook(false);
    }

    private void ToggleMouseLook(bool isConsoleOpen)
    {
        if(MouseLook != null)
            MouseLook.enabled = !isConsoleOpen;

        if(WASDMovement != null)
            WASDMovement.enabled = !isConsoleOpen;
    }
}