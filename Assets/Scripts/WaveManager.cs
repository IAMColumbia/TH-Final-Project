using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int totalWaves = 4; // Default total waves to win
    private int _currentWave = 0;
    private int _enemiesDefeated = 0;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        AdjustDifficulty();
    }

    public void EnemyDefeated()
    {
        _enemiesDefeated++;
        Debug.Log("Enemies Defeated: " + _enemiesDefeated);
    }

    public void WaveCompleted()
    {
        _currentWave++;
        Debug.Log("Wave Completed: " + _currentWave); // Add debug log to track waves
        _enemiesDefeated = 0; // Reset defeated enemy count for the new wave
        if (_currentWave >= totalWaves)
        {
            gameManager.ShowWinMessage();
        }
    }

    public int GetEnemiesDefeated()
    {
        return _enemiesDefeated;
    }

    public void ResetWaves()
    {
        _currentWave = 0;
        _enemiesDefeated = 0;
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
                totalWaves = 3; // Fewer waves for easy mode
                // Adjust other settings as needed for easy mode
                break;
            case 1:
                // Medium settings
                Debug.Log("Difficulty set to Medium");
                totalWaves = 4; // Default waves for medium mode
                // Adjust other settings as needed for medium mode
                break;
            case 2:
                // Hard settings
                Debug.Log("Difficulty set to Hard");
                totalWaves = 5; // More waves for hard mode
                // Adjust other settings as needed for hard mode
                break;
        }
    }
}
