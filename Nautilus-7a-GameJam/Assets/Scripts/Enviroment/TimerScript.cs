//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    GameObject dragon;
    DragonBox scriptTotalDragon;

    GameObject player;
    WellInteraction scriptTotalPlayer;

    public float tiempoLimite = 10.0f;
    private float tiempoRestante;
    private float tiempoTranscurrido = 0;

    public float duracionFadeOut = 4.0f;
    public float duracionFadeIn = 2.0f;
    public UnityEngine.UI.Image pantallaFadeOut;
    private bool alive;

    // --------------------------------------------- NIGHTMARES RESULTS
    private int resultNightmare1; //0 win, 1 lose
    private int resultNightmare2; //0 win, 1 lose
    private int resultNightmare3; //0 win, 1 lose
    // ------------

    public Animator playerAnimator;

    public Sprite[] spritesReloj;  // Array de sprites para las horas del reloj
    public Image imagenReloj;  // Componente Image donde mostrar el sprite
    private bool ultimoSpriteAlcanzado = false;

    public AudioSource dyeSound;
    public AudioSource clockTickingSound;

    public ChangeSceneTrigger finalTriggerScript;

    // Start is called before the first frame update
    void Start()
    {
        dragon = GameObject.Find("BagRoof");

        if (SceneManager.GetActiveScene().name == "Pesadilla1")
            scriptTotalDragon = dragon.GetComponent<DragonBox>();

        player = GameObject.Find("InteractZone");

        if (SceneManager.GetActiveScene().name == "Pesadilla1")
            scriptTotalPlayer = player.GetComponent<WellInteraction>();

        tiempoRestante = tiempoLimite;

        StartCoroutine(FadeIn());

        Invoke("IniciarContador", duracionFadeIn);


        alive = true;

        // We set it as win, but if it loses will be set to 1
        resultNightmare1 = 0;
        PlayerPrefs.SetInt("Nightmare1", resultNightmare1);

        resultNightmare2 = 0;
        PlayerPrefs.SetInt("Nightmare2", resultNightmare2);

        resultNightmare3 = 0;
        PlayerPrefs.SetInt("Nightmare3", resultNightmare3);


        PlayerPrefs.Save();
    }

    void IniciarContador()
    {
        tiempoRestante = tiempoLimite;
        tiempoTranscurrido = 0;

    }


    // Update is called once per frame
    void Update()
    {
        tiempoRestante -= Time.deltaTime;

        ActualizarSpriteReloj();

        // REALIDADES
        if(finalTriggerScript != null)
        {
            if (alive && ultimoSpriteAlcanzado && !finalTriggerScript.isSleeping)
            {
                if (scriptTotalDragon == null)
                {
                    playerAnimator.SetTrigger("Death");
                    dyeSound.Play();


                    alive = false;

                }

                if (scriptTotalPlayer != null)
                {
                    if (ultimoSpriteAlcanzado)
                    {
                        
                        playerAnimator.SetTrigger("Death");
                        dyeSound.Play();
                        alive = false;

                    }
                }

            }
        }
        else if(scriptTotalPlayer != null) // SUE�OS
        {
            if (alive && ultimoSpriteAlcanzado)
            {
                if (scriptTotalDragon == null)
                {
                    playerAnimator.SetTrigger("Death");
                    dyeSound.Play();


                    alive = false;

                }

                if (scriptTotalPlayer != null)
                {
                    if (ultimoSpriteAlcanzado)
                    {

                        playerAnimator.SetTrigger("Death");
                        dyeSound.Play();
                        alive = false;

                    }
                    
                    //alive = false;
                }

            }

            if (scriptTotalPlayer.monedasPlayerTotales.Count >= 10)
            {
                GanarJuego();
                alive = false;
                ultimoSpriteAlcanzado = true;  // Marcar que se alcanz� el �ltimo sprite

            }
        }
        else
        {
            if (alive && ultimoSpriteAlcanzado)
            {
                if (scriptTotalDragon == null)
                {
                    playerAnimator.SetTrigger("Death");
                    dyeSound.Play();


                    alive = false;

                }

                if (scriptTotalPlayer != null)
                {
                    if (ultimoSpriteAlcanzado)
                    {

                        playerAnimator.SetTrigger("Death");
                        dyeSound.Play();
                        alive = false;

                    }

                    //alive = false;
                }

            }
        }
        

        if (PlatfromCharacter.dead || IsoCharacter.dead)
        {
            PerderJuego();
            PlatfromCharacter.dead = false;
            IsoCharacter.dead = false;
            ultimoSpriteAlcanzado = true;  // Marcar que se alcanz� el �ltimo sprite
        }
    }

    void ActualizarSpriteReloj()
    {
        tiempoTranscurrido += Time.deltaTime;

        float fraccionTiempo = tiempoTranscurrido / tiempoLimite;

        int indiceSprite = Mathf.FloorToInt(fraccionTiempo * spritesReloj.Length);

        indiceSprite = Mathf.Clamp(indiceSprite, 0, spritesReloj.Length - 1);

        if (imagenReloj != null)
        {
            imagenReloj.sprite = spritesReloj[indiceSprite];

            if (indiceSprite==5)
            {
                if (clockTickingSound != null)
                {
                    clockTickingSound.Play();
                }
                   
            }


            // Si se alcanza el �ltimo sprite, marcar la variable correspondiente
            if (indiceSprite == spritesReloj.Length - 1)
            {
                ultimoSpriteAlcanzado = true;
            }
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

    public void PerderJuego()
    {
        Debug.Log("Has perdido, se acab� el tiempo!!!");

        // ------------------------------------------------------- NIGHTMARES RESULTS
        if (SceneManager.GetActiveScene().name == "Pesadilla1")
        {
            Debug.Log("Has perdido la pesadilla 1");
            resultNightmare1 = 1; //lose
            PlayerPrefs.SetInt("Nightmare1", resultNightmare1);
            PlayerPrefs.Save();
        }
        else if (SceneManager.GetActiveScene().name == "Pesadilla2")
        {
            Debug.Log("Has perdido la pesadilla 2");
            resultNightmare2 = 1; //lose
            PlayerPrefs.SetInt("Nightmare2", resultNightmare2);
            PlayerPrefs.Save();
        }
        else if (SceneManager.GetActiveScene().name == "Pesadilla3")
        {
            Debug.Log("Has perdido la pesadilla 3");
            resultNightmare3 = 1; //lose
            PlayerPrefs.SetInt("Nightmare3", resultNightmare3);
            PlayerPrefs.Save();
        }
        // -----------------------------------

        StartCoroutine(FadeOutAndReload());
    }

    public void GanarJuego()
    {
        Debug.Log("Has ganado!!!");

        // ------------------------------------------------------- NIGHTMARES RESULTS
        if (SceneManager.GetActiveScene().name == "Pesadilla1")
        {
            Debug.Log("Has ganado la pesadilla 1");
            resultNightmare1 = 0; //win
            PlayerPrefs.SetInt("Nightmare1", resultNightmare1);
            PlayerPrefs.Save();
        }
        else if (SceneManager.GetActiveScene().name == "Pesadilla2")
        {
            Debug.Log("Has ganado la pesadilla 2");
            resultNightmare2 = 0; //win
            PlayerPrefs.SetInt("Nightmare2", resultNightmare2);
            PlayerPrefs.Save();
        }
        else if (SceneManager.GetActiveScene().name == "Pesadilla3")
        {
            Debug.Log("Has ganado la pesadilla 3");
            resultNightmare3 = 0; //win
            PlayerPrefs.SetInt("Nightmare3", resultNightmare3);
            PlayerPrefs.Save();
        }
        // -----------------------------------

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

        if (SceneManager.GetActiveScene().name == "Pesadilla1"
            || SceneManager.GetActiveScene().name == "Pesadilla2"
            || SceneManager.GetActiveScene().name == "Pesadilla3")
        {
            GoToNextLevel();
        }
        else
        {
            // Recarga la escena despu�s del fade out
            ReiniciarNivel();
        }

    }

    void ReiniciarNivel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void GoToNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Pesadilla1")
            SceneManager.LoadScene("Realidad2");
        else if (SceneManager.GetActiveScene().name == "Pesadilla2")
            SceneManager.LoadScene("Realidad3");
        else if (SceneManager.GetActiveScene().name == "Pesadilla3")
        {
            if(resultNightmare3 == 0)
                SceneManager.LoadScene("GoodFinalCutscene");
            else if (resultNightmare3 == 1)
                SceneManager.LoadScene("BadFinalCutscene");
        }

    }




}
