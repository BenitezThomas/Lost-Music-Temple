using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyMatch_Manager : MonoBehaviour
{
    [SerializeField] MusicalNote[] melodyNotes;
    [SerializeField] OpenGate puzzleGate;

    int nextMelodyNote = 0;

    public void CheckIfNoteIsCorrect(MusicalNote note)
    {
        if (melodyNotes[nextMelodyNote] == note) nextMelodyNote++;
        else
        {
            nextMelodyNote = 0;
        }

        CheckIfMelodyIsOver();
    }

    private void CheckIfMelodyIsOver()
    {
        if (nextMelodyNote == melodyNotes.Length)
        {
            puzzleGate.TryOpenGate();
            nextMelodyNote = 0;
        }
    }
}

public enum MusicalNote
{
    C,
    D,
    E,
    F,
    G,
    A,
    B,
}
