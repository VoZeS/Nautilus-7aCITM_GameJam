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

        // Puedes reiniciar los créditos cuando llegan a una cierta posición en Y
        if (transform.position.y > limiteSuperior)
        {
            // Reinicia la posición a la parte inferior
            transform.position = new Vector3(transform.position.x, limiteInferior, transform.position.z);
        }
    }
}


