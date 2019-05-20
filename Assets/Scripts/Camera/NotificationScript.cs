using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotificationScript : MonoBehaviour {

    // Creates private variables
    private Text notification;                                              // The notification text variable
    private ScoreAndLevel currentStatus;                                    // Singleton score and level instance for checking the current status of the game
    private float pauseTimer;                                               // Pause timer

    // Sets the text component as notification when the scripts first started
    void Awake()
    {
        notification = GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
        notification.text = "";                                             // Resets the notification text
    }

    // Update is called once per frame
    void Update () {
        currentStatus = ScoreAndLevel.Instance;                             // Sets the current status

        // This checks if the game requires to create new level
        if (currentStatus.createNewLevel == true)
        {
            // The game requires a new level if the players is destroyed or reach the end of the level
            if (currentStatus.playerDestroyed == true)
            {
                // Tells the player that it is game over and pause for 5 seconds to show the notification
                notification.text = "Game Over";
                PauseGame(5);
            }
            else
            {
                // Tells the player that they reach a new level and pause for 2 seconds for them to read the text
                notification.text = "Level " + currentStatus.currentLevel + "\n";
                PauseGame(2);
            }
        }
    }

    // This is used to paused the game
    void PauseGame(float timer)
    {
        // Checks if the game is still running
        if (Time.timeScale > 0)
        {
            // Pauses the game by setting the time scale to 0
            pauseTimer = timer;                                             // Sets the amount of seconds the game need to pause for the player to read the text
            currentStatus.notificationShowed = false;                       // Sets the notification showed to false to give player time to read
            Time.timeScale = 0;                                             // Pauses the game
        }

        pauseTimer -= Time.unscaledDeltaTime;                               // Decrements the pause timer as time passes
        
        // Resets the game when timer finally reaches zero
        if (pauseTimer <= 0)
        {
            notification.text = "";                                         // Resets the text to none
            Time.timeScale = 1;                                             // Resets the time scale back to normal
            SceneManager.LoadScene("Game");                                 // Reloads the scene
            currentStatus.createNewLevel = false;                           // Resets the current status create new level
        }
    }
}
