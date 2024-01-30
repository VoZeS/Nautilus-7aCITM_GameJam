using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressNightmaresScript : MonoBehaviour
{
    private string keyNightmare1 = "Nightmare1";

    private int resultNightmare1; //0 win, 1 lose

    void Start()
    {
        // Recuperar el valor del bool desde PlayerPrefs
        resultNightmare1 = PlayerPrefs.GetInt(keyNightmare1, 0);

        // Hacer algo basado en el valor del bool
        if (resultNightmare1 == 0)
        {
            Debug.Log("El progreso bool es verdadero.");
            // Realiza acciones según tu lógica
        }
        else
        {
            Debug.Log("El progreso bool es falso.");
            // Realiza acciones según tu lógica
        }
    }

    void Update()
    {
        // Ejemplo: Cambiar el valor del bool cuando se presiona la tecla "Espacio"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            resultNightmare1 = 0;

            // Guardar el valor del bool en PlayerPrefs
            PlayerPrefs.SetInt(keyNightmare1, resultNightmare1);
            PlayerPrefs.Save();
        }

    }
}
