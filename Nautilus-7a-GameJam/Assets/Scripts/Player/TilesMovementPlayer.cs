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

    private Animator playerAnimator;
    public GameObject parent;

    public GameObject particles;

    private void Start()
    {
        targetPosition = transform.position;
        hasCollided = false;
        orientation = 1;

        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOrientation();

        if(!IsoCharacter.dying)
        {
            if ((Vector2)parent.transform.position != targetPosition)
            {
                if (!hasCollided)
                {
                    parent.transform.position = Vector2.MoveTowards(parent.transform.position, targetPosition, velocidadMovimiento * Time.deltaTime);
                }
                else
                {
                    targetPosition = lastPosition;
                    parent.transform.position = Vector2.MoveTowards(parent.transform.position, targetPosition, velocidadMovimiento * Time.deltaTime);
                }
            }
            else
            {
                particles.SetActive(false);

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

        particles.SetActive(true);

        if (movimientoHorizontal > 0f)
        {
            playerAnimator.SetBool("Walking", true);
            orientation = 1;
            lastPosition = parent.transform.position;
            targetPosition = new Vector2(parent.transform.position.x + tileOffset, parent.transform.position.y);

        }
        else if(movimientoHorizontal < 0f)
        {
            playerAnimator.SetBool("Walking", true);
            orientation = 0;
            lastPosition = parent.transform.position;
            targetPosition = new Vector2(parent.transform.position.x - tileOffset, parent.transform.position.y);

        }
        else if(movimientoVertical > 0f)
        {
            playerAnimator.SetBool("Walking", true);
            lastPosition = parent.transform.position;
            targetPosition = new Vector2(parent.transform.position.x, parent.transform.position.y + tileOffset);

        }
        else if(movimientoVertical < 0f)
        {
            playerAnimator.SetBool("Walking", true);
            lastPosition = parent.transform.position;
            targetPosition = new Vector2(parent.transform.position.x, parent.transform.position.y - tileOffset);

        }
        else
        {
            playerAnimator.SetBool("Walking", false);

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
