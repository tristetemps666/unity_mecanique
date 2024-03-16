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
        // sniper.StopAllCoroutines();
        // gun.StopAllCoroutines();

        if (!isInSniperMode)
        {
            StopAllCoroutines();
            StartCoroutine(ScopeCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(UnScopeCoroutine());
        }
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
