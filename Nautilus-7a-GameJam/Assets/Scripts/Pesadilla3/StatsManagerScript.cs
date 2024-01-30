using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManagerScript : MonoBehaviour
{
    private int resultNightmare1; // 0 win, 1 lose
    //private int resultNightmare2; // 0 win, 1 lose

    public TilesMovementPlayer playerScript;
    public TilesMovementDoppelganger doppelScript;

    // Start is called before the first frame update
    void Start()
    {
        resultNightmare1 = PlayerPrefs.GetInt("Nightmare1");
        //resultNightmare2 = PlayerPrefs.GetInt("Nightmare2");

        if (resultNightmare1 == 1)
        {
            playerScript.velocidadMovimiento *= 0.5f;
            doppelScript.velocidadMovimiento *= 0.5f;
        }

        Debug.Log(Followers.followers);

        switch (Followers.followers)
        {
            case 0:
                HitsNightmare3.lives = 2;
                break;
            case 1:
                HitsNightmare3.lives = 3;
                break;
            case 2:
                HitsNightmare3.lives = 4;
                break;
            case 3:
                HitsNightmare3.lives = 5;
                break;
            default:
                HitsNightmare3.lives = 2;
                break;
        }
    }

}
