using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private bool IsSpawning = false;

    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private float SpawnRate = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update() { }

    private void OnDisable() { }

    private void OnEnable() { }

    private void SpawnBullet()
    {
        GameObject go = Instantiate(Bullet);
        // set the transform;
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
    }

    private IEnumerator SpawnCoroutine()
    {
        while (IsSpawning)
        {
            SpawnBullet();
            yield return new WaitForSeconds(SpawnRate);
        }
    }

    public void StartSpawning()
    {
        IsSpawning = true;
        Debug.Log("on commence à spawn !! ");
        StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawning()
    {
        IsSpawning = false;
        Debug.Log("on arrete de spawn !! ");
        StopCoroutine(SpawnCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * 40f);
        Gizmos.DrawCube(transform.position, Vector3.one * 2f);
    }

    public void SetupDammages(int newDammage)
    {
        Bullet.GetComponent<Bullet>().dammage = newDammage;
    }
}
