using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DefaultKeybinds
{
    // Define constants for default keybindings
    public const KeyCode Jump = KeyCode.Space;
    public const KeyCode Interact = KeyCode.E;
    public const KeyCode MoveForward = KeyCode.W;
    public const KeyCode MoveBackward = KeyCode.S;
    public const KeyCode MoveLeft = KeyCode.A;
    public const KeyCode MoveRight = KeyCode.D;

    // Method to get default keybindings as a dictionary
    public static Dictionary<string, KeyCode> GetDefaultKeybinds()
    {
        return new Dictionary<string, KeyCode>
        {
            { "Jump", Jump },
            { "Interact", Interact },
            { "MoveForward", MoveForward },
            { "MoveBackward", MoveBackward },
            { "MoveLeft", MoveLeft },
            { "MoveRight", MoveRight }
        };
    }
}
