using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject powerupPrefab;

    private float _spawnRange = 9.0f;
    private int _enemyCount;
    private int _waveNumber = 1;
    private int _enemiesToSpawn = 1; // Start with 1 enemy in the first wave

    private WaveManager waveManager;

    void Start()
    {
        waveManager = GameObject.FindObjectOfType<WaveManager>();
        AdjustDifficulty();
        SpawnEnemyWave(_enemiesToSpawn);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    void Update()
    {
        _enemyCount = FindObjectsOfType<Enemy>().Length;
        if (_enemyCount == 0)
        {
            Debug.Log("All enemies defeated in wave: " + _waveNumber);
            if (waveManager.GetEnemiesDefeated() >= _enemiesToSpawn)
            {
                Debug.Log("Wave " + _waveNumber + " completed.");
                waveManager.WaveCompleted(); // Notify WaveManager of wave completion
                _waveNumber++;
                if (_waveNumber <= waveManager.totalWaves) // Check if we should continue spawning
                {
                    _enemiesToSpawn++; // Increase the number of enemies by 1 for the next wave
                    SpawnEnemyWave(_enemiesToSpawn);
                    Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
                    Debug.Log("Wave " + _waveNumber + " started with " + _enemiesToSpawn + " enemies."); // Debug log to track spawning
                }
            }
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
        Debug.Log("Spawned " + enemiesToSpawn + " enemies for wave " + _waveNumber); // Debug log to track spawning
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-_spawnRange, _spawnRange);
        float spawnPosZ = Random.Range(-_spawnRange, _spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    void AdjustDifficulty()
    {
        // Retrieve the selected difficulty from PlayerPrefs
        int difficulty = PlayerPrefs.GetInt("Difficulty", 0); // Default to Easy (0) if not set
        switch (difficulty)
        {
            case 0:
                // Easy settings
                Debug.Log("Difficulty set to Easy");
                _enemiesToSpawn = 1; // Start with fewer enemies
                // Adjust other settings for easy mode as needed
                break;
            case 1:
                // Medium settings
                Debug.Log("Difficulty set to Medium");
                _enemiesToSpawn = 2; // Start with a medium number of enemies
                // Adjust other settings for medium mode as needed
                break;
            case 2:
                // Hard settings
                Debug.Log("Difficulty set to Hard");
                _enemiesToSpawn = 3; // Start with more enemies
                // Adjust other settings for hard mode as needed
                break;
        }
    }
}
