using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BigEnnemi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject smallEnnemiPrefab;

    [SerializeField]
    private Transform spawnPointSmallEnnemies;

    void Start()
    {
        InvokeRepeating("SpawnSalveSmalls", 3f, 5f);
    }

    // Update is called once per frame
    void Update() { }

    public Vector3 getTargetPosition(float prediction, Vector3 sourcePosition)
    {
        float distanceFactor = Vector3.Distance(sourcePosition, Player.transform.position);
        distanceFactor = Mathf.Lerp(0, 10, distanceFactor / 100);

        return Player.transform.position
            + prediction
                * distanceFactor
                * Player.GetComponent<CharacterMovement>().getPlayerVectorVelocity()
                * Time.deltaTime;
    }

    private void SpawnSmallEnnemy()
    {
        Debug.Log("spawn");
        GameObject newSmall = Instantiate(smallEnnemiPrefab);
        newSmall.transform.position = spawnPointSmallEnnemies.transform.position;
    }

    private IEnumerator SpawnSalveSmallsCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnSmallEnnemy();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void SpawnSalveSmalls()
    {
        StartCoroutine(SpawnSalveSmallsCoroutine());
    }
}
