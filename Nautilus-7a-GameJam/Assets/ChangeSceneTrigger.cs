using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneTrigger : MonoBehaviour
{
    public float duracionFadeOut = 4.0f;
    public UnityEngine.UI.Image pantallaFadeOut;

    public Animator bedAnimator;

    // --------------------------------------------- REALITY RESULTS
    private int resultReality1; //0 win, 1 lose
    private int resultReality2; //0 win, 1 lose
    private int resultReality3; //0 win, 1 lose
    // ------------

    private bool isInTrigger;
    // Start is called before the first frame update
    void Start()
    {
        isInTrigger = false;

        // We set it as lose, but if it wins will be set to 0
        //resultReality1 = 1;
        //PlayerPrefs.SetInt("Realidad1", resultReality1);

        //resultReality2 = 1;
        //PlayerPrefs.SetInt("Realidad2", resultReality2);

        //resultReality3 = 1;
        //PlayerPrefs.SetInt("Realidad3", resultReality3);

        //PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            bedAnimator.SetTrigger("Sleep");

            // ------------------------------------------------------- REALITY RESULTS
            if (SceneManager.GetActiveScene().name == "Realidad1")
            {
                Debug.Log("Has ganado la realidad 1");
                resultReality1 = 0; //win
                PlayerPrefs.SetInt("Realidad1", resultReality1);
                PlayerPrefs.Save();
            }
            else if (SceneManager.GetActiveScene().name == "Realidad2")
            {
                Debug.Log("Has ganado la realidad 2");
                resultReality2 = 0; //win
                PlayerPrefs.SetInt("Realidad2", resultReality2);
                PlayerPrefs.Save();
            }
            else if (SceneManager.GetActiveScene().name == "Realidad3")
            {
                Debug.Log("Has ganado la realidad 3");
                resultReality3 = 0; //win
                PlayerPrefs.SetInt("Realidad3", resultReality1);
                PlayerPrefs.Save();
            }
            // -----------------------------------

            StartCoroutine(FadeOut());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInTrigger = false;
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

        // Recarga la escena después del fade out
        SiguienteEscena();
    }

    void SiguienteEscena()
    {

        int indiceEscenaActual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // Carga la siguiente escena en la lista
        UnityEngine.SceneManagement.SceneManager.LoadScene(indiceEscenaActual + 1);

    }

}
