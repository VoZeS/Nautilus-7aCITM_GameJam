using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesMovementPlayer : MonoBehaviour
{
    public float tileOffset;
    public float smooth = 0.125f;
    public float velocidadMovimiento;

    private Vector2 targetPosition;
    private Vector2 lastPosition;

    static public bool hasCollided;

    private void Start()
    {
        targetPosition = transform.position;
        hasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            if(!hasCollided)
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidadMovimiento * Time.deltaTime);
            else
            {
                targetPosition = lastPosition;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidadMovimiento * Time.deltaTime);

            }

        }
        else
        {
            MoverPorTiles();
        }
    }

    void MoverPorTiles()
    {
        // Obtener la entrada del jugador
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        hasCollided = false;

        if (movimientoHorizontal > 0f)
        {
            lastPosition = transform.position;
            targetPosition = new Vector2(transform.position.x + tileOffset, transform.position.y);

        }
        if (movimientoHorizontal < 0f)
        {
            lastPosition = transform.position;
            targetPosition = new Vector2(transform.position.x - tileOffset, transform.position.y);

        }
        if (movimientoVertical > 0f)
        {
            lastPosition = transform.position;
            targetPosition = new Vector2(transform.position.x, transform.position.y + tileOffset);

        }
        if (movimientoVertical < 0f)
        {
            lastPosition = transform.position;
            targetPosition = new Vector2(transform.position.x, transform.position.y - tileOffset);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasCollided = true;
    }
}
