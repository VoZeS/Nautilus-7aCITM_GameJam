using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetKey : MonoBehaviour
{
    static public bool canGetKey;
    private bool inZone;
    static public bool hasKey;

    [Header("UI")]
    public Image interactImage;

    [Header("Player")]
    public Animator playerAnimator;

    [Header("Key")]
    public GameObject key;

    private void Start()
    {
        inZone = false;
        canGetKey = false;
        hasKey = false;
        interactImage.gameObject.SetActive(false);


        if (SceneManager.GetActiveScene().name == "Realidad2")
        {
            canGetKey = true;
        }
        else
        {
            canGetKey = false;
        }

    }

    private void Update()
    {
        if(canGetKey && inZone)
        {
            interactImage.gameObject.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("HAS COGIDO LA LLAVE");

                hasKey = true;

                playerAnimator.SetBool("HasKey", true);

                if(SceneManager.GetActiveScene().name == "Realidad2")
                {
                    key.SetActive(false);
                }

                //TODO: Cambio sprite viejo (con llave --> sin llave)

                //TODO: Cambio sprite personaje (sin llave --> con llave)

            }
        }
        else
        {
            interactImage.gameObject.SetActive(false);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            inZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            inZone = false;
        }
    }
}
