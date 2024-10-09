using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce; // Add jump force
    public TMP_Text ScoreText;
    public TMP_Text WinText;
    public GameObject Wall;
    public Rigidbody rb;
    public int Score;
    public GameObject player;
    public Camera cam; // Reference to the camera
    private bool isGrounded; // Check if the player is grounded

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Score = 0;
        SetScoreText();
        WinText.text = "";
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        // Restart level
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        // Quit Game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");

        // Rotate the player around the y-axis for horizontal movement
        player.transform.Rotate(0, y, 0);

        // Rotate the player around the x-axis for vertical movement
        player.transform.Rotate(-x, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            Score++;
            SetScoreText();
            if (Score >= 5)
            {
                Wall.gameObject.SetActive(false);
            }
        }
        if (other.gameObject.tag == "danger")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void SetScoreText()
    {
        ScoreText.text = "Score: " + Score.ToString();
        if (Score >= 10)
        {
            WinText.text = "You Won! Press R to restart or ESC to quit game";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
