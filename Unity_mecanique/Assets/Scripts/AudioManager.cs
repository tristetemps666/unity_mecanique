using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Tooltip("this is used only in game, not in main menu")]
    public AudioSource CriticalHitSFX;

    [Tooltip("this is used only in game, not in main menu")]
    public AudioSource SmallCriticalHitSFX;

    [Tooltip("this is used only in game, not in main menu")]
    public AudioSource PlayerHitHitSFX;

    [Space]
    public AudioSource DragSliderSound;
    public AudioSource ClickSound;
    public AudioSource HoverSound;

    [Space]
    public AudioMixer GeneralMixer;

    [Space]
    [SerializeField]
    AudioSource[] AudioSourcesNotAffectedByPause;

    // public AudioMixer SFXMixer;
    // public AudioMixer MusicMixer;

    // Start is called before the first frame update

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (AudioSource audioSource in AudioSourcesNotAffectedByPause)
        {
            audioSource.ignoreListenerPause = true;
        }
        UpdateVolumeSettings();
    }

    public void playCritical()
    {
        if (CriticalHitSFX.isPlaying)
            return;
        CriticalHitSFX.Play();
    }

    public void playSmallCritical()
    {
        // if (SmallCriticalHitSFX.isPlaying)
        //     return;
        SmallCriticalHitSFX.Play();
    }

    public void PlayDragSliderSound()
    {
        if (DragSliderSound.isPlaying)
            return;
        DragSliderSound.Play();
    }

    public void PlayClickSound()
    {
        ClickSound.Play();
    }

    public void PlayHoverSound()
    {
        HoverSound.Play();
    }

    public void PlayPlayerHit()
    {
        PlayerHitHitSFX.Play();
    }

    public void UpdateAudioMixerGeneral(float newValue)
    {
        GeneralMixer.SetFloat("VolumeMaster", 20f * Mathf.Log(newValue));
    }

    public void UpdateAudioMixerSFX(float newValue)
    {
        GeneralMixer.SetFloat("VolumeSFX", 20f * Mathf.Log(newValue));
    }

    public void UpdateAudioMixerMusic(float newValue)
    {
        GeneralMixer.SetFloat("VolumeMusic", 20f * Mathf.Log(newValue));
    }

    public void UpdateVolumeSettings()
    {
        UpdateAudioMixerGeneral(PlayerPrefs.GetFloat("MasterVolume"));
        UpdateAudioMixerSFX(PlayerPrefs.GetFloat("SFXVolume"));
        UpdateAudioMixerMusic(PlayerPrefs.GetFloat("MusicVolume"));
    }
}
