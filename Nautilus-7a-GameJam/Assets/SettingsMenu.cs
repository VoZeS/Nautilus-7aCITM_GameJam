using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Toggle toggleFullScreen;
    public Slider sliderVolumen;
    public AudioListener audioListener;
    

    //public string groupName = "NombreDelGrupo";  // Reemplaza con el nombre del grupo en tu mezclador

    void Start()
    {
        // Configuración inicial
        toggleFullScreen.isOn = Screen.fullScreen;
        sliderVolumen.value = PlayerPrefs.GetFloat("Volumen", 1.0f); // Recupera el volumen guardado (si existe)
        AdjustVolume();

    }

    public void Update()
    {
        ToggleFullScreen();

        AdjustVolume();

    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = toggleFullScreen.isOn;
    }

    public void AdjustVolume()
    {
        AudioListener.volume = sliderVolumen.value;
    }
    

}
