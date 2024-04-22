using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsAttack : IAttack
{
    // Start is called before the first frame update
    [SerializeField]
    private BulletManager bulletManager;

    void Start()
    {
        // OnAttackStart.AddListener(bulletManager.StartSpawning);
    }

    // void StartSpawningBullets(){
    //     ;
    // }

    public void StartBulletAttackAnimation()
    {
        animator.SetTrigger("TriggerRotationBulletAttack");
    }

    public void StopBulletAttackAnimation()
    {
        animator.ResetTrigger("TriggerRotationBulletAttack");
    }

    // Update is called once per frame
    void Update() { }
}
