using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class CoinCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        bool isCoinCollected = BallLauncher.isCoinCollected;
        String tag = other.gameObject.tag;
        if (other.CompareTag("Player") && !isCoinCollected)
        {
            AudioSourceManager.PlaySound(tag);
            gameObject.GetComponent<Renderer>().enabled = false;
            BallLauncher.isCoinCollected = true;
        }

        if (other.CompareTag("Target") || other.CompareTag("Ground"))
        {
            AudioSourceManager.PlaySound(tag);
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }
}
