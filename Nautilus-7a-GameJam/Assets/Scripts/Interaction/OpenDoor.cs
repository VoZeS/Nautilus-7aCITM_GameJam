using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    static public bool canGetKey;
    private bool inZone;
    static public bool hasKey;
    private bool hasOpenedDoor;

    [Header("UI")]
    public Image interactImage;

    [Header("Door")]
    public BoxCollider2D doorCol;
    public BoxCollider2D openedColRight;
    public BoxCollider2D openedColLeft;
    public Animator doorAnimator;

    [Header("Player")]
    public Animator playerAnimator;
    public Camera cameraComp;
    public GameObject lightPlayer;

    [Header("Followers")]
    public Follow girlFollowScript;
    public Follow brokenHeartFollowScript;
    public Follow abusedFollowScript;

    [Header("Sound")]
    public AudioSource doorOpenAudio;
    public AudioSource padlockAudio;

    public BoxCollider2D doorTrigger;




    private void Start()
    {
        inZone = false;
        canGetKey = false;
        hasKey = false;
        hasOpenedDoor = false;
        interactImage.gameObject.SetActive(false);

        if(doorTrigger != null)
            doorTrigger.enabled = true;


        if(openedColRight != null)
            openedColRight.gameObject.SetActive(false);

        if (openedColLeft != null)
            openedColLeft.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (inZone && GetKey.hasKey)
        {

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("HAS ABIERTO LA PUERTA");

                doorAnimator.SetTrigger("OpenDoor");

                doorTrigger.enabled = false;
                lightPlayer.SetActive(false);

                if (SceneManager.GetActiveScene().name=="Pesadilla2")
                {
                    padlockAudio.Play();
                    doorOpenAudio.Play();
                }

                if (SceneManager.GetActiveScene().name == "Realidad2")
                {
                    padlockAudio.Play();
                    doorOpenAudio.PlayDelayed(1);
                }


                doorCol.enabled = false;

                hasKey = false;

                hasOpenedDoor = true;

                playerAnimator.SetBool("HasKey", false);


                if (openedColRight != null)
                    openedColRight.gameObject.SetActive(true);

                if (openedColLeft != null)
                    openedColLeft.gameObject.SetActive(true);

                if(girlFollowScript != null)
                    girlFollowScript.enabled = false;

                if (brokenHeartFollowScript != null)
                    brokenHeartFollowScript.enabled = false;

                if (abusedFollowScript != null)
                    abusedFollowScript.enabled = false;
            }
        }

        if (hasOpenedDoor && SceneManager.GetActiveScene().name == "Pesadilla2")
        {
            if (cameraComp.orthographicSize <= 7f)
                cameraComp.orthographicSize += (0.5f * Time.deltaTime);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inZone = true;
            interactImage.gameObject.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inZone = false;
            interactImage.gameObject.SetActive(false);

        }
    }
}
