using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFollow : MonoBehaviour
{
    
    public Transform objetoAseguir;  // Objeto que seguir�
    public LayerMask capaSeguible;  // Capa de objetos seguibles
    bool enCaja = false;

    GameObject player;
    [SerializeField] public float speed = 5f;

    public float distance2player;
    [SerializeField] public float range = 10f;
    [SerializeField] public float nearRange = 1.2f;
    bool alreadyFollowing = false;
    GameObject collision;
    IsoCharacter playerScript = null;
    Dragon dragonScript = null;
    Collider2D collider;

    public GameObject coinParent;
    private int orientation; //0 left, 1 right


    public AudioSource coinGrab;


    private void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        collision = other.gameObject;
        // Verifica si el objeto que activ� el trigger est� en la capa seguible
        if (capaSeguible == (capaSeguible | (1 << other.gameObject.layer)) && !alreadyFollowing )
        {
           
            if(other.gameObject.tag == "Player")
            {
                playerScript = other.GetComponent<IsoCharacter>();
                coinGrab.Play();
                

            }else if (other.gameObject.tag == "Dragon")
            {
                dragonScript = other.GetComponent<Dragon>();
                
            }

            // Establece el objeto a seguir como el objeto que activ� el trigger
            if (other.gameObject.tag=="Player" && playerScript.monedasRecogidas.Count < 5)
            {
                if (playerScript.monedasRecogidas.Count == 0 )
                {

                    playerScript.monedasRecogidas.Insert(0, coinParent.transform);
                    objetoAseguir = other.transform;
                    alreadyFollowing = true;
                }
                else
                {
                    Transform monedaAnterior = playerScript.monedasRecogidas[playerScript.monedasRecogidas.Count - 1];


                    playerScript.monedasRecogidas.Add(coinParent.transform);

                    if (monedaAnterior.gameObject.activeInHierarchy == true)
                    {
                        objetoAseguir = monedaAnterior.transform;
                    }

                    alreadyFollowing = true;
                }
                Debug.Log("+1 Coin, you are slower!");
                playerScript.velocidadMovimiento = playerScript.velocidadMovimiento - (playerScript.monedasRecogidas.Count * 0.10f);
            }
            else if(other.gameObject.tag == "Dragon" && dragonScript.monedasDragonRecogidas.Count < 1)
            {
                dragonScript.monedasDragonRecogidas.Add(coinParent.transform);
                objetoAseguir = other.transform;
                collider.enabled = false;
            }
            else
            {
                objetoAseguir = null;
            }

            

            

        } 
    }

    void Update()
    {
        // Si hay un objeto a seguir, realiza el seguimiento
        if (objetoAseguir != null && !enCaja && collision.tag == "Player")
        {
            distance2player = Vector2.Distance(coinParent.transform.position, objetoAseguir.position);
            Vector2 direction = objetoAseguir.position - coinParent.transform.position;
            direction.Normalize();

            if (distance2player < range && distance2player > nearRange)
            {
                coinParent.transform.position = Vector2.MoveTowards(coinParent.transform.position, objetoAseguir.position, speed * Time.deltaTime);
            }

            UpdateOrientation(direction);
        }
        else if(objetoAseguir != null && collision.tag == "Dragon")
        {
            // Calcula la direcci�n hacia el objeto a seguir
            Vector3 direccion = objetoAseguir.position - coinParent.transform.position;

            // Normaliza la direcci�n para mantener una velocidad constante
            direccion.Normalize();

            // Mueve el objeto hacia el objeto a seguir
            coinParent.transform.Translate(direccion * speed * Time.deltaTime);
        }
        else if(objetoAseguir == null)
        {
            //Debug.Log("F");
        }
    }

    void UpdateOrientation(Vector3 direction)
    {
        // Actualizar la orientación solo si hay un movimiento
        if (direction.x >= 0)
        {
            orientation = 1;
        }
        else
        {
            orientation = 0;
        }

        switch (orientation)
        {
            case 0:
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
            case 1:
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
            default:
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
        }
    }
}

