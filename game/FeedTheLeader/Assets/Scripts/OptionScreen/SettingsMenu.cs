// Author: Rubén

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
    public AudioMixer musicMixer;
    [Header("Resoluciones")]
    public TMP_Dropdown resolutionDropdown;
    [Header("Slider Música")]
    public UnityEngine.UI.Slider musicSlider;


    List<Resolution> resolutions = new List<Resolution>();
    private int width, height;
    private RefreshRate refreshRate;
    //private bool fullscreen = true;

    private void Start()
    {
        width = Screen.currentResolution.width;
        height = Screen.currentResolution.height;
        refreshRate = Screen.currentResolution.refreshRateRatio;


        musicSlider.value = PlayerPrefs.GetFloat("SliderValue", 1);
        Debug.Log("Valor del slider cargado a" + musicSlider.value);


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

    public void SetVolume(float sliderVolume)
    {
        // cambio el volumen del audioMixer al del slider
        // como el sonido es logaritmo, debo aplicar la func logaritmo
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderVolume) * 20);
    }

    // Comentado porque ahora mismo no es necesario tener un desplegable para elegir la calidad
    /*public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    */

    public void SetFullscreen(bool fullScreen)
    {
        // cambio el modo pantalla completa en función de si está el checkmark guardado o no
        Screen.fullScreen = fullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        // actualizo de verdad mi resolución
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Parte de guardado: Edu

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("SliderValue", musicSlider.value);
        Debug.Log("Valor del slider guardado a " + musicSlider.value);
    }

}
