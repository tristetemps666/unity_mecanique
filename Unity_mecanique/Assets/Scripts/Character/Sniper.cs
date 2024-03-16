using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sniper : MonoBehaviour, GunInterface
{
    public float fireRate = 1f;
    private bool canShoot = true;

    private float delayShoot = 0f;

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
        Debug.Log("j'affiche");
    }

    public void Shoot()
    {
        if (!canShoot)
            return;
        Debug.Log("il shoot au snipeerrrr");
        delayShoot = fireRate;
    }

    // void OnShoot()
    // {
    //     Shoot();
    // }
}
