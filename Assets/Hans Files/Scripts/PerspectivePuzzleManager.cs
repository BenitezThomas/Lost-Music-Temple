// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
// Author: Emirhan Bulut
// Date 6/11/2024 US

using System;
using System.Collections.Generic;
using UnityEngine;

public class PerspectivePuzzleManager : MonoBehaviour
{
    // List of GameObjects representing the blocks in the puzzle
    public List<GameObject> Blocks;
    // Boolean to determine if the script has finished execution
    private bool finishScript = false;
    // The amount of tolerance allowed for the rotation. Serialized field to adjust in the Unity editor.
    [SerializeField] private float tolerance;

    // Update is called once per frame
    void Update()
    {
        // Check if the script has not finished execution
        if (!finishScript) {
            // If all block rotations and positions are correct, print a debug message
            if (AllBlockRotationsAreCorrect() && AllBlockPositionsAreCorrect()) { 
                Debug.Log("All True"); 
            }
        }
    }

    // Function to check if all block positions are correct
    private bool AllBlockPositionsAreCorrect()
    {
        // Iterate through each block in the list
        foreach (var block in Blocks)
        {
            // Get the BlockMovement component of the current block
            BlockMovement tempBlockCode = block.GetComponent<BlockMovement>();
            // If the block's inner hitbox is not correct, return false
            if (!tempBlockCode.boolInnerHitbox)
            {
                return false;
            }
        }
        // If all block positions are correct, return true
        return true;
    }

    // Function to check if all block rotations are correct
    private bool AllBlockRotationsAreCorrect()
    {   
        // Iterate through each block in the list
        foreach (var block in Blocks)
        {
            // Get the BlockMovement component of the current block
            BlockMovement tempBlockCode = block.GetComponent<BlockMovement>();
            // If the block can rotate
            if (tempBlockCode.canRotate == true)
            {
                // Get the rotation of the block
                Vector3 rotation = tempBlockCode.transform.rotation.eulerAngles;
                // Check if the rotation is within the allowed tolerance range
                if (rotation.y < 360 - tolerance && rotation.y > Math.Abs(tolerance))
                {
                    // If not within tolerance, return false
                    return false;
                }
                else
                {
                    // If within tolerance, set rotateOverride to false
                    tempBlockCode.rotateOverride = false;
                }
            }
        }
        // If all block rotations are correct, return true
        return true;
    }
}