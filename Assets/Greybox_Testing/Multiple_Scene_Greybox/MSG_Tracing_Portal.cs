using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSG_Tracing_Portal : MonoBehaviour
{
    public Renderer[] black_blocks;
    public Renderer[] grey_blocks;

    public Color yellow_black;
    public Color yellow_grey;

	// Use this for initialization
	void Start ()
    {
		for (int i = 0; i < black_blocks.Length; i++)
        {
            black_blocks[i].material.color = yellow_black;
            grey_blocks[i].material.color = yellow_grey;
        }
	}

    private bool stickers_collected ()
    {
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().tutorial_stickers[i] == false)
            {
                return false;
            }
        }

        return true;
    }

    void OnTriggerEnter ()
    {
        if(stickers_collected())
        {
            GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Tracing_Active();

            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.transform.position = new Vector3(-10000, 0, -10);
        }
    }
}
