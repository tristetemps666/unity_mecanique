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

    [SerializeField]
    Vector3 gunScopeLocalPosition;

    private Vector3 gunLocalPosition;

    [SerializeField]
    Transform gunTransform;

    // Animator playerAnimator;
    [SerializeField]
    private float scopingSpeed = 3f;
    private float scopingAmount = 0f;

    private bool isScopeHold = false;
    private bool isShootHold = false;

    void Start()
    {
        gun = GetComponent<Gun>();
        sniper = GetComponent<Sniper>();
        // playerAnimator = GetComponent<Animator>();

        gunLocalPosition = gunTransform.localPosition;

        // if (ForceSniperMode)
        // {
        //     // sniper.enabled = true;
        //     gun.enabled = false;
        // }
        // sniper.enabled = false;
    }

    // Update is called once per frame
    void Update() { }

    void UpdateScopeGunPosition()
    {
        gunTransform.localPosition = Vector3.Lerp(
            gunLocalPosition,
            gunScopeLocalPosition,
            scopingAmount
        );
    }

    void OnScope()
    {
        isScopeHold = !isScopeHold;
        // sniper.StopAllCoroutines();
        // gun.StopAllCoroutines();
        // we don't want to scope if the player is shooting with the gun

        if (isShootHold && isScopeHold) // we don't scope if we are already shooting
            return;

        if (!isInSniperMode && isScopeHold)
        {
            StopAllCoroutines();
            StartCoroutine(ScopeCoroutine());
        }
        if (isInSniperMode && !isScopeHold)
        {
            StopAllCoroutines();
            StartCoroutine(UnScopeCoroutine());
        }
    }

    void OnShoot()
    {
        isShootHold = !isShootHold;

        if (isShootHold)
        {
            if (isInSniperMode)
                sniper.Shoot();
            else
                gun.StartShooting();
        }
        else
        {
            gun.StopShooting();
        }
    }

    IEnumerator ScopeCoroutine()
    {
        isInSniperMode = true;
        while (scopingAmount < 1f)
        {
            scopingAmount += Time.deltaTime * scopingSpeed;
            UpdateScopeGunPosition();
            yield return null;
        }

        sniper.canShoot = true;
        scopingAmount = 1f;
    }

    IEnumerator UnScopeCoroutine()
    {
        isInSniperMode = false;

        sniper.canShoot = false;
        while (scopingAmount > 0f)
        {
            scopingAmount -= Time.deltaTime * scopingSpeed;
            UpdateScopeGunPosition();
            yield return null;
        }
        scopingAmount = 0f;
    }
}
