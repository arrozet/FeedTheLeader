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


    public void Start()
    {
        UpdateStats(StatsManager.Instance.getAccumulatedPoints(), StatsManager.Instance.getPointsPerSecond(), StatsManager.Instance.getPointsPerClick());
    }



    public void UpdateStats(float acumulados, float porSegundo, int porClick)
    {
        AccumulatedPoints.text = "Puntos acumulados: " + StatsManager.Instance.getAccumulatedPoints();
        PointsPerSecond.text = "Puntos por segundo: " + StatsManager.Instance.getPointsPerSecond();
        PointsPerClick.text = "Puntos por clic: " + StatsManager.Instance.getPointsPerClick();
    }
}
