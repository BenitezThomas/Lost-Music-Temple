using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Eduardo "edu05" Chiaratto
//Date: 07/16/2024 BR

/// <summary>
/// This is where the classes for DialogueSystem is, and where the player will trigger the npc's dialogue
/// If the player collide with a dialogue npc, will start the dialogue
/// </summary>

public class DialogueData : MonoBehaviour
{
    [Tooltip("The object that will show what key the player need to click to start the dialogue")]
    public GameObject pressText;
    [Tooltip("The dialogue that will apear after player click to start")]
    public Dialogue dialogue;
    private bool isReadyToDialogue;

    //Han Adjustment 7/24/2024
    public int dialogueNumber;
    public int npcID;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isReadyToDialogue)
        {
            //will start the dialogue that is in this object
            DialogueManager.instance.StartDialogue(dialogue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //will see that the collider that trigger this is for the player
        if(other.tag == "Player")
        {
            isReadyToDialogue = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //will see if will show the text above npc's head or not
        if (pressText && !DialogueManager.instance.isDialogueActive) pressText.SetActive(true);
        else if(pressText) pressText.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        //when player leave the area, end the dialogue and disable the npc's head text
        if (pressText) pressText.SetActive(false);
        DialogueManager.instance.EndDialogue();
        isReadyToDialogue = false;
    }
}

//the class for character that speaking
[System.Serializable]
public class DialogueCharacter
{
    public string characterName;
    public Sprite characterIcon;
}

//the class for the dialogue's lines
[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    public AudioSource characterAudio;
    [TextArea(10, 100)]
    public string line;
}

//a class that contains a list with DialogueLines
[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> lines = new List<DialogueLine>();
}
