using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StatsManagerScript : MonoBehaviour
{
    private int resultNightmare1; // 0 win, 1 lose
    //private int resultNightmare2; // 0 win, 1 lose

    public TilesMovementPlayer playerScript;
    public TilesMovementDoppelganger doppelScript;

    public Light2D globalLight;

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
                globalLight.color = new Color(0, 0, 0);
                break;
            case 1:
                globalLight.color = new Color(34f / 255f, 34f / 255f, 34f / 255f);

                break;
            case 2:
                globalLight.color = new Color(56f / 255f, 56f / 255f, 56f / 255f);

                break;
            case 3:
                globalLight.color = new Color(84f / 255f, 84f / 255f, 84f / 255f);

                break;
            default:
                globalLight.color = new Color(0, 0, 0);

                break;
        }
    }

}
