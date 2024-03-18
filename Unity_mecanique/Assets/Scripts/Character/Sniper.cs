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

    [SerializeField]
    Material sniperTrailMaterial;

    [SerializeField]
    GameObject sniperTrailParticules;

    private int dammageAmount = 10;

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
    }

    bool isShootDelayed() => delayShoot > 0f;

    private void Update()
    {
        delayShoot -= Time.deltaTime;
        if (delayShoot <= 0f)
        {
            delayShoot = 0f;
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

        if (!canShoot || isShootDelayed())
            return;

        Debug.Log("il shoot au snipeerrrr");
        // we change the gun status
        delayShoot = fireRate;
        isShootHold = true;

        // we make the effect
        createSniperTrail(
            endBarrelTransform.position,
            endBarrelTransform.position + playerCam.transform.forward * 1000
        );

        // we do the shoot
        RaycastHit[] hits = Physics.RaycastAll(
            playerCam.transform.position,
            playerCam.transform.forward,
            1000,
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
            if (hit.transform.gameObject.TryGetComponent(out IDammagable dammagable))
            {
                dammagable.TakeDammage(dammageAmount);
            }
        }
    }

    void createSniperTrail(Vector3 start, Vector3 end)
    {
        // we create the game object
        GameObject goNewTrail = new GameObject("trail");
        goNewTrail.transform.position = endBarrelTransform.position;
        // we add the components needed
        goNewTrail.AddComponent<LineRenderer>();

        // setup the new trail
        SniperTrail newTrail = goNewTrail.AddComponent<SniperTrail>();
        newTrail.SniperTrailMat = sniperTrailMaterial;
        newTrail.particulesTrail = sniperTrailParticules;
        newTrail.setupLineRenderer();

        // we setup the trail
        newTrail.setupLine(start, end, 10);
    }
}
