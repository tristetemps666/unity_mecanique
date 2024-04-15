using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MediumEnnemy : MonoBehaviour, IDammagable
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform Target;

    [SerializeField]
    private GameObject shockWave;

    [SerializeField]
    Animator animator;

    public float shockWaveRate = 5f;

    public float delayFirstWave = 1f;

    public float jumpTime = 1f;

    public float jumpAnimationOffset = 2f;

    private NavMeshAgent navMeshAgent;

    private BigEnnemiHeath health;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        delayFirstWave = Random.Range(0, 4f);
        health = GetComponent<BigEnnemiHeath>();
        animator = GetComponentInChildren<Animator>();

        InvokeRepeating("CreaShockWaveWithDelay", 2f + delayFirstWave, shockWaveRate);
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(Target.position);
    }

    void CreaShockWaveWithDelay()
    {
        animator.SetTrigger("Jump");

        // Delay to match with the jump animation
        // Invoke("CreateShockWave", jumpTime);
        Invoke("ResetJump", 3.8f); // time of the animation
    }

    public void CreateShockWave()
    {
        GameObject newWave = Instantiate(shockWave);
        newWave.transform.position = transform.position + transform.forward * jumpAnimationOffset;
        animator.ResetTrigger("Jump");
    }

    public void TakeDammage(int dammageAmmount, GameObject goHitPart)
    {
        health.ReduceHealth(dammageAmmount);
    }

    void ResetJump()
    {
        animator.ResetTrigger("Jump");
    }
}
