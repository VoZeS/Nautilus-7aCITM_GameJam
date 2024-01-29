using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    public float tiempoLimite = 10.0f;
    private float tiempoRestante;

    public float duracionFadeOut = 2.0f;
    public UnityEngine.UI.Image pantallaFadeOut;

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
        StartCoroutine(FadeOutAndReload());
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

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

}
