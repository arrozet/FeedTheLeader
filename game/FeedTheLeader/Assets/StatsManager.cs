//Author: Artur
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsManager : MonoBehaviour
{

    private float AccumulatedPoints;
    private float PointsPerSecond;
    private int PointsPerClick;
    

    public static StatsManager Instance;

    // Otros atributos y m�todos...

    public void Empieza()
    {
        UpdateStats(0, 0, 0);
    }

    private void Awake()
    {
        if (StatsManager.Instance == null)
        {
            StatsManager.Instance = this;
            DontDestroyOnLoad(this.gameObject); // Para que el objeto persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // M�todo para actualizar las estad�sticas
    public void UpdateStats(float acumulados, float porSegundo, int porClick)
    {
        AccumulatedPoints += acumulados;
        PointsPerSecond += porSegundo;
        PointsPerClick += porClick;
    }

    public float getAccumulatedPoints()
    {
        return AccumulatedPoints;
    }

    public float getPointsPerSecond()
    {
        return PointsPerSecond;
    }

    public int getPointsPerClick()
    {
        return PointsPerClick;
    }
}
