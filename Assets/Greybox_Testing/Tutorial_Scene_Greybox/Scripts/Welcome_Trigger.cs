using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welcome_Trigger : MonoBehaviour
{
    public Tutorial_Triggers tutorialTriggers;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (tutorialTriggers.welcomeTrigger == true)
            {
                tutorialTriggers.WelcomeDialogue();
            }
            //If The Trigger Detects The Player Tag Instatiate The WelcomeDialogue
        }
    }
}
