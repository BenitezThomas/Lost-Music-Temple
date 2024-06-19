using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] RectTransform[] tutorialStageUI; 

    int currentTutorialStage = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ShowCurrentStageUI();
    }

    public void ShowCurrentStageUI()
    {
        for (int i = 0; i < tutorialStageUI.Length; i++)
        {
            if (i == currentTutorialStage) tutorialStageUI[i].gameObject.SetActive(true);
            else tutorialStageUI[i].gameObject.SetActive(false);
        }
    }

    public void ShowNextStageUI(int newStage)
    {
        if (newStage > currentTutorialStage)
        {
            currentTutorialStage++;
            ShowCurrentStageUI();
        }
    }
}
