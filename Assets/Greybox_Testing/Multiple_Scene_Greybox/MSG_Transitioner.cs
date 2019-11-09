using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public enum camera_states { None, Pause, Movement, Tracing, Dragging, Obstacle , Sticker_Book}

public class MSG_Transitioner : MonoBehaviour
{
    public static MSG_Transitioner data;

    [Header("The State that the camera is in")]
    public camera_states cam_states;

    public bool obstacle_lock_out;

    [Header("Tutorial Arrays")]
    public bool[] tutorial_obstacle_passed;
    public bool[] tutorial_stickers;
    public Sprite[] tutorial_sticker_blank_sprites;
    public Sprite[] tutorial_sticker_sprites;

    [Header("Level_A Arrays")]
    public bool[] level_a_obstacle_passed;
    public bool[] level_a_stickers;
    public Sprite[] level_a_sticker_blank_sprites;
    public Sprite[] level_a_sticker_sprites;

    [Header("Level_B Arrays")]
    public bool[] level_b_obstacle_passed;
    public bool[] level_b_stickers;
    public Sprite[] level_b_sticker_blank_sprites;
    public Sprite[] level_b_sticker_sprites;

    [Header("Level_C Arrays")]
    public bool[] level_c_obstacle_passed;
    public bool[] level_c_stickers;
    public Sprite[] level_c_sticker_blank_sprites;
    public Sprite[] level_c_sticker_sprites;

    [Header("Whether Saving is active or not")]
    public bool saving_active = true;

    // Use this for initialization
    void Awake()
    {
        if (data == null)
        {
            DontDestroyOnLoad(gameObject);
            data = this;
        }
        else if (data != this)
        {
            Destroy(gameObject);
        }

        Load_Data();
        Save_Data();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Reset_Data();
        }
    }

    public IEnumerator Obstacle_Exit_Timer ()
    {
        obstacle_lock_out = true;
        yield return new WaitForSeconds(5.0f);
        obstacle_lock_out = false;
    }

    public void Load_Data ()
    {
        if (File.Exists(Application.persistentDataPath + "/AlphabetExplorerGameData.dat") && saving_active)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/AlphabetExplorerGameData.dat", FileMode.Open);
            Game_Data game_data = ((Game_Data)bf.Deserialize(file));
            file.Close();

            tutorial_obstacle_passed = game_data.tutorial_obstacle_passed;
            tutorial_stickers = game_data.tutorial_stickers;

            level_a_obstacle_passed = game_data.level_a_obstacle_passed;
            level_a_stickers = game_data.level_a_stickers;

            level_b_obstacle_passed = game_data.level_b_obstacle_passed;
            level_b_stickers = game_data.level_b_stickers;

            level_c_obstacle_passed = game_data.level_c_obstacle_passed;
            level_c_stickers = game_data.level_c_stickers;
        }
    }

    public void Save_Data ()
    {
        if (saving_active)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/AlphabetExplorerGameData.dat");
            Game_Data game_data = new Game_Data();

            game_data.tutorial_obstacle_passed = tutorial_obstacle_passed;
            game_data.tutorial_stickers = tutorial_stickers;

            game_data.level_a_obstacle_passed = level_a_obstacle_passed;
            game_data.level_a_stickers = level_a_stickers;

            game_data.level_b_obstacle_passed = level_b_obstacle_passed;
            game_data.level_b_stickers = level_b_stickers;

            game_data.level_c_obstacle_passed = level_c_obstacle_passed;
            game_data.level_c_stickers = level_c_stickers;

            bf.Serialize(file, game_data);
            file.Close();
        }
    }

    public void Default_Active()
    {
        cam_states = camera_states.None;
    }

    public void Movement_Active ()
    {
        cam_states = camera_states.Movement;
    }

    public void Obstacle_Active ()
    {
        cam_states = camera_states.Obstacle;
    }

    public void Tracing_Active()
    {
        cam_states = camera_states.Tracing;
    }

    public void Sticker_Book_Active ()
    {
        cam_states = camera_states.Sticker_Book;
    }

    //public void End ()
    //{
    //    cam_states = camera_states.None;

    //    for (int i = 0; i < tutorial_obstacle_passed.Length; i++)
    //    {
    //        tutorial_obstacle_passed[i] = false;
    //    }

    //    for (int i = 0; i < tutorial_stickers.Length; i++)
    //    {
    //        tutorial_stickers[i] = false;
    //    }

    //    Reset_Data();
    //}

    public void Reset_Data()
    {
        if(File.Exists(Application.persistentDataPath + "/AlphabetExplorerGameData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/AlphabetExplorerGameData.dat");
        }
    }
}

[Serializable]
class Game_Data
{
    // Tutorial
    public bool[] tutorial_obstacle_passed;
    public bool[] tutorial_stickers;

    // Level A
    public bool[] level_a_obstacle_passed;
    public bool[] level_a_stickers;

    // Level B
    public bool[] level_b_obstacle_passed;
    public bool[] level_b_stickers;

    // Levels C
    public bool[] level_c_obstacle_passed;
    public bool[] level_c_stickers;
}
