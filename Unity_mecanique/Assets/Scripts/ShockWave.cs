using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    // Start is called before the first frame update
    public float spreadSpeed = 2f;

    public float delayBeforeFade = 3f;
    public int dammage = 800;
    public float fadeWaveTime = 2f;

    [SerializeField]
    private LayerMask layerMaskIgnoringShockWave;

    private Renderer renderer;

    private Transform spawnTransform;

    private bool canDammage = true;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        Invoke("StartFadeWave", delayBeforeFade);

        spawnTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // we increase the size of the shockwave
        transform.localScale += Vector3.one * Time.deltaTime * spreadSpeed;
    }

    void StartFadeWave()
    {
        StartCoroutine(FadeWave());
        Invoke("DisableDammage", fadeWaveTime * 0.7f);
    }

    void OnTriggerEnter(Collider other)
    {
        // if we cannot dammage, there is nothing to do
        if (!canDammage)
            return;

        Debug.Log("nom de la personne touchée : " + other.name);

        if (other.gameObject.TryGetComponent(out IDammagable otherDammagable))
        {
            // we need to raycast to see if the character is projected by an obstacle
            RaycastHit hit;
            Debug.DrawLine(spawnTransform.position, other.transform.position, Color.green, 3f);
            if (
                Physics.Linecast(
                    other.transform.position,
                    spawnTransform.position,
                    out hit,
                    layerMaskIgnoringShockWave
                )
            )
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.tag == "BlockShockWave")
                {
                    Debug.Log("il est protégé !!");
                    return;
                }
            }
            Debug.Log("on peut lui enlever des pvs PAR EXPLOSION");
            otherDammagable.TakeDammage(dammage);
        }
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

    private void DisableDammage()
    {
        canDammage = false;
    }
}
