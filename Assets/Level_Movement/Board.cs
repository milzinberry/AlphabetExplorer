using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject up_point;
    public GameObject down_point;

    public Text angle_text;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        angle_text.text = "Board X Rotation: " + transform.eulerAngles.x;
        up_point.transform.position = transform.GetChild(0).position;
        down_point.transform.position = transform.GetChild(1).position;
    }
}
