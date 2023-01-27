using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnAreaSize;
    [SerializeField] float spawnInterval;
    float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            spawnTimer += spawnInterval;
            Vector3 spawnPos = new Vector3(
                Random.Range(-spawnAreaSize.x * 0.5f, spawnAreaSize.x * 0.5f),
                Random.Range(-spawnAreaSize.y * 0.5f, spawnAreaSize.y * 0.5f),
                0);
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }
}
