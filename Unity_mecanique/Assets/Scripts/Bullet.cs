using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 200f;

    private float timeToDie = 4f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DestroyWithDelay();
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
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
        if (other.CompareTag("BigEnnemi"))
        {
            Destroy(gameObject);
            other.GetComponent<BigEnnemi>().TakeDammage();
        }
        if (other.CompareTag("SmallEnnemi"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    void DestroyWithDelay()
    {
        Destroy(gameObject, timeToDie);
    }
}
