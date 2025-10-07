using System;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    public void Explode()
    {
        MealLauncher[] mealLaunchers = FindObjectsOfType<MealLauncher>();
        foreach (var mealLauncher in mealLaunchers)
        {
            mealLauncher.ResetState();
        }
        AudioSourceManager.PlayTempSound("crash");
    }
}
