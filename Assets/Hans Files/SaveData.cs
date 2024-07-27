using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public LevelInfo levelInfo = new LevelInfo();
    public GameObject player;

    private KeyPreferences keyPreferences;
    public KeyPreferences keyInput;
    private void Awake() 
    {
        player = GameObject.FindWithTag("Player");
        LoadFromJson();
        Debug.Log("Start Load Executed Succesfully");
    }


    private void Update()
    {

        //Tests here
        //
        //  \
        //  V
        // if(Input.GetKeyDown(KeyCode.G))
        // {
        //     SaveToJson();
        // }
        // if(Input.GetKeyDown(KeyCode.H))
        // {
        //     SaveGameProgress();
        // }
        // if(Input.GetKeyDown(KeyCode.J))
        // {
        //     LoadGameSave();
        // }
        // if(Input.GetKeyDown(KeyCode.K))
        // {
        //     LoadFromJson();
        // }

        

    }

    public void SaveToJson()
    {
        string gameSaveData = JsonUtility.ToJson(levelInfo);
        string filePath = Application.persistentDataPath + "/SaveData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, gameSaveData);
        Debug.Log("Save File Created");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        string gameSaveData = System.IO.File.ReadAllText(filePath);

        levelInfo = JsonUtility.FromJson<LevelInfo>(gameSaveData);
        Debug.Log("Save system loaded save file from location: " + Application.persistentDataPath + "/SaveData.json");
        //LoadGameSave();
        
    }

    public void SaveGameProgress()
    {
        //create a list of temporary dialogue infos
        List<DialogueInfo> dialogueInfos= new List<DialogueInfo>();
        
        //Grab the Scene and put it into levelInfo
        Scene scene = SceneManager.GetActiveScene();
        levelInfo.lastLoadedScene = scene.name;

        //Grab the Transform of the player and give it to levelInfo
        try 
        {
            Transform playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
            levelInfo.playerTransform = playerTransform;
        }
        catch(Exception ex)
        {
            Debug.Log(ex.ToString());
        }
        
        //Grab all NPC tagged objects in the scene
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag ("NPC");
        dialogueInfos = levelInfo.dialogueInformations;

        foreach(GameObject npc in npcObjects)
        {
            foreach(DialogueInfo info in levelInfo.dialogueInformations)
            {
                if(npc.GetComponent<DialogueData>().npcID == info.npcID)
                {
                    info.dialogueNumber = npc.GetComponent<DialogueData>().dialogueNumber;
                }
                
                
            }
            
        }

       //Code below could be activated if you would like to display the dialog infos list. 
       //                     /
       //                   /
       //                  V


        // foreach(DialogueInfo info in dialogueInfos)
        // {
        //     Debug.Log(info.npcID);
        //     Debug.Log(info.dialogueNumber);
        // }


        //grab the DialogueInfos list and push it into LevelInfo.dialogueInformations
        levelInfo.dialogueInformations = dialogueInfos;

        Debug.Log("NPC Dialogue Saved");

    }

    public void LoadGameSave()
    {
        try
        {
            SceneManager.LoadScene(levelInfo.lastLoadedScene);
            Debug.Log("Scene Data Loaded");
            player.transform.position = levelInfo.playerTransform.position;
            Debug.Log("Player Position Data Loaded");
        }
        catch (Exception ex)
        {
            Debug.Log("Player Positon or Scene Data had an error while loading error type: " + ex.ToString());
        }
        
        //Grab all NPC tagged objects in the scene
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag ("NPC");

        foreach(GameObject npc in npcObjects)
        {
            foreach(DialogueInfo npcData in levelInfo.dialogueInformations)
            {
                if(npc.GetComponent<DialogueData>().npcID == npcData.npcID)
                {
                    npc.GetComponent<DialogueData>().dialogueNumber = npcData.dialogueNumber;
                    Debug.Log("NPC Dialogues Data Loaded");
                }
            }
        }

        Debug.Log("NPC Dialogues Data Loaded");

    }
}



[System.Serializable]
public class LevelInfo
{
    public string lastLoadedScene;
    public Transform playerTransform;
    //public Transform playerLocation;
    public List<DialogueInfo> dialogueInformations = new List<DialogueInfo>();
    public int floorUnlocked;
    
}

[System.Serializable]
public class DialogueInfo
{
    public int npcID;
    public int dialogueNumber;
    
}
