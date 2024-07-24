using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] int menuScene;
    [SerializeField] GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                onPause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

   
    public void onPause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(menuScene);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
