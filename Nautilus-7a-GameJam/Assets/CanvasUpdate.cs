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

    [Header("UI")]
    public Image atrapa1;
    public Sprite atrapa1Off;
    public Sprite atrapa1On;

    public Image atrapa2;
    public Sprite atrapa2Off;
    public Sprite atrapa2On;

    public Image atrapa3;
    public Sprite atrapa3Off;
    public Sprite atrapa3On;

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
