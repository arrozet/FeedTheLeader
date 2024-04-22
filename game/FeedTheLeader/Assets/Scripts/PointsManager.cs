// Autos Juanma
//Edit: Guardado de datos, Edu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PointsManager : MonoBehaviour, IDataPersistence
{
    public static PointsManager Instance;
    public double currentScore;
    public double scoreUp;// no se que es serializefield
    public double accumulatedScore;
    public double PointsPerSecond;
    private double pointsAdded = 0f; // esto no se si es del todo necesario, pero lo voy a usar para añadir los puntos
    // Start is called before the first frame update

    public void LoadData(GameData data)
    {
        this.currentScore = data.currentScore;
        this.scoreUp = data.scoreUp;
    }

    public void SaveData(ref GameData data)
    {
        data.currentScore = this.currentScore;
        data.scoreUp = this.scoreUp;
    }
    void Update()
    {
        double pointsThisFrame = PointsPerSecond * Time.deltaTime;
        pointsAdded += pointsThisFrame;
        while (pointsAdded >= 1f)
        {
            // Subtract 1 point from the total added
            pointsAdded -= 1f;

            // Increment the total score
            currentScore++;
        }

    }
    public void Awake()
    { 
        // esto es de un tutorial, es para que no destruya el objeto entre escenas
        if(PointsManager.Instance == null)
        {
            PointsManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
    
    public void comienzo()
    {
        if(scoreUp == 0)
        {
            scoreUp = 1;
        } 
    }
    public void SumarPuntos(double puntos)
    {
        currentScore += puntos;
        accumulatedScore += puntos;
    }
    public void AddPPs(double puntos)
    {
        PointsPerSecond += puntos;
    }

    public void multiplicarMultiplicador(double num)
    {
        scoreUp *=num;
    }
    public bool RestarPuntos(double num)
    {
        if(currentScore >= num)
        {
            currentScore -= num;
            return true;
        } else
        {
            return false;
        }
    }

    public void ResetPoints()
    {
        currentScore = 0; // Restablece los puntos a cero
        scoreUp = 1; // Restablece multiplicador a uno
        accumulatedScore = 0;
    }

    public double getPuntos()
    {
        return currentScore;
    }

    public double getAccumulatedScore()
    {
        return accumulatedScore;
    }
    public void AddAlot()
    {
        currentScore += 5000;
    }

}
