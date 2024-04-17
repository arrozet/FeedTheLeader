//Author: Artur
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public TMP_Text AccumulatedPoints;
    public TMP_Text PointsPerSecond;
    public TMP_Text PointsPerClick;

    // Método para actualizar las estadísticas
    public void UpdateStats(int acumulados, float porSegundo, int porClick)
    {
        AccumulatedPoints.text = "Puntos acumulados: " + acumulados.ToString();
        PointsPerSecond.text = "Puntos por segundo: " + porSegundo.ToString("F2"); //Formatea a dos decimales
        PointsPerClick.text = "Puntos por clic: " + porClick.ToString();
    }
}
