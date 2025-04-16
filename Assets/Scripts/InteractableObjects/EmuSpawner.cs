using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmuSpawner : MonoBehaviour
{
    public GameObject RunningEmuPrefab;
    public float spawnInterval = 5f;
    public Vector2 minSpawnBounds = new Vector2(-8f, -8f);
    public Vector2 maxSpawnBounds = new Vector2(8f, 8f);

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            for(int i = 0; i < 5; i++)
            {
                SpawnEmu();
            }
          timer = spawnInterval;
        }
    }

    void SpawnEmu()
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(minSpawnBounds.x, maxSpawnBounds.x),
            Random.Range(minSpawnBounds.y, maxSpawnBounds.y)
        );

        Instantiate(RunningEmuPrefab, randomPosition, Quaternion.identity);
    }
}