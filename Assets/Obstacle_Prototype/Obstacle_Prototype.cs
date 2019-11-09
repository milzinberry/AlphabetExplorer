using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle_Prototype : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject[] obstacles;

    public GameObject reset_button;

    public int max_node_count;
    public int current_node_count;
    public GameObject current_node_a;
    public bool incorrect;
    public bool end_hit;

    public Image checker;

    public Vector3 pointer_world_position = new Vector3();

    public bool dragging;

    public LineRenderer obstacle_linerenderer;

    private Vector3 pointer_position = new Vector3();
    private Camera c;
    private Event e;

    private Ray ray;
    private RaycastHit hit;

    private RaycastHit line_hit;

    private Obstacle_Dot od;
    private GameObject raycast_point;

    // Use this for initialization
    void Start ()
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
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            Debug.Log("time");
        }

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
        //Vector3 beggining_direction = raycast_point.transform.position;
        //Vector3 endding_direction = new Vector3(pointer_world_position.x, pointer_world_position.y, 0.0f);
        //Vector3 direction = endding_direction - beggining_direction;


        //if (Physics.Raycast(raycast_point.transform.position, direction, out line_hit))
        //{
        //    Debug.Log("ray out");

        //    if (hit.transform.gameObject != null)
        //    {
        //        Debug.Log("Object: " + hit.transform.gameObject);
        //    }

        //if (hit.transform.gameObject.tag == "obstacle_immoveable")
        //{
        //    Debug.Log("ray hit");
        //    obstacle_linerenderer.SetPosition(1, hit.point);
        //}
        //}

        obstacle_linerenderer.SetPosition(1, pointer_world_position);
    }

    void Lifted()
    {
        dragging = false;
        obstacle_linerenderer.SetPosition(1, obstacle_linerenderer.GetPosition(0));

        if(current_node_a != null && current_node_a.tag == "obstacle_end" || end_hit)
        {
            End();
        }
    }

    void End ()
    {
        if(current_node_count == max_node_count && !incorrect)
        {
            checker.color = Color.green;
        }
        else
        {
            checker.color = Color.red;
        }
    }

    public void Change_Dot (GameObject current_node, LineRenderer lr, bool clicked)
    {
        if(current_node.tag == "obstacle_immoveable")
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
        current_node_count++;
    }

    public void Obstacle_Selector(GameObject obstacle)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }

        for (int i = 0; i < obstacles.Length; i++)
        {
            if(obstacles[i] == obstacle)
            {
                obstacles[i].SetActive(true);
            }
        }

        reset_button.SetActive(true);
        max_node_count = obstacle.GetComponent<Obstacle_Stats>().max_node_count;
    }
}
