using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Animation FadeAnimation;
    public bool canUseKey = false;

    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && canUseKey)
        {
            Debug.Log("on fait le fade !");
            // The next scene will be loaded after this animation is played
            FadeAnimation.Play("Fade");
        }
    }

    public void LoadMenuScene() => SceneManager.LoadScene("MenuScene");

    public void EnableKey()
    {
        Debug.Log("on peut maintenant appuyer");
        canUseKey = true;
    }
}
