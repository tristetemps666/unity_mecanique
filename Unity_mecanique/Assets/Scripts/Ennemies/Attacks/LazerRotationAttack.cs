using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LazerRotationAttack : IAttack
{
    // Start is called before the first frame update

    // public delegate void AttackEvent();
    public UnityEvent eventU;

    void Start()
    {
        // Start
        // OnAttackStart.AddListener(func);
        // OnAttackStart.AddListener(StartAttackAnimation);

        // // Finished
        // // OnAttackFinished.AddListener(func);
        // OnAttackFinished.AddListener(StopAttackAnimation);

        // InvokeRepeating("StartAnimation", 2f, 10f);

        // DoAttack();
    }

    // Update is called once per frame
    void Update() { }

    void func()
    {
        Debug.Log("il se passe un truc");
    }

    public void StartAttackAnimation()
    {
        Debug.Log("ON FAIT ROTATE LE LAZER OUG OUG");

        animator.SetTrigger("TriggerRotationLazerAttack");
    }

    public void StopAttackAnimation()
    {
        animator.ResetTrigger("TriggerRotationLazerAttack");
    }

    void EndAnim()
    {
        OnAttackFinished.Invoke();
    }
}
