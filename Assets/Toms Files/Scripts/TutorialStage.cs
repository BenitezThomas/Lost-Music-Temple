using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStage : MonoBehaviour
{
    [SerializeField] TutorialManager manager;
    [SerializeField] int stage;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) manager.ShowNextStageUI(stage);
    }
}
