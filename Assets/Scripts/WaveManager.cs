using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int totalWaves = 4; // Total waves to win
    private int currentWave = 0;
    private int enemiesDefeated = 0;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        Debug.Log("Enemies Defeated: " + enemiesDefeated);
    }

    public void WaveCompleted()
    {
        currentWave++;
        Debug.Log("Wave Completed: " + currentWave); // Add debug log to track waves
        enemiesDefeated = 0; // Reset defeated enemy count for the new wave
        if (currentWave >= totalWaves)
        {
            gameManager.ShowWinMessage();
        }
    }

    public int GetEnemiesDefeated()
    {
        return enemiesDefeated;
    }

    public void ResetWaves()
    {
        currentWave = 0;
        enemiesDefeated = 0;
    }
}
