using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private bool winNightmare2;

    public TimerScript timerScript;

    // Start is called before the first frame update
    void Start()
    {
        winNightmare2 = false;
    }

    private void Update()
    {
        if(winNightmare2)
        {
            timerScript.GanarJuego();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            winNightmare2 = true;
        }
    }
}
