using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float mouvementSpeed = 5f;

    [Range(0, 1)]
    public float airControl = 0.5f;

    [SerializeField]
    private CapsuleCollider capsuleCollider;

    private Rigidbody rb;

    private PlayerInput playerInput;

    private Vector2 movementInput = Vector2.zero;

    [SerializeField]
    private LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        // capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() { }

    bool IsGrounded()
    {
        return Physics
                .OverlapSphere(
                    transform.position + Vector3.down * capsuleCollider.height / 2,
                    0.3f,
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.down * capsuleCollider.height / 2, 0.3f);
    }

    private void ApplyJumpForce()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ApplyMovement(Vector2 Inputs)
    {
        Vector3 InputsVec3 = transform.forward * Inputs.y + transform.right * Inputs.x;
        // Vector3 newPosition = transform.position + InputsVec3 * Time.deltaTime * mouvementSpeed;
        // rb.Move(newPosition, transform.rotation);
        rb.AddForce(InputsVec3 * mouvementSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    // event from the input action
    private void OnJump()
    {
        if (!IsGrounded())
            return;
        // Debug.Log("jump");
        ApplyJumpForce();
    }

    private void OnMovement(InputValue inputValue)
    {
        Debug.Log("ça bouuuge");
        Debug.Log(inputValue.Get<Vector2>());

        // ApplyMovement(inputValue.Get<Vector2>());
        movementInput = inputValue.Get<Vector2>();
    }

    private void OnLook(InputValue inputValue)
    {
        Debug.Log(inputValue.Get<Vector2>());
    }
}
