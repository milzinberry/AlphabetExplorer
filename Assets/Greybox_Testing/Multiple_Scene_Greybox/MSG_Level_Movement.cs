using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSG_Level_Movement : MonoBehaviour
{
    public GameObject player;
    public float speed;

    public Vector3 pointer_position = new Vector3();

    public GameObject view_anchor;
    public GameObject view_point;

    public bool moving;

    private Vector3 pointer_world_position = new Vector3();
    private Camera c;
    private Event e;

    public Vector3 screen_center;


    // Use this for initialization
    void Start ()
    {
        c = Camera.main;
    }

    void OnGUI()
    {
        screen_center = new Vector3(c.pixelWidth / 2, c.pixelHeight / 2, 0);

        e = Event.current;
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            pointer_position.x = e.mousePosition.x;
            pointer_position.y = c.pixelHeight - e.mousePosition.y;
            pointer_world_position = c.ScreenToWorldPoint(new Vector3(pointer_position.x, pointer_position.y, pointer_position.z));

            Vector3 differecne = pointer_position - screen_center;
            float rotationZ = Mathf.Atan2(differecne.y, differecne.x) * Mathf.Rad2Deg;
            view_anchor.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().cam_states == camera_states.Movement)
        {
            float step = speed * Time.deltaTime;

            Vector3 cam_position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10);
            transform.position = cam_position;

            // Mouse Input
            if (Input.GetButton("Fire1"))
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hit;


                // Old Mouse Movement
                //if (Physics.Raycast(ray, out hit, 100))
                //{
                //    Vector3 differecne = hit.transform.position - view_anchor.transform.position;
                //    float rotationZ = Mathf.Atan2(differecne.y, differecne.x) * Mathf.Rad2Deg;
                //    view_anchor.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

                //    RaycastHit blockage;

                //    if (Physics.Linecast(view_anchor.transform.position, view_point.transform.position, out blockage))
                //    {
                //        moving = true;
                //        player.transform.position = Vector3.MoveTowards(player.transform.position, hit.point, step);
                //    }
                //}

                // New Mouse Movement
                RaycastHit blockage;

                if (Physics.Linecast(view_anchor.transform.position, view_point.transform.position, out blockage))
                {
                    if (blockage.transform.tag != "Environment")
                    {
                        moving = true;
                        player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(view_point.transform.position.x, view_point.transform.position.y, 0), step);
                    }
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                moving = false;
            }

            // Touch Input

            // When the screen is tapped
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                pointer_position = Input.GetTouch(0).position;

                Vector3 differecne = pointer_position - screen_center;
                float rotationZ = Mathf.Atan2(differecne.y, differecne.x) * Mathf.Rad2Deg;
                view_anchor.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

                //pointer_world_position = c.ScreenToWorldPoint(pointer_position);

                //Ray ray = Camera.main.ScreenPointToRay(pointer_world_position);
                //RaycastHit hit;

                //if (Physics.Raycast(ray, out hit, 100))
                //{
                //    Vector3 differecne = hit.transform.position - view_anchor.transform.position;
                //    float rotationZ = Mathf.Atan2(differecne.y, differecne.x) * Mathf.Rad2Deg;
                //    view_anchor.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

                //    RaycastHit blockage;

                //    if (Physics.Linecast(view_anchor.transform.position, view_point.transform.position, out blockage))
                //    {
                //        moving = true;
                //        player.transform.position = Vector3.MoveTowards(player.transform.position, hit.point, step);
                //    }
                //}

                // New Mouse Movement
                RaycastHit blockage;

                if (Physics.Linecast(view_anchor.transform.position, view_point.transform.position, out blockage))
                {
                    if (blockage.transform.tag != "Environment")
                    {
                        moving = true;
                        player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(view_point.transform.position.x, view_point.transform.position.y, 0), step);
                    }
                }
            }

            // When the screen is detected a finger movement
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                pointer_position = Input.GetTouch(0).position;

                Vector3 differecne = pointer_position - screen_center;
                float rotationZ = Mathf.Atan2(differecne.y, differecne.x) * Mathf.Rad2Deg;
                view_anchor.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

                //pointer_world_position = c.ScreenToWorldPoint(pointer_position);

                //Ray ray = Camera.main.ScreenPointToRay(pointer_world_position);
                //RaycastHit hit;

                //if (Physics.Raycast(ray, out hit, 100))
                //{
                //    Vector3 differecne = hit.transform.position - view_anchor.transform.position;
                //    float rotationZ = Mathf.Atan2(differecne.y, differecne.x) * Mathf.Rad2Deg;
                //    view_anchor.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

                //    RaycastHit blockage;

                //    if (Physics.Linecast(view_anchor.transform.position, view_point.transform.position, out blockage))
                //    {
                //        moving = true;
                //        player.transform.position = Vector3.MoveTowards(player.transform.position, hit.point, step);
                //    }
                //}

                // New Mouse Movement
                RaycastHit blockage;

                if (Physics.Linecast(view_anchor.transform.position, view_point.transform.position, out blockage))
                {
                    moving = true;
                    if (blockage.transform.tag != "Environment")
                    {
                        moving = true;
                        player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(view_point.transform.position.x, view_point.transform.position.y, 0), step);
                    }
                }
            }

            // When the screen tap is lifted
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {

            }

        }
    }
}
