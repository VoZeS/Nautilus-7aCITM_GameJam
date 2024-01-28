using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doppelganger : MonoBehaviour
{
    [Header("Character")]
    public int characterScale = 5;
    public float velocidadMovimiento = 5f;

    private Rigidbody2D rb;
    private int orientation; // 0 left, 1 right

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orientation = 1;
    }

    // Update is called once per frame
    void Update()
    {

        // Movimiento en un juego 2D isométrico
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector2 velocidad = new Vector2(movimientoHorizontal * velocidadMovimiento, movimientoVertical * velocidadMovimiento);

        // Aplicar la velocidad al Rigidbody
        rb.velocity = new Vector2(velocidad.x, velocidad.y);

        UpdateOrientation(movimientoHorizontal, velocidad);

        UpdateOrientation(movimientoHorizontal, velocidad);

    }

    public void UpdateOrientation(float movimientoHorizontal, Vector2 direccion)
    {
        // Actualizar la orientación solo si hay un movimiento
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
