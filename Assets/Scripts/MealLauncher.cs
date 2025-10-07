using System.Collections;
using UnityEngine;

public class MealLauncher : MonoBehaviour
{
    
    public GameObject plane;
    private AirplaneController _airplaneController;
    private Transform _launchPoint;       // Where to spawn the ball

    private const float Gravity = -9.81f;
    private readonly Vector3 _initialVelocity = Vector3.up * Gravity;

    [Tooltip("Destroy the meal after this many seconds (0 = never).")]
    public float lifeTime = 10f;

    public static bool isLaunching;
    private static int _numMealCollected = 0;
    private static int _numLaunchedMeals = 0;
    private static int _remainingMeals = 3;

    public bool IsLaunched { get; private set; }
    public bool IsCollected { get; private set; }

    private void Start()
    {
        _launchPoint = plane.transform;
        _airplaneController = plane.GetComponent<AirplaneController>();
    }

    public void Collect(GameObject mealGo)
    {
        if(gameObject == mealGo)
        {
            IsCollected = true;
            _remainingMeals--;
            _numMealCollected++;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&!IsLaunched && IsCollected)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            Launch();
        }else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetState();
        }
    }

    public void ResetState()
    {
        if (gameObject.CompareTag("Coin"))
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        _numMealCollected = _numLaunchedMeals = 0;
        _remainingMeals = 3;
        IsLaunched = false;
        IsCollected = false;
        _airplaneController.Reset();
        UpdateMealCounterUI();
        ObjectiveManager.SetObjectiveText("Collect Meal");
        ObjectiveManager.SetObjectiveColor(false);
    }
    
    private IEnumerator ResetBallLaunched(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Wait for the specified time

        isLaunching = false;
        ObjectiveManager.SetObjectiveText("Deliver Meal into Target");
        ObjectiveManager.SetObjectiveColor(true);
    }
    
    public void UpdateMealCounterUI()
    {
        ObjectiveManager.SetMealCounterText(_numMealCollected - _numLaunchedMeals, _remainingMeals);
    }
    
    public void Launch()
    {
        if (gameObject == null || _launchPoint == null || IsLaunched || isLaunching)
        {
            Debug.LogWarning("Cannot deliver the meal either already dropped or null.");
            return;
        }

        isLaunching = true;

        // Create the meal
        GameObject meal = Instantiate(gameObject, _launchPoint.position, _launchPoint.rotation);

        // Ensure it has a Rigidbody
        Rigidbody rb = meal.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = meal.AddComponent<Rigidbody>();
        }
        MealLauncher mealLauncher = meal.GetComponent<MealLauncher>();
        IsLaunched = mealLauncher.IsLaunched = mealLauncher.IsCollected = true;
        _numLaunchedMeals++;
        UpdateMealCounterUI();
        ObjectiveManager.SetObjectiveColor(false);
        ObjectiveManager.SetObjectiveText("Cooldown for relaunch");
        meal.GetComponentInChildren<MeshRenderer>().enabled = true;
        // Reset velocity in case prefab had something
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Apply initial velocity
        rb.linearVelocity = _launchPoint.TransformDirection(_initialVelocity);

        // Optional lifetime
        if (lifeTime > 0f)
        {
            Destroy(meal, lifeTime);
            StartCoroutine(ResetBallLaunched(lifeTime));
        }
    }
}