using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] Animator animator;

    [Tooltip("Character Controller component attached to the player.")]
    [SerializeField] private CharacterController controller;

    [Tooltip("Transform of the camera to get the player's forward direction.")]
    [SerializeField] private Transform cam;

    [Tooltip("Walking speed of the player.")]
    [SerializeField] private float speed = 6f;

    [Tooltip("Running speed of the player.")]
    [SerializeField] private float runSpeed = 12f;
    private bool isRunning = false;

    [Tooltip("Time taken to smoothly turn the player.")]
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    [Tooltip("Height of the player's jump.")]
    [SerializeField] private float jumpHeight = 1.5f;

    [Tooltip("Gravity force applied to the player.")]
    [SerializeField] private float gravity = -9.81f;

    [Tooltip("Distance from the player to the ground check sphere.")]
    [SerializeField] private float groundDistance = 0.4f;

    [Tooltip("Layer mask to identify what is considered ground.")]
    [SerializeField] private LayerMask groundMask;

    private Vector3 velocity;
    private Rigidbody rb;
    private bool isGrounded;

    [Tooltip("Transform position to check if the player is grounded.")]
    [SerializeField] private Transform groundCheck;

    public bool canMove;

    void Start()
    {
        groundCheck = transform.Find("Ground Check");
        controller = GetComponent<CharacterController>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        Movement();
        Jump();
        Run();
    }

    //Checks if the player is grounded
    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    // Controls the player's movement 
    void Movement()
    {
        if (canMove) {
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                float currentSpeed = isRunning ? runSpeed : speed;
                controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);

                animator.SetFloat("Speed", 1);
            }
            else animator.SetFloat("Speed", 0);
        }
        else
        {
            //rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            animator.SetFloat("Speed", 0);
        }
    }


    // Controls player jumping 
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && canMove)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Checks if the player is running (holding down the Left Shift key) and adjusts speed.
    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public void JumpHigher(float jumpMultiplier)
    {
        velocity.y = Mathf.Sqrt(jumpHeight * jumpMultiplier * -2f * gravity);
        Jump();
    }
}
