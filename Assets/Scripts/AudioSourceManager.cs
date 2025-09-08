using System;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    
    private static GameObject _audioObj; 
    private static AudioSource _audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioObj = new GameObject("AudioObject");
        _audioSource = _audioObj.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        _audioSource.clip = Resources.Load<AudioClip>(soundName);
        _audioSource.Play();
    }
}
