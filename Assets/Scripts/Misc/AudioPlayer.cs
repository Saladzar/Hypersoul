using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void PlayAudioClip(AudioClipType clipType)
    {
        int index = (int)clipType;

        AudioClip clipToPlay = audioClips[index];
        audioSource.PlayOneShot(clipToPlay);
    }
}

public enum AudioClipType
{
    Enter = 0,
    Quit,
    Bounce,
    Die
}
