using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private bool winNightmare2;

    public TimerScript timerScript;

    public Collider2D[] NPCsColliders;
    public Collider2D[] limitsColliders;

    // Start is called before the first frame update
    void Start()
    {
        winNightmare2 = false;

        for(int i = 0; i < limitsColliders.Length; i++)
        {
            for(int j = 0; j < NPCsColliders.Length; j++)
            {
                Physics2D.IgnoreCollision(NPCsColliders[j], limitsColliders[i], true);

            }
        }
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
