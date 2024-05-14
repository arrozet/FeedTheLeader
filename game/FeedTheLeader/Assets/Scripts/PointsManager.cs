// Autos Juanma
//Edit: Guardado de datos, Edu

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PointsManager : MonoBehaviour, IDataPersistence
{
    public static PointsManager Instance;
    public double currentScore;
    public double scoreUp;// no se que es serializefield
    private int clics=0;
    public double accumulatedScoreStat;
    public double PointsPerSecond;
    public double EventsClicked = 0;
    private double pointsAdded = 0f; // esto no se si es del todo necesario, pero lo voy a usar para añadir los puntos
    // Start is called before the first frame update

    //Esto a lo mejor habría que hacerlo en un TimeManager
    private bool primeraVez = true;
    private double startTime;

    public void LoadData(GameData data)
    {
        this.currentScore = data.currentScore;
        this.scoreUp = data.scoreUp;
        this.accumulatedScoreStat = data.accumulatedScoreStat;
        this.PointsPerSecond = data.pointsPerSecond;
        
    }

    public void SaveData(ref GameData data)
    {
        data.currentScore = this.currentScore;
        data.scoreUp = this.scoreUp;
        data.accumulatedScoreStat = this.accumulatedScoreStat;
        data.pointsPerSecond = this.PointsPerSecond;

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
            accumulatedScoreStat++;
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

        //Esto es de tiempo
        if (primeraVez)
        {
            startTime = Time.time;
            primeraVez = false;
        }
    }
    public void SumarPuntos(double puntos)
    {
        clics++;
        currentScore += puntos;
        accumulatedScoreStat += puntos;
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
        accumulatedScoreStat = 0;
        PointsPerSecond = 0;
    }

    public double getPuntos()
    {
        return currentScore;
    }

    public double getAccumulatedScore()
    {
        return accumulatedScoreStat;
    }

    public double getPointsPerSecond()
    {
        return PointsPerSecond;
    }

    public double getScoreUp()
    {
        return scoreUp;
    }

    public void UpdateEventsClicked(int n)
    {
        EventsClicked += n;
    }

    public double getEventsClicked()
    {
        return EventsClicked;
    }

    public double getStartTime()
    {
        return startTime;
    }

    public int getClics()
    {
        return clics;
    }

    public void setScoreUp(double n)
    {
        scoreUp = n;
    }


    public void AddAlot()
    {
        currentScore += 500000000000000000;
        accumulatedScoreStat += 500000000000000000;
    }

}
