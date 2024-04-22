// Author: Rub�n

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
    [Header("Resoluciones")]
    public TMP_Dropdown resolutionDropdown;
    [Header("Slider M�sica")]
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Slider effectsSlider;


    List<Resolution> resolutions = new List<Resolution>();
    private int width, height;
    private RefreshRate refreshRate;
    //private bool fullscreen = true;

    private void Start()
    {
        getResolutions();
        musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue", 1f);
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsSliderValue", 1f);
    }

    public void getResolutions()
    {
        width = Screen.currentResolution.width;
        height = Screen.currentResolution.height;
        refreshRate = Screen.currentResolution.refreshRateRatio;

        // busco las resoluciones que el pc tiene a nuestra disposici�n, solo las de mi refreshRate
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

        // guardo el �ndice para que se muestre correctamente en el display de resoluciones
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Count; i++)
        {
            string newResolution = resolutions[i].width + "x" + resolutions[i].height;
            myResolutionsWithCurrentRefresh.Add(newResolution);

            // para que se ponga autom�ticamente la resolucion en el dropDown
            if (resolutions[i].width == width && resolutions[i].height == height)
            {
                currentResolutionIndex = i; // va con indices pq los dropdowns van con indices
            }
        }

        // las a�ado a las opciones del dropdown
        resolutionDropdown.AddOptions(myResolutionsWithCurrentRefresh);

        // pongo mi resoluci�n autom�ticamente (en el dropDown)
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMusicVolume(float sliderVolume)
    {
        // cambio el volumen del volumen de la m�sica al del slider
        // como el sonido es logaritmico, debo aplicar la func logaritmo
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(sliderVolume) * 20);
    }

    public void SetEffectsVolume(float sliderVolume)
    {
        // cambio el volumen del volumen de la m�sica al del slider
        // como el sonido es logaritmico, debo aplicar la func logaritmo
        mainMixer.SetFloat("EffectsVolume", Mathf.Log10(sliderVolume) * 20);
    }

    public void SetFullscreen(bool fullScreen)
    {
        // cambio el modo pantalla completa en funci�n de si est� el checkmark guardado o no
        Screen.fullScreen = fullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        // actualizo de verdad mi resoluci�n
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Parte de guardado: Edu
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("MusicSliderValue", musicSlider.value);
        PlayerPrefs.SetFloat("EffectsSliderValue", effectsSlider.value);
    }

    // Comentado porque ahora mismo no es necesario tener un desplegable para elegir la calidad
    /*public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    */
}