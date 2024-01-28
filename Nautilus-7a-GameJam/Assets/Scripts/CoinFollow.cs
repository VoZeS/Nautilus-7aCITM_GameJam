using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFollow : MonoBehaviour
{
    private Transform objetoAseguir;  // Objeto que seguir�
    public LayerMask capaSeguible;  // Capa de objetos seguibles
    bool enCaja = false;

    public GameObject player;
    [SerializeField] public float speed = 5f;

    public float distance2player;
    [SerializeField] public float range = 10f;
    [SerializeField] public float nearRange = 1.2f;
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que activ� el trigger est� en la capa seguible
        if (capaSeguible == (capaSeguible | (1 << other.gameObject.layer)))
        {
            // Establece el objeto a seguir como el objeto que activ� el trigger
            objetoAseguir = other.transform;
        }
        else if (other.gameObject.tag == "Caja")
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
            distance2player = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance2player < range && distance2player > nearRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }
}

