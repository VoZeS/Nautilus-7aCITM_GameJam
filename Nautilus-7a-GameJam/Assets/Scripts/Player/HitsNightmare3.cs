using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitsNightmare3 : MonoBehaviour
{
    static public int lives = 2;
    private bool hitted;

    public GameObject[] hearts;

    public TimerScript changeSceneScript;

    private int resultNightmare3; //0 win, 1 lose

    private void Start()
    {
        hitted = false;
    }

    private void Update()
    {
        if (hitted)
        {
            Debug.Log("-1 vida");
            lives--;
            hitted = false;
        }

        switch (lives)
        {
            case 0:
                Debug.Log("HAS PERDIDO");

                resultNightmare3 = 1; //lose
                PlayerPrefs.SetInt("Nightmare3", resultNightmare3);
                PlayerPrefs.Save();

                hearts[0].SetActive(false);
                hearts[1].SetActive(false);

                changeSceneScript.PerderJuego();

                break;
            case 1:
                hearts[0].SetActive(true);
                hearts[1].SetActive(false);
                break;
            case 2:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                break;
            default:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                break;
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
