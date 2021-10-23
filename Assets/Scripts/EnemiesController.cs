using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public GameObject bossPrefab;
    public GameObject enemyPrefab;

    public float enemySpeed = 1;
    public float enemyRotationSpeed = 0.25f;
    public int enemyHealth = 100;
    public int enemyDamage = 10;
    public int enemyOverhit = 60;
    public int enemiesSpawnCount = 4;

    public int enemyIslandsCount;

    private void Start()
    {
        enemyIslandsCount = GameObject.FindGameObjectsWithTag("EnemyIsland").Length;
    }

    public void IncreaseDifficulty()
    {
        enemyOverhit -= 5;
        enemySpeed *= 1.05f;
        enemyRotationSpeed *= 1.05f;
        enemyHealth += 40;
        enemyDamage += 15;
        enemiesSpawnCount += 1;

        enemyIslandsCount--;
        if (enemyIslandsCount == 0)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        for (int i = 0; i < 7; i++)
        {
            var circle = Random.insideUnitCircle * 20;
            Instantiate(enemyPrefab, circle, new Quaternion());
        }

        enemySpeed = 1.5f;
        enemyRotationSpeed = 0.3f;
        enemyDamage += 50;

        Instantiate(bossPrefab, new Vector2(10, 0), new Quaternion());
    }
}
