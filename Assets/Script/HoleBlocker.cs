// Author: Daniela Duwe
// Date: 7/23/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBlocker : MonoBehaviour
{
    // Indicates if the block is currently held by the player
    private bool isHeld = false;

    // Reference to the player's transform
    private Transform player;

    // Static variable to check if the player is holding a block
    private static bool playerHolding = false;

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            transform.position = player.position;
        }
    }

    // Method called when the block is clicked by the player
    private void OnMouseDown()
    {
        // Find the FlutePuzzle instance
        FlutePuzzle flutePuzzle = Object.FindAnyObjectByType<FlutePuzzle>();

        // Check if the block is not held and the player is not holding any other block
        if (!isHeld && !flutePuzzle.IsHoldingBlock())
        {
            // Get the player's transform and set the block as held
            player = GameObject.FindGameObjectWithTag("PlayerHold").transform;
            isHeld = true;
            flutePuzzle.SetHoldingBlock(true);
        }
        else
        {
            isHeld = false;
            playerHolding = false;

        }
    }

    //Check if the block is currently held
    public bool IsHeld()
    {
        return isHeld;

    }

}
