//Author: Daniela Duwe
//Date 7/13/2024 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteHole : MonoBehaviour
{
    // Reference to the FlutePuzzle manager
    public FlutePuzzle puzzleManager;

    // Index of this hole in the puzzle
    private int index;

    // Reference to the MeshRenderer component
    private MeshRenderer meshRenderer;

    // Method to initialize the hole with the puzzle manager and its index
    public void Initialize(FlutePuzzle manager, int i)
    {
        puzzleManager = manager;
        index = i;
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }
        meshRenderer.enabled = true; // Set initial state to active
    }

    // Method called when the hole is clicked
    private void OnMouseDown()
    {
        // Toggle the active state of the MeshRenderer
        bool newState = !meshRenderer.enabled;
        meshRenderer.enabled = newState;

        // Notify the puzzle manager of the new state
        puzzleManager.CheckHoleState(index, newState);
    }
}
