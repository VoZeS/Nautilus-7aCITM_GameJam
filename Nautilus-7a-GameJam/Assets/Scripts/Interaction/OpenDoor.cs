using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    static public bool canGetKey;
    private bool inZone;
    static public bool hasKey;

    [Header("UI")]
    public Image interactImage;

    [Header("Door")]
    public BoxCollider2D doorCol;

    [Header("Player")]
    public Animator playerAnimator;

    private void Start()
    {
        inZone = false;
        canGetKey = false;
        hasKey = false;
        interactImage.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (inZone && GetKey.hasKey)
        {
            interactImage.gameObject.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("HAS ABIERTO LA PUERTA");

                doorCol.enabled = false;

                hasKey = false;

                playerAnimator.SetBool("HasKey", false);

            }
        }
        else
        {
            interactImage.gameObject.SetActive(false);

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
