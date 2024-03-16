using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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

    public bool isShootHold { get; private set; } = false;

    private bool canShoot = true;

    private float delayShoot = 0f;

    // Start is called before the first frame update
    void Start() { }

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

        Debug.DrawRay(cam.transform.position, cam.transform.forward * 5f, Color.red, 5f);

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

    private IEnumerator CreateBulletRepeate()
    {
        while (isShootHold)
        {
            CreateBullet();
            yield return new WaitForSeconds(shootRate);
        }
    }

    public void Shoot()
    {
        isShootHold = !isShootHold;
        if (!isShootHold)
        {
            Debug.Log("Stop shoot");
            StopCoroutine(CreateBulletRepeate());
            canShoot = false;
            return;
        }
        if (canShoot)
        {
            Debug.Log("Start shoot");
            StartCoroutine(CreateBulletRepeate());
            delayShoot = shootRate;
        }
    }

    public void StopShoot()
    {
        // StopCoroutine(CreateBulletRepeate());
    }

    // void OnShoot()
    // {
    //     Shoot();
    // }
}
