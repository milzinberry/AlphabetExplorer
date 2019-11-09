using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Triggering : MonoBehaviour
{
    [Header("Universal Variables")]
    public GameObject mainCamera;

    [Header("Movement Variables")]
    public Level_Movement levelMovement;
    public GameObject worldLayout;
    public GameObject player;

    [Header("Obstacle Variables")]
    public Obstacle_Prototype obstaclePrototype;
    public GameObject obstacle;

    // Use this for initialization
    void Start ()
    {
        //Find the gameObject with the 'Player' tag
        player = GameObject.FindGameObjectWithTag("Player");

        //Find the gameObjectg with the name 'Main Camera'
        mainCamera = GameObject.Find("Main Camera");

        //Ensure that the ObstaclePrototype script is disabled
        //obstaclePrototype.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //Reset the camera to it's default position before the player moved
        mainCamera.transform.position = new Vector3(0,0,0);

        if (other.gameObject.tag == "Player")
        {
            //Disable initial scripts and gameobjects first
            levelMovement.enabled = false;
            player.gameObject.SetActive(false);
            worldLayout.gameObject.SetActive(false);

            //Enable scripts and gameobjects next
            obstaclePrototype.enabled = true;
            obstacle.gameObject.SetActive(true);
        }
    }
}
