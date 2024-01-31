using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public bool bossStarted = false;
    float coinsDroped = 0f;

    public GameObject coinsHand;
    public GameObject hitHand;
    public GameObject coinSpawner;
    public GameObject bagRoof;
    public GameObject timer;

    Dragon coinHandScript;
    DragonHitHand hitHandScript;

    public float tiempoEspera = 1f;

    bool esperando = false;

    public List<Transform> monedasPlayerTotales = new List<Transform>();


    private void Start()
    {
        player = GameObject.Find("PlayerIso");
        scriptLista = player.GetComponent<IsoCharacter>();

        interactSprite.gameObject.SetActive(false);
        interactSlider.gameObject.SetActive(false);
        interactTimer = 0;

        scriptLista.monedasRecogidas.Clear();
        Debug.Log("Lista de Monedas del Player Limpia");

        coinHandScript = coinsHand.GetComponent<Dragon>();
        hitHandScript = hitHand.GetComponent<DragonHitHand>();


        coinsHand.SetActive(false);
        timer.SetActive(false);
        coinsHand.SetActive(false);
        hitHand.SetActive(false);
        coinSpawner.SetActive(false);
        bagRoof.SetActive(false);

    }

    private void Update()
    {
        if(coinsDroped >= 3f && !bossStarted)
        {
            bossStarted = true;
            timer.SetActive(true);
            coinsHand.SetActive(true);
            hitHand.SetActive(true);
            coinSpawner.SetActive(true);
            bagRoof.SetActive(true);
            esperando = true;

            


        }

        if (esperando)
        {
            tiempoEspera -= Time.deltaTime;
            if (tiempoEspera <= 0f)
            {
                coinHandScript.enabled = true;
                hitHandScript.enabled = true;
                Debug.Log("Pesadilla 1 START");
                esperando = false;
            }
        }

        if (readyToInteract)
        {
            
            //interactSprite.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                DropCoins();
                Debug.Log("Interaction");
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
        scriptLista.velocidadMovimiento = 3;
        for (int i = 0; i < scriptLista.monedasRecogidas.Count; i++)
        {
            monedasPlayerTotales.Add(scriptLista.monedasRecogidas[i]);
            scriptLista.monedasRecogidas[i].gameObject.SetActive(false);

            coinsDroped++;
            
            
            Debug.Log("Player ha Tirado Monedas al Pozo");
        }
        scriptLista.monedasRecogidas.Clear();


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
