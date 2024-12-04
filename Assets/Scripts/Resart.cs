using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resart : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1;
        GameManager.lives = 4;
        SceneManager.LoadScene(0);
        
    }

}
