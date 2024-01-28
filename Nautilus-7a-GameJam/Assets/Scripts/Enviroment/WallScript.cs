using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallScript : MonoBehaviour
{

    public GameObject wall;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        wall.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        wall.SetActive(true);
    }

}
