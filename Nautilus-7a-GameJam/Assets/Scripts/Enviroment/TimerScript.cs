using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{

    public float tiempoLimite = 10.0f;
    private float tiempoRestante;

    // Start is called before the first frame update
    void Start()
    {
        tiempoRestante = tiempoLimite;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0)
        {
            PerderJuego();
        }
    }

    void PerderJuego()
    {
        Debug.Log("Has perdido, se acabó el tiempo!!!");

        Invoke("ReiniciarNivel", 3.0f);
    }

    void ReiniciarNivel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }
}
