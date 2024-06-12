// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
// Author: Emirhan Bulut
// Date 6/11/2024 US

using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    // Booleans for function controls
    public bool xAxisCanMove;
    public bool yAxisCanMove;
    public bool zAxisCanMove;
    public bool canRotate;

    // Movement input variables
    public KeyCode positive_X_Input;
    public KeyCode negative_X_Input;
    public KeyCode positive_Y_Input;
    public KeyCode negative_Y_Input;
    public KeyCode positive_Z_Input;
    public KeyCode negative_Z_Input;

    // Rotation input variables
    public KeyCode positive_Rotation_Input;
    public KeyCode negative_Rotation_Input;

    // Recommended rotation speed: 32
    [SerializeField] private int RotationSpeed;
    
    // Recommended movement speed: 7
    [SerializeField] private int MoveSpeed;

    // Correct location hitboxes
    public GameObject outerHitbox;
    public GameObject innerHitbox;

    // Object status booleans
    public bool boolOuterHitbox = false;
    public bool boolInnerHitbox = false;
    
    public bool rotateOverride;
    
    // Start is called before the first frame update
    void Start()
    {
        rotateOverride = true;
        if (canRotate == true) {
            boolInnerHitbox = true;
            boolOuterHitbox = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Call move and rotate functions
        Move(xAxisCanMove, yAxisCanMove, zAxisCanMove);
        Rotate(canRotate);
    }

    // Function to handle rotation
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

    // Function to handle movement along the specified axes
    private void Move(bool xCondition, bool yCondition, bool zCondition)
    {
        // Move along the X-axis
        if (xAxisCanMove)
        {
            if (Input.GetKey(positive_X_Input))
            {
                this.transform.position = new Vector3(transform.position.x + MoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else if (Input.GetKey(negative_X_Input))
            {
                this.transform.position = new Vector3(transform.position.x - MoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }

        // Move along the Y-axis
        if (yAxisCanMove)
        {
            if (Input.GetKey(positive_Y_Input))
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y + MoveSpeed * Time.deltaTime, transform.position.z);
            }
            else if (Input.GetKey(negative_Y_Input))
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y - MoveSpeed * Time.deltaTime, transform.position.z);
            }
        }

        // Move along the Z-axis
        if (zAxisCanMove)
        {
            if (Input.GetKey(positive_Z_Input))
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + MoveSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(negative_Z_Input))
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - MoveSpeed * Time.deltaTime);
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
            xAxisCanMove = false;
            yAxisCanMove = false;
            zAxisCanMove = false;
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
