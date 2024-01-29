using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flotación : MonoBehaviour
{

    public float amplitud = 0.5f;
    public float velocidad = 1.0f;

    private Vector2 posicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calcula la nueva posición Y usando una función seno para crear la oscilación
        float newY = posicionInicial.y + amplitud * Mathf.Sin(Time.time * velocidad);

        // Actualiza la posición del objeto
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

