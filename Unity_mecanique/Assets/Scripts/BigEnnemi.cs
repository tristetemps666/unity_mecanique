using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnnemi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject Player;

    void Start() { }

    // Update is called once per frame
    void Update() { }

    public Vector3 getTargetPosition(float prediction)
    {
        return Player.transform.position
            + prediction
                * Player.GetComponent<CharacterMovement>().getPlayerVectorVelocity()
                * Time.deltaTime;
    }
}
