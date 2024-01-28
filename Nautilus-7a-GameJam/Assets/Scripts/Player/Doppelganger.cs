using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doppelganger : MonoBehaviour
{
    [Header("Character")]
    public int characterScale = 5;

    [Header("Movement")]
    public GameObject player;
    public Vector3 offset;

    private Rigidbody2D rb;
    private int orientation; // 0 left, 1 right

    private bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orientation = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position + offset;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}
