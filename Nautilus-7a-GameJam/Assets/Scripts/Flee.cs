using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public Transform jugador;  // Transform del jugador
    public float distanciaUmbral = 5f;  // Distancia mínima para activar el alejamiento
    public float velocidadAlejamiento = 2f;  // Velocidad a la que el cubo se alejará

    private IsoCharacter playerScript;

    public GameObject parent;
    private Animator fleeAnimator;
    public Animator shadowAnimator;

    private Vector3 lastPosition;

    private SpriteRenderer spriteRend;

    private int orientation; //0 left, 1 right

    private void Start()
    {
        playerScript = jugador.gameObject.GetComponent<IsoCharacter>();

        fleeAnimator = GetComponent<Animator>();

        spriteRend = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // Check if X is flipped and flip it
        if (spriteRend.flipX)
            spriteRend.flipX = false;

        bool isMoving = DetectMovement();

        // Calcular la distancia entre el jugador y el cubo
        float distanciaAlJugador = Vector3.Distance(parent.transform.position, jugador.position);

        // Verificar si la distancia es menor que la umbral
        if (distanciaAlJugador < distanciaUmbral)
        {
            // Calcular la dirección para alejarse del jugador
            Vector3 direccionAlejamiento = (parent.transform.position - jugador.position).normalized;

            // Mover el cubo en la dirección opuesta con una velocidad dada
            parent.transform.position += direccionAlejamiento * velocidadAlejamiento * Time.deltaTime;

            UpdateOrientation(direccionAlejamiento);

        }


        if (fleeAnimator != null && isMoving)
        {
            fleeAnimator.SetBool("Walking", true);
            shadowAnimator.SetBool("Walking", true);
        }
        else if (fleeAnimator != null && !isMoving)
        {
            fleeAnimator.SetBool("Walking", false);
            shadowAnimator.SetBool("Walking", false);

        }
    }

    bool DetectMovement()
    {
        // Compara la posición actual con la última posición registrada
        if (parent.transform.position != lastPosition)
        {
            // El GameObject se está moviendo
            lastPosition = parent.transform.position;
            return true;
        }
        else
        {
            // El GameObject está quieto
            return false;
        }
    }

    void UpdateOrientation(Vector3 direction)
    {
        // Actualizar la orientación solo si hay un movimiento
        if (direction.x >= 0)
        {
            orientation = 1;
        }
        else
        {
            orientation = 0;
        }

        switch (orientation)
        {
            case 0:
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
            case 1:
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
            default:
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
        }
    }


}

