using System;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    
    private static GameObject _audioObj; 
    private static AudioSource _audioSource;
    private static AudioSource _tempAudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioObj = new GameObject("AudioObject");
        _audioSource = _audioObj.AddComponent<AudioSource>();
        _audioSource.clip = Resources.Load<AudioClip>("flying");
        _audioSource.loop = true;
        _audioSource.volume = 0.5f;
        _audioSource.Play();
        _tempAudioSource = _audioObj.AddComponent<AudioSource>();
    }

    public static void PlayTempSound(string soundName)
    {
        _tempAudioSource.clip = Resources.Load<AudioClip>(soundName);
        _tempAudioSource.Play();
    }

    public static void PlaySound(String tag)
    {
        String soundName = "";
        switch (tag)
        {
            case "Player":
                soundName = "register";
                break;
            case "Target":
                soundName = "success";
                break;
            case "Ground":
                soundName = "failure";
                break;
            case "Birds":
                soundName = "chirping";
                break;
            case "Cloud":
                soundName = "thunder";
                break;
        }
        PlayTempSound(soundName);
    }
}
