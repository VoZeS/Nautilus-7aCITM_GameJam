using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Caida : MonoBehaviour
{

    public float duracionFadeOut = 4.0f;
    public UnityEngine.UI.Image pantallaFadeOut;
    

    // Update is called once per frame
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

        // Recarga la escena después del fade out
        ReiniciarNivel();
    }

    void ReiniciarNivel()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }
}
