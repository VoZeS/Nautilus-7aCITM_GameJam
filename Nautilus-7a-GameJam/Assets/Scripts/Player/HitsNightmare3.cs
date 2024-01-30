using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitsNightmare3 : MonoBehaviour
{
    static public int lives = 2;
    private bool hitted;

    private void Start()
    {
        hitted = false;
    }

    private void Update()
    {
        if(hitted)
        {
            Debug.Log("-1 vida");
            lives--;
            hitted = false;
        }

        if(lives <= 0)
        {
            Debug.Log("HAS PERDIDO");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bitter"))
        {
            hitted = true;
        }
    }

}
