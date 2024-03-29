using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject WASD;
    public GameObject spaceBar;

    private bool inJump;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        WASD.SetActive(true);
        spaceBar.SetActive(false);

        timer = 0;

        inJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer <= 4f && !inJump)
            WASD.SetActive(true);
        else
            WASD.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            WASD.SetActive(false);
            spaceBar.SetActive(true);

            inJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spaceBar.SetActive(false);
            inJump = false;

        }
    }
}
