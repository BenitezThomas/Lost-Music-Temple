using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicNoteCollect : MonoBehaviour
{
    [Tooltip("Scene to play the puzzle")]
    [SerializeField] int puzzleLevel;

    [SerializeField] UI_Manager ui_Manager;

    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material interactableMaterial;
    [SerializeField] Material notInteractableMaterial;

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
            SceneManager.LoadScene(puzzleLevel);
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
            Material[] materials = mesh.materials;
            materials[1].CopyPropertiesFromMaterial(interactableMaterial);

            mesh.materials = materials;

            ui_Manager.ShowInteractableButton(true);
        }
    }

    //Player move away detection
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            playerNear = false;

            Material[] materials = mesh.materials;
            materials[1].CopyPropertiesFromMaterial(notInteractableMaterial);

            mesh.materials = materials;

            ui_Manager.ShowInteractableButton(false);
        }    
    }
}
