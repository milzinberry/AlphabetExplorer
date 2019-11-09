using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_Buttons : MonoBehaviour
{
    [Header("Audio SFX")]
    public AudioSource audioMain;
    public AudioClip gameStart;

    public string scene;

    public GameObject[] menus;

    public Image[] tutorial_stickers;

    void Start()
    {
        audioMain = gameObject.GetComponent<AudioSource>();
    }

    public void Start_Button ()
    {
        audioMain.clip = gameStart;
        audioMain.Play();

        MSG_Transitioner.data.Movement_Active();
        SceneManager.LoadScene(scene);
    }

    public void Settings_Button ()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if(i == 1)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }

    public void Settings_Return_Button ()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == 0)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }

    public void Reset_Data_Button ()
    {
        MSG_Transitioner.data.Reset_Data();
    }
}
