using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScroll : MonoBehaviour
{
 
    public float velocidadDesplazamiento = 1.0f;
    public float limiteSuperior = 500f;
    public float limiteInferior = -500f;

    void Update()
    {
        // Mueve el objeto hacia arriba a una velocidad constante
        transform.Translate(Vector3.up * velocidadDesplazamiento * Time.deltaTime);

        // Puedes reiniciar los cr�ditos cuando llegan a una cierta posici�n en Y
        if (transform.position.y > limiteSuperior)
        {
            // Reinicia la posici�n a la parte inferior
            transform.position = new Vector3(transform.position.x, limiteInferior, transform.position.z);
        }
    }
}


