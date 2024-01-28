using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFollowDragon : MonoBehaviour
{
    public float velocidad = 10f;  // Velocidad de seguimiento
    private Transform objetoAseguir;  // Objeto que seguirá
    public LayerMask capaSeguible;  // Capa de objetos seguibles
    bool enCaja = false;
    private void Start()
    {


    }

    // Se llama cuando otro objeto con un Collider2D entra en el trigger.
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que activó el trigger está en la capa seguible
        if (capaSeguible == (capaSeguible | (1 << other.gameObject.layer)))
        {

            // Establece el objeto a seguir como el objeto que activó el trigger
            objetoAseguir = other.transform;

        }
        if (other.gameObject.tag == "Caja")
        {
            // Deja de seguir si es una caja
            objetoAseguir = null;
            enCaja = true;
            Debug.Log("Caja");

        }
    }

    void Update()
    {
        // Si hay un objeto a seguir, realiza el seguimiento
        if (objetoAseguir != null && !enCaja)
        {
            // Calcula la dirección hacia el objeto a seguir
            Vector3 direccion = objetoAseguir.position - transform.position;

            // Normaliza la dirección para mantener una velocidad constante
            direccion.Normalize();

            // Mueve el objeto hacia el objeto a seguir
            transform.Translate(direccion * velocidad * Time.deltaTime);
        }
    }
}
