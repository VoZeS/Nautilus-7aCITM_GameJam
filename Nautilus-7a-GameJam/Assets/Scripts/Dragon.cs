using UnityEngine;

public class Dragon : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public Transform objetivo; // Asigna el GameObject hacia el cual se dirigir� despu�s de recoger la moneda
    private GameObject monedaActual;
    bool hasMoneda = false;

    void Start()
    {
        // Llamamos al m�todo para buscar la moneda m�s cercana al inicio
        BuscarMonedaCercana();
    }

    void Update()
    {
               
        // Si hay una moneda a la que seguir, mueve al enemigo hacia ella
        if (monedaActual != null&& !hasMoneda)
        {
            MoverHaciaObjetivo(monedaActual.transform.position);
        }
        // Si no hay una moneda, mueve al enemigo hacia el objetivo general
        else if (objetivo != null&&hasMoneda)
        {
            MoverHaciaObjetivo(objetivo.position);
        }
    }

    void MoverHaciaObjetivo(Vector3 posicionObjetivo)
    {
        // Calcula la direcci�n hacia el objetivo
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
            // Encuentra la moneda m�s cercana
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

    // Puedes llamar a este m�todo desde alg�n evento, por ejemplo, cuando el enemigo recoge la moneda
    public void CambiarObjetivoGeneral()
    {
        // Cambia el objetivo a otro GameObject si es necesario
        // Por ejemplo: objetivo = GameObject.Find("NombreDelObjetivo").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Coin")
        {
            hasMoneda = true;
        }
    }
}
