using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("The buttons")]
    [SerializeField]
    Button BackHomeButton;

    [SerializeField]
    Button RestartButton;

    [SerializeField]
    Button QuitButton;

    void Start()
    {
        SetupButtons();
    }

    // Update is called once per frame
    void Update()
    {
        // we cannot pause if we are holding shoot to avoid inputs bugs
    }

    void SetupButtons()
    {
        BackHomeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });

        // On Quit
        QuitButton.onClick.AddListener(Quit);

        RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level 1");
        });
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
