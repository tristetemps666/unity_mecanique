using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SniperTrail : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer lineRenderer;

    public Material SniperTrailMat;

    public GameObject particulesTrail;

    float particuleRate = 0.0001f;
    int particulesToSpawn = 200;
    int maxAvancement = 50;

    void Start()
    {
        StartCoroutine(FadeOutTrail());
        StartCoroutine(SpawnParticulesAlongTrail());
    }

    // Update is called once per frame
    void Update() { }

    public void setupLine(Vector3 start, Vector3 end, int count)
    {
        lineRenderer.positionCount = count;
        for (int i = 0; i < count; i++)
        {
            if (i > maxAvancement)
                continue;
            Vector3 position = Vector3.Lerp(start, end, i / (count - 1f));
            lineRenderer.SetPosition(i, position);

            Debug.Log(
                i
                    + " // "
                    + "position qu'on veut set : "
                    + position
                    + " // position réelle : "
                    + lineRenderer.GetPosition(i)
            );
        }
    }

    public void setupLineRenderer()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = SniperTrailMat;
        lineRenderer.widthMultiplier = 0.5f;
    }

    private IEnumerator FadeOutTrail()
    {
        while (transform.localScale.y >= 0.01f)
        {
            // transform.localScale = transform.localScale - Vector3.up * Time.deltaTime; // marche pas
            if (lineRenderer != null)
            {
                lineRenderer.widthMultiplier -= Time.deltaTime;
            }

            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator SpawnParticulesAlongTrail()
    {
        // we don't spawn while it's null
        while (lineRenderer == null)
        {
            yield return null;
        }

        Vector3 start = lineRenderer.GetPosition(0);
        Vector3 end = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        // we start at 2 to avoid to spawn particules in the face of the player
        for (int i = 2; i < particulesToSpawn; i++)
        {
            Instantiate(particulesTrail);
            particulesTrail.transform.position = Vector3.Lerp(
                start,
                end,
                i / (particulesToSpawn - 1f)
            );
            yield return new WaitForSeconds(particuleRate);
        }
    }
}
