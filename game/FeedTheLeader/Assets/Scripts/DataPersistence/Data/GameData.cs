//Autor: Edu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public double currentScore;
    public double scoreUp;
    public double accumulatedScoreStat;
    public double pointsPerSecond;
    public Dictionary<string, int> shopData;
    public Dictionary<int, bool> achievementData;

    //Se inicializan los valores que se van a guardar a default (Nuevos Datos)
    //Ser� necesario a�adir en un futuro algo que guarde las mejoras de la tienda
    public GameData()
    {
        this.currentScore = 0;
        this.scoreUp = 1;
        this.accumulatedScoreStat = 0;
        this.pointsPerSecond = 0;
    }
}
