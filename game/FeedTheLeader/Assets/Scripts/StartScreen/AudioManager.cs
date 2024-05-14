using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Author: Rubén
public class AudioManager : MonoBehaviour
{ 
    [Header("Audio Mixer")]
    public AudioMixer mainMixer;

    [Header("Audio source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource effectsSource;

    [Header("Audio clip")]
    public AudioClip[] ost;
    public AudioClip[] clickingEffects;

    public System.Random r = new System.Random();
    public static AudioManager instance;


    // Índice de Ode ad ducem que se reproducirá en bucle en el menú principal
    private int mainMenuSongIndex = 0;
    private bool firstTime = true;

    // para crearlo como singleton (solo va a haber 1 solo uno siempre)
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
        musicSource.clip = ost[mainMenuSongIndex];
        float sliderValue = PlayerPrefs.GetFloat("MusicSliderValue", 1);
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(musicSource.clip.length - musicSource.time <= 0) // si ha acabado la canción actual
        {
            // Verificar si estamos en la escena del menú principal
            if (SceneManager.GetActiveScene().name == "StartScreen")
            {
                // Reproducir la canción del menú principal en bucle
                musicSource.clip = ost[mainMenuSongIndex];
            }
            else
            {
                // Seleccionar aleatoriamente una canción diferente
                int randomSongIndex;
                if (firstTime)
                {
                    randomSongIndex = r.Next(1, ost.Length);
                }
                else {
                    randomSongIndex = r.Next(0, ost.Length);
                }
                 
                musicSource.clip = ost[randomSongIndex];
            }
            musicSource.Play();

            // para que la primera vez no salga de nuevo la canción del mainMenu
            if (firstTime)
            {
                firstTime = false;
            }
        }

        
    }

    public void playRandomClickingEffect() {
        int randomInt = r.Next(0, clickingEffects.Length);
        effectsSource.clip = clickingEffects[randomInt];
        effectsSource.Play();
        Debug.Log("Sound played!");
    }
}
