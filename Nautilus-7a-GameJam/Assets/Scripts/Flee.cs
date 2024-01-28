using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public Transform jugador;  // Transform del jugador
    public float distanciaUmbral = 5f;  // Distancia mínima para activar el alejamiento
    public float velocidadAlejamiento = 2f;  // Velocidad a la que el cubo se alejará

    void Update()
    {
        // Calcular la distancia entre el jugador y el cubo
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        // Verificar si la distancia es menor que la umbral
        if (distanciaAlJugador < distanciaUmbral)
        {
            // Calcular la dirección para alejarse del jugador
            Vector3 direccionAlejamiento = (transform.position - jugador.position).normalized;

            // Mover el cubo en la dirección opuesta con una velocidad dada
            transform.position += direccionAlejamiento * velocidadAlejamiento * Time.deltaTime;
        }
    }
}

