using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // Start is called before the first frame update

    List<BulletSpawner> ListSpawners = new List<BulletSpawner>();
    bool isSpawning = false;

    void Start()
    {
        ListSpawners.AddRange(GetComponentsInChildren<BulletSpawner>());
        if (ListSpawners.Count == 0)
            Debug.Log("PAS DE SPAWNER A MANGER");
        StopSpawning();
    }

    // Update is called once per frame
    void Update() { }

    public void StartSpawning()
    {
        if (isSpawning)
            return;
        foreach (BulletSpawner spawner in ListSpawners)
        {
            spawner.StartSpawning();
        }
        isSpawning = true;
    }

    public void StopSpawning()
    {
        foreach (BulletSpawner spawner in ListSpawners)
        {
            spawner.StopSpawning();
        }
        isSpawning = false;
    }
}






////////////////////////////////////////////////
////////////////////////////////////////////////
////////////////////////////////////////////////
