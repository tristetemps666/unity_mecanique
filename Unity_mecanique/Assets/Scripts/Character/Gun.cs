using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour, GunInterface
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletSpawnTransform;

    [SerializeField]
    private LayerMask layerMaskRay;

    [SerializeField]
    private float shootRate = 0.2f;

    [SerializeField]
    AudioClip gunSound;

    public bool isShootHold { get; private set; } = false;

    public bool canShoot = true;

    AudioSource audioSource;

    private float delayShoot = 0f;

    // Animation
    [SerializeField]
    Animator gunAnimator;

    public UnityEvent ShootEvent = new();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // setup shoot

        ShootEvent.AddListener(CreateBullet);
    }

    // Update is called once per frame
    void Update()
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

    void CreateBullet()
    {
        // Debug.Break();
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = bulletSpawnTransform.transform.position;

        Camera cam = GetComponentInChildren<Camera>();

        audioSource.PlayOneShot(gunSound);

        RaycastHit hit;
        Vector3 target;
        if (
            Physics.Raycast(
                cam.transform.position,
                cam.transform.forward,
                out hit,
                5000,
                layerMaskRay
            )
        )
        {
            Debug.DrawLine(transform.position, hit.point);
            target = hit.point;
        }
        else
        {
            target = newBullet.transform.position + cam.transform.forward * 2000f;
            newBullet.transform.localScale *= 3;
        }
        newBullet.transform.LookAt(target);
    }

    private IEnumerator ShootRepeate()
    {
        while (true) // caca ???
        {
            ShootEvent.Invoke();
            CreateBullet();
            yield return new WaitForSeconds(shootRate);
        }
    }

    public void StartShooting()
    {
        if (canShoot)
        {
            gunAnimator.SetBool("IsShooting", true);
            StartCoroutine(ShootRepeate());
            delayShoot = shootRate;
        }
    }

    public void StopShooting()
    {
        gunAnimator.SetBool("IsShooting", false);
        Debug.Log("on stop le shoot");
        StopAllCoroutines();
        canShoot = false;
    }

    public void Shoot()
    {
        // isShootHold = !isShootHold;
        // if (!isShootHold)
        // {
        //     // we stop playing the animation

        //     StopCoroutine(CreateBulletRepeate());
        //     canShoot = false;
        //     return;
        // }
        // if (canShoot)
        // {
        //     // we play the animation
        //     gunAnimator.SetBool("IsShooting", true);

        //     StartCoroutine(CreateBulletRepeate());
        //     delayShoot = shootRate;
        // }
    }
}
