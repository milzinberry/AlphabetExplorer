using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Controls : MonoBehaviour
{
    public GameObject input_object_test;
    public GameObject output_object_test;


    private Vector3 touch_world_position = new Vector3();
    public Camera c;
    public Vector3 touch_position = new Vector3();
    private Event e;

    public GameObject current_input_node;
    public Input_Node_P input_node_script;
    private GameObject current_output_node;

    void OnGUI ()
    {
        e = Event.current;
    }

    // Update is called once per frame
    void Update ()
    {
        // When the mouse is clicked
        if (Input.GetButtonDown("Fire1"))
        {
            touch_position.x = e.mousePosition.x;
            touch_position.y = c.pixelHeight - e.mousePosition.y;
            touch_world_position = c.ScreenToWorldPoint(new Vector3(touch_position.x, touch_position.y, 10.0f));

            Tapped();
        }

        // When the mouse is held down
        if (Input.GetButton("Fire1"))
        {
            touch_position.x = e.mousePosition.x;
            touch_position.y = c.pixelHeight - e.mousePosition.y;
            touch_world_position = c.ScreenToWorldPoint(new Vector3(touch_position.x, touch_position.y, 10.0f));

            Holding();
        }

        // When the mouse click is lifted
        if (Input.GetButtonUp("Fire1"))
        {
            Lifted();
        }

        // When the screen is tapped
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touch_position.x = Input.GetTouch(0).position.x;
            touch_position.y = Input.GetTouch(0).position.y;
            touch_world_position = c.ScreenToWorldPoint(new Vector3(touch_position.x, touch_position.y, 10.0f));

            Tapped();
        }

        // When the screen is held
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            touch_position.x = Input.GetTouch(0).position.x;
            touch_position.y = Input.GetTouch(0).position.y;
            touch_world_position = c.ScreenToWorldPoint(new Vector3(touch_position.x, touch_position.y, 10.0f));

            Holding();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touch_position.x = Input.GetTouch(0).position.x;
            touch_position.y = Input.GetTouch(0).position.y;
            touch_world_position = c.ScreenToWorldPoint(new Vector3(touch_position.x, touch_position.y, 10.0f));

            Holding();
        }

        // When the screen tap has been lifted
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Lifted();
        }
    }

    void Tapped ()
    {
        current_input_node = Instantiate(input_object_test, touch_world_position, transform.rotation) as GameObject;
        input_node_script = current_input_node.GetComponent<Input_Node_P>();
        current_output_node = Instantiate(output_object_test, touch_world_position, transform.rotation) as GameObject;
        input_node_script.lr.SetPosition(1, current_output_node.transform.position);
    }

    void Holding ()
    {
        current_output_node.transform.position = touch_world_position;
        input_node_script.lr.SetPosition(1, current_output_node.transform.position);
    }

    void Lifted ()
    {
        current_input_node = null;
        input_node_script = null;
        current_output_node = null;
    }
}
