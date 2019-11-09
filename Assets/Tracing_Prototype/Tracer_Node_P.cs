using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracer_Node_P : MonoBehaviour
{
    public Counter_P counter;
    private LineRenderer lr;
    public GameObject reciever;

	// Use this for initialization
	void Start ()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, reciever.transform.position);
    }

    public void Find_Nodes ()
    {
        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, 0.5f);
        int i = 0;
        while (i < hitcolliders.Length)
        {
            if (hitcolliders[i].tag == "input_node")
            {
                counter.node_count++;
                i = hitcolliders.Length + 1;
            }
            else
            {
                i++;
            }
        }
    }
}
