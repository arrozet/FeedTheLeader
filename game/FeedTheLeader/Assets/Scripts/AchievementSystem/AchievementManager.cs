//Autor: Edu

using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements;
    public AchievementLoaderScript achievementLoader;

    void Start()
    {
        achievements = findAllAchievements();
    }

    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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

    private List<Achievement> findAllAchievements()
    {
        IEnumerable<Achievement> achievementsEnum = FindObjectsOfType<Achievement>();

        return new List<Achievement>(achievementsEnum);
    }
}