using UnityEngine;
using System;
using UnityConsole;
using UnityConsole.Commands;

/// <summary>
/// Two custom commands being registered with the console. Registered commands persist between scenes but don't persist between multiple application executions.
/// </summary>
public class CustomCommands : MonoBehaviour
{
    void Start()
    {
        ConsoleCommandsDatabase.RegisterCommand("SPAWN", "Spawn a new game object from the given name and primitve type in front of the main camera. See PrimitiveType.", "SPAWN name primitiveType", Spawn);
        ConsoleCommandsDatabase.RegisterCommand("DESTROY", "Destroy the specified game object by name.", "DESTROY gameobject", Destroy);
    }

    /// <summary>
    /// Spawn a new game object from the given name and primitve type in front of the main camera. See PrimitiveType.
    /// </summary>
    private static ConsoleCommandResult Spawn(params string[] args)
    {
        string name;
        PrimitiveType primitiveType;
        GameObject spawned;
        
        if(args.Length < 2)
        {
            return HelpCommand.Execute("SPAWN");
        }
        else
        {
            name = args[0];
            try
            {
                primitiveType = (PrimitiveType) Enum.Parse(typeof(PrimitiveType), args[1], true);
            }
            catch
            {
                return ConsoleCommandResult.Failed("Invalid primitive type specified: " + args[1]);
            }

            spawned = GameObject.CreatePrimitive(primitiveType);
            spawned.name = name;
            spawned.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
            return ConsoleCommandResult.Succeeded("Spawned a new " + primitiveType + " named " + name + ".");
        }
    }

    /// <summary>
    /// Destroy the specified game object by name.
    /// </summary>
    private static ConsoleCommandResult Destroy(params string[] args)
    {
        if (args.Length == 0)
        {
            return HelpCommand.Execute("DESTROY");
        }
        else
        {
            string name = args[0];
            GameObject gameobject = GameObject.Find(name);
            
            if (gameobject != null)
            {
                GameObject.Destroy(gameobject);
                return ConsoleCommandResult.Succeeded("Destroyed game object " + name + ".");
            }
            else
            {
                return ConsoleCommandResult.Failed("No game object named " + name + ".");
            }
        }
    }
}