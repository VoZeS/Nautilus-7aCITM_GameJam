using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    public Sprite[] numerosSprites; // Arreglo que contiene las im�genes de los n�meros del 0 al 9
    public Image puntuacionMaxImage;
    public Image puntuacionImage;
    public Image puntuacionMax1Image;
    public Image puntuacion1Image;



    void Start()
    {
        // Establecer la puntuaci�n inicial
        ActualizarPuntuacion(0, 3);
        

    }

    public void ActualizarPuntuacion(int puntuacionActual, int puntuacionMaxima)
    {

        if (puntuacionActual == 10)
        {
            puntuacion1Image.enabled = true;
            puntuacionImage.sprite = numerosSprites[0];

        }
        
        if (puntuacionMaxima == 10)
        {
            puntuacionMax1Image.enabled = true;
            puntuacionMaxImage.sprite = numerosSprites[0];
        }

        // Asegurarse de que la puntuaci�n actual no sea mayor que la puntuaci�n m�xima
        //puntuacionActual = Mathf.Min(puntuacionActual, puntuacionMaxima);

        
        if (puntuacionActual != 10 )
        {
            // Obtener el sprite correspondiente al n�mero actual
            Sprite spriteNumero = numerosSprites[puntuacionActual];
            // Asignar el sprite al objeto Image
            puntuacionImage.sprite = spriteNumero;

            
            puntuacion1Image.enabled=false;
        }

        if (puntuacionMaxima != 10)
        {
            // Obtener el sprite correspondiente al n�mero maximo
            Sprite spriteNumero2 = numerosSprites[puntuacionMaxima];
            puntuacionMaxImage.sprite = spriteNumero2;
            puntuacionMax1Image.enabled = false;
        }

      

        
       
       

    }

}
