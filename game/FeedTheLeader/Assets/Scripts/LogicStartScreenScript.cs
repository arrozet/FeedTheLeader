//Author: Javi
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicStartScreenScript : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameScreen;
    public GameObject optionsScreen;

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

    public void Salir() //cierra el juego
    {
        Application.Quit();
    }
}