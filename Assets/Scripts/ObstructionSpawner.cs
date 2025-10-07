using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstructionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdsPrefab;
    [SerializeField] private GameObject cloudsPrefab;
    private GameObject _player;

    private readonly System.Collections.Generic.List<GameObject> activeObstructions
        = new System.Collections.Generic.List<GameObject>();

    [SerializeField] private float cloudSpeed = 1f;
    [SerializeField] private float birdSpeed = 3f;

    [Header("Spawner Settings")]
    [SerializeField] private float spawnInterval = 3f; // Adjustable spawn interval
    [SerializeField] private float minSpawnInterval = 1f; // optional (for randomization)
    [SerializeField] private float maxSpawnInterval = 5f; // optional (for randomization)
    [SerializeField] private bool randomizeSpawn = false; // toggle for randomness

    IEnumerator RepeatAction()
    {
        while (true)
        {
            float waitTime = randomizeSpawn
                ? Random.Range(minSpawnInterval, maxSpawnInterval)
                : spawnInterval;

            yield return new WaitForSeconds(waitTime);

            float probability = Random.Range(0f, 1f);
            GenerateObstruction(probability < 0.5f ? cloudsPrefab : birdsPrefab);
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(RepeatAction());
    }

    private void Update()
    {
        // Move all active obstructions backwards (negative Z)
        for (int i = activeObstructions.Count - 1; i >= 0; i--)
        {
            GameObject obj = activeObstructions[i];
            if (obj == null)
            {
                activeObstructions.RemoveAt(i);
                continue;
            }

            float speed = obj.CompareTag("Cloud") ? cloudSpeed : birdSpeed;
            obj.transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

            if (obj.transform.position.z < _player.transform.position.z - 15f)
            {
                Destroy(obj);
                activeObstructions.RemoveAt(i);
            }
        }
    }

    private void GenerateObstruction(GameObject prefab)
    {
        float offset = Random.Range(7.5f, 50f);
        Vector3 pos = _player.transform.position + new Vector3(0, 0, offset);
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

        activeObstructions.Add(obj);
    }
}
