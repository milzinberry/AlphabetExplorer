using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Color pickup_color;

    public Renderer object_renderer;

    public float distance;

    public GameObject player;
    public Pickup_System pickup_system;

    public bool locked = false;

    public string temp_data;

    // Use this for initialization
    void Start ()
    {
        object_renderer = GetComponent<Renderer>();
        pickup_color = object_renderer.material.color;

        player = GameObject.FindGameObjectWithTag("Player");
        pickup_system = GameObject.Find("Main Camera").GetComponent<Pickup_System>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < 1.5f && !locked)
        {
            locked = true;
            pickup_system.pickup_object = gameObject;
            pickup_system.pickup = gameObject.GetComponent<Pickup>();
            pickup_system.Picked_Up();
        }
    }
}
