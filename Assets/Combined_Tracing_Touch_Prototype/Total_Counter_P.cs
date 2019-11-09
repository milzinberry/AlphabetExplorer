using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Total_Counter_P : MonoBehaviour
{
    public Combined_Draw_Touch drawing_script;
    public GameObject[] tracers;
    public int correctnumber = 0;

    public int total_min_count;
    public int total_max_count;
    public int total_node_count = 0;

    private Combined_Draw_Touch cam_script;

	// Use this for initialization
	void Start ()
    {
        cam_script = GetComponent<Combined_Draw_Touch>();

		for(int i = 0; i < tracers.Length; i++)
        {
            Counter_P counter = tracers[i].GetComponent<Counter_P>();
            total_min_count = total_min_count + counter.min_count;
            total_max_count = total_max_count + counter.max_count;
        }
	}

    public void Count ()
    {

        for (int i = 0; i < tracers.Length; i++)
        {
            Debug.Log("Loop: " + i);
            Counter_P counter = tracers[i].GetComponent<Counter_P>();
            counter.Count();
            if(counter.iscorrect)
            {
                correctnumber++;
            }
        }

        GameObject checker = GameObject.Find("Checker");
        Image checker_image = checker.GetComponent<Image>();

        if (correctnumber == tracers.Length && cam_script.total_nodes < total_max_count)
        {
            checker_image.color = Color.green;
        }
        else
        {
            checker_image.color = Color.red;
        }
    }
}
