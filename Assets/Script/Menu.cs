using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] int SceneNum;
    [SerializeField] GameObject settings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneNum);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Setting()
    {
        if (settings.activeSelf)
        {
            settings.SetActive(false);
        }
        else
        {
            settings.SetActive(true);
        }
    }


}
