using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Splines;

public class LaserShotAttack : IAttack
{
    // Start is called before the first frame update
    public float timeToBuildTheShot = 4f;
    public float lazerSpeed = 10f;

    [SerializeField]
    private Transform sourceLaserShot;

    [SerializeField]
    AudioSource AudioSourceCharging;

    [SerializeField]
    private Transform SphereTransform;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float rayNoiseStrenght = 12f;

    // SPLINE PART
    private SplineContainer spline;

    private MeshRenderer splineRenderer;

    private SplineExtrude extrude;

    private Material rayMat;
    private Material sphereMat;

    private Collider collider;

    void Start()
    {
        spline = GetComponent<SplineContainer>();
        splineRenderer = GetComponent<MeshRenderer>();
        extrude = GetComponent<SplineExtrude>();
        collider = GetComponent<Collider>();

        splineRenderer.enabled = false;
        collider.enabled = false;

        rayMat = splineRenderer.materials[0];
        sphereMat = SphereTransform.gameObject.GetComponent<Renderer>().materials[0];
        rayMat.SetFloat("_noiseStrength", rayNoiseStrenght);
    }

    void Update()
    {
        // this is a hack because of my fucked up scales on my mesh
        sourceLaserShot.position = SphereTransform.position;
        // sourceLaserShot.localScale = SphereTransform.localScale;
    }

    public void StartLaserAttackAnimation()
    {
        animator.SetTrigger("TriggerOpenBigShootLaser");
        AudioSourceCharging.Play();
    }

    public void StopLaserAttackAnimation()
    {
        animator.ResetTrigger("TriggerOpenBigShootLaser");
    }

    void Shoot()
    {
        Debug.Log("ON FAIT UN MEGA SHOOT SA MERE LA PUTE");
        StartCoroutine(SendRay());
        Invoke("StartRetriveLaserAnimation", 2);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log("C'est le joueur qu'on touche ???");
        }
        if (other.TryGetComponent<IDammagable>(out IDammagable dammagable))
        {
            Debug.Log("le laser met des dégats à qq'un !!");
            dammagable.TakeDammage(Dammages);
        }
    }

    IEnumerator SendRay()
    {
        AudioSourceCharging.Stop();
        // UpdateRayMaterial(1f, 1f);
        PostProcessManager.Instance.SetRayEffect(true);
        yield return new WaitForSeconds(0.3f);
        // UpdateRayMaterial(15f, 1f);
        AudioManager.Instance.PlayShootLazer();

        Vector3 destination;

        // WE DO A RAYCAST TO CHECK IF WE HIT SOMETHING BEFORE THE PLAYER
        RaycastHit hit;

        if (
            Physics.Linecast(
                sourceLaserShot.position,
                target.position,
                out hit,
                ~collider.includeLayers
            )
        )
        {
            destination = hit.point;

            destination =
                destination + (target.position - sourceLaserShot.position).normalized * 3f;
        }
        else
        {
            destination =
                target.position + (target.position - sourceLaserShot.position).normalized * 3f; // we add a small distance
            Debug.Log("on a rien touché ptn de merde");
        }
        SetupSpline(destination);

        // We calculate the time to reach the target according to the speed
        float distance = Vector3.Distance(sourceLaserShot.position, destination);
        float timeToReach = distance / lazerSpeed;
        Debug.Log("temps pour atteindre : " + timeToReach);
        float t = 0;

        //  Visual sign to alert about the shoot.
        while (t < timeToReach)
        {
            t += Time.deltaTime;
            extrude.Range = new Vector2(0f, t / timeToReach);
            extrude.Radius = Mathf.Lerp(3, 10, t / timeToReach);
            // extrude.Rebuild();

            yield return null;
        }
    }

    public void ShootWithDelay() // OLD
    {
        Debug.Log("on charge le shoot !!");
        // on repart pour l'animation
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
            // WE DONT DO IT WHILE THE SCALE IS FUCKED UP
            // sourceLaserShot.rotation = Quaternion.Slerp(
            //     sourceLaserShot.rotation,
            //     Quaternion.LookRotation(
            //         target.position - sourceLaserShot.position,
            //         sourceLaserShot.up
            //     ),
            //     0.02f
            // );
            // sourceLaserShot.LookAt(target);
            t -= Time.deltaTime;
            UpdateRayMaterial(
                Mathf.Lerp(1, 20f, (timeToBuildTheShot - t) / timeToBuildTheShot),
                1f
            );

            yield return null;
        }
        Shoot();
    }

    void UpdateRayMaterial(float emission, float alpha)
    {
        rayMat.SetFloat("_EmissionMutliplier", emission);
        rayMat.SetFloat("_alpha", alpha);

        sphereMat.SetFloat("_EmissionMutliplier", emission);
        sphereMat.SetFloat("_alpha", alpha);
    }

    // we setup the start and end of it
    void SetupSpline(Vector3 destination)
    {
        spline.Spline.SetKnot(
            0,
            new BezierKnot(transform.InverseTransformPoint(sourceLaserShot.position))
        );
        spline.Spline.SetKnot(1, new BezierKnot(transform.InverseTransformPoint(destination)));

        collider.enabled = true;
        splineRenderer.enabled = true;

        // setup the points
        // spline.Spline.Add(new BezierKnot(sourceLaserShot.position));
        // spline.Spline.Add(new BezierKnot(target.position));
    }

    void ResetSpline()
    {
        splineRenderer.enabled = false;
        collider.enabled = false;
        UpdateRayMaterial(1f, 1f);

        spline.Spline.SetKnot(
            1,
            new BezierKnot(transform.InverseTransformPoint(sourceLaserShot.position))
        );
    }

    public void StartRetriveLaserAnimation()
    {
        PostProcessManager.Instance.SetRayEffect(false);

        StopAllCoroutines();
        ResetSpline();

        animator.SetTrigger("TriggerLeaveIdleBigShootLaser");
    }

    public void EndRetriveLaserAnimation()
    {
        animator.ResetTrigger("TriggerLeaveIdleBigShootLaser");
    }
}
