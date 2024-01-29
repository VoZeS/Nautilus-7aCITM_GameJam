using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFollow : MonoBehaviour
{
    

    private Transform objetoAseguir;  // Objeto que seguirá
    public LayerMask capaSeguible;  // Capa de objetos seguibles
    bool enCaja = false;

    public GameObject player;
    [SerializeField] public float speed = 5f;

    public float distance2player;
    [SerializeField] public float range = 10f;
    [SerializeField] public float nearRange = 1.2f;
    bool alreadyFollowing = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // Verifica si el objeto que activó el trigger está en la capa seguible
        if (capaSeguible == (capaSeguible | (1 << other.gameObject.layer)) && !alreadyFollowing)
        {

            IsoCharacter player = other.GetComponent<IsoCharacter>();
            // Establece el objeto a seguir como el objeto que activó el trigger

            if (player.monedasRecogidas.Count == 0)
            {

                Debug.Log("P");
                player.monedasRecogidas.Insert(0, transform);
                objetoAseguir = other.transform;
                alreadyFollowing = true;
            }
            else 
            {
                Transform monedaAnterior = player.monedasRecogidas[player.monedasRecogidas.Count - 1];
                
                
                player.monedasRecogidas.Add(transform);

                objetoAseguir = monedaAnterior.transform;

                alreadyFollowing = true;
            }
        }
        else if (other.gameObject.tag == "Caja")
        {
            // Deja de seguir si es una caja
            // objetoAseguir = null;
            // enCaja = true;
            // Debug.Log("Caja");
        }
    }

    void Update()
    {
        // Si hay un objeto a seguir, realiza el seguimiento
        if (objetoAseguir != null && !enCaja)
        {
            distance2player = Vector2.Distance(transform.position, objetoAseguir.position);
            Vector2 direction = objetoAseguir.position - transform.position;
            direction.Normalize();

            if (distance2player < range && distance2player > nearRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, objetoAseguir.position, speed * Time.deltaTime);
            }
        }
    }
}

