using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    [Header("UI")]
    public Image pressE;
    public Image pressQ;
    public Image pressR;
    public Image pressX;
    public Slider sliderTime;

    [Header("Logic")]
    public Follow followScript;
    public Animator brokenManAnimator;

    private bool inZone;
    private float timer;
    private int counter;
    private bool youLost;
    private bool youWin;
    private bool completed;

    private void Start()
    {
        inZone = false;
        completed = false;
        timer = 2f;
        counter = 0;

        pressE.gameObject.SetActive(false);
        pressQ.gameObject.SetActive(false);
        pressR.gameObject.SetActive(false);
        pressX.gameObject.SetActive(false);
        sliderTime.gameObject.SetActive(false);

        followScript.enabled = false;
    }

    private void Update()
    {
        if (inZone && !completed)
        {
            sliderTime.value = timer * 0.5f;

            switch (counter)
            {
                case 0:
                    pressE.gameObject.SetActive(true);
                    pressQ.gameObject.SetActive(false);
                    pressR.gameObject.SetActive(false);
                    pressX.gameObject.SetActive(false);

                    if (Input.GetKeyDown("e"))
                    {
                        timer = 2f;
                        counter++;
                    }

                    sliderTime.gameObject.SetActive(true);

                    break;
                case 1:
                    pressQ.gameObject.SetActive(true);
                    pressE.gameObject.SetActive(false);
                    pressR.gameObject.SetActive(false);
                    pressX.gameObject.SetActive(false);

                    if (Input.GetKeyDown("q"))
                    {
                        timer = 1.8f;
                        counter++;
                    }
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else if (timer <= 0f)
                    {
                        youLost = true;
                    }
                    break;
                case 2:
                    pressX.gameObject.SetActive(true);
                    pressE.gameObject.SetActive(false);
                    pressQ.gameObject.SetActive(false);
                    pressR.gameObject.SetActive(false);

                    if (Input.GetKeyDown("x"))
                    {
                        timer = 1.6f;
                        counter++;
                    }
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else if (timer <= 0f)
                    {
                        youLost = true;
                    }
                    break;
                case 3:
                    pressR.gameObject.SetActive(true);
                    pressE.gameObject.SetActive(false);
                    pressQ.gameObject.SetActive(false);
                    pressX.gameObject.SetActive(false);

                    if (Input.GetKeyDown("r"))
                    {
                        timer = 1.4f;
                        counter++;
                    }
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else if (timer <= 0f)
                    {
                        youLost = true;
                    }
                    break;
                case 4:
                    pressQ.gameObject.SetActive(true);
                    pressE.gameObject.SetActive(false);
                    pressR.gameObject.SetActive(false);
                    pressX.gameObject.SetActive(false);

                    if (Input.GetKeyDown("q"))
                    {
                        timer = 1.2f;
                        counter++;
                    }
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else if (timer <= 0f)
                    {
                        youLost = true;
                    }
                    break;
                case 5:
                    pressX.gameObject.SetActive(true);
                    pressE.gameObject.SetActive(false);
                    pressQ.gameObject.SetActive(false);
                    pressR.gameObject.SetActive(false);

                    if (Input.GetKeyDown("x"))
                    {
                        timer = 1f;
                        counter++;
                    }
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else if (timer <= 0f)
                    {
                        youLost = true;
                    }
                    break;
                case 6:
                    pressQ.gameObject.SetActive(true);
                    pressE.gameObject.SetActive(false);
                    pressR.gameObject.SetActive(false);
                    pressX.gameObject.SetActive(false);

                    if (Input.GetKeyDown("q"))
                    {
                        timer = 1f;
                        youWin = true;
                    }
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else if (timer <= 0f)
                    {
                        youLost = true;
                    }
                    break;
            }
        }
        else
        {
            pressE.gameObject.SetActive(false);
            pressQ.gameObject.SetActive(false);
            pressR.gameObject.SetActive(false);
            pressX.gameObject.SetActive(false);
            sliderTime.gameObject.SetActive(false);
        }


        if (youLost)
        {
            Debug.Log("HAS PERDIDO EL QTE");
            counter = 0;
            timer = 2f;
            youLost = false;
        }

        if (youWin)
        {
            brokenManAnimator.SetTrigger("PlayerWon");

            completed = true;
            Followers.followers++;

            pressE.gameObject.SetActive(false);
            pressQ.gameObject.SetActive(false);
            pressR.gameObject.SetActive(false);
            pressX.gameObject.SetActive(false);
            sliderTime.gameObject.SetActive(false);

            followScript.enabled = true;

            youWin = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inZone = false;
        }
    }
}
