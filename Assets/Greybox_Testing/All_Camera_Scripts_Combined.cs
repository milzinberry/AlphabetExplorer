using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class All_Camera_Scripts_Combined : MonoBehaviour
{
    [Header("- Place on the main camera -")]

    [Header("The State that the camra is in")]
    public camera_states cam_states;

    // Variables used by two or more sections
    [Header("Universal Variables")]

    [Header("_Needs to be set in inspector_")]

    [Header("Gameobject of the player")]
    public GameObject player;

    [Header("_Does not need to be set_")]

    [Header("Current pointer position")]
    public Vector3 pointer_position = new Vector3();

    // Private variables
    private Vector3 pointer_world_position = new Vector3();
    private Camera c;
    private Event e;

    // Movement
    [Header("Movement Variables")]

    [Header("_Needs to be set in inspector_")]

    public Image[] pickup_slots;

    [Header("Speed at which player moves")]
    public float player_speed;

    //[Header("_Does not need to be set_")]

    // Private Variables
    private int array_number = 0;
    private Vector3 pickup_distance;
    private float step;

    // Tracing
    [Header("Tracing Variables")]


    [Header("_Needs to be set in inspector_")]

    [Header("Object used in line construction")]
    public GameObject input_node;

    [Header("Array for the tracing templates")]
    public List<GameObject> tracers = new List<GameObject>();

    [Header("_Does not need to be set_")]

    [Header("Total number of nodes in scene")]
    public int total_nodes = 0;

    [Header("Node list")]
    public List<GameObject> node_list = new List<GameObject>();

    [Header("Node settings for tracing")]
    public GameObject current_input_node;
    public Input_Node input_node_script;
    public GameObject current_output_node;

    [Header("Total numbers from counters in templates")]
    public int total_min_count;
    public int total_max_count;
    public int total_node_count = 0;

    [Header("Number of correct templates")]
    public int correctnumber = 0;

    // Private Variables
    private bool locked = false;
    private float tracing_distacne;
    private bool clicked = false;

    // Dragging
    [Header("Dragging Variables")]

    [Header("_Needs to be set in inspector_")]

    [Header("How many draggables are there")]
    public int draggable_number;

    [Header("_Does not need to be set_")]

    [Header("Draggable's original position")]
    public Vector3 draggable_original_position;

    // Private Variables
    private int counted_draggable_number;
    private GameObject temp_drag_object;
    private Draggable draggable;
    private GameObject drag_slot;
    private bool drag_dragging;

    // Obstacles
    [Header("Obstacle Variables")]

    [Header("_Needs to be set in inspector_")]

    [Header("Number of nodes that needs to be passed through")]
    public int max_node_count;

    [Header("_Does not need to be set_")]

    public int current_node_count;
    public GameObject current_node_a;
    public bool incorrect;
    public bool end_hit;

    public LineRenderer obstacle_linerenderer;

    // Private Variables
    private bool obstacle_dragging;
    private Obstacle_Dot od;
    private GameObject raycast_point;





    // Use this for initialization
    void Start()
    {
        c = Camera.main;

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void OnGUI()
    {
        e = Event.current;
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            pointer_position.x = e.mousePosition.x;
            pointer_position.y = c.pixelHeight - e.mousePosition.y;
            if(cam_states == camera_states.Movement)
            {
                pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, pointer_position.z));
            }
            else
            {
                pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(cam_states == camera_states.Movement)
        {
            step = player_speed * Time.deltaTime;

            Vector3 cam_position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10);
            transform.position = cam_position;
        }

        // Input

        // Keyboard & Mouse
        if (Input.GetButtonDown("Fire1"))
        {
            if(cam_states == camera_states.Tracing)
            {
                clicked = true;
                Node_Start();
            }
        }

        if(Input.GetButton("Fire1"))
        {
            if(cam_states == camera_states.Movement)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, hit.point, step);
                }
            }
        }

        if(Input.GetButtonUp("Fire1"))
        {
            if(cam_states == camera_states.Tracing)
            {
                Lifted();
            }
        }
	}

    void Node_Start()
    {
        current_input_node = Instantiate(input_node, pointer_world_position, transform.rotation) as GameObject;
        node_list.Add(current_input_node);
        input_node_script = current_input_node.GetComponent<Input_Node>();
        total_nodes++;
    }

    void Node()
    {
        current_input_node = current_output_node;
        current_output_node = null;
        input_node_script = current_input_node.GetComponent<Input_Node>();
        total_nodes++;
    }

    void Lifted()
    {
        clicked = false;
        current_output_node = Instantiate(input_node, pointer_world_position, transform.rotation) as GameObject;
        node_list.Add(current_output_node);
        input_node_script.lr.SetPosition(1, current_output_node.transform.position);
        current_input_node = current_output_node;
        input_node_script = current_input_node.GetComponent<Input_Node>();
        input_node_script.lr.SetPosition(1, current_input_node.transform.position);
        current_input_node = null;
        current_output_node = null;
        total_nodes++;
    }
}
