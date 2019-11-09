using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Node : MonoBehaviour
{
    public LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
    }
}
