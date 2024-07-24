using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] int floor;
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

        if (levels.Length > 0)
        {
            int index = GameManager.Instance.currentNoteIndex;
            if (floor == 2) index -= 3;
            levels[index].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
