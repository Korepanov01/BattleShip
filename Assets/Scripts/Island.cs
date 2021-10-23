using UnityEngine;
using System.Collections.Generic;

public class Island : MonoBehaviour
{
    public float spawnRadius;
    public GameObject enemyPrefab;
    public GameObject playerIslandPrefab;
    public GameObject battleZonePrefab;

    private bool activated = false;
    private List<GameObject> enemies = new List<GameObject>();
    private Transform playerTransform;
    private EnemiesController enemiesController;
    private GameObject battleZone;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemiesController = GameObject.FindGameObjectWithTag("EnemiesController").GetComponent<EnemiesController>();
    }

    void Update()
    {
        var playerPosition = playerTransform.position;
        if (IsPlayerInRadius())
        {
            if (!activated)
            {
                activated = true;
                SpawnEnemies();
                battleZone = Instantiate(battleZonePrefab, new Vector3(transform.position.x, transform.position.y, 1), new Quaternion());
            }
            else
            {
                if (!IsThereAliveEnemy())
                {
                    Destroy(gameObject);
                    Destroy(battleZone);
                    Instantiate(playerIslandPrefab, transform.position, transform.rotation);
                    enemiesController.IncreaseDifficulty();
                }
            }
        }
        else
        {
            if (activated)
            {
                Destroy(battleZone);
                DespawnEnemies();
                activated = false;
            }
        }
    }

    bool IsThereAliveEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy)
            {
                return true;
            }
        }
        return false;
    }

    void DespawnEnemies()
    {
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies = new List<GameObject>();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesController.enemiesSpawnCount; i++)
        {
            var circle = Random.insideUnitCircle * 5;
            var enemyPosition = new Vector3(transform.position.x + circle.x, transform.position.y + circle.y, 0);
            var enemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation);
            enemies.Add(enemy);
        }
    }

    bool IsPlayerInRadius()
    {
        return Mathf.Pow(playerTransform.position.x - transform.position.x, 2) + Mathf.Pow(playerTransform.position.y - transform.position.y, 2) < spawnRadius * spawnRadius;
    }
}


