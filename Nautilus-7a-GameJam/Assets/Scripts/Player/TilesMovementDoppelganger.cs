using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesMovementDoppelganger : MonoBehaviour
{
    public float tileOffset;
    public float smooth = 0.125f;
    public float velocidadMovimiento;

    private Vector2 targetPosition;
    private Vector2 lastPosition;

    private bool hasCollided;

    public GameObject parent;
    private Animator doppelAnimator;

    private void Start()
    {
        targetPosition = transform.position;
        hasCollided = false;

        doppelAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsoCharacter.dying)
        {
            if ((Vector2)parent.transform.position != targetPosition)
            {
                if (!hasCollided && !TilesMovementPlayer.hasCollided)
                    parent.transform.position = Vector2.MoveTowards(parent.transform.position, targetPosition, velocidadMovimiento * Time.deltaTime);
                else
                {
                    targetPosition = lastPosition;
                    parent.transform.position = Vector2.MoveTowards(parent.transform.position, targetPosition, velocidadMovimiento * Time.deltaTime);

                }

            }
            else
            {
                MoverPorTiles();
            }
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
            doppelAnimator.SetBool("Walking", true);
            Doppelganger.orientation = 1;
            lastPosition = parent.transform.position;
            targetPosition = new Vector2(parent.transform.position.x + tileOffset, parent.transform.position.y);

        }
        else if(movimientoHorizontal < 0f)
        {
            doppelAnimator.SetBool("Walking", true);
            Doppelganger.orientation = 0;
            lastPosition = parent.transform.position;
            targetPosition = new Vector2(parent.transform.position.x - tileOffset, parent.transform.position.y);

        }
        else if(movimientoVertical > 0f)
        {
            doppelAnimator.SetBool("Walking", true);
            lastPosition = parent.transform.position;
            targetPosition = new Vector2(parent.transform.position.x, parent.transform.position.y + tileOffset);

        }
        else if(movimientoVertical < 0f)
        {
            doppelAnimator.SetBool("Walking", true);
            lastPosition = parent.transform.position;
            targetPosition = new Vector2(parent.transform.position.x, parent.transform.position.y - tileOffset);

        }
        else
        {
            doppelAnimator.SetBool("Walking", false);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasCollided = true;
    }
}
