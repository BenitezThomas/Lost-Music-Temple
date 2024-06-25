using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyMatch_Manager : MonoBehaviour
{
    [SerializeField] MusicalNote[] melodyNotes;
    [SerializeField] OpenGate puzzleGate;

    [SerializeField] MeshRenderer feedbackMaterial;
    [SerializeField] float[] emissionSlider;

    int nextMelodyNote = 0;

    private void Start()
    {
        feedbackMaterial.material.SetFloat("_Slider", 0);
    }

    public void CheckIfNoteIsCorrect(MusicalNote note)
    {
        if (melodyNotes[nextMelodyNote] == note) nextMelodyNote++;
        else
        {
            nextMelodyNote = 0;
        }

        feedbackMaterial.material.SetFloat("_Slider", emissionSlider[nextMelodyNote]);
        CheckIfMelodyIsOver();
    }

    private void CheckIfMelodyIsOver()
    {
        if (nextMelodyNote == melodyNotes.Length)
        {
            puzzleGate.TryOpenGate();
            //nextMelodyNote = 0;
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
    cUp,
    dUp,
    eUp,
    fUp,
    gUp,
    aUp,
    bUp,
}
