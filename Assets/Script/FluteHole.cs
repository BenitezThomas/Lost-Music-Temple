// Author: Daniela Duwe
// Date: 7/13/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteHole : MonoBehaviour
{
    // Reference to the FlutePuzzle manager
    [Tooltip("Reference to the FlutePuzzle manager.")]
    public FlutePuzzle puzzleManager;

    // Index of this hole in the puzzle
    private int index;

    // Indicates if this hole is currently blocked
    private bool isBlocked;

    // Reference to the current object blocking the hole
    private GameObject currentBlocker;

    // Reference to the player's transform
    [Tooltip("Reference to the player's transform.")]
    public Transform player;

    // The radius within which the player can interact with the hole
    [Tooltip("The radius within which the player can interact with the hole.")]
    public float interactionRadius = 2f;

    // Indicates if the player is blocking the hole
    private bool playerBlocking;

    // Method to initialize the hole with the puzzle manager and its index
    public void Initialize(FlutePuzzle manager, int i, Transform playerTransform)
    {
        puzzleManager = manager;
        index = i;
        player = playerTransform;
    }

    // Method called when another collider stays within the trigger collider
    private void OnTriggerStay(Collider other)
    {
        HoleBlocker holeBlocker = other.GetComponent<HoleBlocker>();

        // Check if the collider is a blocker and the player is within interaction radius
        if (other.CompareTag("Blocker") && holeBlocker != null)
        {
            if (Vector3.Distance(player.position, transform.position) <= interactionRadius)
            {
                // Block the hole if the player is holding the blocker and clicks
                if (holeBlocker.IsHeld() && !isBlocked && Input.GetMouseButtonDown(0))
                {
                    isBlocked = true;
                    other.transform.position = transform.position;
                    holeBlocker.enabled = false;
                    puzzleManager.SetHoldingBlock(false); // Release the block from the player
                    currentBlocker = other.gameObject;

                    // Notify the puzzle manager about the new state
                    puzzleManager.CheckHoleState(index, true);
                }
                // Unblock the hole if the player clicks on the currently blocked hole
                else if (isBlocked && currentBlocker == other.gameObject && !puzzleManager.IsHoldingBlock() && Input.GetMouseButtonDown(0))
                {
                    isBlocked = false;
                    currentBlocker.GetComponent<HoleBlocker>().enabled = true;
                    puzzleManager.SetHoldingBlock(true);
                    currentBlocker = null;

                    // Notify the puzzle manager about the new state
                    puzzleManager.CheckHoleState(index, false);
                }
            }
        }

        // Check if the player is within the interaction radius and not blocking the hole
        if (other.CompareTag("Player") && !playerBlocking && !isBlocked)
        {
            playerBlocking = true;
            puzzleManager.CheckHoleState(index, true);
        }
    }

    // Method called when another collider exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        // If the player exits the trigger and is not blocking the hole, update the state
        if (other.CompareTag("Player") && playerBlocking && !isBlocked)
        {
            playerBlocking = false;
            puzzleManager.CheckHoleState(index, false);
        }
    }
}
