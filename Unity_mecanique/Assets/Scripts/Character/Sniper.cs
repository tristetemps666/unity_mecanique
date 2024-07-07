using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

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

    public float SniperPowerFactor { get; private set; } = 1f;

    [SerializeField]
    private Renderer sniperRenderer;

    [SerializeField]
    AudioClip sniperSound;

    AudioSource audioSource;

    [SerializeField]
    Animator weaponAnimator;

    public UnityEvent ShootSniperEvent = new();

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
        audioSource = GetComponent<AudioSource>();
        sniperRenderer.material.SetFloat("_maxFactor", maxPowerFactor);

        UpdatePowerFactorVisuals();

        // setup shoot event :
        ShootSniperEvent.AddListener(ProceedShoot);
        ShootSniperEvent.AddListener(ResetPowerFactor);
        ShootSniperEvent.AddListener(() => StartCoroutine(ShootAnimationTrigger()));
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

    public void setPowerFactor(float powerFactor)
    {
        SniperPowerFactor = Mathf.Clamp(powerFactor, 0f, maxPowerFactor);
        UpdatePowerFactorVisuals();
    }

    IEnumerator ShootAnimationTrigger()
    {
        weaponAnimator.SetTrigger("SniperShoot");
        yield return new WaitForSeconds(0.2f);
        weaponAnimator.ResetTrigger("SniperShoot");
    }

    private void ProceedShoot()
    {
        List<IDammagable> alreadyDammagables = new List<IDammagable>();

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

        weaponAnimator.SetTrigger("SniperShoot");

        audioSource.PlayOneShot(sniperSound, 0.5f);

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
            IDammagable dammagable = null;
            // If we hit a weak point, we need to get the health in parent
            if (hit.transform.CompareTag("WeakPoint"))
            {
                dammagable = hit.transform.gameObject.GetComponent<WeakPoint>().dammagable;
                AudioManager.Instance.playCritical();
            }
            if (hit.transform.gameObject.TryGetComponent(out IDammagable Outdammagable))
            {
                dammagable = Outdammagable;
            }

            // check if we have already hit the Idammagable
            if (!alreadyDammagables.Contains(dammagable) && dammagable != null)
            {
                dammagable.TakeDammage(
                    Mathf.RoundToInt(dammageAmount * SniperPowerFactor),
                    hit.transform.gameObject
                );
                alreadyDammagables.Add(dammagable);
            }
        }
    }

    public void Shoot()
    {
        if (!canShoot || isShootDelayed())
            return;

        ShootSniperEvent.Invoke();
    }

    public void IncreasePowerFactor()
    {
        SniperPowerFactor = Mathf.Min(SniperPowerFactor + PFIncreaseAmount, maxPowerFactor);
        UpdatePowerFactorVisuals();
    }

    public void ResetPowerFactor()
    {
        SniperPowerFactor = 1f;
        UpdatePowerFactorVisuals();
    }

    void UpdatePowerFactorVisuals()
    {
        powerFactorText.text = ((int)(SniperPowerFactor * 100)).ToString() + "%";
        sniperRenderer.material.SetFloat("_PowerFactor", SniperPowerFactor);
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
