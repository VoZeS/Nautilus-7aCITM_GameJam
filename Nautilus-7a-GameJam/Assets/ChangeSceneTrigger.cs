using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneTrigger : MonoBehaviour
{
    public float duracionFadeOut = 4.0f;
    public UnityEngine.UI.Image pantallaFadeOut;

    public Animator bedAnimator;


    private bool isInTrigger;
    // Start is called before the first frame update
    void Start()
    {
        isInTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            bedAnimator.SetTrigger("Sleep");
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
