using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSG_UI_Transition : MonoBehaviour
{
    public GameObject movement_UI;
    public GameObject obstacle_UI;
    public GameObject tracing_UI;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Movement)
        {
            movement_UI.SetActive(true);
            obstacle_UI.SetActive(false);
            tracing_UI.SetActive(false);
        }

        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Obstacle)
        {
            movement_UI.SetActive(false);
            obstacle_UI.SetActive(true);
            tracing_UI.SetActive(false);
        }

        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Tracing)
        {
            movement_UI.SetActive(false);
            obstacle_UI.SetActive(false);
            tracing_UI.SetActive(true);
        }
    }
}
