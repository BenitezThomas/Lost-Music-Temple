using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event footStep;

    private void Start()
    {
        
    }

    public void PlayFootStepSound()
    {
        footStep.Post(gameObject);
    }
}
