
// Juanma: lo he editado para que guarde las variables entre escenas


//Parte de guardado: Edu
//Autor: ?

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickingScript : MonoBehaviour
{
    // CLICKER
    public TMP_Text scoreText;

    private float currentScore;
    private float scoreUp;

    void Start()
    {
        PointsManager.Instance.comienzo();

    }

    void Update()
    {
        scoreText.text = PointsManager.Instance.currentScore.ToString();
    }

    public void click()
    {
        PointsManager.Instance.SumarPuntos(PointsManager.Instance.scoreUp);
    }
}
