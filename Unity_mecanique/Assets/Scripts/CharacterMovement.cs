using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private bool isGrounded = false;

    private CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update() { }

    private void OnCollisionEnter(Collision other)
    {
        foreach (var contactPoint in other.contacts)
        {
            Debug.Log(contactPoint.normal);
            Debug.Log(Vector3.Dot(contactPoint.normal, Vector3.up));
            isGrounded = Vector3.Dot(contactPoint.normal, Vector3.up) > 0;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
}
