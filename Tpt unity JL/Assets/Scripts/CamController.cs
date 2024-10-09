using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject PlayerObject;
    public Vector3 offset;
    public float mouseSensitivity;
    private float xRotation;
    private float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - PlayerObject.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Follow the player's position
        transform.position = PlayerObject.transform.position + offset;

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player around the y-axis
        PlayerObject.transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera around the x-axis and y-axis
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
