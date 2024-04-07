// Juanma: lo he editado para que guarde las variables entre escenas

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickingScript : MonoBehaviour
{
    // CLICKER
    public TMP_Text scoreText;


    void Start()
    {
        ControladorPuntos.Instance.comienzo();
    }

    void Update()
    {
        scoreText.text = ControladorPuntos.Instance.cantidadPuntos.ToString();
    }

    public void click()
    {
        ControladorPuntos.Instance.SumarPuntos(ControladorPuntos.Instance.multiplicadorPuntos);
    }
}
