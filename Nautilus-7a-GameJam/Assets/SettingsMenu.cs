using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Toggle toggleFullScreen;
    public Slider sliderVolumen;
    public AudioListener audioListener;

    void Start()
    {
        // Configuración inicial
        toggleFullScreen.isOn = Screen.fullScreen;
        sliderVolumen.value = AudioListener.volume;
    }

    public void Update()
    {
        AdjustVolume();
        ToggleFullScreen();
    }

    public void ToggleFullScreen()
    {
        if(toggleFullScreen.isOn)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
        
    }

    public void AdjustVolume()
    {
        AudioListener.volume = sliderVolumen.value;
    }
}
