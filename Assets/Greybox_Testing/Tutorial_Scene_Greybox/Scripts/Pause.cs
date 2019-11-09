using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [Header("Pause UI")]
    public Transform pauseCanvas;
    public GameObject pauseButton;

    [Header("Audio SFX")]
    public AudioSource audioMain;
    public AudioClip UIOpen;
    public AudioClip UIClose;
    public AudioClip UISelect;

    public string scene;

    void Start()
    {
        audioMain = gameObject.GetComponent<AudioSource>();
    }

    public void PauseGame()
    {
        audioMain.clip = UIOpen;
        audioMain.Play();

        Time.timeScale = 0;

        pauseCanvas.gameObject.SetActive(true);
        pauseButton.GetComponent<Button>().interactable = false;
    }

	public void ReturnToMenu()
    {
        audioMain.clip = UISelect;
        audioMain.Play();

        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void Continue()
    {
        audioMain.clip = UIClose;
        audioMain.Play();
        Time.timeScale = 1;

        pauseCanvas.gameObject.SetActive(false);
        pauseButton.GetComponent<Button>().interactable = true;
    }
}
