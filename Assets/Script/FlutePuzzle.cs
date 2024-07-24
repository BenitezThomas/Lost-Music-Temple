//Author: Daniela Duwe
//Date 7/13/2024 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlutePuzzle : MonoBehaviour
{
    // List of holes that can be set in the inspector
    [Tooltip("List of hole objects in the puzzle")]
    [SerializeField] List<HoleInfo> holes = new List<HoleInfo>();

    // The correct sequence of active states for the holes
    [Tooltip("The correct sequence of active states for the holes (true for active, false for inactive)")]
    [SerializeField] List<bool> correctSequence = new List<bool>();

    // Array to track the current active states of the holes
    private bool[] currentStates;

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
                // Set the initial state of the hole to active
                holes[i].holeObject.SetActive(true);
                currentStates[i] = true;

                // Add the FluteHole script to each hole object and initialize it
                FluteHole holeScript = holes[i].holeObject.AddComponent<FluteHole>();
                holeScript.Initialize(this, i);
            }
        }
    }

    // Method to check the state of a specific hole
    public void CheckHoleState(int index, bool isActive)
    {
        // Update the current state of the hole
        currentStates[index] = isActive;

        // Check if the current configuration matches the correct sequence
        if (IsSequenceCorrect())
        {
            Debug.Log("Puzzle Done!");
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
}
