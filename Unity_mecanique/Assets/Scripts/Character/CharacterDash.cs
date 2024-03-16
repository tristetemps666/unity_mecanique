using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterDash : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterMovement CM;

    [SerializeField]
    private GameObject dashParticules;

    public float dashPower = 70f;

    public float dashReloadTime = 2f;
    public float timeAtMaxDashSpeed = 1f;

    public float TimeRecoverDefaultMaxSpeed = 0.5f;

    private float defaultMaxSpeed = 0f;

    private float reloadingTimeRemaining = 0f;

    private TrailRenderer trailRenderer;

    void Start()
    {
        CM = GetComponent<CharacterMovement>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
        defaultMaxSpeed = CM.maxSpeed;
    }

    // Update is called once per frame
    void Update() { }

    private void OnDash()
    {
        if (IsReloading())
            return;

        // will play on Awake
        GameObject newDashParticules = Instantiate(dashParticules, transform);
        if (newDashParticules != null)
        {
            newDashParticules.transform.localPosition = Vector3.zero;
            newDashParticules.SetActive(true);
        }
        trailRenderer.enabled = true;

        Vector3 InputsVec3 = CM.getInputVec3();

        Vector3 dashForce =
            InputsVec3 != Vector3.zero ? InputsVec3 * dashPower : dashPower * transform.forward;

        if (CM.IsGroundedVal)
        {
            dashForce = CM.alignVectorToGround(dashForce);
        }

        CM.rb.velocity = Vector3.zero;
        CM.rb.AddForce(dashForce, ForceMode.VelocityChange);

        ChangeMaxSpeedAlongDash();
        StartCoroutine(ApplyDashReloadTime());
    }

    private void ChangeMaxSpeedAlongDash()
    {
        CM.maxSpeed = dashPower;
        Invoke("MaxSpeedBackToDefault", timeAtMaxDashSpeed);
    }

    private void MaxSpeedBackToDefault()
    {
        StartCoroutine(ReduceMaxSpeed());
    }

    private IEnumerator ReduceMaxSpeed()
    {
        float time = TimeRecoverDefaultMaxSpeed;
        float factor = CM.maxSpeed - defaultMaxSpeed;
        while (time >= 0)
        {
            CM.maxSpeed -= Time.deltaTime * factor;
            time -= Time.deltaTime;
            yield return null;
        }
        CM.maxSpeed = defaultMaxSpeed;
    }

    private bool IsReloading() => reloadingTimeRemaining > 0f;

    private IEnumerator ApplyDashReloadTime()
    {
        reloadingTimeRemaining = dashReloadTime;
        while (reloadingTimeRemaining > 0)
        {
            reloadingTimeRemaining -= Time.deltaTime;
            yield return null;
        }
        reloadingTimeRemaining = 0f;
        trailRenderer.enabled = false;
    }
}
