using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

//Author: Eduardo Chiaratto (edu05)
//Date: 07/13/2024 BR

/// <summary>
/// Registers when player jumps in or walk in bongos top.
/// If he jump or fall in bongos top side, he will jump higher than normal.
/// </summary>

public class BongosDrum : MonoBehaviour
{
    public bool isEnable;
    [Tooltip("The number that will be multiplied when player jumps. The default to work when player jumps in bongo's drum is 2.")]
    public float jumpMultiplier = 2f;
    [Tooltip("The number that calculate if the player fall's velocity is enough to automatically jump. The higher the value, the greater the supported fall height.")]
    public float fallFactor = 1;
    [Tooltip("The number from velocity of the highest height the player fell. Your Fall Factor need to be greater than this value")]
    public float fallMaxVelocity;
    public bool isJumpAgain = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isEnable)
        {
            var player = other.GetComponent<CharacterController>();

            //will see if active the jump loop
            if (player.collisionFlags == CollisionFlags.None && -player.velocity.y / 6.5f < fallFactor && jumpMultiplier > 0)
            {
                isJumpAgain = true;
            }else if (player.collisionFlags != CollisionFlags.None && player.velocity.y == 0)
            {
                isJumpAgain = false;
            }

            //will see if the player y velocity is less than Fall Factor
            if(-player.velocity.y / 6.5f > fallFactor) isJumpAgain = false;
        }
    }
}
