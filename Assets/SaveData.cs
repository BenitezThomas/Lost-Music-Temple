using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public LevelInfo levelInfo = new LevelInfo();
    public GameObject player;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            SaveToJson();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadFromJson();
        }
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
        Debug.Log("Data Has Been Loaded");
        LoadGameSave();
    }

    public void SaveGameProgress()
    {
        //create a list of temporary dialogue infos
        List<DialogueInfo> dialogueInfos= new List<DialogueInfo>();
        
        //Grab the Scene and put it into levelInfo
        Scene scene = SceneManager.GetActiveScene();
        levelInfo.lastLoadedScene = scene.name;

        //Grab the Transform of the player and give it to levelInfo
        Transform playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        levelInfo.playerTransform = playerTransform;
        
        //Grab all NPC tagged objects in the scene
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag ("NPC");

        foreach(GameObject npc in npcObjects)
        {

            // For Here We need to grab the NPC Dialogue Script and grab the dialogue Number from it. 
            // Lets Asssume this magical variable is that integer

            int randomNpcID = UnityEngine.Random.Range(0,10);
            int randomDialogueNumber = UnityEngine.Random.Range(0,10);

            DialogueInfo tempDialog = new DialogueInfo();
            tempDialog.npcID = randomNpcID;
            tempDialog.dialogueNumber = randomDialogueNumber;    


            dialogueInfos.Add(tempDialog);
        }

        //grab the DialogueInfos list and push it into LevelInfo.dialogueInformations
        levelInfo.dialogueInformations = dialogueInfos;
        dialogueInfos.Clear();



        Debug.Log("NPC Dialogue Saved");

    }

    public void LoadGameSave()
    {
        SceneManager.LoadScene(levelInfo.lastLoadedScene);
        player.transform.position = levelInfo.playerTransform.position;

        //Grab all NPC tagged objects in the scene

        foreach(DialogueInfo npcData in levelInfo.dialogueInformations)
        {
            GameObject[] npcObjects = GameObject.FindGameObjectsWithTag ("NPC");

            foreach(GameObject npc in npcObjects)
            {
            //    if(npcData.npcID == npc.ID)
            //    {
                    //npc.dialogueNumber = npcData.dialogueNumber
            //    }
            }
        }
    }
}



[System.Serializable]
public class LevelInfo
{
    public string lastLoadedScene;
    public Transform playerTransform;
    //public Transform playerLocation;
    public List<DialogueInfo> dialogueInformations = new List<DialogueInfo>();    
    
}

[System.Serializable]
public class DialogueInfo
{
    public int npcID;
    public int dialogueNumber;
    
}
