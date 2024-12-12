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
                Debug.Log("Difficulty set to Easy");
                lives = 3;
                break;
            case 1:
                Debug.Log("Difficulty set to Medium");
                lives = 3;
                break;
            case 2:
                Debug.Log("Difficulty set to Hard");
                lives = 2;
                break;
        }

        Debug.Log("Initial lives set to: " + lives); // Log initial lives

        UpdateLivesDisplay();

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
            Debug.Log("Player fell off. Current lives: " + lives);
            LoseLife();
        }
    }

    void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            Debug.Log("Lost a life. Current lives: " + lives);
            UpdateLivesDisplay();
            if (lives > 0)
            {
                player.transform.position = new Vector3(0, 1, 0);
                playerRb.velocity = Vector3.zero; // Reset velocity to lose momentum
            }
        }
    }

    public void UpdateLivesDisplay()
    {
        Debug.Log("Updating lives display. Current lives: " + lives); // Log when updating the display

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

    public void RestartLevel()
    {
        Time.timeScale = 1;
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
        Debug.Log("Lives reset on restart. Initial lives: " + lives);
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
