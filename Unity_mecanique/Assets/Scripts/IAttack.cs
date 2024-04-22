using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IAttack : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent OnAttackStart = new UnityEvent();

    // This allows to time the attack with the animation
    [Tooltip("This allows to time the attack with the animation")]
    public UnityEvent OnAnimationAttackStart = new UnityEvent();

    // public delegate void AttackEvent();
    public UnityEvent OnAttackFinished = new UnityEvent();

    public bool isAttacking { get; private set; } = false;
    public int Dammages = 300;

    // The animator can be used by any of the child attacks
    [SerializeField]
    protected Animator animator;

    void Start()
    {
        // with this, we can know if an attack is performing or not
        OnAttackStart.AddListener(() => isAttacking = true);
        OnAttackFinished.AddListener(() => isAttacking = false);

        // We get the animator (not necessary but can be used if the attack has animation)
        // animator = GetComponentInParent<Animator>();
        // if (animator == null)
        //     Debug.Log("on a pas réussi à récup l'animator :/");
    }

    // Update is called once per frame
    void Update() { }

    void func() { }

    public void DoAttack()
    {
        Debug.Log("l'attaque : " + name + " s'exécute !");
        OnAttackStart.Invoke();
    }

    void Reset() { }
}
