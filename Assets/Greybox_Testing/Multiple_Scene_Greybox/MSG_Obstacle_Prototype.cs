using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSG_Obstacle_Prototype : MonoBehaviour
{
    public GameObject[] obstacles;

    public GameObject current_node_a;
    public bool incorrect;
    public bool end_hit;

    public Image checker;
    public GameObject continue_button;

    public Vector3 pointer_world_position = new Vector3();
    public Vector3 location;

    public bool dragging;

    public LineRenderer obstacle_linerenderer;

    public Vector3 original_cam_position;
    public int obstacle_int = -1;
    private MSG_Obstacle_Trigger trigger;

    private Vector3 pointer_position = new Vector3();
    private Camera c;
    private Event e;

    private Ray ray;
    private RaycastHit hit;

    private RaycastHit line_hit;

    private Obstacle_Dot od;
    private GameObject raycast_point;

    // Use this for initialization
    void Start()
    {
        c = Camera.main;

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // For detecting where the mouse is
    void OnGUI()
    {
        e = Event.current;
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            pointer_position.x = e.mousePosition.x;
            pointer_position.y = c.pixelHeight - e.mousePosition.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Obstacle)
        {
            // Mouse Input

            // When the mouse is clicked
            if (Input.GetButtonDown("Fire1"))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Clicked();
            }

            // When the mouse is held
            if (Input.GetButton("Fire1") && dragging)
            {
                Drag();
            }

            // when the mouse is lifted
            if (Input.GetButtonUp("Fire1") && dragging)
            {
                Lifted();
            }

            // Touch Controls

            // When the screen is tapped
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                pointer_position.x = Input.GetTouch(0).position.x;
                pointer_position.y = Input.GetTouch(0).position.y;
                pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));

                ray = Camera.main.ScreenPointToRay(pointer_world_position);
                Clicked();
            }

            // When the screen is held & moving
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                pointer_position.x = Input.GetTouch(0).position.x;
                pointer_position.y = Input.GetTouch(0).position.y;
                pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));

                Drag();
            }

            // When the screen is held & isn't moving
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                pointer_position.x = Input.GetTouch(0).position.x;
                pointer_position.y = Input.GetTouch(0).position.y;
                pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));

                Drag();
            }

            // When the screen is lifted
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                pointer_position.x = Input.GetTouch(0).position.x;
                pointer_position.y = Input.GetTouch(0).position.y;
                pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));

                Lifted();
            }
        }
    }

    internal void obstacles_active(Tutorial_Triggers tutorial_Triggers)
    {
        throw new NotImplementedException();
    }

    void Clicked()
    {
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (current_node_a == hit.transform.gameObject)
            {
                obstacle_linerenderer = hit.transform.gameObject.GetComponent<LineRenderer>();
                dragging = true;
            }

            if (hit.transform.gameObject.tag == "obstacle_start")
            {
                obstacle_linerenderer = hit.transform.gameObject.GetComponent<LineRenderer>();
                dragging = true;
            }
        }
    }

    void Drag()
    {
        if(current_node_a != null)
        {
            RaycastHit blockage;
            if (Physics.Linecast(current_node_a.transform.position, pointer_world_position, out blockage))
            {
                if (blockage.transform.gameObject.tag == ("obstacle_immoveable"))
                {
                    location = new Vector3(blockage.point.x, blockage.point.y, blockage.point.z);
                }
                else
                {
                    location = new Vector3(pointer_world_position.x, pointer_world_position.y, pointer_world_position.z);
                }
            }
            else
            {
                location = new Vector3(pointer_world_position.x, pointer_world_position.y, pointer_world_position.z);
            }

            obstacle_linerenderer.SetPosition(1, location);
        }
    }

    void Lifted()
    {
        dragging = false;
        obstacle_linerenderer.SetPosition(1, obstacle_linerenderer.GetPosition(0));

        if (current_node_a != null && current_node_a.tag == "obstacle_end" || end_hit)
        {
            End();
        }
    }

    void End()
    {
        if (!incorrect)
        {
            checker.color = Color.green;
            continue_button.SetActive(true);
        }
        else
        {
            checker.color = Color.red;
        }
    }

    public void Change_Dot(GameObject current_node, LineRenderer lr, bool clicked)
    {
        if (current_node.tag == "obstacle_immoveable")
        {
            incorrect = true;
        }

        if (current_node.tag == "obstacle_end")
        {
            end_hit = true;
        }

        current_node_a = current_node;
        od = current_node_a.GetComponent<Obstacle_Dot>();
        raycast_point = od.ray_position;
        obstacle_linerenderer = lr;
    }

    public void obstacles_active ( MSG_Obstacle_Trigger trigg)
    {
        trigger = trigg;
        obstacles[obstacle_int].SetActive(true);
    }

    public void Reset_Button ()
    {
        Obstacle_Dot[] obd = FindObjectsOfType<Obstacle_Dot>();

        for (int i = 0; i < obd.Length; i++)
        {
            obd[i].lr.SetPosition(0, obd[i].og_pos);
            obd[i].lr.SetPosition(1, obd[i].og_pos);

            obd[i].locked = false;
        }

        continue_button.SetActive(false);
        current_node_a = null;
        checker.color = Color.white;
        incorrect = false;
        end_hit = false;
    }

    public void Return_Button ()
    {
        Obstacle_Dot[] obd = FindObjectsOfType<Obstacle_Dot>();

        for (int i = 0; i < obd.Length; i++)
        {
            obd[i].lr.SetPosition(0, obd[i].og_pos);
            obd[i].lr.SetPosition(1, obd[i].og_pos);

            obd[i].locked = false;
        }

        continue_button.SetActive(false);
        current_node_a = null;
        checker.color = Color.white;
        obstacles[obstacle_int].SetActive(false);
        obstacle_int = -1;
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Movement_Active();
        trigger = null;
        transform.position = original_cam_position;
        original_cam_position = new Vector3(0, 0, 0);
    }

    public void Continue_Button ()
    {
        continue_button.SetActive(false);
        current_node_a = null;
        checker.color = Color.white;
        obstacles[obstacle_int].SetActive(false);
        obstacle_int = -1;
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Movement_Active();
        trigger.passed = true;
        trigger = null;
        transform.position = original_cam_position;
        original_cam_position = new Vector3(0, 0, 0);
    }
}
