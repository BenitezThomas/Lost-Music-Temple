// Code by Tom Benitez
// Last Date: 6/12/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AK.Wwise.Event openGateSound;

    bool isOpen = false;

    public void TryOpenGate()
    {
        if (!isOpen)
        {
            isOpen = true;

            animator.Play("OpenGate");

            openGateSound.Post(gameObject);
        }
    }
}
