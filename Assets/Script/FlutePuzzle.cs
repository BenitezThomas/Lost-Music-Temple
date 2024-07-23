//Author: Daniela Duwe
//Date 7/13/2024 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlutePuzzle : MonoBehaviour
{
    // Array to track the current active states of the holes
    private bool[] currentStates;

    // Variable to track if the player is holding a block
    private bool isHoldingBlock = false;

    // List of holes that can be set in the inspector
    [Tooltip("List of hole objects in the puzzle")]
    [SerializeField] List<HoleInfo> holes = new List<HoleInfo>();

    // The correct sequence of active states for the holes
    [Tooltip("The correct sequence of active states for the holes (true for active, false for inactive)")]
    [SerializeField] List<bool> correctSequence = new List<bool>();
   
    // Reference to the player's transform
    [Tooltip("Reference to the player's transform.")]
    public Transform player;

    // Boolean to check if the flute is receiving wind
    [Tooltip("Boolean to check if the flute is receiving wind.")]
    public bool isReceivingWind = false;


    // Class to store information about each hole
    [System.Serializable]
    public class HoleInfo
    {
        [Tooltip("The GameObject representing the hole")]
        public GameObject holeObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the currentStates array with the size of the holes list
        currentStates = new bool[holes.Count];

        // Initialize each hole object and set its initial state
        for (int i = 0; i < holes.Count; i++)
        {
            if (holes[i].holeObject != null)
            {
                currentStates[i] = false;

                // Add the FluteHole script to each hole object and initialize it
                FluteHole holeScript = holes[i].holeObject.AddComponent<FluteHole>();
                holeScript.Initialize(this, i, player);
            }
        }
    }

    // Check the state of a specific hole
    public void CheckHoleState(int index, bool isActive)
    {
        // Update the current state of the hole
        currentStates[index] = isActive;

        Debug.Log("Current State Updated: " + string.Join(", ", currentStates));
        Debug.Log("Is Sequence Correct: " + IsSequenceCorrect());


        if (isReceivingWind)
        {
            // Check if the current configuration matches the correct sequence
            if (IsSequenceCorrect())
            {
                Debug.Log("Puzzle Done!");
                PlayCorrectMusic();
            }

            else
            {
                Debug.Log("Incorrect Note Played!");
                PlayIncorrectMusic();
            }
        }
        else
        {
            StopMusic();
        }
    }

    // Method to check if the current sequence matches the correct sequence
    bool IsSequenceCorrect()
    {
        // Iterate through each hole and check if the current state matches the correct state
        for (int i = 0; i < holes.Count; i++)
        {
            if (currentStates[i] != correctSequence[i])
            {
                return false;
                
            }
        }
        return true;
    }

    // Play the correct music
    void PlayCorrectMusic()
    {
        //audio

    }

    // Play the incorrect music
    void PlayIncorrectMusic()
    {
        //audio

    }

    // Stop the music
    void StopMusic()
    {
        //audio

    }

    // Method to check if the player is holding a block
    public bool IsHoldingBlock()
    {
        return isHoldingBlock;
    }

    // Method to set the player holding block status
    public void SetHoldingBlock(bool holding)
    {
        isHoldingBlock = holding;
    }

    // Method to set the wind status for the flute
    public void SetReceivingWind(bool receivingWind)
    {
        isReceivingWind = receivingWind;
    }
}
