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
    public Dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        //will see that the collider that trigger this is for the player
        if(other.tag == "Player")
        {
            var player = other.GetComponent<CharacterController>();
            //will start the dialogue that is in this object
            DialogManager.instance.StartDialogue(dialogue);
            player.velocity.Set(0.0f, 0.0f, 0.0f);
        }
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
    [TextArea(10, 100)]
    public string line;
}

//a class that contains a list with DialogueLines
[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> lines = new List<DialogueLine>();
}
