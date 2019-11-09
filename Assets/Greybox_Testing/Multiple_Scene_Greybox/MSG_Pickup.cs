using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MSG_Pickup : MonoBehaviour
{
    public bool gold;
    public int sticker_number;

    private SpriteRenderer object_sprite;

    public float distance;

    public GameObject player;
	public GameObject portal;
    public MSG_Pickup_System pickup_system;

    public bool locked = false;

    // Use this for initialization
    void Start ()
    {
        object_sprite = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");
        pickup_system = GameObject.Find("Main Camera").GetComponent<MSG_Pickup_System>();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial_Level_Final"))
        {
            if (MSG_Transitioner.data.tutorial_stickers[sticker_number] == false)
            {
                if (object_sprite != null) gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                locked = true;
                if (object_sprite != null) gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
        {
            if (MSG_Transitioner.data.level_a_stickers[sticker_number] == false)
            {
                if (object_sprite != null) gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                locked = true;
                if (object_sprite != null) gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
        {
            if (MSG_Transitioner.data.level_b_stickers[sticker_number] == false)
            {
                if (object_sprite != null) gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                locked = true;
                if (object_sprite != null) gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
        {
            if (MSG_Transitioner.data.level_c_stickers[sticker_number] == false)
            {
                if (object_sprite != null) gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                locked = true;
                if (object_sprite != null) gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < 1.5f && !locked)
        {
            locked = true;
            pickup_system.pickup_object = gameObject;
            pickup_system.pickup = gameObject.GetComponent<MSG_Pickup>();
            pickup_system.Picked_Up(sticker_number);

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A") & gold)
            {
                portal.SetActive(true);
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B") & gold)
            {
                portal.SetActive(true);
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C") & gold)
            {
                portal.SetActive(true);
            }
        }

    }
}
