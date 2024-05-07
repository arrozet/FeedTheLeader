using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteStats : MonoBehaviour
{
    public TMP_Text CurrentPoints;
    public TMP_Text AccumulatedPoints;
    public TMP_Text PointsPerSecond;
    public TMP_Text PointsPerClick;
    public TMP_Text EventsClicked;
    public TMP_Text PlayedTime;

    private double puntosActuales;
    private double puntosAcumulados;
    private double puntosPorSegundo;
    private double puntosPorClick;
    private double eventosClicados;
    private double tiempoJugado;

    /*Pasos:
     -Creas un  public TMP_Text con el nombre que le quieras dar
     -En el proyecto creas un objeto texto y se lo referencias al public TMP_Text (arrastrar al hueco de unity)
     -En pointsManager creas la variable que necesites para llevar la cuenta
     -Le asignas la variable al texto

     IMPORTANTE:
        El jugador tiene que meterse antes en clickingScreen ya que si no  no estará creado PointsManager y saltará NullReferenceException
        Falta hacer el guardado de las variables
    */
    public void Start()
    {
        
    }

    public void Update()
    {
        if (PointsManager.Instance == null)
        {
            CurrentPoints.text = "Puntos Actuales: No disponibles";
            AccumulatedPoints.text = "Puntos acumulados: No disponibles";
            PointsPerSecond.text = "Puntos por segundo: No disponibles";
            PointsPerClick.text = "Puntos por clic: No disponibles";
            EventsClicked.text = "Eventos aleatorios clicados: No disponibles";
            PlayedTime.text = "Tiempo jugado : No disponible";
        }
        else
        {
            puntosActuales = PointsManager.Instance.getPuntos();
            puntosAcumulados = PointsManager.Instance.getAccumulatedScore();
            puntosPorSegundo = PointsManager.Instance.getPointsPerSecond();
            puntosPorClick = PointsManager.Instance.getScoreUp();
            eventosClicados = PointsManager.Instance.getEventsClicked();
            tiempoJugado = Time.time - (PointsManager.Instance.getStartTime());
            tiempoJugado = Mathf.RoundToInt((float)tiempoJugado);

            CurrentPoints.text = "Puntos actuales: " + puntosActuales;
            AccumulatedPoints.text = "Puntos acumulados: " + puntosAcumulados;
            PointsPerSecond.text = "Puntos por segundo: " + puntosPorSegundo;
            PointsPerClick.text = "Puntos por click: " + puntosPorClick;
            EventsClicked.text = "Eventos aleatorios clicados: " + eventosClicados;
            PlayedTime.text = "Tiempo jugado: " + tiempoJugado + " segundos";
        }

    }

}
