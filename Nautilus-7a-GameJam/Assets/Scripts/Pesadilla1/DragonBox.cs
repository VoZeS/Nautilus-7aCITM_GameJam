using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonBox : MonoBehaviour
{
    public List<Transform> monedasDragonTotales = new List<Transform>();
    GameObject dragon;
    Dragon scriptLista;


    private void Start()
    {
        dragon = GameObject.Find("DragonCoinHand");
        scriptLista = dragon.GetComponent<Dragon>();

        scriptLista.monedasDragonRecogidas.Clear();
        Debug.Log("Lista Dragon Limpia");

    }

    private void Update()
    {

    }

    void DropCoins()
    {
        for (int i = 0; i < scriptLista.monedasDragonRecogidas.Count; i++)
        {
            monedasDragonTotales.Add(scriptLista.monedasDragonRecogidas[i]);
            scriptLista.monedasDragonRecogidas[i].gameObject.SetActive(false);
            scriptLista.monedasDragonRecogidas.Clear();

            Debug.Log("Monedas dragon puestas");
        }

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

