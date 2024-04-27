using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionInputsManager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Save Player Data Names")]
    [SerializeField]
    string mouseSensitivityDataName;

    [SerializeField]
    string globalVolumeDataName;

    [SerializeField]
    string sfxVolumeDataName;

    [SerializeField]
    string musicVolumeDataName;

    [Header("Option Sliders")]
    [SerializeField]
    private Slider mouseSensitivitySlider;

    [SerializeField]
    private Slider globalVolumeSlider;

    [SerializeField]
    private Slider sfxVolumeSlider;

    [SerializeField]
    private Slider musicVolumeSlider;

    void Start()
    {
        SetupSliders();
    }

    // Update is called once per frame
    void Update() { }

    void SetupSliders()
    {
        mouseSensitivitySlider.onValueChanged.AddListener(
            (float newValue) =>
            {
                SaveFloatValue(mouseSensitivityDataName, newValue);
            }
        );

        globalVolumeSlider.onValueChanged.AddListener(
            (float newValue) =>
            {
                SaveFloatValue(globalVolumeDataName, newValue);
            }
        );

        sfxVolumeSlider.onValueChanged.AddListener(
            (float newValue) =>
            {
                SaveFloatValue(sfxVolumeDataName, newValue);
            }
        );

        musicVolumeSlider.onValueChanged.AddListener(
            (float newValue) =>
            {
                SaveFloatValue(musicVolumeDataName, newValue);
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
