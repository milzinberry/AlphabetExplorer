using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Levels { none, tutorial, level_a, level_b, level_c }

public class MSG_Sticker_Selector : MonoBehaviour
{
    public Levels level;

    public Outline[] image_outlines;

    public Vector3[] sticker_positions;

    public int selected_sticker = -1;

    public LineRenderer lr;

    private bool finder_active;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(selected_sticker == -1)
        {
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
        }
        else
        {
            lr.SetPosition(0, GameObject.FindGameObjectWithTag("Player").transform.position);
            lr.SetPosition(1, sticker_positions[selected_sticker]);
        }

	    for (int i = 0; i < image_outlines.Length; i++)
        {
            if(i == selected_sticker)
            {
                image_outlines[i].enabled = true;
            }
            else
            {
                image_outlines[i].enabled = false;
            }
        }

        if (level == Levels.tutorial)
        {
            if (finder_active && MSG_Transitioner.data.tutorial_stickers[selected_sticker])
            {
                finder_active = false;
                selected_sticker = -1;
            }
        }

        if (level == Levels.level_a)
        {
            if (finder_active && MSG_Transitioner.data.level_a_stickers[selected_sticker])
            {
                finder_active = false;
                selected_sticker = -1;
            }
        }

        if (level == Levels.level_b)
        {
            if (finder_active && MSG_Transitioner.data.level_b_stickers[selected_sticker])
            {
                finder_active = false;
                selected_sticker = -1;
            }
        }

        if (level == Levels.level_c)
        {
            if (finder_active && MSG_Transitioner.data.level_c_stickers[selected_sticker])
            {
                finder_active = false;
                selected_sticker = -1;
            }
        }
    }

    public void Button_Click (int number)
    {
        if (level == Levels.tutorial)
        {
            if (!MSG_Transitioner.data.tutorial_stickers[number])
            {
                if (number != selected_sticker)
                {
                    finder_active = true;
                    selected_sticker = number;
                }
                else
                {
                    finder_active = false;
                    selected_sticker = -1;
                }
            }
        }

        if (level == Levels.level_a)
        {
            if (!MSG_Transitioner.data.level_a_stickers[number])
            {
                if (number != selected_sticker)
                {
                    finder_active = true;
                    selected_sticker = number;
                }
                else
                {
                    finder_active = false;
                    selected_sticker = -1;
                }
            }
        }

        if (level == Levels.level_b)
        {
            if (!MSG_Transitioner.data.level_b_stickers[number])
            {
                if (number != selected_sticker)
                {
                    finder_active = true;
                    selected_sticker = number;
                }
                else
                {
                    finder_active = false;
                    selected_sticker = -1;
                }
            }
        }

        if (level == Levels.level_c)
        {
            if (!MSG_Transitioner.data.level_c_stickers[number])
            {
                if (number != selected_sticker)
                {
                    finder_active = true;
                    selected_sticker = number;
                }
                else
                {
                    finder_active = false;
                    selected_sticker = -1;
                }
            }
        }
    }
}
