// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
// Author: Emirhan Bulut
// Date 6/11/2024 US

using UnityEngine;

public class FakeBlockRotation : MonoBehaviour
{
    public bool canRotate;
    public bool rotateOverride;
    public GameObject player;
    private Rigidbody rb;

    [Tooltip("KeyCode for the Positive Y rotation")]
    public KeyCode positive_Rotation_Input;
    
    [Tooltip("KeyCode for the Negative Y rotation")]
    public KeyCode negative_Rotation_Input;

    [Tooltip("Rotation Speed of the Object, Recommended 32")]
    [SerializeField] private int RotationSpeed = 32;

    [Header("Wwise")]
    [SerializeField] AK.Wwise.Event playSoundEvent;
    [SerializeField] AK.Wwise.Event stopSoundEvent;

    private bool isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        rotateOverride = true;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Rotate(bool rotate)
    {
        // Check if rotation is allowed and not overridden
        if (rotate && rotateOverride != false)
        {
            // Rotate positively around the Y-axis
            if (Input.GetKey(positive_Rotation_Input))
            {
                this.transform.Rotate(0, RotationSpeed * Time.deltaTime, 0, Space.World);
                if (!isRotating) playSoundEvent.Post(gameObject);
                isRotating = true;
            }
            // Rotate negatively around the Y-axis
            else if (Input.GetKey(negative_Rotation_Input))
            {
                this.transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0, Space.World);
                if (!isRotating) playSoundEvent.Post(gameObject);
                isRotating = true;
            }
            else
            {
                stopSoundEvent.Post(gameObject);
                isRotating = false;
            }
        }
        else
        {
            stopSoundEvent.Post(gameObject);
            isRotating = false;
        }
    }


    void OnTriggerStay(UnityEngine.Collider other)
    {
            if (other.gameObject == player)
            {
                if(Input.GetKey(KeyCode.E))
                {
                    player.GetComponent<FakeThirdPersonMovement>().canMove = false;
                    rb.constraints = RigidbodyConstraints.FreezeRotationX  | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePosition;
                    Rotate(true);
                    //collision.gameObject.GetComponent<FakeThirdPersonMovement>().canMove = false;

                }
                else
                {
                    player.GetComponent<FakeThirdPersonMovement>().canMove = true;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
            }
    }

    


    // Trigger exit event handler
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            player.GetComponent<FakeThirdPersonMovement>().canMove = true;
        }
    }
}
