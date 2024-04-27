using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MenuSlider : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Save Player Data Names")]
    [SerializeField]
    string dataName;

    [Header("Sliders")]
    private Slider sliderComp;

    void Start()
    {
        sliderComp = GetComponent<Slider>();
        SetupSliders();
    }

    // Update is called once per frame
    void Update() { }

    void SetupSliders()
    {
        sliderComp.onValueChanged.AddListener(
            (float newValue) =>
            {
                SaveFloatValue(dataName, newValue);
                AudioManager.Instance.PlayDragSliderSound();
                AudioManager.Instance.UpdateVolumeSettings();
            }
        );
    }

    void SaveFloatValue(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        PlayerPrefs.Save();

        Debug.Log("nouvelle valeur : " + PlayerPrefs.GetFloat(name));
    }
}
