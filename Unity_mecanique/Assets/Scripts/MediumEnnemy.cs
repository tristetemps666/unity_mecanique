using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MediumEnnemy : MonoBehaviour, IDammagable, IHealth
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform Target;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(Target.position);
    }

    public void TakeDammage(int dammageAmmount) { }

    // Health manager
    public void ReduceHealth(int reduceAmount) { }

    public void AddHealth(int AddAmount) { }

    public bool IsDead()
    {
        return false;
    }
}
