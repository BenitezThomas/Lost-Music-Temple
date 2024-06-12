// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
//Author: Emirhan Bulut
//Date 6/6/2024 US

using System.Collections.Generic;
using UnityEngine;

public class WheelPuzzle : MonoBehaviour
{
    // List to hold all WheelInteraction objects in the puzzle
    public List<WheelInteraction> cylinders;

    void Update()
    {
        // Check if all cylinders are in the correct state
        if (AllCylindersAreCorrect())
        {
            //This Part should be changed with a win mechanic whenever the game design allows to do so.
            Debug.Log("All Correct");
        }
    }

    private bool AllCylindersAreCorrect()
    {
        foreach (var cylinder in cylinders)
        {
            // If any cylinder is not correct, return false
            if (!cylinder.isCorrect)
            {
                return false;
            }
        }
        // If all cylinders are correct, return true
        return true;
    }
}
