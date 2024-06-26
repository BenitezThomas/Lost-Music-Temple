using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] RectTransform interactableUI;

    private void Start()
    {
        
    }

    public void ShowInteractableButton(bool show)
    {
        interactableUI.gameObject.SetActive(show);
    }
}
