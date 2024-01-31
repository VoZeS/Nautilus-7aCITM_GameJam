using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickEMultipleTimes : MonoBehaviour
{
    [Header("UI")]
    public Slider sliderPress;

    [Header("Abucheador")]
    public Animator[] abucheadorAnimator;
    public Flee[] fleeScript;

    [Header("Abucheado")]
    public Follow followScript;
    public BoxCollider2D triggerCol;
    public Animator abucheadoAnimator;
    public GameObject cloud;


    private bool inZone;
    private float pressCharge;

    private void Start()
    {
        pressCharge = 0;
        sliderPress.gameObject.SetActive(false);

        for(int i = 0; i < fleeScript.Length; i++)
        {
            fleeScript[i].enabled = false;
        }

        followScript.enabled = false;

        cloud.SetActive(true);
    }

    private void Update()
    {
        if(inZone)
        {
            sliderPress.gameObject.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                if (pressCharge <= 1)
                {
                    pressCharge += 0.05f * Time.fixedDeltaTime;
                }
                   
                else
                    pressCharge = 1;
            }
            else
            {
                if (pressCharge > 0)
                {
                    pressCharge -= 0.001f * Time.fixedDeltaTime;
                    pressCharge = Mathf.Clamp01(pressCharge);
                }
                   
                else
                    pressCharge = 0;
            }
            //Debug.Log(pressCharge);
        }
        else
        {
            sliderPress.gameObject.SetActive(false);
            pressCharge = 0;
        }

        sliderPress.value = pressCharge;

        // CHALLENGE COMPLETED
        if(pressCharge >= 1)
        {
            abucheadoAnimator.SetTrigger("PlayerWon");

            cloud.SetActive(false);

            Followers.followers++;

            sliderPress.gameObject.SetActive(false);
            sliderPress.gameObject.SetActive(false);

            for (int i = 0; i < fleeScript.Length; i++)
            {
                abucheadorAnimator[i].SetTrigger("PlayerWins");
                fleeScript[i].enabled = true;
            }

            followScript.enabled = true;
            triggerCol.enabled = false;

            pressCharge = 0;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
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
