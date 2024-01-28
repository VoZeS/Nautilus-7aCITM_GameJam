using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCharacter : MonoBehaviour
{

    [Header("Character")]
    public int characterScale = 5;
    public float velocidadMovimiento = 5f;

    private Rigidbody2D rb;
    private int orientation; // 0 left, 1 right

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orientation = 1;
    }

    void Update()
    {
        // Movimiento en un juego 2D isom�trico
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular la direcci�n de movimiento en funci�n de la perspectiva isom�trica
        Vector2 direccion = new Vector2(movimientoHorizontal, movimientoVertical).normalized;

        // Calcular la velocidad de movimiento teniendo en cuenta la perspectiva isom�trica
        Vector2 velocidad = direccion * velocidadMovimiento;

        // Aplicar la velocidad al Rigidbody
        rb.velocity = new Vector2(velocidad.x, velocidad.y);
        
        UpdateOrientation(movimientoHorizontal, direccion);
        
    }

    void UpdateOrientation(float movimientoHorizontal, Vector2 direccion)
    {
        // Actualizar la orientaci�n solo si hay un movimiento
        if (direccion != Vector2.zero)
        {
            //Girar el sprite
            if (movimientoHorizontal >= 0f)
                orientation = 1;
            else if (movimientoHorizontal < 0f)
                orientation = 0;
        }

        switch (orientation)
        {
            case 0:
                transform.localScale = new Vector3(-characterScale, characterScale, characterScale);
                break;
            case 1:
                transform.localScale = new Vector3(characterScale, characterScale, characterScale);
                break;
            default:
                transform.localScale = new Vector3(characterScale, characterScale, characterScale);
                break;
        }
    }
}
