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

    [Header("Followers")]
    public Follow girlFollowScript;
    public Follow brokenHeartFollowScript;
    public Follow abusedFollowScript;

    [Header("Sound")]
    public AudioSource doorOpenAudio;
    public AudioSource padlockAudio;




    private void Start()
    {
        inZone = false;
        canGetKey = false;
        hasKey = false;
        hasOpenedDoor = false;
        interactImage.gameObject.SetActive(false);




        if(openedColRight != null)
            openedColRight.gameObject.SetActive(false);

        if (openedColLeft != null)
            openedColLeft.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (inZone && GetKey.hasKey)
        {
            interactImage.gameObject.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("HAS ABIERTO LA PUERTA");

                doorAnimator.SetTrigger("OpenDoor");
                padlockAudio.Play();
                doorOpenAudio.PlayDelayed(1);

                doorCol.enabled = false;

                hasKey = false;

                hasOpenedDoor = true;

                playerAnimator.SetBool("HasKey", false);


                if (openedColRight != null)
                    openedColRight.gameObject.SetActive(true);

                if (openedColLeft != null)
                    openedColLeft.gameObject.SetActive(true);

                girlFollowScript.enabled = false;
                brokenHeartFollowScript.enabled = false;
                abusedFollowScript.enabled = false;
            }
        }
        else
        {
            interactImage.gameObject.SetActive(false);

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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inZone = false;
        }
    }
}
