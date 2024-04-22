//Autor: Edu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public double currentScore;
    public double scoreUp;

    //Se inicializan los valores que se van a guardar a default (Nuevos Datos)
    //Será necesario añadir en un futuro algo que guarde las mejoras de la tienda
    public GameData()
    {
        this.currentScore = 0;
        this.scoreUp = 1;
    }
}
