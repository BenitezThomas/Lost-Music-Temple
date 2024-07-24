// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
// Author: Han
// Last Update 7/10/2024 US

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPuzzleManager : MonoBehaviour
{
    [Tooltip("The correct sequence of keys to solve the puzzle.")]
    public List<GameObject> correctSequence; // Set this in the Inspector

    [Tooltip("The sequence of keys pressed by the player.")]
    public List<GameObject> playerSequence = new List<GameObject>();

    private int currentStep = 0; // Tracks the current step in the sequence

    [SerializeField] OpenGate gate;

    /// <summary>
    /// Registers a key press and checks it against the correct sequence.
    /// If the key is correct, it moves to the next step in the sequence.
    /// If the key is incorrect, it resets the sequence and starts over.
    /// </summary>
    /// <param name="key">The key GameObject that was pressed by the player.</param>
    public void RegisterKeyPress(GameObject key)
    {
        // Check if the pressed key matches the correct sequence at the current step
        if (key == correctSequence[currentStep])
        {
            currentStep++; 
            playerSequence.Add(key); // Add the key to the player's sequence

            if (currentStep == correctSequence.Count)
            {
                Debug.Log("Puzzle Complete!");
                gate.TryOpenGate();
            }
        }
        else
        {
            Debug.Log("Incorrect Sequence, Try Again!");
            currentStep = 0;
            playerSequence.Clear();
        }
    }
}
