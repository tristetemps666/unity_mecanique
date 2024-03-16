using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Sniper : MonoBehaviour, GunInterface
{
    [SerializeField]
    private float fireRate = 1f;

    [SerializeField]
    private LayerMask maskShoot;
    public bool canShoot = false;

    private float delayShoot = 0f;

    private Camera playerCam;

    private bool isShootHold = false;

    [SerializeField]
    private Transform endBarrelTransform;

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
    }

    bool isShootDelayed() => delayShoot >= 0f;

    private void Update()
    {
        if (!canShoot)
        {
            delayShoot -= Time.deltaTime;
            if (delayShoot <= 0f)
            {
                delayShoot = 0f;
                canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        // to avoid to shoot when the button is released
        if (isShootHold)
        {
            isShootHold = false;
            return;
        }

        if (!canShoot && isShootDelayed())
            return;

        Debug.Log("il shoot au snipeerrrr");
        // we change the gun status
        delayShoot = fireRate;
        isShootHold = true;

        // we do the shoot
        RaycastHit[] hits = Physics.RaycastAll(
            playerCam.transform.position,
            playerCam.transform.forward * 100000,
            maskShoot
        );

        Debug.DrawLine(
            endBarrelTransform.position,
            playerCam.transform.forward * 100000,
            Color.red,
            3f
        );

        // if we hit nobody
        if (hits.Length == 0)
        {
            Debug.Log("on a touché personne :/");
            return;
        }

        // if we hit at least one object
        foreach (RaycastHit hit in hits)
        {
            Debug.Log(hit.transform.name);
        }
    }

    // void OnShoot()
    // {
    //     Shoot();
    // }
}
