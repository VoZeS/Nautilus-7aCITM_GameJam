using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doppelganger : MonoBehaviour
{
    [Header("Character")]
    public int characterScale = 5;

    [Header("Movement")]
    public GameObject player;
    public Vector3 offset;

    static public int orientation; // 0 left, 1 right

    private bool hitted;

    // Start is called before the first frame update
    void Start()
    {
        orientation = 1;
    }
    private void Update()
    {
        UpdateOrientation();

        if(hitted)
        {
            Debug.Log("HAS DERROTADO A TU DOPPELGANGER");

            SceneManager.LoadScene("FinalCutscene");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bitter")
        {
            hitted = true;
        }
    }
}
