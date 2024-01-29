using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCharacter : MonoBehaviour
{
    public List<Transform> monedasRecogidas = new List<Transform>();
    public float coinCount = 0;

    [Header("Character")]
    public int characterScale = 5;
    public float velocidadMovimiento = 5f;

    [Header("Movement")]
    public GameObject parent;
    private Rigidbody2D rb;
    public int orientation; // 0 left, 1 right

    private Animator playerAnimator;


    public float movimientoHorizontal;
    public Vector2 velocidad;
    void Start()
    {
        rb = parent.GetComponent<Rigidbody2D>();
        orientation = 1;

        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Walk(out movimientoHorizontal, out velocidad);

        UpdateOrientation(movimientoHorizontal, velocidad);


    }

    private void Walk(out float movimientoHorizontal, out Vector2 velocidad)
    {
        playerAnimator.SetBool("Walking", true);

        // Movimiento en un juego 2D isométrico
        movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        velocidad = new Vector2(movimientoHorizontal * velocidadMovimiento, movimientoVertical * velocidadMovimiento);

        // Aplicar la velocidad al Rigidbody
        rb.velocity = new Vector2(velocidad.x, velocidad.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Caja")
        {
 
            velocidadMovimiento = 5;

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
        else
        {
            playerAnimator.SetBool("Walking", false);

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
