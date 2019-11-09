using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [Header("-Place in first tracer node-")]

    [Header("Items below need to be set in inspector")]

    [Header("Options")]
    public bool dots_visible = true;
    public bool lines_visible = true;

    [Header("Assign each node into the array in order")]
    public GameObject[] tracer_nodes;

    [Header("The Minimum and Maximum amount of drawn nodes there are allowed to be for this template")]
    public int min_count;
    public int max_count;

    [Header("Items below do not need to be touched")]

    [Header("Total amount of nodes in template")]
    public int total_nodes;

    [Header("The number of drawn nodes detected")]
    public int node_count = 0;

    [Header("Bool stating whether or not this template is being considered correct")]
    public bool iscorrect = false;

    private GameObject cam;
    private MSG_Tracing_Final tfs;
    private SpriteRenderer letter;

    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        tfs = cam.GetComponent<MSG_Tracing_Final>();
        letter = GetComponent<SpriteRenderer>();
        letter.enabled = false;

        //if (lines_visible)
        //{
        //    for (int i = 0; i < tracer_nodes.Length; i++)
        //    {
        //        LineRenderer lr = tracer_nodes[i].GetComponent<LineRenderer>();

        //        if (i == tracer_nodes.Length - 1)
        //        {
        //            lr.SetPosition(0, tracer_nodes[i].transform.position);
        //            lr.SetPosition(1, tracer_nodes[i].transform.position);
        //        }
        //        else
        //        {
        //            lr.SetPosition(0, tracer_nodes[i].transform.position);
        //            lr.SetPosition(1, tracer_nodes[i + 1].transform.position);
        //        }
        //    }
        //}

        //if (dots_visible)
        //{
        //    for (int i = 0; i < tracer_nodes.Length; i++)
        //    {
        //        tracer_nodes[i].GetComponent<Renderer>().enabled = true;
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < tracer_nodes.Length; i++)
        //    {
        //        tracer_nodes[i].GetComponent<Renderer>().enabled = false;
        //    }
        //}
    }

    public void set_active ()
    {
        letter.enabled = true;

        if (lines_visible)
        {
            for (int i = 0; i < tracer_nodes.Length; i++)
            {
                LineRenderer lr = tracer_nodes[i].GetComponent<LineRenderer>();

                if (i == tracer_nodes.Length - 1)
                {
                    lr.SetPosition(0, tracer_nodes[i].transform.position);
                    lr.SetPosition(1, tracer_nodes[i].transform.position);
                }
                else
                {
                    lr.SetPosition(0, tracer_nodes[i].transform.position);
                    lr.SetPosition(1, tracer_nodes[i + 1].transform.position);
                }
            }
        }

        if (dots_visible)
        {
            for (int i = 0; i < tracer_nodes.Length; i++)
            {
                tracer_nodes[i].GetComponent<Renderer>().enabled = true;
                tracer_nodes[i].GetComponent<Collider>().enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < tracer_nodes.Length; i++)
            {
                tracer_nodes[i].GetComponent<Renderer>().enabled = false;
                tracer_nodes[i].GetComponent<Collider>().enabled = true;
            }
        }
    }

    public void set_inactive ()
    {
        letter.enabled = false;

        foreach (GameObject tracers in tracer_nodes)
        {
            LineRenderer lr = tracers.GetComponent<LineRenderer>();

            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);

            tracers.GetComponent<Renderer>().enabled = false;
            tracers.GetComponent<Collider>().enabled = false;
        }
    }

    public void Count ()
    {
        for (int i = 0; i < tracer_nodes.Length; i++)
        {
            GameObject node = tracer_nodes[i];
            Loop(node);
        }

        tfs.total_node_count += node_count;

        if (node_count >= min_count)
        {
            iscorrect = true;
        }
        else
        {
            iscorrect = false;
        }
    }

    void Loop (GameObject node)
    {
        Collider[] hitcolliders = Physics.OverlapSphere(node.transform.position, 0.5f);
        int i = 0;
        while (i < hitcolliders.Length)
        {
            if (hitcolliders[i].tag == "input_node")
            {
                node_count++;
                i = hitcolliders.Length + 1;
            }
            else
            {
                i++;
            }
        }
    }
}
