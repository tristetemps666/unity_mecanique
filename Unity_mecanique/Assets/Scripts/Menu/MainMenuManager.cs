using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("The Menus GameObjects")]
    [SerializeField]
    GameObject MenuHome;

    [SerializeField]
    GameObject MenuOption;

    [SerializeField]
    GameObject MenuCredit;

    GameObject CurrentMenu;

    [Header("The buttons")]
    [SerializeField]
    Button PlayButton;

    [SerializeField]
    Button OptionButton;

    [SerializeField]
    Button CreditButton;

    [SerializeField]
    Button QuitButton;

    [SerializeField]
    Button BackMenuButton;

    void Start()
    {
        InitMenu();
        SetupButtons();
    }

    // Update is called once per frame
    void Update()
    {
        // we cannot pause if we are holding shoot to avoid inputs bugs
        if (
            Input.GetKeyDown(KeyCode.Escape)
            && (GameManager.Instance == null || !GameManager.Instance.IsPlayerHoldingShoot())
        )
        {
            if (CurrentMenu != MenuHome)
            {
                InitMenu();
            }
        }
    }

    void InitMenu()
    {
        MenuHome.SetActive(true);
        MenuOption.SetActive(false);
        MenuCredit.SetActive(false);

        CurrentMenu = MenuHome;
    }

    void EnableMenu(GameObject newMenuEnabled)
    {
        CurrentMenu.SetActive(false);
        CurrentMenu = newMenuEnabled;
        CurrentMenu.SetActive(true);
    }

    void SetupButtons()
    {
        // Play
        PlayButton.onClick.AddListener(StartGame);

        // On Option
        OptionButton.onClick.AddListener(() =>
        {
            EnableMenu(MenuOption);
        });

        // On Credits
        CreditButton.onClick.AddListener(() =>
        {
            EnableMenu(MenuCredit);
        });

        // On Quit
        QuitButton.onClick.AddListener(Quit);

        BackMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MenuScene");
        });
    }

    void StartGame()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            GameManager.Instance.LeavePause();
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
