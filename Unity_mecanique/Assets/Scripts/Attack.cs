using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update

    public delegate void AttackEvent();
    public event AttackEvent OnAttackFinished;
    public event AttackEvent OnAttackStart;

    public Animator animator;

    public int Dammages = 300;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Start
        OnAttackStart += func;
        OnAttackStart += StartAnimation;

        // Finished
        OnAttackFinished += func;
        OnAttackFinished += Reset;

        // InvokeRepeating("StartAnimation", 2f, 10f);

        // DoAttack();
    }

    // Update is called once per frame
    void Update() { }

    void func()
    {
        Debug.Log("il se passe un truc");
    }

    void StartAnimation()
    {
        animator.SetTrigger("TriggerRotation");
    }

    public void DoAttack()
    {
        OnAttackStart();
    }

    void Reset()
    {
        animator.ResetTrigger("TriggerRotation");
    }

    void EndAnim()
    {
        OnAttackFinished();
    }
}
