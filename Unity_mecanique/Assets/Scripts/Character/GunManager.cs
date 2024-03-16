using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    // Start is called before the first frame update

    Gun gun;
    Sniper sniper;

    bool isInSniperMode = false;

    [SerializeField]
    bool ForceSniperMode = false;

    void Start()
    {
        gun = GetComponent<Gun>();
        sniper = GetComponent<Sniper>();

        if (ForceSniperMode)
        {
            // sniper.enabled = true;
            gun.enabled = false;
        }
        sniper.enabled = false;
    }

    // Update is called once per frame
    void Update() { }

    void OnScope()
    {
        // sniper.StopAllCoroutines();
        // gun.StopAllCoroutines();

        isInSniperMode = !isInSniperMode;
    }

    void OnShoot()
    {
        if (isInSniperMode && !gun.isShootHold)
        {
            sniper.Shoot();
        }
        else
        {
            gun.Shoot();
        }
    }
}
