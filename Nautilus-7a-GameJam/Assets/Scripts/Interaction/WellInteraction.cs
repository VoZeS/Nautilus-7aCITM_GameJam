using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WellInteraction : MonoBehaviour
{
    [Header("UI")]
    public Image interactSprite;
    public Slider interactSlider;

    private bool readyToInteract = false;
    private bool interacting = false;

    private float interactTimer = 0;

    private void Start()
    {
        interactSprite.gameObject.SetActive(false);
        interactSlider.gameObject.SetActive(false);
        interactTimer = 0;
    }

    private void Update()
    {
        if (readyToInteract)
        {
            interactSprite.gameObject.SetActive(true);

            if (Input.GetKey("e"))
            {
                if (!interacting)
                {
                    Interact();
                    interacting = true;
                }
                else
                {
                    InteractHold();
                }
            }
            else
            {
                interacting = false;
                interactSlider.gameObject.SetActive(false);

            }

        }
        else
        {
            interactSprite.gameObject.SetActive(false);

        }
    }

    void Interact()
    {
        Debug.Log("Interactuando");

        interactTimer = 0;

        interactSlider.gameObject.SetActive(true);

    }

    void InteractHold()
    {
        interactTimer += Time.deltaTime;

        interactSlider.value = interactTimer * 0.5f;

        // TODO: Only interact if the player has coins to deposit
        if (interactTimer >= 2f)
        {
            Debug.Log("Moneda puesta");
            interactTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            readyToInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            readyToInteract = false;
        }
    }
}
