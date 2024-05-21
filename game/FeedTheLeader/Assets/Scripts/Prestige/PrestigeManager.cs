using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrestigeManager : MonoBehaviour
{

    void Start()
    {

    }


    // Función que realiza el prestigio
    public void Prestigiar(int newScoreUp)
    {
        // Resetea puntos
        PointsManager.Instance.Prestige(newScoreUp);

        // Resetea power-ups y mejoras
        

        // Resetea logros
;

        SceneManager.LoadScene("ClickingScreen");
    }

}
