using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoBehaviour
{
    public ScriptableRendererData rendererData;

    private ScriptableRendererFeature WhiteRayFeature;
    private ScriptableRendererFeature BlackRayFeature;

    [SerializeField]
    Volume postProcessVolume;

    [SerializeField]
    Transform SourceRay;

    [SerializeField]
    Material BlackMat;

    // Start is called before the first frame update

    public static PostProcessManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start()
    {
        RetrivesRayFeatures();
        SetRayEffect(false);
    }

    public void RetrivesRayFeatures()
    {
        foreach (var feature in rendererData.rendererFeatures)
        {
            Debug.Log(feature.name);

            if (feature.name == "BlackOnRay")
            {
                BlackRayFeature = feature;
            }
            if (feature.name == "WhiteOnRay")
            {
                WhiteRayFeature = feature;
            }
        }
    }

    public void SetRayEffect(bool enabled)
    {
        WhiteRayFeature.SetActive(enabled);
        BlackRayFeature.SetActive(enabled);
        postProcessVolume.enabled = !enabled;
        if (enabled)
        {
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            Camera.main.backgroundColor = Color.black;
            BlackMat.SetVector(
                "_EmitPosition",
                new Vector4(SourceRay.position.x, SourceRay.position.y, SourceRay.position.z, 1f)
            );
        }
        else
        {
            Camera.main.clearFlags = CameraClearFlags.Skybox;
        }
    }
}
