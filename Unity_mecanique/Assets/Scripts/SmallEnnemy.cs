using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmallEnnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public BigEnnemi bigEnnemi;
    private Vector3 Target = Vector3.zero;
    Vector3 velocityDamp;

    public float timeToActivate = 2f;
    public bool isSeeking = false;
    public float smoothLook = 0.5f;

    public float speed = 5f;

    public float predictionFactor = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        bigEnnemi = GameObject.FindFirstObjectByType<BigEnnemi>();
        Invoke("ActivateSeeking", timeToActivate);

        Target = bigEnnemi.getTargetPosition(predictionFactor, transform.position);

        // setup the direction to make them spawn toward the player
        Vector3 direction = (Target - transform.position).normalized;
        direction.y = 1f;
        Vector3 randomInitForce = new Vector3(Random.Range(-2f, 2f), 50f, Random.Range(-2f, 2f));
        Vector3 Initforce = new Vector3(
            direction.x * randomInitForce.x,
            direction.y * randomInitForce.y,
            direction.z * randomInitForce.z
        );

        Vector3 randomInitAngularSpeed = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );
        rb.AddForce(Initforce, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isSeeking)
            return;
        MoveTowardTarget();

        Debug.DrawRay(transform.position, rb.velocity);
    }

    public void setSpeed(Vector3 speed)
    {
        Debug.Log("on set la speed");
        rb.velocity = speed;
        Debug.Log("elle est set !");
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(Target, Vector3.one);
    }

    private void MoveTowardTarget()
    {
        Target = bigEnnemi.getTargetPosition(predictionFactor, transform.position);

        Vector3 smoothTargetPosition = Vector3.SmoothDamp(
            transform.position,
            Target,
            ref velocityDamp,
            smoothLook
        );

        transform.LookAt(Target);
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        Debug.DrawLine(transform.position, Target);
    }

    void ActivateSeeking()
    {
        rb.velocity = Vector3.zero;
        Debug.Log("il commence à chasser");
        rb.useGravity = false;
        isSeeking = true;
    }

    public void setBigEnnemi(BigEnnemi Be)
    {
        Debug.Log("on set le big ennemi");

        bigEnnemi = Be;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
