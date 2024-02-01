using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour
{
    public Image image;
    public float fadeDuration;

    public GameObject mainMenu;
    public GameObject firstCutscene;

    public static bool play;

    private void Start()
    {
        play = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            StartFadeOut();
        }
    }

    public void StartFadeOut()
    {
        // Inicia la rutina de fade out
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        float elapsedTime = 0f;
        Color startColor = image.color;

        while (elapsedTime < fadeDuration)
        {
            // Calcula el nuevo color con opacidad reducida
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 1f, elapsedTime / fadeDuration));

            // Aplica el nuevo color a la imagen
            image.color = newColor;

            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 1f);

        mainMenu.SetActive(false);

        firstCutscene.SetActive(true);

        play = true;

    }

}
