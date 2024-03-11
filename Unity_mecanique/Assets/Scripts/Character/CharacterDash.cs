using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterDash : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterMovement CM;

    public float dashPower = 70f;

    public float dashReloadTime = 2f;
    public float timeAtMaxDashSpeed = 1f;

    public float TimeRecoverDefaultMaxSpeed = 0.5f;

    private float defaultMaxSpeed = 0f;

    private float reloadingTimeRemaining = 0f;

    void Start()
    {
        CM = GetComponent<CharacterMovement>();
        defaultMaxSpeed = CM.maxSpeed;
    }

    // Update is called once per frame
    void Update() { }

    private void OnDash()
    {
        if (IsReloading())
            return;

        Debug.Log("IL DASH OMG");
        Vector3 InputsVec3 = CM.getInputVec3();

        Vector3 dashForce =
            InputsVec3 != Vector3.zero ? InputsVec3 * dashPower : dashPower * transform.forward;

        Debug.Log("Force avant alignement : " + dashForce);

        if (CM.IsGroundedVal)
        {
            dashForce = CM.alignVectorToGround(dashForce);
        }
        Debug.Log("Force après alignement : " + dashForce);

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
    }
}
