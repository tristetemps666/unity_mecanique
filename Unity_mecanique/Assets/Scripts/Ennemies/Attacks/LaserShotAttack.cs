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
    private Transform SphereTransform;

    [SerializeField]
    private Transform target;

    // SPLINE PART
    private SplineContainer spline;

    private MeshRenderer splineRenderer;

    private SplineExtrude extrude;

    private Material rayMat;
    private Material sphereMat;

    Collider collider;

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
    }

    public void StopLaserAttackAnimation()
    {
        animator.ResetTrigger("TriggerOpenBigShootLaser");

        // Invoke("Shoot", 5f);
    }

    void Shoot()
    {
        Debug.Log("ON FAIT UN MEGA SHOOT SA MERE LA PUTE");
        StartCoroutine(SendRay());
        Invoke("StartRetriveLaserAnimation", 2);
    }

    IEnumerator SendRay()
    {
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
        }
        else
        {
            destination = target.position;
            Debug.Log("on a rien touché ptn de merde");
        }
        SetupSpline(destination);

        float distance = Vector3.Distance(sourceLaserShot.position, destination);
        float timeToReach = distance / lazerSpeed;
        Debug.Log("temps pour atteindre : " + timeToReach);
        float t = 0;
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

        spline.Spline.SetKnot(
            1,
            new BezierKnot(transform.InverseTransformPoint(sourceLaserShot.position))
        );
    }

    public void StartRetriveLaserAnimation()
    {
        StopAllCoroutines();
        ResetSpline();

        animator.SetTrigger("TriggerLeaveIdleBigShootLaser");
    }

    public void EndRetriveLaserAnimation()
    {
        animator.ResetTrigger("TriggerLeaveIdleBigShootLaser");
    }

    // Update is called once per frame
}
