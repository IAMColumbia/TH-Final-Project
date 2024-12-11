using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        // Exit the game
        Debug.Log("Exiting game.");
        Application.Quit();
    }
}
