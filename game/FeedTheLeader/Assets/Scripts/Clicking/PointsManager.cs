// Autos Juanma
//Edit: Guardado de datos, Edu

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class PointsManager : MonoBehaviour, IDataPersistence
{
    public static PointsManager Instance;

    //public AchievementManager achievementManager;
    private GameData gameData; //guarrada para que se puedan desbloquear logros de la tienda

    // esto es una locura, voy a hacer que por cada vez que sume 1000 puntos por segundo, lo que sume sea en vez de 1 por segundo que sume 2 y asi hasta el infinito
    public double debug;
    public double regPerSecond;


    public double currentScore;
    public double scoreUp;// no se que es serializefield
    private int clics;
    public double accumulatedScoreStat;
    public double PointsPerSecond;
    public double EventsClicked;
    private double pointsAdded; // esto no se si es del todo necesario, pero lo voy a usar para a�adir los puntos
    // Start is called before the first frame update
    public int achievementCounter;

    //Esto a lo mejor habr�a que hacerlo en un TimeManager
    private bool primeraVez = true;
    private double startTime;

    //multiplicador de puntos despues de prestigiar
    public double prestigeMultiplier = 1;

    public void LoadData(GameData data)
    {
        this.gameData = data;
        this.currentScore = data.currentScore;
        this.scoreUp = data.scoreUp;
        this.accumulatedScoreStat = data.accumulatedScoreStat;
        this.PointsPerSecond = data.pointsPerSecond;
        this.debug = data.debug;
        this.regPerSecond = data.regPerSecond;
        this.clics = data.clics;
        this.EventsClicked = data.EventsClicked;
        this.pointsAdded = data.pointsAdded;
        this.achievementCounter = data.achievementCounter;
        //this.prestigeMultiplier = data.prestigeMultiplier;
    }

    public void SaveData(ref GameData data)
    {
        this.gameData = data;
        data.currentScore = this.currentScore;
        data.scoreUp = this.scoreUp;
        data.accumulatedScoreStat = this.accumulatedScoreStat;
        data.pointsPerSecond = this.PointsPerSecond;
        data.debug = this.debug;
        data.regPerSecond = this.regPerSecond;
        data.clics = this.clics;
        data.EventsClicked = this.EventsClicked;
        data.pointsAdded = this.pointsAdded;
        data.achievementCounter = this.achievementCounter;
    }
    void Update()
    {
        /*
        achievementCounter += AchievementManager.Instance.CheckAchievementsByType("Click", clics);
        achievementCounter += AchievementManager.Instance.CheckAchievementsByType("Puntos", currentScore);
        achievementCounter += AchievementManager.Instance.CheckShopAchievements(gameData);
        */


        // GUARRERIA GUARRERIA PERO NO SE BUGEA MAS 
        if (PointsPerSecond < 1000000000000)
        {
            while (PointsPerSecond - regPerSecond >= 1000)
            {
                debug++;
                regPerSecond += 1000;
            }
        }
        else if (PointsPerSecond - regPerSecond >= 100000000 && PointsPerSecond < 1000000000000)
        {
            while (PointsPerSecond - regPerSecond >= 100000)
            {
                debug += 100;
                regPerSecond += 100000;
            }
        }
        else if (PointsPerSecond >= 1000000000000 && PointsPerSecond < 100000000000000)
        {
            while (PointsPerSecond - regPerSecond >= 1000000000)
            {
                debug += 10000;
                regPerSecond += 1000000000;
            }
        }
        else if (PointsPerSecond >= 100000000000000 && PointsPerSecond < 1000000000000000000)
        {
            while (PointsPerSecond - regPerSecond >= 100000000000)
            {
                debug += 1000000;
                regPerSecond += 100000000000;
            }
        }
        else if (PointsPerSecond >= 1000000000000000000 && PointsPerSecond < 10000000000000000000000d)
        {
            while (PointsPerSecond - regPerSecond >= 10000000000000)
            {
                debug += 100000000;
                regPerSecond += 10000000000000;
            }
        }
        else if (PointsPerSecond >= 10000000000000000000000d && PointsPerSecond < 100000000000000000000000000d)
        {
            while (PointsPerSecond - regPerSecond >= 10000000000000)
            {
                debug += 10000000000;
                regPerSecond += 10000000000000;
            }
        } else
        {
            while (PointsPerSecond - regPerSecond >= 1000000000000000000)
            {
                debug += 1000000000000;
                regPerSecond += 10000000000000000000;
            }
        }


        double pointsThisFrame = PointsPerSecond * Time.deltaTime;
        pointsAdded += pointsThisFrame;
        pointsAdded /= debug; 
        while (pointsAdded >= 1f)
        {
            // Quita el debug
            pointsAdded -= debug;

            // Increment the total score
            currentScore+= debug;
            accumulatedScoreStat+= debug;
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

    public void Initialize()
    {
        PointsManager.Instance = this;
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
        debug = 1;
        regPerSecond = 0;
        currentScore = 0; // Restablece los puntos a cero
        scoreUp = 1*prestigeMultiplier; // Restablece multiplicador a uno (por el multiplicador de prestigio)
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

    public double getPrestigeMultiplier()
    {
        return prestigeMultiplier;
    }

    public void setPrestigeMultiplier(double n)
    {
        prestigeMultiplier = n;
    }


    public void AddAlot()
    {
        currentScore *= 100;
        accumulatedScoreStat *= 100;
    }

    public void Prestige(double num)
    {
        prestigeMultiplier *= num;

        ResetPoints();
    }

    public void ResetPrestigeMultiplier()
    {
        if (prestigeMultiplier != 0) scoreUp = scoreUp / prestigeMultiplier;
        prestigeMultiplier = 1;
    }

}
