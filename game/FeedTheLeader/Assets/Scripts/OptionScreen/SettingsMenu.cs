// Author: Rubén

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class SettingsMenu : MonoBehaviour
{
    
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        // busco las resoluciones que el pc tiene a nuestra disposición

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> myResolutions = new List<string>();

        int currentResolutionIndex = 0;
        // guardo las resoluciones disponibles en el pc en una lista
        for (int i=0; i<resolutions.Length; i++)
        {
            // para que no se repitan las resoluciones y solo se muestren las de mi tasa de refresco
            if (Screen.currentResolution.refreshRateRatio.Equals(resolutions[i].refreshRateRatio))
            {
                string newResolution = resolutions[i].width + "x" + resolutions[i].height + "@" + resolutions[i].refreshRateRatio + " Hz";
                myResolutions.Add(newResolution);

                // para que se ponga automáticamente la resolucion 
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i; // va con indices pq los dropdowns van con indices
                }
                
            }

        }

        // las añado a las opciones del dropdown
        resolutionDropdown.AddOptions(myResolutions);

        // pongo mi resolución automáticamente (el el dropDown)
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
    }
    public void SetVolume (float volume)
    {
        // cambio el volumen del audioMixer al del slider
        audioMixer.SetFloat("MasterVolume", volume);
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
    
}
