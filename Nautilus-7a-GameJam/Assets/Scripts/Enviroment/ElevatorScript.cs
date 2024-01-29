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

    private void Update()
    {
        if (isInTrigger && !isMoving && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MoveElevator());
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

        

        // Mueve la caja hacia arriba
        float elapsedTime = 0f;
        Vector3 startPos = elevator.transform.position;
        Vector3 endPos = elevator.transform.position + Vector3.up * liftDistance;

        while (elapsedTime < waitTime)
        {
            elevator.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Espera un momento en la posición superior
        yield return new WaitForSeconds(waitTime);

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