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
    public AudioMixer audioMixer;  // Agrega una referencia al mezclador de audio

    //public string groupName = "NombreDelGrupo";  // Reemplaza con el nombre del grupo en tu mezclador

    void Start()
    {
        // Configuración inicial
        toggleFullScreen.isOn = Screen.fullScreen;
        sliderVolumen.value = PlayerPrefs.GetFloat("Volumen", 1.0f); // Recupera el volumen guardado (si existe)
        AdjustBackgroundVolume();
        AdjustVFXVolume();
    }

    public void Update()
    {
        ToggleFullScreen();
        AdjustBackgroundVolume();
        AdjustVFXVolume();


    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = toggleFullScreen.isOn;
    }

    public void AdjustBackgroundVolume()
    {
        float volumen = sliderVolumen.value;

        // Guarda el volumen actual en PlayerPrefs
        PlayerPrefs.SetFloat("Volumen", volumen);

        // Ajusta el parámetro del grupo en el mezclador de audio
        audioMixer.SetFloat("BackgroundVol", Mathf.Log10(volumen) * 20);
        if (volumen == 0)
        {
            audioMixer.SetFloat("BackgroundVol", Mathf.Log10(0.1f) * 20);
        }
    }
    public void AdjustVFXVolume()
    {
        float volumen = sliderVolumen.value;

        // Guarda el volumen actual en PlayerPrefs
        PlayerPrefs.SetFloat("Volumen", volumen);

        // Ajusta el parámetro del grupo en el mezclador de audio
        audioMixer.SetFloat("VfxVolumen", Mathf.Log10(volumen) * 20);
        if (volumen == 0)
        {
            audioMixer.SetFloat("VfxVolumen", Mathf.Log10(0.1f) * 20);
        }
    }

}
