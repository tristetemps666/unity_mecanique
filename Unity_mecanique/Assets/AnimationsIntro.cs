using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationsIntro : MonoBehaviour
{
    // Start is called before the first frame update
    private Animation animation;
    public UnityEvent OnFadeTitleEnd;

    void Start()
    {
        animation = GetComponent<Animation>();
        animation.Play("FadeInTitle");
    }

    // Update is called once per frame
    void Update() { }

    public void StartLoopingFade()
    {
        animation.Play("FlashAction");
    }

    public void EndFadeTitle()
    {
        OnFadeTitleEnd.Invoke();
    }
}
