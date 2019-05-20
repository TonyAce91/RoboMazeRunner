using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAndLevel : MonoBehaviour {


    // Variables that will be used by other objects in the game
    [HideInInspector] public int score = 0;
    [HideInInspector] public int currentLevel = 1;
    [HideInInspector] public bool createNewLevel = false;
    [HideInInspector] public bool playerDestroyed = false;
    [HideInInspector] public bool notificationShowed = false;
    [HideInInspector] public bool gamePaused = true;

    // Creates a single instance of this class
    private static ScoreAndLevel instance;

    public static ScoreAndLevel Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
