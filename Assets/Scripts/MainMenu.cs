using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown difficultyDropdown;

    void Start()
    {
        // Ensure the dropdown is set to the first option (easy) by default
        if (difficultyDropdown != null)
        {
            difficultyDropdown.value = 0;
        }
    }

    public void StartGame()
    {
        // Save the selected difficulty to PlayerPrefs
        PlayerPrefs.SetInt("Difficulty", difficultyDropdown.value);
        PlayerPrefs.Save();

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
