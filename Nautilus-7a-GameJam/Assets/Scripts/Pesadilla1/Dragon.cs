using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public List<Transform> monedasDragonRecogidas = new List<Transform>();
    public float velocidadMovimiento = 5f;
    public Transform objetivo; // Asigna el GameObject hacia el cual se dirigirá después de recoger la moneda
    public GameObject monedaActual;
    bool hasMoneda = false;

    void Start()
    {
        // Llamamos al método para buscar la moneda más cercana al inicio
        BuscarMonedaCercana();
    }

    void FixedUpdate()
    {
               
        if (!hasMoneda )
        {
            BuscarMonedaCercana();
        }
        // Si hay una moneda a la que seguir, mueve al enemigo hacia ella
        if (monedaActual != null && !hasMoneda)
        {
            MoverHaciaObjetivo(monedaActual.transform.position);
        }
        // Si no hay una moneda, mueve al enemigo hacia el objetivo general
        else if (objetivo != null && hasMoneda)
        {
            MoverHaciaObjetivo(objetivo.position);
        }
    }

    void MoverHaciaObjetivo(Vector3 posicionObjetivo)
    {
        // Calcula la dirección hacia el objetivo
        Vector3 direccion = (posicionObjetivo - transform.position).normalized;

        // Mueve al enemigo hacia el objetivo
        transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
    }

    void BuscarMonedaCercana()
    {
        // Encuentra todas las monedas en la escena
        GameObject[] monedas = GameObject.FindGameObjectsWithTag("Coin");

        // Si hay al menos una moneda
        if (monedas.Length > 0)
        {
            // Encuentra la moneda más cercana
            float distanciaMinima = float.MaxValue;
            foreach (GameObject moneda in monedas)
            {
                float distancia = Vector3.Distance(transform.position, moneda.transform.position);
                if (distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    monedaActual = moneda;
                }
            }
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Coin")
        {
            hasMoneda = true;
        }
        else if (collision.gameObject.tag == "Caja")
        {
            hasMoneda = false;
            for (int i = 0; i < monedasDragonRecogidas.Count; i++)
            {
                monedasDragonRecogidas[i].gameObject.SetActive(false);

                Debug.Log("Monedas puestas");
            }
            monedasDragonRecogidas.Clear();
            Debug.Log("Lista Limpia");
        }
    }
}
