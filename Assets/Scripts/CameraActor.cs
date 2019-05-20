using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActor : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private Transform target;
    private PlayerActor playerScript;
    private Vector3 view;

    private bool isometricView = true;

    // Use this for initialization
    void Start()
    {
        // Gets components from the player game object
        target = player.GetComponent<Transform>();
        playerScript = player.GetComponent<PlayerActor>();
        view = this.transform.position - target.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SwitchView();
            }
            Vector3 targetPos = target.position + view;

            // This is used to get the camera to follow the player and match its speed by speeding up or slowing down depending on what the player is doing
            this.transform.position = Vector3.Lerp(transform.position, targetPos, playerScript.speed * Time.fixedDeltaTime);

        }
        // This is used to ignore collisions with CharacterCollider
    }

    // This is used to switch views between isometric and top down view
    void SwitchView()
    {
        if (isometricView == false)
        {
            this.transform.position = target.position + new Vector3(-10, 25, 0);
            this.transform.rotation = Quaternion.Euler(50, 90, 0);
            isometricView = true;
        }
        else
        {
            this.transform.position = target.position + new Vector3(0, 25, 0);
            this.transform.rotation = Quaternion.Euler(90, 180, 90);

            isometricView = false;
        }
        view = transform.position - target.position;

    }
}
