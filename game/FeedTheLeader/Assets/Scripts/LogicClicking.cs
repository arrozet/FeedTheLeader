//Autor: Juanma (la cabra)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicClicking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tienda()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Back()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void ResetPoints()
    {
        // Llama a la función para resetear los puntos en el GameManager
        ControladorPuntos.Instance.ResetPoints();
    }
}
