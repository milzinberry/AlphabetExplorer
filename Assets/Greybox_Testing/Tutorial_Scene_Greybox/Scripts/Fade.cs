﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour 
{
	[Header("Fadeout")]
	public Texture2D fadeOutTexture;
	public float fadeSpeed;
	private int drawDepth = -1000; // textures draw order
	private float alpha = 1.0f;
	private int fadeDir = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI ()
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01(alpha);

		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	public float BeginFade(int direction)
	{
		fadeDir = direction;
		return (fadeSpeed); // return the fade speed so its easy to time the application.loadlevel();
	}

	public void FadingScene()
	{
		BeginFade (-1);
	}
}
