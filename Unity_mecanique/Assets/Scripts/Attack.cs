using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update

    // public delegate void AttackEvent();
    public UnityEvent OnAttackFinished = new UnityEvent();
    public UnityEvent OnAttackStart = new UnityEvent();

    public UnityEvent eventU;

    public Animator animator;

    public int Dammages = 300;

    void Start()
    {
        animator = GetComponent<Animator>();

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

    public void DoAttack()
    {
        OnAttackStart.Invoke();
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
