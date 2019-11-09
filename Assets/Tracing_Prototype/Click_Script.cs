using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Script : MonoBehaviour
{
    public GameObject input_node;
    public GameObject output_node;

    private Vector3 mouse_world_position = new Vector3();
    public Camera c;
    public Vector3 mouse_pos = new Vector3();
    private Event e;

    public GameObject current_input_node;
    public Input_Node_P input_node_script;
    public GameObject current_output_node;

    public GameObject tracer;

    private bool clicked = false;

    // Use this for initialization
    void Start ()
    {
        c = Camera.main;
    }

    void OnGUI()
    {
        e = Event.current;

        mouse_pos.x = e.mousePosition.x;
        mouse_pos.y = c.pixelHeight - e.mousePosition.y;

        mouse_world_position = c.ScreenToWorldPoint(new Vector3(mouse_pos.x, mouse_pos.y, 10.0f));
    }

    void Node()
    {
        current_input_node = Instantiate(input_node, mouse_world_position, transform.rotation) as GameObject;
        input_node_script = current_input_node.GetComponent<Input_Node_P>();
    }

    void Spawn_Tracer ()
    {
        Instantiate(tracer, mouse_world_position, transform.rotation);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Button Hit");
            clicked = true;
            Node();
            Spawn_Tracer();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Button Lifted");
            clicked = false;
            current_output_node = Instantiate(output_node, mouse_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
        }

        float distance = Vector3.Distance(mouse_world_position, current_input_node.transform.position);

        if(distance > 0.5f && clicked)
        {
            current_output_node = Instantiate(output_node, mouse_world_position, transform.rotation) as GameObject;
            input_node_script.lr.SetPosition(1, current_output_node.transform.position);
            Node();
        }
    }
}
