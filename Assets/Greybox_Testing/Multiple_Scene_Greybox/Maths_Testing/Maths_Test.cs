using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maths_Test : MonoBehaviour
{
    [Header("Arrays for the tracing templates")]
    public List<int> word_length = new List<int>();
    public List<GameObject> tracers = new List<GameObject>();
    public List<GameObject> temp_tracers = new List<GameObject>();


    public void Tracing_Start(int sticker_number)
    {
        int past_templates = 0;
        int current_templates = 0;

        for (int i = 0; i < word_length.Count; i++)
        {
            if (sticker_number == i)
            {
                for (int j = 0; j < word_length[i]; j++)
                {
                    temp_tracers.Add(tracers[j + past_templates]);
                }
            }
            else
            {
                past_templates += word_length[i];
            }
        }
    }
}
