//Author: Javi
//Jorge ha añadido una función
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicStartScreen : MonoBehaviour
{
    private AudioManager audioManager;

    private void OnEnable()
    {
        // Buscar el AudioManager en la escena
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Jugar() //lleva desde la escena de título a la escena donde se clickea
    {
        SceneManager.LoadScene("ClickingScreen");
    }

    public void Opciones() //lleva desde la escena de título a la escena de opciones
    {
        SceneManager.LoadScene("OptionsScreen");
    }

    public void Estadisticas() //lleva desde la escena de título a la escena de opciones
    {
        SceneManager.LoadScene("StatsScreen");
    }

    public void Salir() //cierra el juego
    {
        Application.Quit();
    }

    // Método para llamar al efecto de sonido cuando se presiona el botón (se necesita porque sino pierde la referencia)
    public void PlayRandomClickingEffect()
    {
        // Verificar si se encontró el AudioManager
        if (audioManager != null)
        {
            // Llamar al método para reproducir el efecto de sonido
            audioManager.playRandomClickingEffect();
        }
        else
        {
            Debug.LogError("No se encontró el AudioManager.");
        }
    }
}