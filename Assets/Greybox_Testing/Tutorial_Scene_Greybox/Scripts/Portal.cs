using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour 
{
	public string scene;
	public Fade fading;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			EndPortal ();
		}
	}
		
	public void EndPortal()
	{
		StartCoroutine(PortalFade());
	}

	IEnumerator PortalFade()
	{
		fading.enabled = true;
		SceneManager.LoadScene(scene);
		yield return new WaitForSeconds (3f);
		fading.enabled = true;
		yield return new WaitForSeconds (3f);
		fading.enabled = false;
	}

}
