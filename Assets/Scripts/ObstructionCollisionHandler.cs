using Unity.VisualScripting;
using UnityEngine;

public class ObstructionCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.CompareTag("Player"))
        {
            Destroy(gameObject);
            AudioSourceManager.PlaySound(gameObject.name);
        }
    }
}
