using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigEnnemiAttacksManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private List<IAttack> ListAttacks = new List<IAttack>();

    // [SerializeField]
    // private GameObject Player;

    // [SerializeField]
    // private GameObject smallEnnemiPrefab;

    // [SerializeField]
    // private Transform spawnPointSmallEnnemies;

    // [SerializeField]
    // private Attack LazerAttack;

    private IAttack LastAttack;
    private IAttack CurrentAttack;

    int i = 0;

    void Start()
    {
        // LazerAttack.gameObject.SetActive(false);

        ChooseNextAttackDelayed();
    }

    // Update is called once per frame
    void Update() { }

    ///// SMALL ENNEMIES SPAWN

    // public Vector3 getTargetPosition(float prediction, Vector3 sourcePosition)
    // {
    //     float distanceFactor = Vector3.Distance(sourcePosition, Player.transform.position);
    //     distanceFactor = Mathf.Lerp(0, 10, distanceFactor / 100);

    //     return Player.transform.position
    //         + prediction
    //             * distanceFactor
    //             * Player.GetComponent<CharacterMovement>().getPlayerVectorVelocity()
    //             * Time.deltaTime;
    // }

    // private void SpawnSmallEnnemy()
    // {
    //     GameObject newSmall = Instantiate(smallEnnemiPrefab);
    //     newSmall.transform.position = spawnPointSmallEnnemies.transform.position;
    // }

    // private IEnumerator SpawnSalveSmallsCoroutine()
    // {
    //     for (int i = 0; i < 10; i++)
    //     {
    //         SpawnSmallEnnemy();
    //         yield return new WaitForSeconds(0.2f);
    //     }
    //     ChooseNextAttackDelayed();
    // }

    // private void SpawnSalveSmalls()
    // {
    //     StartCoroutine(SpawnSalveSmallsCoroutine());
    // }



    // TODO : WILL BE CHANGED
    void ChooseNextAttack()
    {
        if (CurrentAttack != null)
        {
            LastAttack = CurrentAttack;
            CurrentAttack.OnAttackFinished.RemoveListener(ChooseNextAttackDelayed);
        }

        CurrentAttack = ListAttacks[i % ListAttacks.Count];
        // CurrentAttack = ListAttacks[Mathf.RoundToInt(Random.Range(0, 1))];
        // This allows to repeatedly choose
        CurrentAttack.OnAttackFinished.AddListener(ChooseNextAttackDelayed);
        CurrentAttack.DoAttack();
        i++;
    }

    // TODO : WILL BE CHANGED

    void ChooseNextAttackDelayed()
    {
        Invoke("ChooseNextAttack", 5f);
    }

    public void EndAttack()
    {
        Debug.Log("quand l'anim se finit");
        CurrentAttack.OnAttackFinished.Invoke();
    }

    public void StartAttackOnAnimation()
    {
        CurrentAttack.OnAnimationAttackStart.Invoke();
    }
}
