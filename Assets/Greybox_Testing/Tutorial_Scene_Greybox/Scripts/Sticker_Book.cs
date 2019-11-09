using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sticker_Book : MonoBehaviour
{
    public int page_number = 0;

    public GameObject left_arrow;
    public GameObject right_arrow;

    public GameObject[] UI;

    public GameObject[] pages;

    public Text level_text;

    [SerializeField]
    private string[] level_string;

    [Header("Tutorial")]
    public Image[] tutorial_sticker_images;

    [Header("Level A")]
    public Image[] level_a_sticker_images;

    [Header("Level B")]
    public Image[] level_b_sticker_images;

    [Header("Level C")]
    public Image[] level_c_sticker_images;

    [Header("Audio SFX")]
    public AudioSource audioMain;
    public AudioClip bookOpen;
    public AudioClip bookClose;
    public AudioClip[] flipPage;
    private AudioClip flipPageClip;

    private camera_states old_state;

    private bool book_open;

    public void Book_Opened ()
    {
        audioMain.clip = bookOpen;
        audioMain.Play();

        audioMain.clip = bookOpen;
        audioMain.Play();

        book_open = true;

        old_state = MSG_Transitioner.data.cam_states;
        MSG_Transitioner.data.Sticker_Book_Active();

        foreach (GameObject ui in UI)
        {
            ui.SetActive(true);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial_Level_Final"))
        {
            page_number = 0;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
        {
            page_number = 1;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
        {
            page_number = 2;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
        {
            page_number = 3;
        }

        // Tutorial
        for (int i = 0; i < tutorial_sticker_images.Length; i++)
        {
            if (MSG_Transitioner.data.tutorial_stickers[i])
            {
                tutorial_sticker_images[i].sprite = MSG_Transitioner.data.tutorial_sticker_sprites[i];
            }
            else
            {
                tutorial_sticker_images[i].sprite = MSG_Transitioner.data.tutorial_sticker_blank_sprites[i];
            }
        }

        // Level A
        for (int i = 0; i < level_a_sticker_images.Length; i++)
        {
            if (MSG_Transitioner.data.level_a_stickers[i])
            {
                level_a_sticker_images[i].sprite = MSG_Transitioner.data.level_a_sticker_sprites[i];
            }
            else
            {
                level_a_sticker_images[i].sprite = MSG_Transitioner.data.level_a_sticker_blank_sprites[i];
            }
        }

        // Level B
        for (int i = 0; i < level_b_sticker_images.Length; i++)
        {
            if (MSG_Transitioner.data.level_b_stickers[i])
            {
                level_b_sticker_images[i].sprite = MSG_Transitioner.data.level_b_sticker_sprites[i];
            }
            else
            {
                level_b_sticker_images[i].sprite = MSG_Transitioner.data.level_b_sticker_blank_sprites[i];
            }
        }

        // Level C
        for (int i = 0; i < level_c_sticker_images.Length; i++)
        {
            if (MSG_Transitioner.data.level_c_stickers[i])
            {
                level_c_sticker_images[i].sprite = MSG_Transitioner.data.level_c_sticker_sprites[i];
            }
            else
            {
                level_c_sticker_images[i].sprite = MSG_Transitioner.data.level_c_sticker_blank_sprites[i];
            }
        }
    }

    public void Book_Closed ()
    {
        audioMain.clip = bookClose;
        audioMain.Play();

        book_open = false;

        foreach (GameObject ui in UI)
        {
            ui.SetActive(false);
        }

        if (old_state == camera_states.Movement)
        {
            MSG_Transitioner.data.Movement_Active();
        }

        if (old_state == camera_states.Tracing)
        {
            MSG_Transitioner.data.Tracing_Active();
        }

        old_state = camera_states.None;
    }

    void Start()
    {
        audioMain = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (book_open)
        {
            // Enabling And Disabling arrows at beginning and ending 
            if (page_number <= 0)
            {
                left_arrow.SetActive(false);
            }
            else
            {
                left_arrow.SetActive(true);
            }

            if (page_number >= 3)
            {
                right_arrow.SetActive(false);
            }
            else
            {
                right_arrow.SetActive(true);
            }

            level_text.text = level_string[page_number];

            for (int i = 0; i < pages.Length; i++)
            {
                if (i == page_number)
                {
                    pages[i].SetActive(true);
                }
                else
                {
                    pages[i].SetActive(false);
                }
            }
        }
    }

    public void Arrow_Left ()
    {
        int index = Random.Range(0, flipPage.Length);
        flipPageClip = flipPage[index];
        audioMain.clip = flipPageClip;
        audioMain.Play();

        page_number--;
    }

    public void Arrow_Right ()
    {
        int index = Random.Range(0, flipPage.Length);
        flipPageClip = flipPage[index];
        audioMain.clip = flipPageClip;
        audioMain.Play();

        page_number++;
    }
}
