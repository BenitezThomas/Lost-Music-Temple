// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
// Author: Emirhan Bulut
// Date 6/11/2024 US

using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [SerializeField] Transform camTransform;

    public GameObject player;
    private Rigidbody rb;
    private Transform ParentObject;
    public bool canMove;
    public bool inPos;
    public Transform correctPos;

    [Tooltip("Threshold for both axis to be considered in correct Position")]
    public float Tolerance = 5f;

    [Tooltip("Threshold for when the object starts shining. Based on collective range of pos.x and pos.z")]
    public float brightnessThreshold = 3f;

    [Tooltip("How fast the block & player Move // Recommended 6f")]
    [SerializeField] [Range(0f, 10f)] private float carrySpeed = 6f;

    [SerializeField] AK.Wwise.Event playMovingSound;
    [SerializeField] AK.Wwise.Event stopMovingSound;

    bool isplayingSound = false;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        ParentObject = transform.parent.GetComponent<Transform>();
    }

    void Update()
    {
        CheckPos();
        DetermineBrightnessLevel();
    }

    // Trigger enter event handler
    private void OnTriggerStay (Collider collision) 
    {
        ThirdPersonMovement thirdPersonMovement = player.GetComponent<ThirdPersonMovement>();
        if (collision.gameObject == player)
        {
            if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
            {
                if (canMove)
                {
                    Debug.Log("Pressing Grab");

                    thirdPersonMovement.canMove = false;

                    float vertical = Input.GetAxisRaw("Vertical");
                    float horizontal = Input.GetAxisRaw("Horizontal");

                    Vector2 input = new Vector2(horizontal, vertical);

                    if (input.magnitude > 0.1f)
                    {
                        Vector3 forward = camTransform.forward;
                        Vector3 right = camTransform.right;

                        forward.y = 0;
                        right.y = 0;

                        forward.Normalize();
                        right.Normalize();

                        // Calcula la dirección de movimiento
                        Vector3 moveDirection = forward * vertical + right * horizontal;

                        // Mueve el bloque en la dirección calculada

                        moveDirection = new Vector3(moveDirection.x, 0, 0);

                        transform.position += moveDirection * carrySpeed * Time.deltaTime;
                        player.transform.position += moveDirection * carrySpeed * Time.deltaTime;

                        PlaySound(true);
                    }
                }
                

                /*if(vertical == 1)
                {
                    PlaySound();
                    transform.position = new Vector3(transform.position.x + carrySpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    player.transform.position = new Vector3(player.transform.position.x + carrySpeed * Time.deltaTime, player.transform.position.y, player.transform.position.z);
                }
                else if(vertical == -1)
                {
                    PlaySound();
                    transform.position = new Vector3(transform.position.x - carrySpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    player.transform.position = new Vector3(player.transform.position.x - carrySpeed * Time.deltaTime, player.transform.position.y, player.transform.position.z);
                }

                if(horizontal == -1)
                {
                    PlaySound();
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + carrySpeed * Time.deltaTime);
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + carrySpeed * Time.deltaTime);
                }
                else if(horizontal == 1)
                {
                    PlaySound();
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z -  carrySpeed * Time.deltaTime);
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - carrySpeed * Time.deltaTime);
                }
                */
            }
            else
            {
                isplayingSound = false;
                stopMovingSound.Post(gameObject);
                thirdPersonMovement.canMove = true;
            }
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

    private void CheckPos()
    {

        if(this.transform.position.x > correctPos.position.x - Tolerance && this.transform.position.x < correctPos.position.x + Tolerance && this.transform.position.z > correctPos.position.z - Tolerance && this.transform.position.z < correctPos.position.z + Tolerance)
        {
            inPos = true;
        }
        else
        {
            inPos = false;
        }
    }

    private float DetermineBrightnessLevel()
    {

        Vector2 pointA = new Vector2(this.transform.position.x, this.transform.position.z);
        
        Vector2 pointB = new Vector2(correctPos.position.x, correctPos.position.z);

        Vector2 difference = pointA - pointB;

        float distance = difference.magnitude;

        if(distance < brightnessThreshold)
        {
            float clampedValue = 1.0f - Mathf.InverseLerp(brightnessThreshold, 0.0f, distance);

            //For tom :)
            //This will return a value between 0-1. 
            // ---- Debug.Log("Clamped Value: " + clampedValue);
            return clampedValue;
        }
        else
        {
            return 0f;
        }
    }

    // Trigger exit event handler
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<ThirdPersonMovement>().canMove = true;
            PlaySound(false);
        }
    }
}
