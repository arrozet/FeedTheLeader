//Author: Javi
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicOptionsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolverAClickingScreen() // vuelve a la escena de menú inicio
    {
        SceneManager.LoadScene("ClickingScreen");
    }

    public void VolverAMenuInicio() // vuelve a la escena de menú inicio
    {
        SceneManager.LoadScene("StartScreen");
    }
}
