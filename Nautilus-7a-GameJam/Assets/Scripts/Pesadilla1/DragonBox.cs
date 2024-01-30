using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonBox : MonoBehaviour
{

    GameObject dragon;
    Dragon scriptLista;


    private void Start()
    {
        dragon = GameObject.Find("DragonCoinHand");
        scriptLista = dragon.GetComponent<Dragon>();

    }

    private void Update()
    {

    }

    void DropCoins()
    {
        for (int i = 0; i < scriptLista.monedasDragonRecogidas.Count; i++)
        {
            scriptLista.monedasDragonRecogidas[i].gameObject.SetActive(false);

            Debug.Log("Monedas puestas");
        }
        scriptLista.monedasDragonRecogidas.Clear();
        Debug.Log("Lista Limpia");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Dragon")
        {
            DropCoins();
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DropCoins();
    }

}

