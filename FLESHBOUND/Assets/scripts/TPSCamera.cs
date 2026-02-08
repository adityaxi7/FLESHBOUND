using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (player == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        // Camera up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40f, 70f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Player left/right
        player.Rotate(Vector3.up * mouseX);
    }
}
