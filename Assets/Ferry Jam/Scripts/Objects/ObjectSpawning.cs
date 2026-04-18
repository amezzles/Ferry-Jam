using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] prefabsToSpawn;
    public float initialSpawnRate = 2.0f;
    public float minSpawnRate = 0.5f; // The fastest it will ever spawn

    [Header("Difficulty Scaling")]
    [Tooltip("How much the spawn rate decreases every second")]
    public float spawnDifficultyFactor = 0.02f;
    
    [Header("Position Settings")]
    public float spawnY = 10f;
    public float spawnXRange = 8f;

    private float _currentSpawnRate;
    private float _timer;

    // We make this 'static' so the FallingObjects can read it easily
    public static float GlobalSpeedMultiplier = 1f;
    public float speedIncreasePerSecond = 0.01f;

    void Start()
    {
        _currentSpawnRate = initialSpawnRate;
        GlobalSpeedMultiplier = 1f; // Reset speed on start
    }

    void Update()
    {
        // 1. Scale Difficulty
        if (_currentSpawnRate > minSpawnRate)
        {
            _currentSpawnRate -= spawnDifficultyFactor * Time.deltaTime;
        }

        GlobalSpeedMultiplier += speedIncreasePerSecond * Time.deltaTime;

        // 2. Spawn Timer
        _timer += Time.deltaTime;
        if (_timer >= _currentSpawnRate)
        {
            SpawnObject();
            _timer = 0;
        }
    }

    void SpawnObject()
    {
        if (prefabsToSpawn.Length == 0) return;
        GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        float randomX = Random.Range(-spawnXRange, spawnXRange);
        Vector3 spawnPos = new Vector3(randomX, spawnY, 0);
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}