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

    public float delayFirstWave = 1f;

    private NavMeshAgent navMeshAgent;

    private BigEnnemiHeath health;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        delayFirstWave = Random.Range(0, 4f);
        health = GetComponent<BigEnnemiHeath>();
        InvokeRepeating("CreateShockWave", 2f + delayFirstWave, shockWaveRate);
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
