using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField]
    private bool IsSpawning = false;

    [SerializeField]
    private GameObject Missile;

    [SerializeField]
    private float SpawnRate = 2f;

    [SerializeField]
    private int AmmountPerSalve = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSalveCoroutine());
    }

    // Update is called once per frame
    void Update() { }

    private void OnDisable() { }

    private void OnEnable() { }

    private void SpawnMissile()
    {
        GameObject go = Instantiate(Missile);
        // set the transform;
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
    }

    private IEnumerator SpawnSalveCoroutine()
    {
        for (int i = 0; i < AmmountPerSalve; i++)
        {
            SpawnMissile();
            yield return new WaitForSeconds(SpawnRate);
        }
    }

    public void StartSpawning()
    {
        IsSpawning = true;
        Debug.Log("on commence à spawn !! ");
        StartCoroutine(SpawnSalveCoroutine());
    }

    public void StopSpawning()
    {
        IsSpawning = false;
        Debug.Log("on arrete de spawn !! ");
        StopCoroutine(SpawnSalveCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * 40f);
        Gizmos.DrawCube(transform.position, Vector3.one * 2f);
    }

    public void SetupDammages(int newDammage)
    {
        Missile.GetComponent<SmallEnnemy>().dammage = newDammage;
    }
}
