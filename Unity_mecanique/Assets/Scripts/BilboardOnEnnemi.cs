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
        // the canvas is always above the ennemi
        rectTransform.position = playerCam.WorldToScreenPoint(
            ennemiTransform.position + Vector3.up * offset
        );

        float distance = Vector3.Distance(ennemiTransform.position, playerCam.transform.position);

        // the canvas doesn't appears if we are not in front of the player
        canvasGroup.alpha =
            Vector3.Dot(
                ennemiTransform.position - playerCam.transform.position,
                playerCam.transform.forward
            ) < 0.5f
                ? 0
                : 1;
    }
}
