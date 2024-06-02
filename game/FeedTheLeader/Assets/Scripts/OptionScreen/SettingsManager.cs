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
    private FPSCounter fpsCounter;
    public UnityEngine.UI.Toggle fpsToggle;
    public UnityEngine.UI.Slider fpsSlider;
    private int[] fpsValues = { 30, 60, 120, -1 }; // -1 para 

    [Header("Resoluciones")]
    public TMP_Dropdown resolutionDropdown;
    List<Resolution> resolutions = new List<Resolution>();  // guardo las resoluciones disponibles en el pc en una lista
    
    private int width, height;
    private RefreshRate refreshRate;

    // Lo necesito para poder poner el dropdown en caso de que sea la primera vez (antes estaba declarado en getResolutions)
    private List<string> myResolutionsWithCurrentRefresh = new List<string>();
    int currentResolutionIndex = 0;

    [Header("Slider Música")]
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Slider effectsSlider;


    private void Start()
    {
        //myResolutionsWithCurrentRefresh.Clear();    // si no lo borro, genera bugs
        fpsCounter = GameObject.Find("FpsCounter")?.GetComponent<FPSCounter>();
        getResolutions();
        

        musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue", 1f);
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsSliderValue", 1f);

        //Tengo que coger el valor actual de mi dropdown, para poder "mentir" con la resolución que tengo en realidad
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionDropdownValue", currentResolutionIndex);

        // Configura el slider de FPS
        fpsSlider.minValue = 0;
        fpsSlider.maxValue = fpsValues.Length - 1;
        fpsSlider.wholeNumbers = true;
        //fpsSlider.onValueChanged.AddListener();
        fpsSlider.value = PlayerPrefs.GetFloat("FpsSliderValue", fpsSlider.maxValue);

        // Cargar valor de fpsToggle. No se puede hacer setBool, asi que trabajo con enteros
        int valorFpsToggle = PlayerPrefs.GetInt("FpsToggleValue", 0);
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

    public string getFakeResolution(List<string> myRes, int currentResIndex)
    {       
        // Estas son las resoluciones 16:9 frecuentes
                                         // HD          FHD         QHD          UHD
        string[] frequentSixteenByNine = { "1280x720", "1920x1080", "2560x1440", "3840x2160"};

        // Saco la altura de mi resolución
        string currentRes = myRes[currentResIndex];
        string[] dividedCurrentRes = currentRes.Split('x');
        int currentHeight = int.Parse(dividedCurrentRes[1]);

        
        Boolean found = false;
        int i = 0;
        while (!found)
        {
            // Saco la altura de la resolución frecuente
            string frequentRes = frequentSixteenByNine[i];
            string[] dividedFrequentRes = frequentRes.Split('x');
            int frequentHeight = int.Parse(dividedFrequentRes[1]);
            //Debug.Log("Frequent " + frequentHeight + " Current: " + currentHeight);
            // Busco la resolución frecuente más alta sin pasarse (si tengo 900 de altura, me pondrá 720; si tengo 1080 me pondrá 1080; si tengo 1600 me pondrá 1440)
            if (frequentHeight < currentHeight)    // si sigue siendo más pequeña, paso a la siguiente
            {
                i++;
            }
            else if(frequentHeight == currentHeight)
            {
                found = true;   // es exactamente la que quiero
            }
            else
            {
                if (i > 0)
                {
                    i--;    // si se ha pasado y no es la primera, que coja la anterior -> Para evitar OutOfBounds
                }
                
                found = true;
            }
        }

        // Ya tengo el índice que busco
        return frequentSixteenByNine[i];
    }
    
    public void SetResolution(int resolutionIndex)
    {
        /*
        //También tengo que cambiar esto
        Resolution resolution = resolutions[resolutionIndex];
        
        // actualizo de verdad mi resolución
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        */

        /* Para evitar solucionar problemas de anclaje y como medida desesperada (es una guarrada, pero el tiempo es crucial), 
         * he decidido enmascarar la verdadera resolución por una estándar de 16:9. En 16:9, el juego no se rompe y en pantallas de 
         * diferente aspect ratio, se muestran bordes negros (aceptable). Además, la mayoría de pantallas son 16:9.
         * 
         * Al jugador se le mostrará que tiene cierta resolución, cuando en realidad tiene su primo-hermano en 16:9. Es un pequeño precio
         * a pagar.
         * */

        // Consigo mi resolución falsa
        string fakeRes = getFakeResolution(myResolutionsWithCurrentRefresh, resolutionIndex);
        string[] dividedFakeRes = fakeRes.Split('x');
        int width = int.Parse(dividedFakeRes[0]);
        int height = int.Parse(dividedFakeRes[1]);

        // La pongo
        Screen.SetResolution(width, height, Screen.fullScreen);
        //Debug.Log("SetResolution: set to " + fakeRes
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
    public void EnableFps(bool enabled)
    {
        //fpsCounter = GetComponent<FPSCounter>();
        
        fpsCounter.enabled = enabled;
        fpsCounter.isOn = enabled;
        Debug.Log("FPS counter disabled" + fpsCounter.enabled);
    }
    //Parte de guardado: Edu
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("MusicSliderValue", musicSlider.value);
        PlayerPrefs.SetFloat("EffectsSliderValue", effectsSlider.value);
        PlayerPrefs.SetFloat("FpsSliderValue", fpsSlider.value);

        // Tengo que guardar el valor del dropdown que tengo, para poder "mentir" con la resolución que muestro (en realidad no es la real)
        PlayerPrefs.SetInt("ResolutionDropdownValue", resolutionDropdown.value);

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
