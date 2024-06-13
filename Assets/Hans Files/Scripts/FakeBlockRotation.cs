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
    public KeyCode positive_Rotation_Input;
    public KeyCode negative_Rotation_Input;
    [SerializeField] private int RotationSpeed = 32;

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
            }
            // Rotate negatively around the Y-axis
            else if (Input.GetKey(negative_Rotation_Input))
            {
                this.transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0, Space.World);
            }
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
