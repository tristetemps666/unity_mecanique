using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class RecoilShake
{
    public float shakeAmmount = 0f;
    public float shakeFalloffSpeed = 2f;

    public float shakeAmmountRecoil = 2f;
}

public class CameraShakeManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    CinemachineVirtualCamera cameraController;

    CinemachineBasicMultiChannelPerlin cameraControllerNoise;

    [SerializeField]
    float maxShakeAmount = 5f;

    [Space]
    [SerializeField]
    RecoilShake recoilGunSettings;

    [Space]
    [SerializeField]
    RecoilShake recoilSniperSettings;

    void Start()
    {
        cameraControllerNoise =
            cameraController.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShake(recoilGunSettings);
        UpdateShake(recoilSniperSettings);

        ApplyShake();
    }

    public void SetShakeAmount(RecoilShake recoilShake)
    {
        recoilShake.shakeAmmount = recoilShake.shakeAmmountRecoil;
    }

    void UpdateShake(RecoilShake recoilShake)
    {
        recoilShake.shakeAmmount = Mathf.Max(
            recoilShake.shakeAmmount - Time.deltaTime * recoilShake.shakeFalloffSpeed,
            0f
        );
    }

    void ApplyShake()
    {
        cameraControllerNoise.m_AmplitudeGain = Mathf.Min(
            maxShakeAmount,
            recoilGunSettings.shakeAmmount + recoilSniperSettings.shakeAmmount
        );
    }

    public void GunShake()
    {
        SetShakeAmount(recoilGunSettings);
        Debug.Log("gunshake");
    }

    public void SniperShake()
    {
        SetShakeAmount(recoilSniperSettings);
        Debug.Log("snipershake");
    }
}
