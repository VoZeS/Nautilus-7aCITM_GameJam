using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        PlayerPrefs.GetInt("Reality1", resultReality1);
        PlayerPrefs.GetInt("Reality2", resultReality2);
        PlayerPrefs.GetInt("Reality3", resultReality3);
    }

    // Update is called once per frame
    void Update()
    {
        switch(resultReality1)
        {
            case 0:
                atrapa1.sprite = atrapa1Off;
                break;
            case 1:
                atrapa1.sprite = atrapa1On;
                break;
            default:
                atrapa1.sprite = atrapa1Off;
                break;
        }

        switch(resultReality2)
        {
            case 0:
                atrapa2.sprite = atrapa2Off;
                break;
            case 1:
                atrapa2.sprite = atrapa2On;
                break;
            default:
                atrapa2.sprite = atrapa2Off;
                break;
        }

        switch(resultReality3)
        {
            case 0:
                atrapa3.sprite = atrapa3Off;
                break;
            case 1:
                atrapa3.sprite = atrapa3On;
                break;
            default:
                atrapa3.sprite = atrapa3Off;
                break;
        }
    }
}
