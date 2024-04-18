using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    MediumEnnemy Mutant;

    void Start()
    {
        Mutant = GetComponentInParent<MediumEnnemy>();
    }

    // Update is called once per frame
    void Update() { }

    public void CreateShockWave()
    {
        Mutant.CreateShockWave();
    }

    public void WalkAgain()
    {
        Mutant.CanWalk();
    }
}
