using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    // Start is called before the first frame update
    public float spreadSpeed = 2f;

    public float delayBeforeFade = 3f;
    public float fadeWaveTime = 2f;

    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        Invoke("StartFadeWave", delayBeforeFade);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime * spreadSpeed;
    }

    void StartFadeWave()
    {
        StartCoroutine(FadeWave());
    }

    IEnumerator FadeWave()
    {
        float time = fadeWaveTime;
        while (time > 0)
        {
            time -= Time.deltaTime;
            renderer.material.SetFloat("_alphaFade", time / fadeWaveTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
