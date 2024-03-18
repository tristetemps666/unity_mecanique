using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 200f;

    [SerializeField]
    private int dammage = 10;

    private float timeToDie = 4f;
    Rigidbody rb;

    private static Sniper sniper;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DestroyWithDelay();
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);

        if (sniper == null)
            sniper = FindFirstObjectByType<Sniper>();
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * 5f);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(transform.forward * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.TryGetComponent(out IDammagable otherDammagable))
        {
            otherDammagable.TakeDammage(dammage);
            sniper.IncreasePowerFactor();
        }
        Destroy(gameObject);
    }

    void DestroyWithDelay()
    {
        Destroy(gameObject, timeToDie);
    }
}
