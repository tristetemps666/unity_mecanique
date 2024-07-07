using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class FovManager : MonoBehaviour
{
    // Start is called before the first frame update
    // private Camera cam;
    [SerializeField]
    CinemachineVirtualCamera cinemachineVirtualCamera;

    public AnimationCurve FovFactorAlongSpeed;
    public float highSpeedFov = 100f;
    public float smoothFov = 0.3f;

    private CharacterMovement characterMovement;
    private float baseFov;
    private float fovVelocity;

    void Start()
    {
        // cam = GetComponent<Camera>();
        baseFov = cinemachineVirtualCamera.m_Lens.FieldOfView;
        characterMovement = GetComponentInParent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update() { }

    private void FixedUpdate()
    {
        float targetFov = Mathf.Lerp(
            baseFov,
            highSpeedFov,
            FovFactorAlongSpeed.Evaluate(characterMovement.getPlayerVelocity())
        );

        // cam.fieldOfView = targetFov;
        cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.SmoothDamp(
            cinemachineVirtualCamera.m_Lens.FieldOfView,
            targetFov,
            ref fovVelocity,
            smoothFov
        );
    }
}
