using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Author: Eduardo "edu05" Chiaratto
//Date: 07/16/2024 BR

/// <summary>
/// This is the sciprt that will controll the actual dialogue
/// </summary>

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    //public Image characterIcon;
    [Tooltip("The panel that was the dialogue")]
    public GameObject dialoguePanel;
    [Tooltip("The text that will show the character who is talking")]
    public TextMeshProUGUI characterName;
    [Tooltip("The text that will show the actual dialogue")]
    public TextMeshProUGUI dialogue;

    //this is where will set the current dialogue line
    private DialogueLine currentLine;

    //this is the queue that he will use to organize the dialogues
    public Queue<DialogueLine> lineQueue = new Queue<DialogueLine>();

    [HideInInspector] public bool isDialogueActive;
    [Tooltip("The velocity the speed at which the text will be typed. The lower the value, the faster it will be")]
    public float typingSpeed = 0.5f;

    private void Awake()
    {
        //create a instance for DialogueManager
        if(!instance) instance = this;
    }

    void Update()
    {
        //when player click on left mouse button and have lines to show, will see if need to show the rest of the current line of dialogue or go to next line
        if (Input.GetMouseButtonDown(0) && lineQueue.Count >= 0)
        {
            if (dialogue.text == currentLine.line) DisplayNextDialogueLine();
            else
            {
                StopAllCoroutines();
                dialogue.text = currentLine.line;
            }
        }
    }

    //method that will start the dialogue
    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        lineQueue.Clear();

        foreach(DialogueLine line in dialogue.lines)
        {
            lineQueue.Enqueue(line);
        }

        DisplayNextDialogueLine();
    }

    //method that will change the line of dialogue for the next
    public void DisplayNextDialogueLine()
    {
        if(lineQueue.Count == 0) 
        {
            EndDialogue();
            return;
        }

        currentLine = lineQueue.Dequeue();

        //characterIcon.sprite = currentLine.character.characterIcon;
        characterName.text = currentLine.character.characterName;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    //enumerator which will write each letter in the text box, with a gap in each one, making it look like it is being typed
    IEnumerator TypeSentence(DialogueLine line)
    {
        dialogue.text = "";

        foreach (char letter in line.line.ToCharArray()) 
        { 
            dialogue.text += letter;
            yield return new WaitForSeconds(typingSpeed); 
        }
    }

    //method that will end the dialogue
    public void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
    }
}
