using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlaneBoost : MonoBehaviour
{
    [Header("Boost Settings")]
    [SerializeField] private float boostMultiplier = 2f; // How much faster during boost
    [SerializeField] private float boostDuration = 3f;   // Duration of boost in seconds

    [Header("UI")]
    [SerializeField] private Image boostIcon; // Assign your Boost_Icon.png UI Image here
    private AirplaneController controller;
    private bool boostAvailable = false;
    private bool isBoosting = false;
    private float originalSpeed;

    void Awake()
    {
        controller = GetComponent<AirplaneController>();

        // Hide the icon at start (in case it’s still visible)
        if (boostIcon != null)
            boostIcon.gameObject.SetActive(false);
    }

    void Update()
    {
        // Only trigger if boost is available and not already active
        if (boostAvailable && !isBoosting && Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(DoBoost());
        }
    }

    public void EnableBoost()
    {
        boostAvailable = true;
        Debug.Log("Boost Available!");

        // Show the boost icon
        if (boostIcon != null)
            boostIcon.gameObject.SetActive(true);
    }

    private IEnumerator DoBoost()
    {
        boostAvailable = false;
        isBoosting = true;

        // Hide the icon once boost is used
        if (boostIcon != null)
            boostIcon.gameObject.SetActive(false);

        // Save original speed and apply boosted speed
        originalSpeed = controller.airplaneSpeed;
        controller.airplaneSpeed *= boostMultiplier;
        Debug.Log("Boost Activated!");

        // Wait for duration
        yield return new WaitForSeconds(boostDuration);

        // Restore normal speed
        controller.airplaneSpeed = originalSpeed;
        isBoosting = false;
        Debug.Log("Boost Ended!");
    }
}
