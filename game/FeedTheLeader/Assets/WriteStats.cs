using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteStats : MonoBehaviour
{
    public TMP_Text AccumulatedPoints;
    public TMP_Text PointsPerSecond;
    public TMP_Text PointsPerClick;

    private double puntos;
    private double pps;

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
        puntos = PointsManager.Instance.getAccumulatedScore();
        pps = PointsManager.Instance.getPointsPerSecond();
        AccumulatedPoints.text = "Puntos acumulados: " + puntos;
        PointsPerSecond.text = "Puntos por segundo: " + pps;
        //PointsPerClick.text = "Puntos por clic: " + StatsManager.Instance.getPointsPerClick();
    }

}
