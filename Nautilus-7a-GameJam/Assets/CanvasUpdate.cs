using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasUpdate : MonoBehaviour
{
    // --------------------------------------------- REALITY RESULTS
    private int resultReality1; //0 win, 1 lose
    private int resultReality2; //0 win, 1 lose
    private int resultReality3; //0 win, 1 lose
    // ------------

    [Header("Reality UI")]
    public Image atrapa1;
    public Sprite atrapa1Off;
    public Sprite atrapa1On;

    public Image atrapa2;
    public Sprite atrapa2Off;
    public Sprite atrapa2On;

    public Image atrapa3;
    public Sprite atrapa3Off;
    public Sprite atrapa3On;

    // --------------------------------------------- PESADILLA 2 RESULTS
    private int littleGirlOn; //0 win, 1 lose
    private int brokenManOn; //0 win, 1 lose
    private int abusedManOn; //0 win, 1 lose

    [Header("Pesadilla 2 UI")]
    public Image redOrbImage;
    public Sprite redOrbOff;
    public Sprite redOrbOn;

    public Image blueOrbImage;
    public Sprite blueOrbOff;
    public Sprite blueOrbOn;

    public Image greenOrbImage;
    public Sprite greenOrbOff;
    public Sprite greenOrbOn;

    public Follow littleGirl;
    public Follow brokenMan;
    public Follow abusedMan;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Realidad1")
        {
            resultReality1 = 1;
            PlayerPrefs.SetInt("Realidad1", resultReality1);

            resultReality2 = 1;
            PlayerPrefs.SetInt("Realidad2", resultReality2);

            resultReality3 = 1;
            PlayerPrefs.SetInt("Realidad3", resultReality3);

            PlayerPrefs.Save();
        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateAtrapa();

        if(SceneManager.GetActiveScene().name == "Pesadilla2")
        {
            UpdateOrbs();
        }
    }

    private void UpdateOrbs()
    {
        if(littleGirl.enabled)
        {
            littleGirlOn = 0;
        }
        else
        {
            littleGirlOn = 1;
        }
        
        if(brokenMan.enabled)
        {
            brokenManOn = 0;
        }
        else
        {
            brokenManOn = 1;
        }
        
        if(abusedMan.enabled)
        {
            abusedManOn = 0;
        }
        else
        {
            abusedManOn = 1;
        }

        switch (littleGirlOn)
        {
            case 0:
                redOrbImage.sprite = redOrbOn;
                break;
            case 1:
                redOrbImage.sprite = redOrbOff;
                break;
            default:
                redOrbImage.sprite = redOrbOff;
                break;
        }

        switch (brokenManOn)
        {
            case 0:
                blueOrbImage.sprite = blueOrbOn;
                break;
            case 1:
                blueOrbImage.sprite = blueOrbOff;
                break;
            default:
                blueOrbImage.sprite = blueOrbOff;
                break;
        }

        switch (abusedManOn)
        {
            case 0:
                greenOrbImage.sprite = greenOrbOn;
                break;
            case 1:
                greenOrbImage.sprite = greenOrbOff;
                break;
            default:
                greenOrbImage.sprite = greenOrbOff;
                break;
        }
    }

    private void UpdateAtrapa()
    {
        resultReality1 = PlayerPrefs.GetInt("Realidad1", resultReality1);
        resultReality2 = PlayerPrefs.GetInt("Realidad2", resultReality2);
        resultReality3 = PlayerPrefs.GetInt("Realidad3", resultReality3);


        switch (resultReality1)
        {
            case 0:
                atrapa1.sprite = atrapa1On;
                break;
            case 1:
                atrapa1.sprite = atrapa1Off;
                break;
            default:
                atrapa1.sprite = atrapa1Off;
                break;
        }

        switch (resultReality2)
        {
            case 0:
                atrapa2.sprite = atrapa2On;
                break;
            case 1:
                atrapa2.sprite = atrapa2Off;
                break;
            default:
                atrapa2.sprite = atrapa2Off;
                break;
        }

        switch (resultReality3)
        {
            case 0:
                atrapa3.sprite = atrapa3On;
                break;
            case 1:
                atrapa3.sprite = atrapa3Off;
                break;
            default:
                atrapa3.sprite = atrapa3Off;
                break;
        }
    }
}
