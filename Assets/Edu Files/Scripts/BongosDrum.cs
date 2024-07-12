using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Author: Eduardo Chiaratto (edu05)
//Date: 07/11/2024 BR

/// <summary>
/// Registers when player jumps in or walk in bongos top.
/// If he jump or fall in bongos top side, he will jump higher than normal.
/// </summary>

public class BongosDrum : MonoBehaviour
{
    public bool isEnable;
    [Tooltip("The number that will be multiplied when player jumps. The default to work when player jumps in bongo's drum is 2.")]
    public float jumpMultiplier = 2f;
    [Tooltip("The number that calculate if the player fall's velocity is enough to automatically jump. The higher the value, the greater the supported fall height. The default to work when player jumps with jumpMultiplier is 2 in bongo's drum is 6.5.")]
    public float fallFactor = 6.5f;
    [Tooltip("The number from the highest height the player fell. Your FallFactor need to be more than this.")]
    public float fallMaxVelocity;

    private void OnTriggerEnter(Collider other)
    {
        if (isEnable && other.tag == "Player")
        {
            var player = other.GetComponent<CharacterController>();
            fallMaxVelocity = player.velocity.magnitude;
            if (player.velocity.magnitude > 0 && player.velocity.magnitude < fallFactor && jumpMultiplier > 0)
            {
                var playerJump = player.GetComponent<ThirdPersonMovement>();
                playerJump.JumpHigher(jumpMultiplier);
                //jumpAgain = false;
            }
        }
    }
}
