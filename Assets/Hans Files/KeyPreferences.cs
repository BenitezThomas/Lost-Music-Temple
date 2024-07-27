using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class KeyPreferences : MonoBehaviour
{
    [SerializeField]
    private string KeyBindName;
    public TextMeshProUGUI buttonText;
    public PlayerKeybinds _playerKeybinds = new PlayerKeybinds();
    string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/KeyBinds.json";
    }
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
                }
            }
        }
    }

    public void SaveToJson()
    {
        string keybindSaveData = JsonUtility.ToJson(_playerKeybinds);
        System.IO.File.WriteAllText(filePath, keybindSaveData);
        LoadFromJson();
    }

    public void LoadFromJson()
    {
        if (File.Exists(filePath)) 
        {
            string keybindData = System.IO.File.ReadAllText(filePath);
            _playerKeybinds = JsonUtility.FromJson<PlayerKeybinds>(keybindData);
        }
        else
        {
            RestoreDefault();
        }
        
    }

    public void ChangeKey()
    {
        buttonText.text = "Awaiting Input";
    }
    
    public KeyCode GetKeyBindData(string keyName)
    {
        LoadFromJson();
        foreach(KeybindInfo keybindInfo in _playerKeybinds.keybindInfos)
        {
            if(keybindInfo.keybindName == keyName)
            {
                return keybindInfo.keyCode;                     
            }
        }
        Debug.Log("No Keybind with that name was found || Error recieved from : " + keyName + " Call");
        return KeyCode.F10;
    }

    public void RestoreDefault()
    {
        Dictionary<string, KeyCode> defaultKeybinds = DefaultKeybinds.GetDefaultKeybinds();

        _playerKeybinds.keybindInfos.Clear();
        foreach (var defaultKeybind in defaultKeybinds)
        {
            _playerKeybinds.keybindInfos.Add(new KeybindInfo
            {
                keybindName = defaultKeybind.Key,
                keyCode = defaultKeybind.Value,
                defaultKeyCode = defaultKeybind.Value
            });
        }
        SaveToJson();
        RefreshText();
    }

    private void RefreshText()
    {
        foreach(KeybindInfo keybindInfo in _playerKeybinds.keybindInfos)
        {
            if(keybindInfo.keybindName == KeyBindName)
            {
                buttonText.text = keybindInfo.keyCode.ToString();
                return;
            }
        }              
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
    public KeyCode defaultKeyCode;
}

