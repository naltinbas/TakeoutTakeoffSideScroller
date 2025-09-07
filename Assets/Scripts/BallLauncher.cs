using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BallLauncher : MonoBehaviour
{
    [FormerlySerializedAs("ballPrefab")] [Header("Ball Setup")]
    private GameObject _ball;       // The ball prefab with a Rigidbody

    public GameObject plane;
    private Transform _launchPoint;       // Where to spawn the ball

    [Header("Launch Settings")]
    public Vector3 initialVelocity = new Vector3(0, 5, 10);

    [Tooltip("Destroy the ball after this many seconds (0 = never).")]
    public float lifeTime = 10f;
    
    public static bool isCoinCollected;
    private static bool hasBallLaunched;

    private void Start()
    {
        _ball = gameObject;
        _launchPoint = plane.transform;
    }

    void Update()
    {
        Debug.unityLogger.Log(string.Format("isCoinCollected: {0}", isCoinCollected));
        if (Input.GetKeyDown(KeyCode.Space) && isCoinCollected)
        {
            Renderer r = _ball.GetComponent<Renderer>();
            r.enabled = true;
            if (r.gameObject.name == "MainCoin")
            {
                r.enabled = false;
            }
            Launch();
        }
    }
    
    private IEnumerator ResetBallLaunched(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Wait for the specified time

        hasBallLaunched = false;
    }
    
    public void Launch()
    {
        if (this._ball == null || _launchPoint == null || hasBallLaunched)
        {
            Debug.LogWarning("Cannot launch the ball either already launched or null.");
            return;
        }

        // Create the ball
        GameObject ball = Instantiate(this._ball, _launchPoint.position, _launchPoint.rotation);

        // Ensure it has a Rigidbody
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = ball.AddComponent<Rigidbody>();
        }
        hasBallLaunched = true;

        // Reset velocity in case prefab had something
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Apply initial velocity
        rb.linearVelocity = _launchPoint.TransformDirection(initialVelocity);

        // Optional lifetime
        if (lifeTime > 0f)
        {
            Destroy(ball, lifeTime);
            StartCoroutine(ResetBallLaunched(lifeTime));
        }
    }
}