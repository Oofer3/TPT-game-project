using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Rigidbody rb;
    public Camera cam; // Reference to the camera
    private bool isGrounded; // Check if the player is grounded

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Convert movement direction to camera's local space
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 desiredMoveDirection = camForward * moveVertical + camRight * moveHorizontal;

        rb.AddForce(desiredMoveDirection * speed);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");

        // Rotate the player around the y-axis for horizontal movement
        transform.Rotate(0, y, 0);

        // Rotate the player around the x-axis for vertical movement
        transform.Rotate(-x, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
