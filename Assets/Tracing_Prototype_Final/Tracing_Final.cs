using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Tracing_Final : MonoBehaviour
{
    public Text x_text;
    public Text y_test;
    public Text state;

    [Header("-Place on the main Camera-")]

    [Header("Items below need to be set in inspector")]

    [Header("Object used in line construction")]
    public GameObject input_node;

    [Header("Array for the tracing templates")]
    public List<GameObject> tracers = new List<GameObject>();

    [Header("Variable(s) that can be set but otherwise will be done automatically")]

    [Header("Whether the drawing is active")]
    public bool isactive = false;

    [Header("Items below do not need to be touched")]

    [Header("Total number of nodes in scene")]
    public int total_nodes = 0;

    [Header("Node list")]
    public List<GameObject> node_list = new List<GameObject>();

    [Header("Current pointer position")]
    public Vector3 pointer_position = new Vector3();

    [Header("Node settings for tracing")]
    public GameObject current_input_node;
    public Input_Node input_node_script;
    public GameObject current_output_node;

    [Header("Total numbers form counters in templates")]
    public int total_min_count;
    public int total_max_count;
    public int total_node_count = 0;

    [Header("Number of correct templates")]
    public int correctnumber = 0;

    private Vector3 pointer_world_position_temp_location = new Vector3();
    private Vector3 pointer_world_location;
    private Camera c;
    private Event e;
    private bool clicked = false;
    private float distance;
    private bool locked = false;
    private GameObject checker;
    private Image checker_image;

    public Canvas drawingCanvas;
    public GameObject player;
    public GameObject worldLayout;

    public Level_Movement levelMovement;
    public Tracing_Final tracingFinal;
    public Main_Transition mainTransition;

    // Use this for initialization
    void Start()
    {
        c = Camera.main;

        Screen.orientation = ScreenOrientation.LandscapeLeft;

        if(tracers.Count > 0)
        {
            Begin();
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Begin ()
    {
        for (int i = 0; i < tracers.Count; i++)
        {
            Counter counter = tracers[i].GetComponent<Counter>();
            total_min_count = total_min_count + counter.min_count;
            total_max_count = total_max_count + counter.max_count;

            checker = GameObject.Find("Checker");
            checker_image = checker.GetComponent<Image>();
        }
    }

    // For detecting where the mouse is
    void OnGUI()
    {
        e = Event.current;
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            state.color = Color.red;
            pointer_position.x = e.mousePosition.x;
            pointer_position.y = c.pixelHeight - e.mousePosition.y;
            pointer_world_position_temp_location = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
            pointer_world_location = pointer_world_position_temp_location;
        }
    }

    void Update()
    {
        x_text.text = "X: " + pointer_world_location.x;
        y_test.text = "Y: " + pointer_world_location.y;

        if (isactive)
        {
            // Mouse Input

            // When the left click is pressed
            if (Input.GetButtonDown("Fire1"))
            {
                clicked = true;
                Node_Start();
            }
            // When the Left Click is lifted
            if (Input.GetButtonUp("Fire1"))
            {
                Lifted();
            }

            // Touch Input

            // When the screen is tapped
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                state.text = "State: Begin";
                pointer_position.x = Input.GetTouch(0).position.x;
                pointer_position.y = Input.GetTouch(0).position.y;
                pointer_world_position_temp_location = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
                pointer_world_location = pointer_world_position_temp_location;
                clicked = true;
                Node_Start();
            }
            //// When the screen is detected a finger movement
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                state.text = "State: Moved";
                pointer_position.x = Input.GetTouch(0).position.x;
                pointer_position.y = Input.GetTouch(0).position.y;
                pointer_world_position_temp_location = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
                pointer_world_location = pointer_world_position_temp_location;
            }
            // When the screen tap is lifted
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                state.text = "State: Ended";
                Lifted();
            }

            if (current_input_node != null)
            {
                distance = Vector3.Distance(pointer_world_location, current_input_node.transform.position);
            }

            if (distance > 0.5f && clicked)
            {
                current_output_node = Instantiate(input_node, pointer_world_location, transform.rotation) as GameObject;
                node_list.Add(current_output_node);
                input_node_script.lr.SetPosition(1, current_output_node.transform.position);
                Node();
            }
        }
    }

    void Lifted()
    {
        clicked = false;
        current_output_node = Instantiate(input_node, pointer_world_location, transform.rotation) as GameObject;
        node_list.Add(current_output_node);
        input_node_script.lr.SetPosition(1, current_output_node.transform.position);
        current_input_node = current_output_node;
        input_node_script = current_input_node.GetComponent<Input_Node>();
        input_node_script.lr.SetPosition(1, current_input_node.transform.position);
        current_input_node = null;
        current_output_node = null;
        total_nodes++;
    }

    void Node_Start()
    {
        current_input_node = Instantiate(input_node, pointer_world_location, transform.rotation) as GameObject;
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

    public void Count()
    {
        if(!locked)
        {
            locked = true;
            for (int i = 0; i < tracers.Count; i++)
            {
                Counter counter = tracers[i].GetComponent<Counter>();
                counter.Count();
                if (counter.iscorrect)
                {
                    correctnumber++;
                }
            }

            if (correctnumber == tracers.Count && total_nodes < total_max_count)
            {
                checker_image.color = Color.green;

                SceneManager.LoadScene("Greybox");

            }
            else
            {
                checker_image.color = Color.red;
            }
        }
    }

    public void Reset_Letters ()
    {
        total_nodes = 0;
        total_node_count = 0;
        correctnumber = 0;
        locked = false;

        for (int i = 0; i < tracers.Count; i++)
        {
            Counter counter = tracers[i].GetComponent<Counter>();
            counter.node_count = 0;
            counter.iscorrect = false;
        }

        for (int i = 0;i < node_list.Count; i++)
        {
            Destroy(node_list[i]);
        }
        checker_image.color = Color.white;
        node_list.Clear();
    }
}
