using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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

    private GameObject player;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        Cursor.visible = false;
        LeavePause();
        player = playerInput.gameObject;
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

    public void LeavePause()
    {
        pauseMenu.SetActive(false);
        GameUI.SetActive(true);

        if (UIBlur != null)
            UIBlur.enabled = false;

        Time.timeScale = 1f;
        AudioListener.pause = false;
        playerInput.enabled = true;
    }

    void GoIntoPause()
    {
        // avoid bugs if the player pause while shooting
        player.GetComponent<Gun>().StopShooting();

        pauseMenu.SetActive(true);
        GameUI.SetActive(false);

        UIBlur.enabled = true;

        Time.timeScale = 0f;
        AudioListener.pause = true;
        playerInput.enabled = false;
    }

    public bool IsPlayerHoldingShoot()
    {
        return player.GetComponent<Gun>().isShootHold;
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void LoadLooseScene()
    {
        SceneManager.LoadScene("LooseScene");
    }
}
