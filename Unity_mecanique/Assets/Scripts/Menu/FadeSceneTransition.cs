using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeSceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnFadeEnd;
    public UnityEvent OnFadeInverseEnd;

    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void EndFade()
    {
        OnFadeEnd.Invoke();
    }

    public void EndFadeInverse()
    {
        OnFadeInverseEnd.Invoke();
    }
}
