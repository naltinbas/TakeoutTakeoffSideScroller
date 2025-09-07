using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class CoinCollector : MonoBehaviour
{
    private AudioSource _audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isCoinCollected = BallLauncher.isCoinCollected;
        if (other.CompareTag("Player") && !isCoinCollected)
        {
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position);
            gameObject.GetComponent<Renderer>().enabled = false;
            BallLauncher.isCoinCollected = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
