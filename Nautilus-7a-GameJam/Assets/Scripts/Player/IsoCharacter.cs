using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCharacter : MonoBehaviour
{
    public float coinCount = 0;
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
        

        // Movimiento en un juego 2D isométrico
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        
        Vector2 velocidad = new Vector2(movimientoHorizontal * velocidadMovimiento, movimientoVertical * velocidadMovimiento);

        
        
        // Aplicar la velocidad al Rigidbody
        rb.velocity = new Vector2(velocidad.x, velocidad.y);
        
        UpdateOrientation(movimientoHorizontal, velocidad);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Coin")
        {

            if (coinCount > 0)
            {
                velocidadMovimiento = velocidadMovimiento - (coinCount * 0.10f);
            }
            coinCount= coinCount+0.5f;
            Debug.Log("+1 Coin, you are slower!");
        }
    }

    void UpdateOrientation(float movimientoHorizontal, Vector2 direccion)
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
