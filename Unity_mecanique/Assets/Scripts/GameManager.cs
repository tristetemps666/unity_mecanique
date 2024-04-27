using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioManager audioManager;
    bool isInPause = false;

    [SerializeField]
    private PlayerInput playerInput;

    void Start()
    {
        Cursor.visible = false;
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
        Time.timeScale = 1f;
        AudioListener.pause = false;
        playerInput.enabled = true;
    }

    void GoIntoPause()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        playerInput.enabled = false;
    }
}
