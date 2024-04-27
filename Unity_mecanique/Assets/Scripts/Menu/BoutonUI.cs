using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(AudioManager.Instance.PlayClickSound);
    }

    // Update is called once per frame
    void Update() { }
}
