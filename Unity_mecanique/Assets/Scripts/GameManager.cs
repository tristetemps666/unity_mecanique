using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioManager audioManager;
    bool isInPause = false;

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject GameUI;

    [SerializeField]
    private Volume UIBlur;

    void Start()
    {
        Cursor.visible = false;
        LeavePause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
        {
            if (isInPause)
            {
                LeavePause();
            }
            else
            {
                GoIntoPause();
            }
            isInPause = !isInPause;
        }
    }

    void OnApplicationQuit()
    {
        GlobalVariables.IsQuitting = true;
    }

    void LeavePause()
    {
        pauseMenu.SetActive(false);
        GameUI.SetActive(true);
        UIBlur.enabled = false;

        Time.timeScale = 1f;
        AudioListener.pause = false;
        playerInput.enabled = true;
    }

    void GoIntoPause()
    {
        pauseMenu.SetActive(true);
        GameUI.SetActive(false);
        UIBlur.enabled = true;

        Time.timeScale = 0f;
        AudioListener.pause = true;
        playerInput.enabled = false;
    }
}
