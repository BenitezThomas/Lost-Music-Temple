// Tom
//Latest Update: 06/12/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyMatch_Note : MonoBehaviour
{
    [SerializeField] MusicalNote note;
    [SerializeField] MelodyMatch_Manager puzzleManager;
    [SerializeField] AK.Wwise.Event stringNoteEvent;

    private bool isPlayerNear = false;

    private void PlayNote()
    {
        //play note sound
        stringNoteEvent.Post(gameObject);
        //Keep Track of the Next Note in the Melody List
        puzzleManager.CheckIfNoteIsCorrect(note);
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E)) PlayNote();
    }

    //Detection of player proximity
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("collider");
        }
    }

    //Player move away detection
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
