using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Author: Rub�n
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

    // �ndice de Ode ad ducem que se reproducir� en bucle en el men� principal
    public int mainMenuSongIndex = 0;

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
        // Verificar si estamos en la escena del men� principal
        if (SceneManager.GetActiveScene().name == "StartScreen")
        {
            // Reproducir la canci�n del men� principal en bucle
            musicSource.clip = ost[mainMenuSongIndex];
        }
        else
        {
            // Seleccionar aleatoriamente una canci�n diferente
            int randomSongIndex = Random.Range(0, ost.Length);
            musicSource.clip = ost[randomSongIndex];
        }
        float sliderValue = PlayerPrefs.GetFloat("MusicSliderValue", 1);
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playRandomClickingEffect() {
        int randomInt = r.Next(0, clickingEffects.Length);
        effectsSource.clip = clickingEffects[randomInt];
        effectsSource.Play();
        Debug.Log("Sound played!");
    }
}
