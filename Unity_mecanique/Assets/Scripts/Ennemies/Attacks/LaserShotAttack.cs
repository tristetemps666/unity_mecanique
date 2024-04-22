using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShotAttack : IAttack
{
    // Start is called before the first frame update
    void Start() { }

    public void StartLaserAttackAnimation()
    {
        animator.SetTrigger("TriggerOpenBigShootLaser");
    }

    public void StopLaserAttackAnimation()
    {
        animator.ResetTrigger("TriggerOpenBigShootLaser");

        Invoke("Shoot", 5f);
    }

    void Shoot()
    {
        Debug.Log("ON FAIT UN MEGA SHOOT SA MERE LA PUTE");
    }

    public void ShootWithDelay()
    {
        Debug.Log("on charge le shoot !!");
        // on repart pour l'animation
        Invoke("StartRetriveLaserAnimation", 2);
    }

    public void StartRetriveLaserAnimation()
    {
        animator.SetTrigger("TriggerLeaveIdleBigShootLaser");
    }

    public void EndRetriveLaserAnimation()
    {
        animator.ResetTrigger("TriggerLeaveIdleBigShootLaser");
    }

    // Update is called once per frame
    void Update() { }
}
