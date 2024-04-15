using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource CriticalHitSFX;

    // Start is called before the first frame update

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void playCritical()
    {
        if (CriticalHitSFX.isPlaying)
            return;
        CriticalHitSFX.Play();
    }
}
