using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Screen_Resolution : MonoBehaviour
{
    public bool adjust_screen = true;

    private float defualt_width;


	// Use this for initialization
	void Start ()
    {
        defualt_width = Camera.main.orthographicSize * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (adjust_screen)
        {
            if (MSG_Transitioner.data.cam_states == camera_states.Tracing)
            {
                Camera.main.orthographicSize = defualt_width / Camera.main.aspect + 1;

            }
        }
	}
}
