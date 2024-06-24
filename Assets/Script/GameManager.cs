using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // List of levels (Scenes)
    [SerializeField] private List<GameObject> notes;

    // Current level index
    private int currentNoteIndex = 0;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadGameState(); 

        // Initialize by deactivating all notes and activating the first one
        foreach (GameObject note in notes)
        {
            note.SetActive(false);
        }

        if (notes.Count > 0 && currentNoteIndex < notes.Count)
        {
            notes[currentNoteIndex].SetActive(true);
        }
    }

    public void CollectSong(int puzzleLevel)
    {
        // Deactivate the current note
        notes[currentNoteIndex].SetActive(false);

        // Load the puzzle level
        SceneManager.LoadScene(puzzleLevel);

        // Increment the note index
        currentNoteIndex++;

        // If there are more notes, activate the next one
        if (currentNoteIndex < notes.Count)
        {
            notes[currentNoteIndex].SetActive(true);
        }

        SaveGameState();


    }

    public void SaveGameState()
    {
        SaveSystem.SavePlayer(currentNoteIndex);
    }

    private void LoadGameState()
    {
        currentNoteIndex = SaveSystem.LoadLevel();
    }

}
