using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    public float tiempoLimite = 10.0f;
    private float tiempoRestante;

    public float duracionFadeOut = 4.0f;
    public float duracionFadeIn = 2.0f;
    public UnityEngine.UI.Image pantallaFadeOut;
    private bool alive;

    // Start is called before the first frame update
    void Start()
    {

        tiempoRestante = tiempoLimite;


        StartCoroutine(FadeIn());

        Invoke("IniciarContador", duracionFadeIn);

        alive = true;

    }

    void IniciarContador()
    {
        tiempoRestante = tiempoLimite;
    }


    // Update is called once per frame
    void Update()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0 && alive)
        {
            PerderJuego();
            alive = false;
        }
    }

    IEnumerator FadeIn()
    {
        float tiempoInicioFade = Time.time;

        while (Time.time - tiempoInicioFade < duracionFadeIn)
        {
            float alpha = Mathf.Lerp(1f, 0f, (Time.time - tiempoInicioFade) / duracionFadeIn);
            pantallaFadeOut.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }

    void PerderJuego()
    {
        Debug.Log("Has perdido, se acabó el tiempo!!!");


        StartCoroutine(FadeOutAndReload());
    }

    IEnumerator FadeOutAndReload()
    {
        float tiempoInicioFade = Time.time;

        while (Time.time - tiempoInicioFade < duracionFadeOut)
        {
            float alpha = Mathf.Lerp(0f, 1f, (Time.time - tiempoInicioFade) / duracionFadeOut);
            pantallaFadeOut.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        if(SceneManager.GetActiveScene().name == "Pesadilla1" 
            || SceneManager.GetActiveScene().name == "Pesadilla2"
            || SceneManager.GetActiveScene().name == "Pesadilla3")
        {
            GoToNextLevel();
        }
        else
        {
            // Recarga la escena después del fade out
            ReiniciarNivel();
        }
          
    }

    void ReiniciarNivel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    void GoToNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Pesadilla1")
            SceneManager.LoadScene("Realidad2");
        else if (SceneManager.GetActiveScene().name == "Pesadilla2")
            SceneManager.LoadScene("Realidad3");
        else if (SceneManager.GetActiveScene().name == "Pesadilla3")
            SceneManager.LoadScene("FinalCutscene");

    }


    

}
