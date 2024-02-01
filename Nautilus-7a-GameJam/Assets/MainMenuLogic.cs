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
    public GameObject settingsMenu;

    public GameObject firstCutscene;
         

    public static bool play;

    public bool settings;

    private void Start()
    {
        image.enabled = false;
        play = false;
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            StartFadeOut();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            settingsMenu.SetActive(false);
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

        image.enabled = true;
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

    public void ShowOptions()
    {
        if (settingsMenu.active==false)
        {
            settingsMenu.SetActive(true);
        }
        else if(settingsMenu.active == true)
        {
            settingsMenu.SetActive(false);
        }

    }

}
