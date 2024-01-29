using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followers : MonoBehaviour
{
    static public int followers;

    public Flee fleeOldManScript;

    // Start is called before the first frame update
    void Start()
    {
        followers = 0;
    }

    // Update is called once per frame
    void Update()
    {

        switch(followers)
        {
            case 0:
                fleeOldManScript.distanciaUmbral = 5f;
                fleeOldManScript.velocidadAlejamiento = 3f;
                break;
            case 1:
                fleeOldManScript.distanciaUmbral = 3f;
                fleeOldManScript.velocidadAlejamiento = 2f;
                break;
            case 2:
                fleeOldManScript.distanciaUmbral = 2f;
                fleeOldManScript.velocidadAlejamiento = 2f;
                break;
            case 3:
                fleeOldManScript.distanciaUmbral = 0f;
                fleeOldManScript.velocidadAlejamiento = 0f;
                break;
            default:
                fleeOldManScript.distanciaUmbral = 5f;
                fleeOldManScript.velocidadAlejamiento = 3f;
                break;
        }    
    }
}
