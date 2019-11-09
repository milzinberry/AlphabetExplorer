using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pickup_System : MonoBehaviour
{
    public Image[] pickup_slots;
    private int array_number = 0;

    private Vector3 distance;

    public GameObject pickup_object;
    public Pickup pickup;

    public GameObject data_start;

    public List<string> temp_data_array = new List<string>();

    public Text data_text;
    private Text temp_text;
    private Vector3 start_position;
    private Vector3 temp_position;

    public Canvas drawingCanvas;
    public GameObject drawingObject;
    public GameObject player;
    public GameObject worldLayout;

    public Level_Movement levelMovement;
    public Tracing_Final tracingFinal;
    public Main_Transition mainTransition;

    public GameObject mainCamera;

    // Use this for initialization
    void Start ()
    {
        start_position = data_start.transform.position;

        mainCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Picked_Up ()
    {
        pickup_slots[array_number].color = pickup.pickup_color;
        temp_data_array.Add(pickup.temp_data);

        temp_text = Instantiate(data_text, data_start.transform) as Text;
        temp_position = new Vector3(start_position.x, start_position.y - 40, start_position.z);
        temp_text.transform.position = temp_position;
        start_position = temp_position;
        temp_text.text = pickup.temp_data.ToString();
        temp_text = null;

        array_number++;
        Destroy(pickup_object);
        pickup_object = null;
        pickup = null;

        //If the player collects the pick-up then enable the Drawing UI Canvas
        drawingCanvas.gameObject.SetActive(true);

        if (drawingCanvas == true)
        {
            //Reset the camera to it's default position before the player moved
            mainCamera.transform.position = new Vector3(2.225132f, 0.2261306f, -10.08702f);

            //Disable initial scripts and gameobjects first
            drawingObject.gameObject.SetActive(false);
            player.gameObject.SetActive(false);
            worldLayout.gameObject.SetActive(false);
            levelMovement.enabled = false;

            //Enable scripts and gameobjects next
            tracingFinal.enabled = true;
            mainTransition.enabled = true;
        }        
    }
}
