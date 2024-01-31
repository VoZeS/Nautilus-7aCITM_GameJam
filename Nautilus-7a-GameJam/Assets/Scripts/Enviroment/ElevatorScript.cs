using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ElevatorScript : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float waitTime = 2.0f;
    public float liftDistance = 5.0f;

    public GameObject elevator;

    private bool isMoving = false;
    private bool isInTrigger = false;

    public Animator leverAnimator;

    public AudioSource leverSound;
    public AudioSource elevatorSound;

    private Vector3 startPos;
    private Vector3 endPos;



    private void Update()
    {
        if (isInTrigger && !isMoving && Input.GetKeyDown(KeyCode.E))
        {
            leverAnimator.SetBool("Activated", true);
            leverSound.Play();
            StartCoroutine(MoveElevator());
        }

        if(!isMoving)
        {
            leverAnimator.SetBool("Activated", false);
            elevatorSound.Stop();
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        isInTrigger = true;
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        isInTrigger = false;
        

    }

    private IEnumerator MoveElevator()
    {
        isMoving = true;
        elevatorSound.Play();
        
        
        

        // Mueve la caja hacia arriba
        float elapsedTime = 0f;
        startPos = elevator.transform.position;
        endPos = elevator.transform.position + Vector3.up * liftDistance;

        

        while (elapsedTime < waitTime)
        {
            elevator.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elevatorSound.Stop();
        // Espera un momento en la posición superior
        yield return new WaitForSeconds(waitTime);
        elevatorSound.Play();
        // Mueve la caja de nuevo hacia abajo
        elapsedTime = 0f;
        startPos = elevator.transform.position;
        endPos = elevator.transform.position - Vector3.up * liftDistance;

        while (elapsedTime < waitTime)
        {
            elevator.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }
}