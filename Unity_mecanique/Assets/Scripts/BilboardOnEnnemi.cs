using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardOnEnnemi : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform ennemiTransform;

    public float offset = 1f;

    private RectTransform rectTransform;

    private Camera playerCam;

    private CanvasGroup canvasGroup;

    void Start()
    {
        playerCam = FindFirstObjectByType<Camera>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.position = playerCam.WorldToScreenPoint(
            ennemiTransform.position + Vector3.up * offset
        );

        canvasGroup.alpha = Vector3.Dot(transform.forward, playerCam.transform.forward) < 0 ? 0 : 1;
    }
}
