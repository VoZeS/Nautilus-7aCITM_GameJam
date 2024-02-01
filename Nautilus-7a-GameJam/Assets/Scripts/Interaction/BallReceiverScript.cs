using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallReceiverScript : MonoBehaviour
{
    public GameObject ball;

    private bool readyToGiveBall;

    [Header("UI")]
    public GameObject interactSprite;
    public GameObject cloudGirl;

    [Header("Girl")]
    public GameObject girl;
    private Animator girlAnimator;
    private Follow followScript;

    void Start()
    {
        interactSprite.SetActive(false);
        girlAnimator = girl.GetComponent<Animator>();
        followScript = girl.GetComponent<Follow>();
        followScript.enabled = false;

        cloudGirl.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToGiveBall)
        {
            interactSprite.SetActive(true);

            if (Input.GetKey("e"))
            {
                Interact();
            }
        }
    }

    void Interact()
    {
        Debug.Log("Has entregado la pelota!");

        girlAnimator.SetTrigger("HasBall");
        ball.SetActive(false);
        cloudGirl.SetActive(false);
        followScript.enabled = true;

        Followers.followers++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            readyToGiveBall = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            readyToGiveBall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            readyToGiveBall = false;
            interactSprite.SetActive(false);
        }
    }
}
