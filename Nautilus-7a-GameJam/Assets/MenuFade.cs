using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFade : MonoBehaviour
{

    public float duracionFadeOut = 2.0f;
    public float duracionFadeIn = 2.0f;
    public UnityEngine.UI.Image pantallaFadeOut;

    public float tiempoLimite = 2.0f;
    private float tiempo;
    
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(FadeIn());

    }

   

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;


        if (tiempo>=tiempoLimite)
            StartCoroutine(FadeOut());

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


    IEnumerator FadeOut()
    {
        float tiempoInicioFade = Time.time;

        while (Time.time - tiempoInicioFade < duracionFadeOut)
        {
            float alpha = Mathf.Lerp(0f, 1f, (Time.time - tiempoInicioFade) / duracionFadeOut);
            pantallaFadeOut.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        SceneManager.LoadScene("MainMenu");

    }


}
