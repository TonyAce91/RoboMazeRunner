using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private Text scoreCounter;
    private ScoreAndLevel currentStatus;
    [SerializeField] private Transform playerTransform;
    private int currentScore = 0;

    private float pauseTimer;

    void Awake()
    {
        // Gets the text component to show the player of their score
        scoreCounter = GetComponent<Text>();
    }

    // Use this for initialization
    void Start()
    {
        scoreCounter.text = "Score: " + currentScore;
    }

    // Update is called once per frame
    void Update () {
        currentStatus = ScoreAndLevel.Instance;

        // Adds the previous levels' scores to the current level score
        if (playerTransform != null)
            currentScore = currentStatus.score + (int)(playerTransform.position.x/2);
        
        // Shows the player their score
        scoreCounter.text = "Score: " + currentScore;
	}
}
