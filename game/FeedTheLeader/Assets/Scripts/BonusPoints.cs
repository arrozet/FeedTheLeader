//Author: Artur
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints : MonoBehaviour
{

    public void OnMouseDown()
    {
        // Esta función se ejecuta cuando se hace clic en el objetivo
        int pointsToAdd = Mathf.RoundToInt((float)(PointsManager.Instance.getPuntos() * 0.1f)); // Calculamos el 10% de los puntos totales y lo pasamos a Int
        PointsManager.Instance.UpdateEventsClicked(1);
        if (pointsToAdd < 10)
        {
            pointsToAdd = 10; // Mínimo sumar 10 puntos
        }
        // Incrementamos los puntos
        PointsManager.Instance.SumarPuntos(pointsToAdd); // Llamamos a la función AddPoints del GameManager y le pasamos la cantidad de puntos a agregar
        GameObject.FindGameObjectWithTag("boton").SetActive(false);
    }

}
