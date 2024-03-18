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

    public float shockWaveRate = 5f;

    private NavMeshAgent navMeshAgent;

    private BigEnnemiHeath health;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<BigEnnemiHeath>();
        InvokeRepeating("CreateShockWave", 2f, shockWaveRate);
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(Target.position);
    }

    void CreateShockWave()
    {
        GameObject newWave = Instantiate(shockWave);
        newWave.transform.position = transform.position;
    }

    public void TakeDammage(int dammageAmmount)
    {
        health.ReduceHealth(dammageAmmount);
    }
}
