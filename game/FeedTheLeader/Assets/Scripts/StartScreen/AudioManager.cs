using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
        musicSource.clip = ost[0];  // Ode ad ducem
        float sliderValue = PlayerPrefs.GetFloat("MusicSliderValue", 1);
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playRandomClickingEffect() {
        Debug.Log("Sound entered!");
        int randomInt = r.Next(0, clickingEffects.Length);
        effectsSource.clip = clickingEffects[randomInt];
        effectsSource.Play();
        Debug.Log("Sound played!");
    }
}
