using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

//Author: Eduardo Chiaratto (edu05)
//Date: 07/25/2024 BR

/// <summary>
/// Registers when player collider on piano hammer.
/// If he collide with a piano hammer, the player will be push
/// </summary>


public class PianoHammer : MonoBehaviour
{
    public bool isEnable;
    [Tooltip("The force that will be applied on player")]
    public float force = 10.0f;
    //a timer to stop push the player
    private float time;
    //will get to push player
    private ThirdPersonMovement characterController;
    [SerializeField] AK.Wwise.Event HitPlayer;

    // Update is called once per frame
    void Update()
    {
        if(characterController != null)
        {
            time += Time.deltaTime;

            //while time < 0.5f, will move player as if he was pushed
            if (time < 0.5f)
            {
                characterController.ForceMove((characterController.transform.position - transform.position).normalized, force);
            }
            else
            {
                characterController = null;
                time = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            HitPlayer.Post(gameObject);
            /*var player = other.transform.GetComponent<CharacterController>();
            if (isEnable)
            {
                player.Move(transform.up * force);
            }*/

            //get the script to push player
            characterController = other.transform.GetComponent<ThirdPersonMovement>();
        }
    }
}
