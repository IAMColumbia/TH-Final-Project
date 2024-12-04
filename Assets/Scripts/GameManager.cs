using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOver, _heart1, _heart2, _heart3, _restartButton;

    public static int lives;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        _heart1.gameObject.SetActive(true);
        _heart2.gameObject.SetActive(true);
        _heart3.gameObject.SetActive(true);
        _gameOver.gameObject.SetActive(false);
        _restartButton.SetActive(false); // Hide restart button initially

        player = GameObject.FindGameObjectWithTag("Player"); // Make sure the player GameObject has the tag "Player"
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has fallen below a certain height
        if (player.transform.position.y < -10)
        {
            LoseLife();
        }

        // Update the hearts display based on the number of lives
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
                _restartButton.SetActive(true); // Show restart button
                Time.timeScale = 0; // Stop the game
                break;
        }
    }

    void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            if (lives > 0) // Only respawn if lives remain
            {
                player.transform.position = new Vector3(0, 1, 0); // Respawn the player to a safe position
            }
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        lives = 3; // Reset lives to 3
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
