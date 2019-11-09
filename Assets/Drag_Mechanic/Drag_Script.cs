using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drag_Script : MonoBehaviour
{
    [Header("- Place on main camera -")]

    [Header("Used to determine if all are correct")]
    public Image image;

    [Header("How many draggables are there")]
    public int draggable_number;
    private int counted_draggable_number;

    [Header("These variable do not need to be touched")]

    [Header("Pointer position")]
    public Vector3 pointer_position = new Vector3();

    [Header("Draggable's original position")]
    public Vector3 draggable_original_position;

    private GameObject temp_drag_object;
    private Draggable draggable;
    private GameObject drag_slot;

    private Ray ray;
    private RaycastHit hit;

    private bool dragging;

    private Vector3 pointer_world_position = new Vector3();
    private Camera c;
    private Event e;

    private float distance;

    // Use this for initialization
    void Start ()
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
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, 10.0f));
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(draggable_number == counted_draggable_number)
        {
            image.color = Color.green;
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
    void Clicked ()
    {
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.gameObject.tag == "Draggable")
            {
                temp_drag_object = hit.transform.gameObject;
                draggable_original_position = temp_drag_object.transform.position;
                draggable = temp_drag_object.GetComponent<Draggable>();
                drag_slot = draggable.linked_slot;
                dragging = true;
            }
        }
    }

    void Drag ()
    {
        temp_drag_object.transform.position = pointer_world_position;
    }

    void Lifted ()
    {
        distance = Vector3.Distance(temp_drag_object.transform.position, drag_slot.transform.position);

        if (distance < 1.0f && dragging)
        {
            temp_drag_object.transform.position = drag_slot.transform.position;
            draggable.Lock();
            counted_draggable_number++;

            temp_drag_object = null;
            draggable = null;
            drag_slot = null;
            draggable_original_position = Vector3.zero;
            dragging = false;
        }
        else
        {
            temp_drag_object.transform.position = draggable_original_position;

            temp_drag_object = null;
            draggable = null;
            drag_slot = null;
            draggable_original_position = Vector3.zero;
            dragging = false;
        }
    }
}
