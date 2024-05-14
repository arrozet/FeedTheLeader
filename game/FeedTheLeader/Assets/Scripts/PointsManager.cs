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

    // esto es una locura, voy a hacer que por cada vez que sume 1000 puntos por segundo, lo que sume sea en vez de 1 por segundo que sume 2 y asi hasta el infinito
    public double desbug = 1;
    public double regPerSecond = 0;


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
        // GUARRERIA GUARRERIA PERO NO SE BUGEA MAS 
        if (PointsPerSecond < 1000000000000)
        {
            while (PointsPerSecond - regPerSecond >= 1000)
            {
                desbug++;
                regPerSecond += 1000;
            }
        }
        else if (PointsPerSecond - regPerSecond >= 100000000 && PointsPerSecond < 1000000000000)
        {
            while (PointsPerSecond - regPerSecond >= 100000)
            {
                desbug += 100;
                regPerSecond += 100000;
            }
        }
        else if (PointsPerSecond >= 1000000000000 && PointsPerSecond < 100000000000000)
        {
            while (PointsPerSecond - regPerSecond >= 1000000000)
            {
                desbug += 10000;
                regPerSecond += 1000000000;
            }
        }
        else if (PointsPerSecond >= 100000000000000 && PointsPerSecond < 1000000000000000000)
        {
            while (PointsPerSecond - regPerSecond >= 100000000000)
            {
                desbug += 1000000;
                regPerSecond += 100000000000;
            }
        }
        else if (PointsPerSecond >= 1000000000000000000 && PointsPerSecond < 10000000000000000000000d)
        {
            while (PointsPerSecond - regPerSecond >= 10000000000000)
            {
                desbug += 100000000;
                regPerSecond += 10000000000000;
            }
        }
        else if (PointsPerSecond >= 10000000000000000000000d && PointsPerSecond < 100000000000000000000000000d)
        {
            while (PointsPerSecond - regPerSecond >= 10000000000000)
            {
                desbug += 10000000000;
                regPerSecond += 10000000000000;
            }
        } else
        {
            while (PointsPerSecond - regPerSecond >= 1000000000000000000)
            {
                desbug += 1000000000000;
                regPerSecond += 10000000000000000000;
            }
        }


        double pointsThisFrame = PointsPerSecond * Time.deltaTime;
        pointsAdded += pointsThisFrame;
        pointsAdded /= desbug; 
        while (pointsAdded >= 1f)
        {
            // Quita el desbug
            pointsAdded -= desbug;

            // Increment the total score
            currentScore+= desbug;
            accumulatedScoreStat+= desbug;
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
        desbug = 1;
        regPerSecond = 0;
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
        currentScore += 100000000000000000000000000000000d;
        accumulatedScoreStat += 100000000000000000000000000000000d;
    }

}
