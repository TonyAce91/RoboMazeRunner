using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    // Starts the game when the start button is pressed
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Exits the game when the exit button is pressed
    public void ExitGame()
    {
        Application.Quit();
    }
}
