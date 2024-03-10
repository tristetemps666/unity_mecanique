using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnnemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private BigEnnemi bigEnnemi;
    private Vector3 Target = Vector3.zero;
    public float smoothLook = 0.5f;
    Vector3 velocityDamp;

    public float speed = 5f;

    public float predictionFactor = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Target = bigEnnemi.getTargetPosition(predictionFactor);

        Vector3 smoothTargetPosition = Vector3.SmoothDamp(
            transform.position,
            Target,
            ref velocityDamp,
            smoothLook
        );

        transform.LookAt(smoothTargetPosition);
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        Debug.DrawLine(transform.position, Target);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(Target, Vector3.one);
    }
}
