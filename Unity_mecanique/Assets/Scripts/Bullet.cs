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

    [SerializeField]
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
        IDammagable dammagable = null;
        // If we hit a weak point, we need to get the health in parent
        if (other.transform.CompareTag("WeakPoint"))
        {
            dammagable = other.transform.gameObject.GetComponent<WeakPoint>().dammagable;
            AudioManager.Instance.playSmallCritical();
        }
        if (other.transform.gameObject.TryGetComponent(out IDammagable Outdammagable))
        {
            dammagable = Outdammagable;
        }

        if (dammagable != null)
        {
            dammagable.TakeDammage(dammage, other.gameObject);
            sniper.IncreasePowerFactor();
        }
        Destroy(gameObject);
    }

    void DestroyWithDelay()
    {
        Destroy(gameObject, timeToDie);
    }
}
