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
    public SerializableDictionary<int, int> shopData;
    public SerializableDictionary<int, bool> achievementData;

    public int clics;
    public double EventsClicked;
    public double pointsAdded;
    public int achievementCounter;

    public double debug;
    public double regPerSecond;

    //Se inicializan los valores que se van a guardar a default (Nuevos Datos)
    //Ser� necesario a�adir en un futuro algo que guarde las mejoras de la tienda
    public GameData()
    {
        this.currentScore = 0;
        this.scoreUp = 1;
        this.accumulatedScoreStat = 0;
        this.pointsPerSecond = 0;
        shopData = new SerializableDictionary<int, int>();
        achievementData = new SerializableDictionary<int, bool>();
        this.debug = 1;
        this.regPerSecond = 0;
        this.clics = 0;
        this.EventsClicked = 0;
        this.pointsAdded = 0f;
        this.achievementCounter = 0;
}
}
