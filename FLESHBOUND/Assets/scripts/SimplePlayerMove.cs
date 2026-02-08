using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;

    CharacterController controller;
    Animator animator;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        // CAMERA RELATIVE DIRECTION
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;

        Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;

        // MOVE
        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // ROTATE TOWARDS MOVEMENT (THIS FIXES SLIDING)
        if (moveDir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // GRAVITY
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ANIMATION (use input, not world noise)
        animator.SetFloat("Speed", inputDir.magnitude);
    }
}
