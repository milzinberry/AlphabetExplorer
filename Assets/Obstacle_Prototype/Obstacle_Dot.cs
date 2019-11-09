using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Dot : MonoBehaviour
{
    public GameObject ray_position;

    public LineRenderer lr;

    public Vector3 og_pos;
    public Obstacle_Prototype op;
    public MSG_Obstacle_Prototype mop;

    public float distance;

    public bool locked;

	// Use this for initialization
	void Start ()
    {
        og_pos = transform.position;

        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, og_pos);
        lr.SetPosition(1, og_pos);

        op = GameObject.Find("Main Camera").GetComponent<Obstacle_Prototype>();
        mop = GameObject.Find("Main Camera").GetComponent<MSG_Obstacle_Prototype>();
    }


	
	// Update is called once per frame
	void Update ()
    {
        if(mop.current_node_a != null)
        {
            distance = Vector3.Distance(transform.position, mop.location);
        }
        else
        {
            distance = Vector3.Distance(transform.position, mop.pointer_world_position);
        }

        if (distance < 0.2f && !locked && mop.dragging && gameObject.tag != "obstacle_immoveable")
        {
            locked = true;

            mop.Change_Dot(gameObject, lr, locked);
        }
	}
}
