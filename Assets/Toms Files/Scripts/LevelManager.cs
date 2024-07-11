using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] GameObject[] levels;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }

        if (levels.Length > 0 && GameManager.Instance.currentNoteIndex < levels.Length) levels[GameManager.Instance.currentNoteIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
