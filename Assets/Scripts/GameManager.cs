using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOver, _heart1, _heart2, _heart3, _restartButton, _exitButton, _winMessage;

    public static int lives;

    private GameObject player;
    private Rigidbody playerRb;
    private WaveManager waveManager;

    void Start()
    {
        // Retrieve the selected difficulty from PlayerPrefs
        int difficulty = PlayerPrefs.GetInt("Difficulty", 0); // Default to Easy (0) if not set
        switch (difficulty)
        {
            case 0:
                // Easy settings
                Debug.Log("Difficulty set to Easy");
                // Adjust game settings for easy mode (e.g., more lives, slower enemies, etc.)
                lives = 5;
                // Example: slower enemy speed
                break;
            case 1:
                // Medium settings
                Debug.Log("Difficulty set to Medium");
                // Adjust game settings for medium mode (e.g., default lives, normal enemy speed)
                lives = 3;
                break;
            case 2:
                // Hard settings
                Debug.Log("Difficulty set to Hard");
                // Adjust game settings for hard mode (e.g., fewer lives, faster enemies)
                lives = 2;
                // Example: faster enemy speed
                break;
        }

        _heart1.gameObject.SetActive(true);
        _heart2.gameObject.SetActive(true);
        _heart3.gameObject.SetActive(true);
        _gameOver.gameObject.SetActive(false);
        _restartButton.SetActive(false);
        _exitButton.SetActive(false);
        _winMessage.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();

        if (playerRb == null)
        {
            Debug.LogError("Rigidbody component is missing from the player.");
        }

        waveManager = GameObject.FindObjectOfType<WaveManager>();
    }

    void Update()
    {
        if (player.transform.position.y < -10)
        {
            LoseLife();
        }

        switch (lives)
        {
            case 5:
            case 3:
                _heart1.gameObject.SetActive(true);
                _heart2.gameObject.SetActive(true);
                _heart3.gameObject.SetActive(true);
                break;
            case 2:
                _heart1.gameObject.SetActive(true);
                _heart2.gameObject.SetActive(true);
                _heart3.gameObject.SetActive(false);
                break;
            case 1:
                _heart1.gameObject.SetActive(true);
                _heart2.gameObject.SetActive(false);
                _heart3.gameObject.SetActive(false);
                break;
            case 0:
                _heart1.gameObject.SetActive(false);
                _heart2.gameObject.SetActive(false);
                _heart3.gameObject.SetActive(false);
                _gameOver.gameObject.SetActive(true);
                _restartButton.SetActive(true);
                _exitButton.SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }

    void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            if (lives > 0)
            {
                player.transform.position = new Vector3(0, 1, 0);
                playerRb.velocity = Vector3.zero; // Reset velocity to lose momentum
            }
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        // Reset lives based on difficulty
        int difficulty = PlayerPrefs.GetInt("Difficulty", 0); // Default to Easy (0) if not set
        switch (difficulty)
        {
            case 0:
                lives = 5;
                break;
            case 1:
                lives = 3;
                break;
            case 2:
                lives = 2;
                break;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game.");
        Application.Quit();
    }

    public void ShowWinMessage()
    {
        _winMessage.SetActive(true);
        _restartButton.SetActive(true);
        _exitButton.SetActive(true);
        Time.timeScale = 0;
    }
}
