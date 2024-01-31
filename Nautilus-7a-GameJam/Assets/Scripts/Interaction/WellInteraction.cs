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
    GameObject player;
    IsoCharacter scriptLista;

    public List<Transform> monedasPlayerTotales = new List<Transform>();


    private void Start()
    {
        player = GameObject.Find("PlayerIso");
        scriptLista = player.GetComponent<IsoCharacter>();

        interactSprite.gameObject.SetActive(false);
        interactSlider.gameObject.SetActive(false);
        interactTimer = 0;

        scriptLista.monedasRecogidas.Clear();
        Debug.Log("Lista Limpia");

    }

    private void Update()
    {
        if (readyToInteract)
        {
            
            //interactSprite.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                DropCoins();
                Debug.Log("E");
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
            //scriptLista.monedasRecogidas.Clear();
            //Debug.Log("Monedas puestas");
            //interactTimer = 0;
        }
    }
    void DropCoins()
    {
        scriptLista.velocidadMovimiento = 5;
        for (int i = 0; i < scriptLista.monedasRecogidas.Count; i++)
        {
            monedasPlayerTotales.Add(scriptLista.monedasRecogidas[i]);
            scriptLista.monedasRecogidas[i].gameObject.SetActive(false);
            
            Debug.Log("Monedas puestas");
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
