// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
// Author: Emirhan Bulut
// Date 6/11/2024 US

using UnityEngine;

public class FakeBlockMovement : MonoBehaviour
{

    // Correct location hitboxes
    public GameObject outerHitbox;
    public GameObject innerHitbox;

    // Object status booleans
    public bool boolOuterHitbox = false;
    public bool boolInnerHitbox = false;
    public GameObject player;
    private Rigidbody rb;

    [Header("Wwise")]
    [SerializeField] AK.Wwise.Event playSoundEvent;
    [SerializeField] AK.Wwise.Event stopSoundEvent;

    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }


    void OnCollisionStay(Collision collision)
    {
            if (collision.gameObject == player)
            {
                if(Input.GetKey(KeyCode.E) && !boolInnerHitbox)
                {
                    //canWorkNow = true;
                    if (!isMoving) playSoundEvent.Post(gameObject);
                    
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                    //collision.gameObject.GetComponent<FakeThirdPersonMovement>().canMove = false;
                    isMoving = true;

                }
                else
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    stopSoundEvent.Post(gameObject);
                    isMoving = false;
                }
            }
    }

    

    // Trigger enter event handler
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with the outer hitbox
        if (other.gameObject == outerHitbox)
        {
            boolOuterHitbox = true;
        }

        // Check if the object collided with the inner hitbox
        if (other.gameObject == innerHitbox)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            boolInnerHitbox = true;
        }
    }

    // Trigger exit event handler
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exited the outer hitbox
        if (other.gameObject == outerHitbox)
        {
            boolOuterHitbox = false;
        }

        // Check if the object exited the inner hitbox
        if (other.gameObject == innerHitbox)
        {
            boolInnerHitbox = false;
        }
    }
}
