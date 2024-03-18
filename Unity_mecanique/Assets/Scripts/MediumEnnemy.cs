using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MediumEnnemy : MonoBehaviour, IDammagable
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform Target;

    private NavMeshAgent navMeshAgent;

    private BigEnnemiHeath health;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<BigEnnemiHeath>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(Target.position);
    }

    public void TakeDammage(int dammageAmmount)
    {
        health.ReduceHealth(dammageAmmount);
    }
}
