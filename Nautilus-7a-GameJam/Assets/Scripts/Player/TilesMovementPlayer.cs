using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesMovementPlayer : MonoBehaviour
{
    public int characterScale = 5;
    public float tileOffset;
    public float smooth = 0.125f;
    public float velocidadMovimiento;

    private Vector2 targetPosition;
    private Vector2 lastPosition;

    static public bool hasCollided;

    private int orientation; //0 left, 1 right

    public Animator playerAnimator;

    private void Start()
    {
        targetPosition = transform.position;
        hasCollided = false;
        orientation = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOrientation();

        if ((Vector2)transform.position != targetPosition)
        {
            playerAnimator.SetBool("Walking", true);

            if (!hasCollided)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidadMovimiento * Time.deltaTime);

            }
            else
            {
                targetPosition = lastPosition;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidadMovimiento * Time.deltaTime);

            }

        }
        else
        {
            MoverPorTiles();
            playerAnimator.SetBool("Walking", false);

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
            orientation = 1;
            lastPosition = transform.position;
            targetPosition = new Vector2(transform.position.x + tileOffset, transform.position.y);

        }
        if (movimientoHorizontal < 0f)
        {
            orientation = 0;
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

    public void UpdateOrientation()
    {

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
