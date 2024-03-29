using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetKey : MonoBehaviour
{
    static public bool canGetKey;
    private bool inZone;
    static public bool hasKey;

    [Header("UI")]
    public GameObject interactImage;

    [Header("Animators")]
    public Animator playerAnimator;
    public Animator oldManAnimator;

    [Header("Key")]
    public GameObject key;
    public GameObject lightPlayer;

    private void Start()
    {
        inZone = false;
        canGetKey = false;
        hasKey = false;

        if (interactImage != null)
            interactImage.SetActive(false);

        if (lightPlayer != null)
            lightPlayer.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Realidad2")
        {
            canGetKey = true;
        }
        else
        {
            canGetKey = false;
        }

        if (playerAnimator != null)
            playerAnimator.SetBool("HasKey", false);

        if (oldManAnimator != null)
            oldManAnimator.SetBool("HasKey", true);
    }

    private void Update()
    {
        if (canGetKey && inZone)
        {

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("HAS COGIDO LA LLAVE");

                hasKey = true;
                if (lightPlayer != null)
                    lightPlayer.SetActive(true);

                if (playerAnimator != null)
                    playerAnimator.SetBool("HasKey", true);

                if (oldManAnimator != null)
                    oldManAnimator.SetBool("HasKey", false);

                if (SceneManager.GetActiveScene().name == "Realidad2" && key != null)
                {
                    key.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inZone = true;
            if(canGetKey)
                interactImage.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inZone = false;
            interactImage.SetActive(false);
        }
    }
}
