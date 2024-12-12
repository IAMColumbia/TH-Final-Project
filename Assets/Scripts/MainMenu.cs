using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown difficultyDropdown; // Reference to the difficulty dropdown
    public AudioSource mainMusic; // Reference to the AudioSource component for music

    void Start()
    {
        // Ensure the dropdown is set to the first option (easy) by default
        if (difficultyDropdown != null)
        {
            difficultyDropdown.value = 0;
        }

        // Play the background music
        if (mainMusic != null)
        {
            mainMusic.Play();
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

    public void ToggleDifficultyDropdown()
    {
        // Toggle the visibility of the dropdown (if you have a method to show/hide it)
        difficultyDropdown.gameObject.SetActive(!difficultyDropdown.gameObject.activeSelf);
    }
}
