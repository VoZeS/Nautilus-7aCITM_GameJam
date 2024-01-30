using System.Collections;
using UnityEngine;

public class DragonHitHand : MonoBehaviour
{
    public Transform jugador;
    public float velocidadMovimiento = 5f; // Velocidad de movimiento hacia el jugador.
    public float tiempoQuieto = 2f; // Tiempo que el objeto se queda quieto después de llegar al jugador.
    public float tiempoQuieto2 = 2f; // Tiempo que el objeto se queda quieto después de llegar al jugador.

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
        // Guardar la posición original al inicio.
        posicionOriginal = transform.position;

        // Actualizar la posición original del jugador al inicio.
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
                // Reiniciar el tiempo y volver a la posición original.
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
                // Reiniciar el tiempo y volver a la posición original.
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
            // Calcular la dirección hacia el jugador.
            Vector3 direccion = (posicionJugadorOriginal - transform.position).normalized;

            // Calcular el desplazamiento hacia el jugador.
            float desplazamiento = velocidadMovimiento * Time.deltaTime;

            // Mover el objeto hacia la posición del jugador usando transform.Translate.
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
            // Manejar la situación en la que el jugador no está asignado.
            Debug.LogWarning("El objeto jugador no está asignado.");
        }
    }

    
    private void VolverAPosicionOriginal()
    {
        collider.enabled = false;
        // Mover el objeto de vuelta a la posición original usando transform.Translate.
        transform.Translate((posicionOriginal - transform.position).normalized * velocidadMovimiento * Time.deltaTime);

        // Verificar si ha vuelto a la posición original.
        if (Vector3.Distance(transform.position, posicionOriginal) <= distancia)
        {
            // Reiniciar la bandera al llegar a la posición original.
            llegoAlJugador = false;
            waiting = true;
            // Actualizar la posición original del jugador.
            
        }
    }

    private void ActualizarPosicionJugadorOriginal()
    {
        if (jugador != null)
        {
            // Guardar la posición actual del jugador como posición original.
            posicionJugadorOriginal = jugador.position;
        }
        else
        {
            // Manejar la situación en la que el jugador no está asignado.
            Debug.LogWarning("El objeto jugador no está asignado.");
        }
    }
}

