using System.Collections;
using UnityEngine;

public class DragonHitHand : MonoBehaviour
{
    public Transform jugador;
    public float velocidadMovimiento = 5f; // Velocidad de movimiento hacia el jugador.
    public float tiempoQuieto = 2f; // Tiempo que el objeto se queda quieto despu�s de llegar al jugador.
    public float tiempoQuieto2 = 2f; // Tiempo que el objeto se queda quieto despu�s de llegar al jugador.

    [SerializeField]public float distancia = 0.1f;

    private Vector3 posicionOriginal;
    private bool llegoAlJugador = false;
    private bool waiting = true;
    private bool waiting2 = true;
    private Vector3 posicionJugadorOriginal;

    Collider2D collider;

    void Start()
    {

        collider = GetComponent<Collider2D>();

        collider.enabled = false;
        // Guardar la posici�n original al inicio.
        posicionOriginal = transform.position;

        // Actualizar la posici�n original del jugador al inicio.
        ActualizarPosicionJugadorOriginal();
    }

    void Update()
    {
        if (!llegoAlJugador && !waiting)
        {

            MoverHaciaJugador();

        }
        else if (waiting)
        {
            tiempoQuieto -= Time.deltaTime;
            if (tiempoQuieto <= 0f)
            {
                // Reiniciar el tiempo y volver a la posici�n original.
                waiting = false;
                tiempoQuieto = 4f;
                ActualizarPosicionJugadorOriginal();
            }
            
        }
        else if (waiting2)
        {
            collider.enabled = false;
            tiempoQuieto2 -= Time.deltaTime;
            if (tiempoQuieto2 <= 0f)
            {
                // Reiniciar el tiempo y volver a la posici�n original.
                waiting2 = false;
                tiempoQuieto2 = 2f;
                ActualizarPosicionJugadorOriginal();
            }

        }
        else
        {
            VolverAPosicionOriginal();
        }
    }

    private void MoverHaciaJugador()
    {
        if (jugador != null)
        {
            // Calcular la direcci�n hacia el jugador.
            Vector3 direccion = (posicionJugadorOriginal - transform.position).normalized;

            // Calcular el desplazamiento hacia el jugador.
            float desplazamiento = velocidadMovimiento * Time.deltaTime;

            // Mover el objeto hacia la posici�n del jugador usando transform.Translate.
            transform.Translate(direccion * desplazamiento);

            if (Vector3.Distance(transform.position, posicionJugadorOriginal) <= 0.5) 
            {
                collider.enabled = true;

            }
            // Verificar si ha llegado al jugador.
            if (Vector3.Distance(transform.position, posicionJugadorOriginal) <=distancia)
            {
                
                transform.Translate(Vector3.zero);

                llegoAlJugador = true;
                waiting2 = true;

            }
        }
        else
        {
            // Manejar la situaci�n en la que el jugador no est� asignado.
            Debug.LogWarning("El objeto jugador no est� asignado.");
        }
    }

    
    private void VolverAPosicionOriginal()
    {
        collider.enabled = false;
        // Mover el objeto de vuelta a la posici�n original usando transform.Translate.
        transform.Translate((posicionOriginal - transform.position).normalized * velocidadMovimiento * Time.deltaTime);

        // Verificar si ha vuelto a la posici�n original.
        if (Vector3.Distance(transform.position, posicionOriginal) <= distancia)
        {
            // Reiniciar la bandera al llegar a la posici�n original.
            llegoAlJugador = false;
            waiting = true;
            // Actualizar la posici�n original del jugador.
            
        }
    }

    private void ActualizarPosicionJugadorOriginal()
    {
        if (jugador != null)
        {
            // Guardar la posici�n actual del jugador como posici�n original.
            posicionJugadorOriginal = jugador.position;
        }
        else
        {
            // Manejar la situaci�n en la que el jugador no est� asignado.
            Debug.LogWarning("El objeto jugador no est� asignado.");
        }
    }
}

