using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melody_Match_Controller : MonoBehaviour
{
    private ArrayList melodyArrayList = new ArrayList();

    private bool isPuzzleComplete = false;

    public void CheckPuzzle()
    {
        for (int i = 0; i < melodyArrayList.Count; i++)
        {
            if (melodyArrayList.IndexOf(i) != i)
            {
                return;
            }                
        }

        Debug.Log("Melody Complete");
        isPuzzleComplete = true;
    }

    public void AddMelody(int newMelody)
    {        
        melodyArrayList.Add(newMelody);

        if (melodyArrayList.Count >= transform.childCount)
            CheckPuzzle();
    }

    public void RemoveMelody(int newMelody)
    {
        melodyArrayList.Remove(newMelody);
        melodyArrayList.TrimToSize();
        isPuzzleComplete = false;
    }

    public bool GetIsPuzzleComplete()
    {
        return isPuzzleComplete;
    }
}
