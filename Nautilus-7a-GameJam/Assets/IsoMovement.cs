using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoMovement : MonoBehaviour
{
    [SerializeField] public float velocidadMovimiento = 5f;

    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        // Movimiento en un juego 2D isométrico
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular la dirección de movimiento en función de la perspectiva isométrica
        Vector2 direccion = new Vector2(movimientoHorizontal, movimientoVertical).normalized;

        // Calcular la velocidad de movimiento teniendo en cuenta la perspectiva isométrica
        Vector2 velocidad = direccion * velocidadMovimiento;

        // Aplicar la velocidad al Rigidbody
        rb.velocity = new Vector2(velocidad.x, velocidad.y);

        //Girar el spite
        if (movimientoHorizontal > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (movimientoHorizontal < 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


    }
}
