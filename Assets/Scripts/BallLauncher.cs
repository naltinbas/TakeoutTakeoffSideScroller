using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BallLauncher : MonoBehaviour
{

    public GameObject plane;
    private Transform _launchPoint;       // Where to spawn the ball

    private const float Gravity = -9.81f;
    private readonly Vector3 _initialVelocity = Vector3.up * Gravity;

    [Tooltip("Destroy the ball after this many seconds (0 = never).")]
    public float lifeTime = 10f;
    
    public static bool isCoinCollected;
    private static bool _hasBallLaunched;

    private void Start()
    {
        _launchPoint = plane.transform;
    }

    void Update()
    {
        // Debug.unityLogger.Log(string.Format("isCoinCollected: {0}", isCoinCollected));
        if (Input.GetKeyDown(KeyCode.Space) && isCoinCollected)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            Launch();
        }else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetState();
        }
    }

    private void ResetState()
    {
        if (gameObject.name == "MainCoin")
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        isCoinCollected = false;
        _hasBallLaunched = false;
    }
    
    private IEnumerator ResetBallLaunched(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Wait for the specified time

        _hasBallLaunched = false;
    }
    
    public void Launch()
    {
        if (gameObject == null || _launchPoint == null || _hasBallLaunched)
        {
            Debug.LogWarning("Cannot launch the ball either already launched or null.");
            return;
        }

        // Create the ball
        GameObject ball = Instantiate(gameObject, _launchPoint.position, _launchPoint.rotation);

        // Ensure it has a Rigidbody
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = ball.AddComponent<Rigidbody>();
        }
        _hasBallLaunched = true;
        ball.GetComponent<Renderer>().enabled = true;

        // Reset velocity in case prefab had something
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Apply initial velocity
        rb.linearVelocity = _launchPoint.TransformDirection(_initialVelocity);

        // Optional lifetime
        if (lifeTime > 0f)
        {
            Destroy(ball, lifeTime);
            StartCoroutine(ResetBallLaunched(lifeTime));
        }
    }
}