using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShotAttack : IAttack
{
    // Start is called before the first frame update
    public float timeToBuildTheShot = 4f;

    [SerializeField]
    private Transform sourceLaserShot;

    [SerializeField]
    private Transform target;

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

    public void ShootWithDelay() // OLD
    {
        Debug.Log("on charge le shoot !!");
        // on repart pour l'animation
        Invoke("StartRetriveLaserAnimation", 2);
    }

    public void BuildingTheShoot()
    {
        StartCoroutine(BuildingTheShootCoroutine());
    }

    IEnumerator BuildingTheShootCoroutine()
    {
        float t = timeToBuildTheShot;
        while (t > 0f)
        {
            sourceLaserShot.rotation = Quaternion.Slerp(
                sourceLaserShot.rotation,
                Quaternion.LookRotation(
                    target.position - sourceLaserShot.position,
                    sourceLaserShot.up
                ),
                0.02f
            );
            // sourceLaserShot.LookAt(target);
            yield return null;
        }
        Shoot();
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
