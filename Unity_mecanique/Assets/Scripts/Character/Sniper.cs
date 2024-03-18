using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Sniper : MonoBehaviour, GunInterface
{
    [SerializeField]
    private float fireRate = 1f;

    [SerializeField]
    private LayerMask LayersThatCanBeHit;
    public bool canShoot = false;

    private float delayShoot = 0f;

    private Camera playerCam;

    [SerializeField]
    private Transform endBarrelTransform;

    [SerializeField]
    Material sniperTrailMaterial;

    [SerializeField]
    GameObject sniperTrailParticules;

    [SerializeField]
    private int dammageAmount = 150;

    [SerializeField]
    private float maxPowerFactor = 2f;

    [SerializeField]
    private float PFIncreaseAmount = 0.03f;

    [SerializeField]
    private TextMeshPro powerFactorText;

    private float sniperPowerFactor = 1f;

    [SerializeField]
    private Renderer sniperRenderer;

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
        sniperRenderer.material.SetFloat("_maxFactor", maxPowerFactor);

        UpdatePowerFactorVisuals();
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
        if (!canShoot || isShootDelayed())
            return;

        Debug.Log("il shoot au snipeerrrr");
        // we change the gun status
        delayShoot = fireRate;

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
            LayersThatCanBeHit
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
            ResetPowerFactor();
            return;
        }

        // if we hit at least one object
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.TryGetComponent(out IDammagable dammagable))
            {
                dammagable.TakeDammage(Mathf.RoundToInt(dammageAmount * sniperPowerFactor));
            }
        }
        ResetPowerFactor();
    }

    public void IncreasePowerFactor()
    {
        sniperPowerFactor = Mathf.Min(sniperPowerFactor + PFIncreaseAmount, maxPowerFactor);
        UpdatePowerFactorVisuals();
    }

    public void ResetPowerFactor()
    {
        sniperPowerFactor = 1f;
        UpdatePowerFactorVisuals();
    }

    void UpdatePowerFactorVisuals()
    {
        powerFactorText.text = ((int)(sniperPowerFactor * 100)).ToString() + "%";
        sniperRenderer.material.SetFloat("_PowerFactor", sniperPowerFactor);
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
