using UnityEngine;

public class DragonHitHand : MonoBehaviour
{
    public Transform jugador;
    public float velocidadMovimiento = 5f; // Velocidad de movimiento hacia el jugador.
    public float tiempoQuieto = 2f; // Tiempo que el objeto se queda quieto despu�s de llegar al jugador.
    public float tiempoQuieto2 = 2f;

    private Vector3 posicionOriginal;
    private bool llegoAlJugador = false;
    private Vector3 posicionJugadorOriginal;

    void Start()
    {
        // Guardar la posici�n original al inicio.
        posicionOriginal = transform.position;

        // Actualizar la posici�n original del jugador al inicio.
        ActualizarPosicionJugadorOriginal();
    }

    void Update()
    {
        if (!llegoAlJugador)
        {
            Invoke("MoverHaciaJugador", tiempoQuieto);
            tiempoQuieto = 2f;
        }
        else
        {
            // Permanecer quieto durante el tiempo especificado.
            Invoke("VolverAPosicionOriginal", tiempoQuieto2);
            tiempoQuieto2 = 2f;
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

            // Verificar si ha llegado al jugador.
            if (Vector3.Distance(transform.position, posicionJugadorOriginal) < 0.1f)
            {
                llegoAlJugador = true;
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
        // Mover el objeto de vuelta a la posici�n original usando transform.Translate.
        transform.Translate((posicionOriginal - transform.position).normalized * velocidadMovimiento * Time.deltaTime);

        // Verificar si ha vuelto a la posici�n original.
        if (Vector3.Distance(transform.position, posicionOriginal) < 0.1f)
        {
            // Reiniciar la bandera al llegar a la posici�n original.
            llegoAlJugador = false;

            // Actualizar la posici�n original del jugador.
            ActualizarPosicionJugadorOriginal();
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

