using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CharacterMovement : MonoBehaviour
{
    public bool debugAcceleration = false;
    public float jumpForce = 10f;
    public float mouvementSpeed = 5f;
    public Vector2 mouseSensitvity = Vector2.one;
    public float limitPitch = 80f;

    public float dashPower = 20f;

    public float radiusGroundCheck = 0.7f;

    [Range(0, 1)]
    public float airControlFactor = 0.5f;

    [SerializeField]
    private AnimationCurve aligmentCurveFactor;

    [SerializeField]
    private AnimationCurve accelerationCurveFactor;

    [SerializeField]
    private CapsuleCollider capsuleCollider;

    private Rigidbody rb;

    private PlayerInput playerInput;

    private Vector2 movementInput = Vector2.zero;

    [SerializeField]
    private LayerMask groundMask;

    private Camera playerCam;
    private float rotation_amount = 0f;

    private Vector3 NormalGround = Vector3.zero;

    private bool IsGroundedVal = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCam = GetComponentInChildren<Camera>();
    }

    // void OnShoot()
    // {
    //     Debug.Log("shoot");
    // }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(
            transform.position + Vector3.down * capsuleCollider.height / 2,
            radiusGroundCheck
        );
    }

    // Update is called once per frame
    void Update()
    {
        IsGroundedVal = IsGrounded();
    }

    bool IsGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, groundMask))
        {
            NormalGround = hit.normal;
        }

        return Physics
                .OverlapSphere(
                    transform.position + Vector3.down * capsuleCollider.height / 2,
                    radiusGroundCheck,
                    groundMask
                )
                .Length > 0;
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            ApplyMovement(movementInput);
        }
    }

    private void ApplyJumpForce()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ApplyMovement(Vector2 Inputs)
    {
        // Vector3 InputsVec3 = transform.forward * Inputs.y + transform.right * Inputs.x;
        Vector3 InputsVec3 = getInputVec3();

        // 0 => my inputs are aligned with my speed
        // 1 => my inputs are opposed with my speed
        // the value will be used in order to have a more reactive gameplay
        float alignment = (1 - Vector3.Dot(rb.velocity.normalized, InputsVec3.normalized)) / 2;

        float alignementBoost = aligmentCurveFactor.Evaluate(alignment);
        float accelerationBoost = accelerationCurveFactor.Evaluate(rb.velocity.magnitude);

        if (debugAcceleration)
        {
            DisplayDebugAcceleration(alignementBoost, accelerationBoost);
        }

        Vector3 movementForce = IsGroundedVal
            ? InputsVec3 * mouvementSpeed * Time.deltaTime * alignementBoost * accelerationBoost
            : InputsVec3
                * mouvementSpeed
                * Time.deltaTime
                * alignementBoost
                * accelerationBoost
                * airControlFactor;

        // Movement along surface ?
        if (IsGroundedVal)
            movementForce = movementForce - Vector3.Dot(movementForce, NormalGround) * NormalGround;

        // rb.AddForce(InputsVec3 * mouvementSpeed * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddForce(movementForce, ForceMode.VelocityChange);
    }

    private Vector3 alignVectorToGround(Vector3 vector)
    {
        return vector - Vector3.Dot(vector, NormalGround) * NormalGround;
    }

    private Vector3 getInputVec3()
    {
        return transform.forward * movementInput.y + transform.right * movementInput.x;
    }

    private void DisplayDebugAcceleration(float alignementBoost, float accelerationBoost)
    {
        Debug.Log(
            "alignementBoost : "
                + alignementBoost
                + " // accel boost : "
                + accelerationBoost
                + " // velocity : "
                + rb.velocity.magnitude
        );
    }

    private void ApplyRotation(Vector2 rotationInputs)
    {
        // yaw
        transform.RotateAround(transform.position, Vector3.up, rotationInputs.x);

        if (Mathf.Abs(rotation_amount - rotationInputs.y) > limitPitch)
            return;

        // pitch
        Transform camTransform = playerCam.transform;
        camTransform.RotateAround(camTransform.position, camTransform.right, -rotationInputs.y);
        rotation_amount -= rotationInputs.y;
    }

    // event from the input action
    private void OnJump()
    {
        if (!IsGroundedVal)
            return;
        ApplyJumpForce();
    }

    private void OnMovement(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void OnDash()
    {
        Debug.Log("IL DASH OMG");
        Vector3 InputsVec3 = getInputVec3();

        Vector3 dashForce =
            InputsVec3 == Vector3.zero ? InputsVec3 * dashPower : dashPower * transform.forward;

        Debug.Log("Force avant alignement : " + dashForce);

        if (IsGroundedVal)
        {
            dashForce = alignVectorToGround(dashForce);
        }
        Debug.Log("Force après alignement : " + dashForce);
        rb.AddForce(dashForce, ForceMode.VelocityChange);
    }

    private void OnLook(InputValue inputValue)
    {
        ApplyRotation(mouseSensitvity * inputValue.Get<Vector2>() * Time.deltaTime);
    }

    public float getPlayerVelocity()
    {
        return rb.velocity.magnitude;
    }

    public Vector3 getPlayerVectorVelocity()
    {
        return rb.velocity;
    }
}
