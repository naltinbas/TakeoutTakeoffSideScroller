using System;
using UnityEngine;

public class CoinCollideHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MealLauncher mealLauncher = GetComponent<MealLauncher>();
        bool isCollected = mealLauncher.IsCollected;
        if (other.transform.root.CompareTag("Player") && !isCollected)
        {
            var playerTag = other.transform.parent.tag;
            AudioSourceManager.PlaySound(playerTag);
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            mealLauncher.Collect(gameObject);
            mealLauncher.UpdateMealCounterUI();
            if (MealLauncher.isLaunching) return;
            ObjectiveManager.SetObjectiveText("Deliver Meal into Target");
            ObjectiveManager.SetObjectiveColor(true);
        }

        if (other.CompareTag("Target") || other.CompareTag("Ground"))
        {
            var tag = other.gameObject.tag;
            AudioSourceManager.PlaySound(tag);
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            Destroy(gameObject);
        }
    }
}