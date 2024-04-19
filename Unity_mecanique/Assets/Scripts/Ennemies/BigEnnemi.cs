using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigEnnemi : MonoBehaviour, IDammagable
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject smallEnnemiPrefab;

    [SerializeField]
    private Transform spawnPointSmallEnnemies;

    [SerializeField]
    private Material dammageMaterial;

    [SerializeField]
    private Material defaultMaterial;

    private Material material;

    private BigEnnemiHeath health;

    [SerializeField]
    Attack LazerAttack;

    void Start()
    {
        // InvokeRepeating("SpawnSalveSmalls", 3f, 5f);
        defaultMaterial = GetComponent<Renderer>().material;
        material = GetComponent<Renderer>().material;

        health = GetComponent<BigEnnemiHeath>();

        LazerAttack.gameObject.SetActive(false);

        ChooseNextAttackDelayed();
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
        ChooseNextAttackDelayed();
    }

    private void SpawnSalveSmalls()
    {
        StartCoroutine(SpawnSalveSmallsCoroutine());
    }

    public void TakeDammage(int dammageAmount, GameObject goHitPart)
    {
        Debug.Log("hitted");
        float criticalFactor = 1f;

        if (goHitPart != null)
        {
            // If we hit a weakPoint, we add the critical factor
            criticalFactor = goHitPart.CompareTag("WeakPoint")
                ? GlobalVariables.criticalFactor
                : 1f;
            Debug.Log("partie hit : " + goHitPart);
        }
        health.ReduceHealth((int)(dammageAmount * criticalFactor));
    }

    void ChooseNextAttack()
    {
        LazerAttack.OnAttackFinished -= ChooseNextAttackDelayed;

        bool MissilesOrLazer = Random.Range(0f, 1f) < 0.5f; // true = missiles / Lazer = false
        Debug.Log("attaque : " + MissilesOrLazer);
        if (MissilesOrLazer)
        {
            SpawnSalveSmalls();
            Debug.Log("attaque missiles");
        }
        else
        {
            StartLazer();
            Debug.Log("attaque Lazer");
        }
    }

    void ChooseNextAttackDelayed()
    {
        Invoke("ChooseNextAttack", 5f);
    }

    private void StartLazer()
    {
        LazerAttack.gameObject.SetActive(true);
        LazerAttack.DoAttack();

        LazerAttack.OnAttackFinished += ChooseNextAttackDelayed;
    }
}
