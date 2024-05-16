// Author: Rubén
// Author: Javi, parte de FPS

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;



public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer mainMixer;

    [Header("FPS")]
    public UnityEngine.UI.Toggle fpsToggle;
    public UnityEngine.UI.Slider fpsSlider;
    private int[] fpsValues = { 30, 60, 120, -1 }; // -1 para 

    [Header("Resoluciones")]
    public TMP_Dropdown resolutionDropdown;
    List<Resolution> resolutions = new List<Resolution>();
    private int width, height;
    private RefreshRate refreshRate;

    [Header("Slider Música")]
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Slider effectsSlider;


    private void Start()
    {
        getResolutions();
        musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue", 1f);
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsSliderValue", 1f);

        // Configura el slider de FPS
        fpsSlider.minValue = 0;
        fpsSlider.maxValue = fpsValues.Length - 1;
        fpsSlider.wholeNumbers = true;
        //fpsSlider.onValueChanged.AddListener();
        fpsSlider.value = PlayerPrefs.GetFloat("FpsSliderValue", fpsSlider.maxValue);

        // Cargar valor de fpsToggle. No se puede hacer setBool, asi que trabajo con enteros
        int valorFpsToggle = PlayerPrefs.GetInt("FpsToggleValue", 1);
        if(valorFpsToggle == 1)
        {
            fpsToggle.isOn = true;
        }
        else
        {
            fpsToggle.isOn = false;
        }
    }

    //RESOLUCIONES
    public void getResolutions()
    {
        width = Screen.currentResolution.width;
        height = Screen.currentResolution.height;
        refreshRate = Screen.currentResolution.refreshRateRatio;

        // busco las resoluciones que el pc tiene a nuestra disposición, solo las de mi refreshRate
        foreach (Resolution res in Screen.resolutions)
        {
            if (refreshRate.Equals(res.refreshRateRatio))
            {
                resolutions.Add(res);
            }
        }
        // borro las resoluciones que haya en mi dropdown
        resolutionDropdown.ClearOptions();

        // guardo las resoluciones disponibles en el pc en una lista
        List<string> myResolutionsWithCurrentRefresh = new List<string>();

        // guardo el índice para que se muestre correctamente en el display de resoluciones
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Count; i++)
        {
            string newResolution = resolutions[i].width + "x" + resolutions[i].height;
            myResolutionsWithCurrentRefresh.Add(newResolution);

            // para que se ponga automáticamente la resolucion en el dropDown
            if (resolutions[i].width == width && resolutions[i].height == height)
            {
                currentResolutionIndex = i; // va con indices pq los dropdowns van con indices
            }
        }

        // las añado a las opciones del dropdown
        resolutionDropdown.AddOptions(myResolutionsWithCurrentRefresh);

        // pongo mi resolución automáticamente (en el dropDown)
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        // actualizo de verdad mi resolución
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //PANTALLA COMPLETA
    public void SetFullscreen(bool fullScreen)
    {
        // cambio el modo pantalla completa en función de si está el checkmark guardado o no
        Screen.fullScreen = fullScreen;
    }

    //VOLUMEN
    public void SetMusicVolume(float sliderVolume)
    {
        // cambio el volumen del volumen de la música al del slider
        // como el sonido es logaritmico, debo aplicar la func logaritmo
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(sliderVolume) * 20);
    }

    public void SetEffectsVolume(float sliderVolume)
    {
        // cambio el volumen del volumen de la música al del slider
        // como el sonido es logaritmico, debo aplicar la func logaritmo
        mainMixer.SetFloat("EffectsVolume", Mathf.Log10(sliderVolume) * 20);
    }

    //FPS
    public void SetFpsLimit(float sliderValue)
    {
        int fpsLimit = fpsValues[(int)sliderValue];
        if (fpsLimit == -1)
        {
            // Desactiva la limitación de FPS
            Application.targetFrameRate = -1;
        }
        else
        {
            // Limita los FPS
            Application.targetFrameRate = fpsLimit;
        }
    }
    /*
    public void EnableFps(bool enabled)
    {
        fpsCounter = GetComponent<FPSCounter>();

        fpsCounter.enabled = enabled;
    }
    */
    //Parte de guardado: Edu
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("MusicSliderValue", musicSlider.value);
        PlayerPrefs.SetFloat("EffectsSliderValue", effectsSlider.value);
        PlayerPrefs.SetFloat("FpsSliderValue", fpsSlider.value);

        // Guardar valor de fpsToggle. No se puede hacer setBool, asi que trabajo con enteros
        if (fpsToggle.isOn)
        {
            PlayerPrefs.SetInt("FpsToggleValue", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FpsToggleValue", 0);
        }
        
    }

    
}
