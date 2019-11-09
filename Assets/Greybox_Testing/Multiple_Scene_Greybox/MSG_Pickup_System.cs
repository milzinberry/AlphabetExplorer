using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MSG_Pickup_System : MonoBehaviour
{
    public Tutorial_Triggers tutorialTriggers;

    public Image[] pickup_slots;
    private int array_number = 0;

    private Vector3 distance;
    public GameObject pickup_object;
    public MSG_Pickup pickup;

    public AudioSource audioMain;
    public AudioSource audioOtherMain;
    private GameObject otherAudioMain;
    public AudioClip stickerCollection;
    //public AudioClip golderStickerCollection;

    void Start ()
    {
        audioMain = gameObject.GetComponent<AudioSource>();
        audioOtherMain = otherAudioMain.GetComponent<AudioSource>();

        for(int i = 0; i < pickup_slots.Length; i++)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial_Level_Final"))
            {
                if (MSG_Transitioner.data.tutorial_stickers[i])
                {
                    pickup_slots[i].sprite = MSG_Transitioner.data.tutorial_sticker_sprites[i];
                }
                else
                {
                    pickup_slots[i].sprite = MSG_Transitioner.data.tutorial_sticker_blank_sprites[i];
                }
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
            {
                if (MSG_Transitioner.data.level_a_stickers[i])
                {
                    pickup_slots[i].sprite = MSG_Transitioner.data.level_a_sticker_sprites[i];
                }
                else
                {
                    pickup_slots[i].sprite = MSG_Transitioner.data.level_a_sticker_blank_sprites[i];
                }
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
            {
                if (MSG_Transitioner.data.level_a_stickers[i])
                {
                    pickup_slots[i].sprite = MSG_Transitioner.data.level_b_sticker_sprites[i];
                }
                else
                {
                    pickup_slots[i].sprite = MSG_Transitioner.data.level_b_sticker_blank_sprites[i];
                }
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
            {
                if (MSG_Transitioner.data.level_a_stickers[i])
                {
                    pickup_slots[i].sprite = MSG_Transitioner.data.level_c_sticker_sprites[i];
                }
                else
                {
                    pickup_slots[i].sprite = MSG_Transitioner.data.level_c_sticker_blank_sprites[i];
                }
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}

    public void Picked_Up(int sticker_number)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial_Level_Final"))
        {
            audioOtherMain.clip = stickerCollection;
            audioOtherMain.Play();

            pickup_slots[sticker_number].sprite = MSG_Transitioner.data.GetComponent<MSG_Transitioner>().tutorial_sticker_sprites[sticker_number];
            MSG_Transitioner.data.tutorial_stickers[sticker_number] = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
        {
            audioOtherMain.clip = stickerCollection;
            audioOtherMain.Play();

            pickup_slots[sticker_number].sprite = MSG_Transitioner.data.level_a_sticker_sprites[sticker_number];
            MSG_Transitioner.data.level_a_stickers[sticker_number] = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
        {
            audioOtherMain.clip = stickerCollection;
            audioOtherMain.Play();

            pickup_slots[sticker_number].sprite = MSG_Transitioner.data.level_b_sticker_sprites[sticker_number];
            MSG_Transitioner.data.level_b_stickers[sticker_number] = true;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
        {
            audioOtherMain.clip = stickerCollection;
            audioOtherMain.Play();

            pickup_slots[sticker_number].sprite = MSG_Transitioner.data.level_c_sticker_sprites[sticker_number];
            MSG_Transitioner.data.level_c_stickers[sticker_number] = true;
        }

        MSG_Transitioner.data.Save_Data();
        Destroy(pickup_object);
        pickup_object = null;
        pickup = null;

        if(tutorialTriggers != null) tutorialTriggers.CongratulationsFirst();

    }
}
