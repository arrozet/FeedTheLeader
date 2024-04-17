//Autor: Edu

using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements;
    public AchievementLoaderScript achievementLoader;

    void Start()
    {
        LoadAchievements();
    }

    void OnApplicationQuit()
    {
        SaveAchievements();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void LoadAchievements()
    {
        achievements = achievementLoader.LoadAchievements();
    }


    /*No tengo ni idea de si esto es necesario o no, falta por completarse puesto que esto solo escribe en un string lo que tiene que salir
      en el .csv, pero no se carga en el archivo como tal (fácil de hacer pero quiero seguir haciendo pruebas con esto) */
    void SaveAchievements()
    {
        string csv = "nombre,id,descripcion,condicion,desbloqueado\n";

        foreach (Achievement achievement in achievements)
        {
            csv += $"{achievement.name},{achievement.id},{achievement.description},{achievement.condition},{achievement.unlocked}\n";
        }

        // Aquí se debe guardar el contenido de 'csv' en el archivo CSV
    }

    public void CheckAchievements(int condition)
    {
        foreach (Achievement achievement in achievements)
        {
            if (!achievement.unlocked && condition >= achievement.condition)
            {
                UnlockAchievement(achievement);
            }
        }
    }

    void UnlockAchievement(Achievement achievement)
    {
        achievement.unlocked = true;
        Debug.Log("Achievement unlocked: " + achievement.name);
        // Falta por agregar mensaje de desbloqueo
    }
}