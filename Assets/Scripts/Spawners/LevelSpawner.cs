using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour {

    // Serialised fields
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject statusPrefab;
    private CameraActor cameraScript;
    

	// Use this for initialization
	void Start () {
        // Finds the game object with the camera script on
        cameraScript = GameObject.FindObjectOfType<CameraActor>();

        // Checks if the serialised field are filled
        //---------------------------------------------------------------------------------
        Debug.Assert(ground != null);
        Debug.Assert(cameraScript != null);
        Debug.Assert(statusPrefab != null);



        // Use to spawn the ground and its level generator
        //---------------------------------------------------------------------------------
        ScoreAndLevel scoreAndLevelInstance = GameObject.FindObjectOfType<ScoreAndLevel>();

        // This checks if there is already an instance of it
        if (scoreAndLevelInstance == null)
        {
            scoreAndLevelInstance = Instantiate(statusPrefab).GetComponent<ScoreAndLevel>();        // Creates an instance of the object component
            DontDestroyOnLoad(scoreAndLevelInstance.gameObject);                                    // Stops the instance being destroyed
        }

        //Debug.Log("This is in LevelSpawner " + ScoreAndLevel.Instance.currentLevel);



        // Use to spawn the ground and the player
        // --------------------------------------------------------------------------------
        Vector3 spawnPoint = new Vector3(0, 0, 0);
        Instantiate(ground, spawnPoint, Quaternion.identity);                                       // Creates an instance of ground
    }

    // Update is called once per frame
    void Update () {
    }
}
