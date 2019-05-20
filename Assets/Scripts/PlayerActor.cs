using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    // Hidden public variable
    [HideInInspector]  public float speed;

    // Class private variables
    private CharacterController controller;
    private ScoreAndLevel currentStatus;
    [Range(0, 2)]
    [SerializeField] private float timeSpeed = 1;

    // Use this for initialization
    void Start()
    {
        // Gets the character controller component from the game object
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Sets the singleton instance of score and level as current status
        currentStatus = ScoreAndLevel.Instance;

        // Sets the speed of the player and the direction
        speed = 10 + currentStatus.currentLevel * 5;
        Vector3 moveDirection = new Vector3(speed, 0, 0);

        float horizontalInput;                                                          // Creates a variable for the input
         
        // Changes depending on which platform is used
#if (UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_EDITOR || UNITY_PS4 || UNITY_XBOXONE)
        horizontalInput = Input.GetAxis("Horizontal");                                  // Gets the input from the user through keyboard input
        moveDirection.Set(speed, 0, -horizontalInput * speed * 2);                      // Sets the movement in reference to the input and the speed of the level
#endif
#if (UNITY_IOS || UNITY_ANDROID)
        horizontalInput = Input.acceleration.x;                                         // Gets the input from the user through accelerometer of mobile inputs
        if (currentStatus.currentLevel < 3)
            moveDirection.Set(speed, 0, -horizontalInput * speed * (5 - currentStatus.currentLevel));                      // Sets the movement in reference to the input and the speed of the level
        else
            moveDirection.Set(speed, 0, -horizontalInput * speed * 2);                  // Sets the movement in reference to the input and the speed of the level

#endif

        // Applies gravity to the player
        if (!controller.isGrounded)
            moveDirection -= new Vector3(0, 9.8f, 0);

        controller.Move(moveDirection * Time.fixedDeltaTime);                            // Moves the controller in a specified direction
        
        // Destroyes and resets the game when the player fell out of the game
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
            currentStatus.playerDestroyed = true;
            currentStatus.createNewLevel = true;
            currentStatus.currentLevel = 1;
            currentStatus.score = 0;
        }
    }

    private void Update()
    {
        Time.timeScale = timeSpeed;
    }

    // Reads any collision with the player
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Reads the rigidbody that the player hits
        Rigidbody body = hit.collider.attachedRigidbody;

        // Check if this object has a rigidbody component
        if (body != null)
        {
            // Checks if the object is an obstacle and resets the gamee
            if (body.tag == "Obstacle")
            {
                Destroy(gameObject);
                currentStatus.playerDestroyed = true;
                currentStatus.currentLevel = 1;
                currentStatus.score = 0;
                currentStatus.createNewLevel = true;
            }

            // Checks if the player is at the end of the current level
            else if (body.tag == "New Level")
            {
                currentStatus.score += (int)(transform.position.x/2);
                transform.position = new Vector3(1, 0, 0);
                currentStatus.createNewLevel = true;
                currentStatus.currentLevel++;
            }
        }
    }
}
