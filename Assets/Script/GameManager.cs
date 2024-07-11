using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // List of levels (Scenes)
    //[SerializeField] private List<GameObject> notes;

    // Current level index
    public int currentNoteIndex = 0;

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

        //LoadGameState();
    }

    private void Start()
    {
        //LoadGameState(); 
    }

    public void CollectSong(int puzzleLevel)
    {
        if (puzzleLevel > 1)
        {
            // Deactivate the current note
            //notes[currentNoteIndex].SetActive(false);

            // Increment the note index
            currentNoteIndex++;

            // If there are more notes, activate the next one
        }

        // Load the puzzle level
        SceneManager.LoadScene(puzzleLevel);

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
