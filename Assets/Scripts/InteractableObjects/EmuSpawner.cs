using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmuSpawner : MonoBehaviour
{
    public GameObject RunningEmuPrefab;
    public GameObject EmuLeaderPrefab;
    public float spawnInterval = 5f;
    public Vector2 minSpawnBounds = new Vector2(-8f, -8f);
    public Vector2 maxSpawnBounds = new Vector2(8f, 8f);
    public Vector2 leaderSpawnPosition = Vector2.zero; // Always spawn leader here

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
            // Spawn 5 regular emus at random positions
            for (int i = 0; i < 5; i++)
            {
                SpawnEmu();
            }

            // Spawn 1 emu leader in the center (or defined point)
            SpawnLeader();

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

    void SpawnLeader()
    {
        Instantiate(EmuLeaderPrefab, leaderSpawnPosition, Quaternion.identity);
    }
}