using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour {

    // This exits the current game to the menu when the menu button is pressed
    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
