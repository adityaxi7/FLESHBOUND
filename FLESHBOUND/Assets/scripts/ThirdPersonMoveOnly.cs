using UnityEngine;

public class ThirdPersonMoveOnly : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float mouseSensitivity = 2f;
    public Transform cameraPivot;

    float xRot;
    float yRot;

    CharacterController controller;
    Camera cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraLook();
        Move();
    }

    void CameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        yRot += mouseX;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -60f, 60f);

        cameraPivot.rotation = Quaternion.Euler(xRot, yRot, 0f);
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraPivot.forward;
        Vector3 right = cameraPivot.right;

        forward.y = 0;
        right.y = 0;

        Vector3 moveDir = (forward.normalized * v + right.normalized * h);

        controller.Move(moveDir * moveSpeed * Time.deltaTime);
    }
}
