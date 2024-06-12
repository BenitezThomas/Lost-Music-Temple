using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melody_Match_Controller : MonoBehaviour
{
    
    private bool isMelodyComplete = true;
    
    private int[] melodyArray;
    private int melodyArrayIndex = 0;

    public void OnEnable()
    {
        melodyArray = new int[transform.childCount];
    }

    public void CheckPuzzle()
    {
        if (isMelodyComplete)
        {
            Debug.Log("Melody Complete");

            this.gameObject.SetActive(false);
        }
        else
        {
            melodyArrayIndex = 0;
            isMelodyComplete = true;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Melody_Match_Object>().StopPlayingMelody();
            }
        }
    }

    public void AddMelody(int newMelody)
    {
        if (melodyArrayIndex != newMelody)
            isMelodyComplete = false;
        
        melodyArray[melodyArrayIndex] = newMelody;
        melodyArrayIndex++;

        if (melodyArrayIndex >= transform.childCount)
            CheckPuzzle();
    }

    public void RemoveMelody(int removeIndex)
    {
        melodyArrayIndex--;
        isMelodyComplete = true;

        int[] tempArray = new int[melodyArray.Length];

        if (removeIndex > 0)
        {
            for (int i = 0; i < melodyArrayIndex; i++)
            {
                if (i >= removeIndex)
                {
                    if (i != melodyArray[i + 1])
                        isMelodyComplete = false;

                    tempArray[i] = melodyArray[i + 1];
                }
                else
                {
                    if (i != melodyArray[i])
                        isMelodyComplete = false;

                    tempArray[i] = melodyArray[i];
                }
            }
        }

        melodyArray = tempArray;
    }

    public int GetMelodyArrayIndex()
    {
        return melodyArrayIndex;
    }
}
