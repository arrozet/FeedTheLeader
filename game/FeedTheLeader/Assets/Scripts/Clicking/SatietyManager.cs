using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SatietyManager : MonoBehaviour
{
    UnityEngine.UI.Slider slider;
    public float act = 0;
    public float max = 100;
    private int clics;
    public TMP_Text textoPorcentaje;
    public UnityEngine.UI.Button boton;
    private double ScoreUp;
    private bool waitingToResetScoreUp = false;

    private void Awake()
    {
        boton.onClick.AddListener(RegistrarClic);
        slider = GetComponent<UnityEngine.UI.Slider>();
        // Llama a la función 'MiFuncion' cada 0.5 segundo, comenzando después de 0.5 segundos.
        InvokeRepeating("Resta", 0.5f, 0.5f);
    }

    private void Resta()
    {
        if (clics > 0)
        {
            clics--;
        }
    }

    private void RegistrarClic()
    {
        clics++;
    }

    private void Update()
    {
        if (clics <= max) 
        {
            ActualizarValor(max, clics);
        }
        else
        {
            if (!waitingToResetScoreUp)
            {
                ScoreUp = PointsManager.Instance.getScoreUp();
                PointsManager.Instance.setScoreUp(0);
                waitingToResetScoreUp = true;
                Invoke("ResetScoreUp", 10f); // Llama a ResetScoreUp después de 10 segundos
            }
        }
    }

    private void ResetScoreUp()
    {
        PointsManager.Instance.setScoreUp(ScoreUp);
        waitingToResetScoreUp = false;
    }

    void ActualizarValor(float max, float act)
    {
        float porcentaje = act / max;
        slider.value = porcentaje;
        textoPorcentaje.text = $"Saciedad: {(Math.Round(porcentaje, 2) * 100)}%"; // Utilizar formato de cadena directamente
    }
}
