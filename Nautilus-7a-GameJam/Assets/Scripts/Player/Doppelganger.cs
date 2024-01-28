using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doppelganger : MonoBehaviour
{
    [Header("Character")]
    public int characterScale = 5;

    [Header("Movement")]
    public GameObject player;
    public Vector3 offset;

    private Rigidbody2D rb;
    static public int orientation; // 0 left, 1 right

    private bool isColliding = false;

    private bool hitted;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orientation = 1;
    }
    private void Update()
    {
        UpdateOrientation();

        if(hitted)
        {
            Debug.Log("HAS DERROTADO A TU DOPPELGANGER");

            // TODO: WIN GAME
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bitter")
        {
            hitted = true;
        }
    }
}
