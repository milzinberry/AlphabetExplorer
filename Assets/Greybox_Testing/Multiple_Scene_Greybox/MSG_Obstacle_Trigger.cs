using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSG_Obstacle_Trigger : MonoBehaviour
{
    public int obstacle_int;

    public bool passed;

    public GameObject[] bridge;

    public Sprite[] flag_sprites;
    public SpriteRenderer sprite_renderer;

    private bool locked;

    // Use this for initialization
    void Start ()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial_Level_Final"))
        {
            if (MSG_Transitioner.data.tutorial_obstacle_passed[obstacle_int] == true)
            {
                passed = true;
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
        {
            if (MSG_Transitioner.data.level_a_obstacle_passed[obstacle_int] == true)
            {
                passed = true;
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
        {
            if (MSG_Transitioner.data.level_b_obstacle_passed[obstacle_int] == true)
            {
                passed = true;
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
        {
            if (MSG_Transitioner.data.level_c_obstacle_passed[obstacle_int] == true)
            {
                Debug.Log("Cucked");
                passed = true;
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(!passed)
        {
            foreach (GameObject bridge_piece in bridge)
            {
                bridge_piece.SetActive(false);
            }

            sprite_renderer.sprite = flag_sprites[0];

            float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if (distance < 1.3f)
            {
                if (!MSG_Transitioner.data.obstacle_lock_out)
                {
                    Obstacle_Active();
                }
            }
        }

        if (passed)
        {
            foreach (GameObject bridge_piece in bridge)
            {
                bridge_piece.SetActive(true);
            }

            sprite_renderer.sprite = flag_sprites[1];

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial_Level_Final"))
            {
                MSG_Transitioner.data.tutorial_obstacle_passed[obstacle_int] = true;
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
            {
                MSG_Transitioner.data.level_a_obstacle_passed[obstacle_int] = true;
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
            {
                MSG_Transitioner.data.level_b_obstacle_passed[obstacle_int] = true;
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
            {
                MSG_Transitioner.data .level_c_obstacle_passed[obstacle_int] = true;
            }

            Save();
        }
    }

    public void Obstacle_Active()
    {
        MSG_Transitioner.data.obstacle_lock_out = true;

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        //MSG_Obstacle_Prototype cam_ob = cam.GetComponent<MSG_Obstacle_Prototype>();

        //cam_ob.original_cam_position = cam.transform.position;
        //cam_ob.obstacle_int = obstacle_int;

        MSG_Tracing_Final cam_tr = cam.GetComponent<MSG_Tracing_Final>();

        cam_tr.original_cam_position = cam.transform.position;

        cam.transform.position = new Vector3(-10000, 0, -10);

        MSG_Transitioner.data.Tracing_Active();
        //cam_ob.obstacles_active(this);

        cam_tr.Obstacle_Start(obstacle_int, this);
    }

    void Save ()
    {
        if(!locked)
        {
            locked = true;
            MSG_Transitioner.data.Save_Data();
        }
    }
}
