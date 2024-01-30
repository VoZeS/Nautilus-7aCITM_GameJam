using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class WallScript : MonoBehaviour
{
    public GameObject wall;
    public float fadeDuration = 0.5f;

    private SpriteRenderer wallSpriteRenderer;
    private bool fading = false;

    private void Start()
    {
        wallSpriteRenderer = wall.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!fading)
        {
            StartCoroutine(FadeOut());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!fading)
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeOut()
    {
        fading = true;
        float elapsedTime = 0f;

        Debug.Log("Fading OUT");

        while (elapsedTime < fadeDuration)
        {
            wallSpriteRenderer.color = new Color(0.7647059f, 0.7607843f, 0.7607843f, Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        wallSpriteRenderer.color = new Color(0.7647059f, 0.7607843f, 0.7607843f, 0f);
        wall.SetActive(false);
        fading = false;

    }

    private IEnumerator FadeIn()
    {
        fading = true;

        Debug.Log("Fading IN");

        wall.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            wallSpriteRenderer.color = new Color(0.7647059f, 0.7607843f, 0.7607843f, Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        wallSpriteRenderer.color = new Color(0.7647059f, 0.7607843f, 0.7607843f, 1f);
        fading = false;
    }


}

