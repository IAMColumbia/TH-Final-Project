using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver, heart1, heart2, heart3;
    public static int lives;

    private GameObject player; // Reference to the player GameObject

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);

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
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(true);
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
}
