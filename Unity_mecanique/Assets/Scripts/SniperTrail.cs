using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SniperTrail : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer lineRenderer;

    public Material SniperTrailMat;

    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void setupLine(Vector3 start, Vector3 end, int count)
    {
        lineRenderer.positionCount = count;
        for (int i = 0; i < count; i++)
        {
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
    }
}
