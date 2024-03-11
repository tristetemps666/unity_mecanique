using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterDash : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterMovement CM;

    public float dashPower = 20f;

    void Start()
    {
        CM = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update() { }

    private void OnDash()
    {
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
    }
}
