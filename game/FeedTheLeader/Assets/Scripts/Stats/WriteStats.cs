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

    private int dias;
    private int horas;
    private int minutos;
    private int segundosRestantes;

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
            //tiempoJugado = Mathf.RoundToInt((float)tiempoJugado);
            ConvertirTiempo(tiempoJugado);

            CurrentPoints.text = "Puntos actuales: " + puntosActuales;
            AccumulatedPoints.text = "Puntos acumulados: " + puntosAcumulados;
            PointsPerSecond.text = "Puntos por segundo: " + puntosPorSegundo;
            PointsPerClick.text = "Puntos por click: " + puntosPorClick;
            EventsClicked.text = "Eventos aleatorios clicados: " + eventosClicados;
            PlayedTime.text = "Tiempo jugado: " + dias + " días " + horas + " horas " + minutos + " minutos " + segundosRestantes + " segundos";
        }

    }

    public void ConvertirTiempo(double segundos)
    {
        // Calcula los días, horas, minutos y segundos
        dias = (int)(segundos / 86400); // 86400 segundos en un día
        segundos %= 86400;
        horas = (int)(segundos / 3600); // 3600 segundos en una hora
        segundos %= 3600;
        minutos = (int)(segundos / 60); // 60 segundos en un minuto
        segundos %= 60;
        segundosRestantes = (int)segundos;

    }

}
