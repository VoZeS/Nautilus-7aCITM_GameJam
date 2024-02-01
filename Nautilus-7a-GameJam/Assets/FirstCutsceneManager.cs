using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstCutsceneManager : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 2f;

    private float timer;

    public Animator firstVAnimator;
    public Animator secondVAnimator;
    public Animator thirdVAnimator;
    public Animator fourthVAnimator;
    public Animator fifthVAnimator;

    public AudioSource swoosh1;
    public AudioSource swoosh2;
    public AudioSource swoosh3;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if(MainMenuLogic.play)
        {
            timer += Time.deltaTime;

            if (timer >= 2f && timer < 4f)
            {
                firstVAnimator.SetTrigger("FirstV");

            }
            else if (timer >= 4f && timer < 6f)
            {
                secondVAnimator.SetTrigger("SecondV");

            }
            else if (timer >= 6f && timer < 8f)
            {
                thirdVAnimator.SetTrigger("ThirdV");

            }
            else if (timer >= 8f && timer < 9f)
            {
                fourthVAnimator.SetTrigger("FourthV");

            }
            else if (timer >= 9f && timer < 11f)
            {
                fifthVAnimator.SetTrigger("FifthV");

            }
            else if (timer >= 11f)
            {
                StartFadeOut();
            }
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
        SceneManager.LoadScene("Realidad1");

    }

}
