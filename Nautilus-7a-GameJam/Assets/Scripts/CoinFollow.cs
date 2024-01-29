using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFollow : MonoBehaviour
{
    
    public Transform objetoAseguir;  // Objeto que seguirá
    public LayerMask capaSeguible;  // Capa de objetos seguibles
    bool enCaja = false;

    public GameObject player;
    [SerializeField] public float speed = 5f;

    public float distance2player;
    [SerializeField] public float range = 10f;
    [SerializeField] public float nearRange = 1.2f;
    bool alreadyFollowing = false;
    IsoCharacter playerScript;

   
    void OnTriggerEnter2D(Collider2D other)
    {
        
        // Verifica si el objeto que activó el trigger está en la capa seguible
        if (capaSeguible == (capaSeguible | (1 << other.gameObject.layer)) && !alreadyFollowing )
        {
            playerScript = other.GetComponent<IsoCharacter>();
            
            

            // Establece el objeto a seguir como el objeto que activó el trigger
            if (other.gameObject.tag=="Player" && playerScript.monedasRecogidas.Count < 5)
            {
                if (playerScript.monedasRecogidas.Count == 0 )
                {

                    playerScript.monedasRecogidas.Insert(0, transform);
                    objetoAseguir = other.transform;
                    alreadyFollowing = true;
                }
                else
                {
                    Transform monedaAnterior = playerScript.monedasRecogidas[playerScript.monedasRecogidas.Count - 1];


                    playerScript.monedasRecogidas.Add(transform);

                    objetoAseguir = monedaAnterior.transform;

                    alreadyFollowing = true;
                }
                Debug.Log("+1 Coin, you are slower!");
                playerScript.velocidadMovimiento = playerScript.velocidadMovimiento - (playerScript.monedasRecogidas.Count * 0.10f);
            }
            else if(other.gameObject.tag == "Dragon")
            {
                objetoAseguir = other.transform;
            }
            else
            {
                objetoAseguir = null;
            }

            Collider collider = GetComponent<Collider>();

            collider.enabled = false;

        }
        
    }

    void Update()
    {
        // Si hay un objeto a seguir, realiza el seguimiento
        if (objetoAseguir != null && !enCaja &&playerScript.gameObject.tag=="Player")
        {
            distance2player = Vector2.Distance(transform.position, objetoAseguir.position);
            Vector2 direction = objetoAseguir.position - transform.position;
            direction.Normalize();

            if (distance2player < range && distance2player > nearRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, objetoAseguir.position, speed * Time.deltaTime);
            }
        }else if(playerScript.gameObject.tag == "Dragon")
        {
            // Calcula la dirección hacia el objeto a seguir
            Vector3 direccion = objetoAseguir.position - transform.position;

            // Normaliza la dirección para mantener una velocidad constante
            direccion.Normalize();

            // Mueve el objeto hacia el objeto a seguir
            transform.Translate(direccion * speed * Time.deltaTime);
        }
    }
}

