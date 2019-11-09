using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MSG_Gold_Block : MonoBehaviour
{
    public Renderer[] black_blocks;
    public Renderer[] grey_blocks;

    public GameObject[] bridge;

    public Color gold_black;
    public Color gold_grey;

    public Color green_black;
    public Color green_grey;

    public GameObject top_text;
    public GameObject bottom_text;

    private bool stickers_collected()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial_Level_Final"))
        {
            for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers.Length - 1; i++)
            {
                if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[i] == false)
                {
                    return false;
                }
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
        {
            for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_stickers.Length - 1; i++)
            {
                if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_stickers[i] == false)
                {
                    return false;
                }
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
        {
            for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_b_stickers.Length - 1; i++)
            {
                if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_b_stickers[i] == false)
                {
                    return false;
                }
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
        {
            for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_c_stickers.Length - 1; i++)
            {
                if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_c_stickers[i] == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void FixedUpdate()
    {
        if (!stickers_collected())
        {
            foreach (GameObject bridge_piece in bridge)
            {
                bridge_piece.SetActive(false);
            }

            foreach (Renderer black_renderer in black_blocks)
            {
                black_renderer.material.color = gold_black;
            }

            foreach (Renderer grey_renderer in grey_blocks)
            {
                grey_renderer.material.color = gold_black;
            }

            float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if (distance < 1.3f)
            {
                Text count_text = bottom_text.GetComponent<Text>();
                int number = 0;

                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial_Level_Final"))
                {
                    for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers.Length - 1; i++)
                    {
                        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[i])
                        {
                            number++;
                        }
                    }
                    count_text.text = (number.ToString() + " / " + (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers.Length - 1).ToString());
                }

                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_A"))
                {
                    for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_stickers.Length - 1; i++)
                    {
                        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_stickers[i])
                        {
                            number++;
                        }
                    }
                    count_text.text = (number.ToString() + " / " + (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_a_stickers.Length - 1).ToString());
                }

                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_B"))
                {
                    for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_b_stickers.Length - 1; i++)
                    {
                        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_b_stickers[i])
                        {
                            number++;
                        }
                    }
                    count_text.text = (number.ToString() + " / " + (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_b_stickers.Length - 1).ToString());
                }

                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_C"))
                {
                    for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_c_stickers.Length - 1; i++)
                    {
                        if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_c_stickers[i])
                        {
                            number++;
                        }
                    }
                    count_text.text = (number.ToString() + " / " + (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().level_c_stickers.Length - 1).ToString());
                }

                top_text.SetActive(true);
                bottom_text.SetActive(true);
            }
            else
            {
                top_text.SetActive(false);
                bottom_text.SetActive(false);
            }
        }

        if (stickers_collected())
        {
            foreach (GameObject bridge_piece in bridge)
            {
                bridge_piece.SetActive(true);
            }

            foreach (Renderer black_renderer in black_blocks)
            {
                black_renderer.material.color = green_black;
            }

            foreach (Renderer grey_renderer in grey_blocks)
            {
                grey_renderer.material.color = green_black;
            }

            top_text.SetActive(false);
            bottom_text.SetActive(false);
        }
    }
}
