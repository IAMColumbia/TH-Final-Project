using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOver, _heart1, _heart2, _heart3, _restartButton, _winMessage;

    public static int lives;

    private GameObject player;
    private WaveManager waveManager;

    void Start()
    {
        lives = 3;
        _heart1.gameObject.SetActive(true);
        _heart2.gameObject.SetActive(true);
        _heart3.gameObject.SetActive(true);
        _gameOver.gameObject.SetActive(false);
        _restartButton.SetActive(false);
        _winMessage.SetActive(false); // Hide win message initially

        player = GameObject.FindGameObjectWithTag("Player");
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
            }
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        lives = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowWinMessage()
    {
        _winMessage.SetActive(true);
        Time.timeScale = 0;
    }
}
