using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPreferences : MonoBehaviour
{
    [SerializeField]
    private string KeyBindName;
    public TextMeshProUGUI buttonText;
    public PlayerKeybinds _playerKeybinds = new PlayerKeybinds();

    private void Start() 
    {
        buttonText = GetComponent<TextMeshProUGUI>();
        LoadFromJson();

        foreach(KeybindInfo keybindInfo in _playerKeybinds.keybindInfos)
        {
            if(keybindInfo.keybindName == KeyBindName)
            {
                buttonText.text = keybindInfo.keyCode.ToString();
            }
        }
    }

    private void Update()
    {                       
        if(buttonText.text == "Awaiting Input")
        {
            foreach(KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if(Input.GetKey(keycode))
                {
                    foreach(KeybindInfo keybindInfo in _playerKeybinds.keybindInfos)
                    {
                        if(keybindInfo.keybindName == KeyBindName)
                        {
                            keybindInfo.keyCode = keycode;
                            buttonText.text = keycode.ToString();
                            SaveToJson();                          
                            return;
                        }
                    }

                    // KeybindInfo tempKeyBindInfo = new KeybindInfo();
                    // tempKeyBindInfo.keybindName = KeyBindName;
                    // tempKeyBindInfo.keyCode = keycode;

                    // _playerKeybinds.keybindInfos.Add(tempKeyBindInfo);
                    

                }

            }
        }
    }

    public void SaveToJson()
    {
        string keybindSaveData = JsonUtility.ToJson(_playerKeybinds);
        Debug.Log("%%%% - " + keybindSaveData);
        string filePath = Application.persistentDataPath + "/KeyBinds.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, keybindSaveData);
        Debug.Log("Keybind File Created");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/KeyBinds.json";
        string keybindData = System.IO.File.ReadAllText(filePath);

        _playerKeybinds = JsonUtility.FromJson<PlayerKeybinds>(keybindData);
        Debug.Log(" system loaded : " + Application.persistentDataPath + "/KeyBinds.json");
        
    }

    public void ChangeKey()
    {
        buttonText.text = "Awaiting Input";
    }

}

[System.Serializable]
public class PlayerKeybinds
{
    public List<KeybindInfo> keybindInfos = new List<KeybindInfo>();

}

[System.Serializable]
public class KeybindInfo
{
    public string keybindName;
    public KeyCode keyCode;
}

