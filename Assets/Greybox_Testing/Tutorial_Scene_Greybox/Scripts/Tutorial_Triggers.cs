using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Triggers : MonoBehaviour
{
    [Header("Universal Scripts")]
	public Fade fading;
    public Tutorial_Triggers tutorialTriggers;
    public MSG_Level_Movement msgLevelMovement;
    public MSG_Sticker_Selector msgStickerSelector;
    public MSG_Obstacle_Prototype msgObstaclePrototype;
    public MSG_Tracing_Final msgTracingFinal;
    public MSG_Obstacle_Trigger msgObstacleTrigger;

    [Header("Gameobject Triggers")]
    public GameObject welcomeTrigger;
    public GameObject selectStickerTrigger;
    public GameObject obstacleTrigger;
    public GameObject stickerBookTrigger;
    public GameObject tracingTrigger;
    public GameObject firstSticker;
    public GameObject dragAndDropTrigger;

    [Header("Obstacles and Tracing")]
    public GameObject tutorialObstacle;
    public GameObject activeSticker;
    public GameObject retractedBridge;
    public GameObject bridge;
    public GameObject notCompletedFlag;
    public GameObject completedFlag;
	public GameObject portal;

    public int obstacle_int;


    [Header("Audio")]
    public AudioSource audioMain;
    public AudioClip welcomeVoice;
    public AudioClip beginVoice;
    public AudioClip movementVoice;

    public AudioClip stickerSelectVoice;
    public AudioClip stickerPathVoice;
    public AudioClip stickerPathVoice2;

    public AudioClip obstacleVoice;
    public AudioClip obstacleInstructionsVoice;

    public AudioClip firstStickerVoice;
    public AudioClip viewStickerBook;

    [Header("UI")]
    public Button stickerBookButton;
    public Button pauseBackButton;
    public GameObject universalUI;

    void Start()
    {
        audioMain = gameObject.GetComponent<AudioSource>();
        //Find The GameObject's AudioSource
    }

    public void Update()
    {

    }
		
    public void WelcomeDialogue()
    {
        StartCoroutine(playWelcomeDialogue());

        msgLevelMovement.enabled = false;
        //Disable Level Movement
    }

    IEnumerator playWelcomeDialogue()
    {
        audioMain.clip = welcomeVoice;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = beginVoice;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = movementVoice;
        audioMain.Play();

        Invoke("WelcomeDialogueEnd", audioMain.clip.length);
        //Once DialogueVoice Has Reached The End Of The Length (Seconds) Invoke WelcomeDialogueEnd
    }

    void WelcomeDialogueEnd()
    {
        welcomeTrigger.SetActive(false);
        msgLevelMovement.enabled = true;

        selectStickerTrigger.SetActive(true);

        //Disable WelcomeTrigger GameObject and Enable Level Movement
    }

    public void SelectStickerDialogue()
    {
        StartCoroutine(playSelectSticker());
        msgStickerSelector.enabled = true;
        msgLevelMovement.enabled = false;
    }

    IEnumerator playSelectSticker()
    {
        audioMain.clip = stickerSelectVoice;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = stickerPathVoice;
        audioMain.Play();
		yield return new WaitForSeconds (5f);
        //yield return new WaitForSeconds(audioMain.clip.length);
        audioMain.clip = stickerPathVoice2;
        audioMain.Play();

        Invoke("SelectStickerDialogueEnd", audioMain.clip.length);
    }

    void SelectStickerDialogueEnd()
    {
        selectStickerTrigger.SetActive(false);
        obstacleTrigger.SetActive(true);
        msgLevelMovement.enabled = true;
    }

    public void ObstacleDialogue()
    {
        universalUI.SetActive(false);
        StartCoroutine(playObstacle());
        msgLevelMovement.enabled = false;
}

IEnumerator playObstacle()
    {
        audioMain.clip = obstacleVoice;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
		fading.enabled = true;
        ObstacleTrigger();
		yield return new WaitForSeconds (1f);
		fading.enabled = false;
        msgTracingFinal.enabled = false;
        audioMain.clip = obstacleInstructionsVoice;
        audioMain.Play();

        Invoke("ObstacleDialogueEnd", audioMain.clip.length);
    }

    void ObstacleDialogueEnd()
    {
        universalUI.SetActive(true);
        retractedBridge.SetActive(false);
        notCompletedFlag.SetActive(false);

        obstacleTrigger.SetActive(false);
        msgTracingFinal.enabled = true;
        msgLevelMovement.enabled = true;

        bridge.SetActive(true);
        completedFlag.SetActive(true);

        msgTracingFinal.universalUI.SetActive(true);
    }

    public void CongratulationsFirst()
    {
        StartCoroutine(playFinished());
        msgLevelMovement.enabled = false;
    }

    IEnumerator playFinished()
    {
        audioMain.clip = firstStickerVoice;
        audioMain.Play();
        yield return new WaitForSeconds(audioMain.clip.length);
        yield return new WaitForSeconds(1f);
        audioMain.clip = viewStickerBook;
        audioMain.Play();

        Invoke("CongratulationsFirstEnd", audioMain.clip.length);
    }
    void CongratulationsFirstEnd()
    {
        msgLevelMovement.enabled = true;
        stickerBookButton.interactable = true;
        pauseBackButton.interactable = true;
		portal.SetActive (true);
    }

    void ObstacleTrigger()
    {
        //GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        ////MSG_Obstacle_Prototype cam_ob = cam.GetComponent<MSG_Obstacle_Prototype>();

        ////cam_ob.original_cam_position = cam.transform.position;
        ////cam_ob.obstacle_int = obstacle_int;

        //MSG_Tracing_Final cam_tr = cam.GetComponent<MSG_Tracing_Final>();

        //cam_tr.original_cam_position = cam.transform.position;

        //cam.transform.position = new Vector3(-10000, 0, -10);

        //GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Tracing_Active();
        ////cam_ob.obstacles_active(this);

        //cam_tr.Obstacle_Start(obstacle_int, msgObstacleTrigger);

        msgObstacleTrigger.obstacle_int = 0;

        msgObstacleTrigger.Obstacle_Active();
    }
}
