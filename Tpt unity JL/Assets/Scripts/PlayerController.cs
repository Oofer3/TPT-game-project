using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TMP_Text DoorText;
    public TMP_Text ScoreText;
    public TMP_Text WinText;
    public GameObject Door;
    public int Score;
    public GameObject player;

    private float doorTextDisplayTime = 3f; // Time in seconds to display the door text

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        SetScoreText();
        WinText.text = "";
        DoorText.text = "";
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            Score++;
            SetScoreText();
            if (Score >= 9)
            {
                Door.gameObject.SetActive(false);
                StartCoroutine(DisplayDoorText());
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

    IEnumerator DisplayDoorText()
    {
        DoorText.text = "Door Opened!";
        yield return new WaitForSeconds(doorTextDisplayTime);
        DoorText.text = "";
    }
}
