using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Transition : MonoBehaviour
{
    public GameObject[] main_menu;
    public GameObject[] case_selection;
    public GameObject[] game;

    public GameObject[] letters;

    private Tracing_Final tf;

    private int letter_number = 0;
    private int letter_case = 0;

	// Use this for initialization
	void Start ()
    {
		for(int i = 0; i < main_menu.Length; i++)
        {
            main_menu[i].SetActive(true);
        }

        tf = GetComponent<Tracing_Final>();
    }

    public void Letter_A ()
    {
        for (int i = 0; i < main_menu.Length; i++)
        {
            main_menu[i].SetActive(false);
        }
        for (int i = 0; i < case_selection.Length; i++)
        {
            case_selection[i].SetActive(true);
        }

        letter_number = 1;
    }

    public void Letter_B ()
    {
        for (int i = 0; i < main_menu.Length; i++)
        {
            main_menu[i].SetActive(false);
        }
        for (int i = 0; i < case_selection.Length; i++)
        {
            case_selection[i].SetActive(true);
        }

        letter_number = 2;
    }

    public void Letter_C ()
    {
        for (int i = 0; i < main_menu.Length; i++)
        {
            main_menu[i].SetActive(false);
        }
        for (int i = 0; i < case_selection.Length; i++)
        {
            case_selection[i].SetActive(true);
        }

        letter_number = 3;
    }

    public void Capitol ()
    {
        for (int i = 0; i < case_selection.Length; i++)
        {
            case_selection[i].SetActive(false);
        }
        for (int i = 0; i < game.Length; i++)
        {
            game[i].SetActive(true);
        }

        letter_case = 1;

        Begin();
    }

    public void Lowercase ()
    {
        for (int i = 0; i < case_selection.Length; i++)
        {
            case_selection[i].SetActive(false);
        }
        for (int i = 0; i < game.Length; i++)
        {
            game[i].SetActive(true);
        }

        letter_case = 2;

        Begin();
    }

    public void Both ()
    {
        for (int i = 0; i < case_selection.Length; i++)
        {
            case_selection[i].SetActive(false);
        }
        for (int i = 0; i < game.Length; i++)
        {
            game[i].SetActive(true);
        }

        letter_case = 3;

        Begin();
    }

    void Begin ()
    {
        tf.isactive = true;

        if(letter_number == 1)
        {
            if(letter_case == 1)
            {
                tf.tracers.Add(letters[0]);

                letters[0].SetActive(true);

                tf.Begin();
            }

            if (letter_case == 2)
            {
                tf.tracers.Add(letters[3]);

                letters[3].SetActive(true);

                tf.Begin();
            }

            if (letter_case == 3)
            {
                tf.tracers.Add(letters[0]);
                tf.tracers.Add(letters[3]);

                letters[0].SetActive(true);
                letters[3].SetActive(true);

                tf.Begin();
            }
        }

        if (letter_number == 2)
        {
            if (letter_case == 1)
            {
                tf.tracers.Add(letters[1]);

                letters[1].SetActive(true);

                tf.Begin();
            }

            if (letter_case == 2)
            {
                tf.tracers.Add(letters[4]);

                letters[4].SetActive(true);

                tf.Begin();
            }

            if (letter_case == 3)
            {
                tf.tracers.Add(letters[1]);
                tf.tracers.Add(letters[4]);

                letters[1].SetActive(true);
                letters[4].SetActive(true);

                tf.Begin();
            }
        }

        if (letter_number == 3)
        {
            if (letter_case == 1)
            {
                tf.tracers.Add(letters[2]);

                letters[2].SetActive(true);

                tf.Begin();
            }

            if (letter_case == 2)
            {
                tf.tracers.Add(letters[5]);

                letters[5].SetActive(true);

                tf.Begin();
            }

            if (letter_case == 3)
            {
                tf.tracers.Add(letters[2]);
                tf.tracers.Add(letters[5]);

                letters[2].SetActive(true);
                letters[5].SetActive(true);

                tf.Begin();
            }
        }
    }
}
