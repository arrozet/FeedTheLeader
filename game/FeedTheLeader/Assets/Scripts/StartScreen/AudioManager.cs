using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Author: Rubén
public class AudioManager : MonoBehaviour
{ 
    [Header("Audio Mixer")]
    public AudioMixer musicMixer;

    [Header("Audio source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource effectsSource;

    [Header("Audio clip")]
    public AudioClip bgMusic;

    public static AudioManager instance;

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
        musicSource.clip = bgMusic;
        float sliderValue = PlayerPrefs.GetFloat("SliderValue", 1);
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        musicSource.Play(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
