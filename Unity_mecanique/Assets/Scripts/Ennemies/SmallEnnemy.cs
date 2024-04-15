using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SmallEnnemy : MonoBehaviour, IDammagable
{
    // Start is called before the first frame update

    public BigEnnemi bigEnnemi;
    private Vector3 Target = Vector3.zero;
    Vector3 velocityDamp;

    public int dammage = 50;

    public float timeToActivate = 2f;
    public bool isSeeking = false;
    public float smoothLook = 0.5f;

    public float speed = 5f;

    public float predictionFactor = 2f;

    [SerializeField]
    private AudioClip explosionSound;

    private AudioSource audioSource;

    private Rigidbody rb;

    private Renderer[] Renderers;

    private bool canDammage = true;

    void Start()
    {
        Renderers = GetComponentsInChildren<Renderer>();
        audioSource = GetComponent<AudioSource>();
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
        rb.velocity = speed;
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

        Target = smoothTargetPosition;

        // transform.LookAt(Target);
        var newRot = Quaternion.LookRotation(Target - transform.position, transform.up);
        var newPos = transform.position + transform.forward * Time.fixedDeltaTime * speed;
        // rb.MoveRotation(newRot);

        rb.MovePosition(newPos);
        rb.MoveRotation(newRot);

        // rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        Debug.DrawLine(transform.position, Target);
    }

    void ActivateSeeking()
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        isSeeking = true;
        rb.isKinematic = true;
    }

    public void setBigEnnemi(BigEnnemi Be)
    {
        bigEnnemi = Be;
    }

    // kill the small ennemy if it hits an object
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BigEnnemi"))
            return;
        if (other.gameObject.TryGetComponent(out IDammagable otherDammagable))
        {
            // if (!canDammage)
            //     return;
            otherDammagable.TakeDammage(dammage);
            canDammage = false;
            rb.detectCollisions = false;
        }
        DestroyShip();
    }

    public void TakeDammage(int dammageAmount, GameObject goHitPart)
    {
        DestroyShip();
    }

    void OnTriggerEnter(Collider other)
    {
        DestroyShip();
    }

    void DestroyShip()
    {
        canDammage = false;
        rb.detectCollisions = false;

        if (audioSource.isPlaying && audioSource.clip != explosionSound)
        {
            audioSource.Stop();
            audioSource.clip = explosionSound;
            audioSource.Play();
            audioSource.volume = 0.7f;
        }
        foreach (Renderer rend in Renderers)
        {
            rend.enabled = false;
        }
        GetComponent<Rigidbody>().GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 3f);
    }
}
