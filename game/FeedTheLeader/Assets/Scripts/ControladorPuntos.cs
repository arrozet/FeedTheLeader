// Autos Juanma
//Edit: Guardado de datos, Edu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPuntos : MonoBehaviour, IDataPersistence
{
    public static ControladorPuntos Instance;
    public float currentScore;
    public float scoreUp;// no se que es serializefield
    // Start is called before the first frame update

    public void LoadData(GameData data)
    {
        this.currentScore = data.currentScore;
        this.scoreUp = data.scoreUp;
    }

    public void SaveData(ref GameData data)
    {
        data.currentScore = this.currentScore;
    }
    public void Awake()
    { 
        // esto es de un tutorial, es para que no destruya el objeto entre escenas
        if(ControladorPuntos.Instance == null)
        {
            ControladorPuntos.Instance = this;
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
   
}
