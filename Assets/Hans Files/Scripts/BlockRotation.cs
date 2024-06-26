// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
// Author: Emirhan Bulut
// Date 6/17/2024 US

using UnityEngine;
using System;
public class BlockRotation : MonoBehaviour
{
    public bool canRotate;
    public GameObject player;
    private Rigidbody rb;

    [Tooltip("KeyCode for the Positive Y rotation")]
    public KeyCode positive_Rotation_Input;
    
    [Tooltip("KeyCode for the Negative Y rotation")]
    public KeyCode negative_Rotation_Input;

    [Tooltip("Rotation Speed of the Object, Recommended 32")]
    [SerializeField] private int RotationSpeed = 32;

    [Tooltip("Tolerance level for how close the rotation can get before we consider it true")]
    [SerializeField] private float tolerance;

    [Tooltip("Threshold for when the object starts shining. Based on y axis")]
    [SerializeField] private float brightnessThreshold;

    [SerializeField] AK.Wwise.Event playMovingSound;
    [SerializeField] AK.Wwise.Event stopMovingSound;

    bool isplayingSound = false;

    // Start is called before the first frame update
    void Start()
    {
        canRotate = true;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Rotate(bool rotate)
    {
        // Check if rotation is allowed and not overridden
        if (rotate && canRotate != false)
        {
            // Rotate positively around the Y-axis
            if (Input.GetKey(positive_Rotation_Input))
            {
                PlaySound(true);
                this.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0, Space.World);
            }
            // Rotate negatively around the Y-axis
            else if (Input.GetKey(negative_Rotation_Input))
            {
                PlaySound(true);
                this.transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0, Space.World);
            }
            else PlaySound(false);
        }
    }

    private void PlaySound(bool play)
    {
        if (play)
        {
            if (!isplayingSound)
            {
                playMovingSound.Post(gameObject);
                isplayingSound = true;
            }
        }
        else
        {
            stopMovingSound.Post(gameObject);
            isplayingSound = false;
        }
    }


    public bool RotationCorrect()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        if(rotation.y < 360 - tolerance && rotation.y > Math.Abs(tolerance))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private float DetermineBrightnessLevel()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        if(rotation.y < 360 - brightnessThreshold && rotation.y > Math.Abs(brightnessThreshold))
        {
            //Do Nothing
            return 0f;
        }
        else if(rotation.y > 360 - brightnessThreshold && rotation.y <= 360)
        {
            float absValue = 360 - rotation.y;
            float clampedValue = 1.0f - Mathf.InverseLerp(brightnessThreshold, 0.0f, absValue);

            //For tom :)
            //This will return a value between 0-1. 
            // ----- Debug.Log("Clamped Value: " + clampedValue);
            return clampedValue;
        }
        else if(rotation.y < Math.Abs(brightnessThreshold) && rotation.y > 0)
        {
            float clampedValue = 1.0f - Mathf.InverseLerp(brightnessThreshold, 0.0f, rotation.y);

            //For tom :)
            //This will return a value between 0-1. 
            // ----- Debug.Log("Clamped Value: " + clampedValue);
            return clampedValue;
        }
        else
        {
            return 0f;
        }
    }

    void OnTriggerStay(UnityEngine.Collider other)
    {
            if (other.gameObject == player)
            {
                if(Input.GetKey(KeyCode.E))
                {
                    player.GetComponent<ThirdPersonMovement>().canMove = false;
                    rb.constraints = RigidbodyConstraints.FreezeRotationX  | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePosition;
                    Rotate(canRotate);
                    //collision.gameObject.GetComponent<FakeThirdPersonMovement>().canMove = false;

                }
                else
                {
                    player.GetComponent<ThirdPersonMovement>().canMove = true;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
            }
    }

    


    // Trigger exit event handler
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            player.GetComponent<ThirdPersonMovement>().canMove = true;
            PlaySound(false);
        }
    }
}
