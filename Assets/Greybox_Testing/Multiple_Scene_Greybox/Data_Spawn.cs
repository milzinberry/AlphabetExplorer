using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Spawn : MonoBehaviour
{
    public GameObject data;

	// Use this for initialization
	void Start ()
    {
        if (GameObject.FindGameObjectWithTag("Data") == null)
        {
            Instantiate(data);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
