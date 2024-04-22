// Autos Juanma
//Edit: Guardado de datos, Edu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PointsManager : MonoBehaviour, IDataPersistence
{
    public static PointsManager Instance;
    public float currentScore;
    public float scoreUp;// no se que es serializefield
    public float accumulatedScore;
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
    public void SumarPuntos(float puntos)
    {
        currentScore += puntos;
        accumulatedScore += puntos;
    }

    public void multiplicarMultiplicador(float num)
    {
        scoreUp *=num;
    }
    public bool RestarPuntos(float num)
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

    public float getPuntos()
    {
        return currentScore;
    }

    public float getAccumulatedScore()
    {
        return accumulatedScore;
    }

}
