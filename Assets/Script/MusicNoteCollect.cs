using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicNoteCollect : MonoBehaviour
{
    [Tooltip("Scene to play the puzzle")]
    [SerializeField] int puzzleLevel;

    bool playerNear;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetNote();
    }

    //Get note when player's near and press E button
    void GetNote()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.CollectSong(puzzleLevel);
            //SceneManager.LoadScene(puzzleLevel);
            gameObject.SetActive(false);
            //Save Logic
        }
    }

    //Detection of player proximity
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("collider");
        }
    }

    //Player move away detection
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            playerNear = false; 
        }    
    }
}
