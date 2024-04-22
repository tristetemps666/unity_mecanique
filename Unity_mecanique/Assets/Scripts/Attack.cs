using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attack : IAttack
{
    // Start is called before the first frame update

    // public delegate void AttackEvent();
    public UnityEvent eventU;

    void Start()
    {
        // Start
        OnAttackStart.AddListener(func);
        OnAttackStart.AddListener(StartAnimation);

        // Finished
        OnAttackFinished.AddListener(func);
        OnAttackFinished.AddListener(Reset);

        // InvokeRepeating("StartAnimation", 2f, 10f);

        // DoAttack();
    }

    // Update is called once per frame
    void Update() { }

    void func()
    {
        Debug.Log("il se passe un truc");
    }

    public void StartAnimation()
    {
        animator.SetTrigger("TriggerRotation");
    }

    void Reset()
    {
        animator.ResetTrigger("TriggerRotation");
    }

    void EndAnim()
    {
        OnAttackFinished.Invoke();
    }
}
