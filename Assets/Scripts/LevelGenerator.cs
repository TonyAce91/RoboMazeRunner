using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelGenerator : MonoBehaviour
{

    // Serialised Fields
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject endOfLevel;
    [SerializeField] private int obstacleGap = 20;
    [SerializeField] private List<Texture> wallTextures = new List<Texture>();

    private Transform groundTransform;
    private Renderer obstacleRenderer;
    private Renderer groundRenderer;
    private ScoreAndLevel currentStatus;
    private GameObject obstacleInstance;


    // Use this for initialization
    void Start()
    {

        currentStatus = ScoreAndLevel.Instance;

        currentStatus.createNewLevel = false;
        currentStatus.playerDestroyed = false;
        currentStatus.notificationShowed = false;

        // Creates private reference to components of ground and obstacle
        //-------------------------------------------------------------------------------
        groundTransform = this.transform;                                                       // Create a reference to the transform component of ground
        groundRenderer = this.GetComponent<Renderer>();                                         // Get the renderer component of ground
        obstacleRenderer = obstacle.GetComponent<Renderer>();                           // Creates a reference to the renderer component of obstacle
        //Debug.Log(groundTransform.localScale.x);
        //if (wallTextures.Count > 0)
        //{
        //    obstacleRenderer.material.SetTexture
        //}


        // Creates the level depending on the level
        //-------------------------------------------------------------------------------
        groundTransform.localScale += new Vector3(50 * currentStatus.currentLevel, 0, 0);       // Increase the scale of ground to match the level it is in
        Vector3 groundPos = groundTransform.position;
        groundPos.x = (groundRenderer.bounds.size).x / 2;
        groundTransform.position = groundPos;


        // Sets the obstacle size so that it can be manipulated in level generation
        Vector3 obstacleSize = obstacleRenderer.bounds.size;

        // Creates a randomised position of the walls in game
        for (int i = (obstacleGap + currentStatus.currentLevel*3); i < (groundRenderer.bounds.size).x;)
        {
            int hole = Random.Range(1, 4);
            for (int j = 1; j < 4; j++)
            {
                if (j != hole)
                {
                    Vector3 spawnPoint = new Vector3(i, obstacleSize.y / 2, (j*10)-20);
                    Instantiate(obstacle, spawnPoint, Quaternion.identity);
                }
            }
            i += obstacleGap;
            obstacle.tag = "Obstacle";
        }

        // Sets the end level collision object at the end of the level
        Vector3 spawnPointForEnd = new Vector3((groundRenderer.bounds.size).x, obstacleSize.y / 2, 0);
        Instantiate(endOfLevel, spawnPointForEnd, Quaternion.identity);
        endOfLevel.tag = "New Level";

        StaticOcclusionCulling.Compute();

    }
}
