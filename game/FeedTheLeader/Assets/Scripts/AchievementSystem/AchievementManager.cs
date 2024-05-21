//Autor: Edu
//Editor: Juanma

using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AchievementManager : MonoBehaviour, IDataPersistence
{
    public List<Achievement> achievements;
    //public AchievementLoaderScript achievementLoader;

    public PointsManager pointsManager;
    // no voy a tocar lo tuyo eduardo

    public Achievement[] AchievementSO;
    public GameObject[] AchievementPanelSO;
    public AchievementTemplate[] AchievementPanel;

    void Start()
    {
        achievements = findAllAchievements(); //Hasta que se añadan los logros a AchievementSO se usa esto
        // esto es de juanminator
        for (int i = 0; i < AchievementSO.Length; i++)
        {
                AchievementPanelSO[i].SetActive(true);
        }

        loadPanels();
    }

    public void LoadData(GameData gameData)
    {
        foreach(Achievement a in AchievementSO)
        {
            a.LoadData(gameData);
        }
    }

    public void SaveData(ref GameData data)
    {
        foreach(Achievement a in AchievementSO)
        {
            a.SaveData(ref data);
        }
    }

    //To esto cosas que hizo edu (no voy a tocar)

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public int CheckAchievementsByType(string type, int condition)
    {
        int n = 0;
        foreach (Achievement achievement in achievements)
        {
            if (!achievement.unlocked && type.Equals(achievement.type) && condition >= achievement.condition)
            {
                UnlockAchievement(achievement);
                n++;
            }
        }
        if (n > 0) Debug.Log("Desbloqueados " + n + " logros");
        return n;
    }

    public int CheckShopAchievements(GameData data)
    {
        Dictionary<int,int> dic = data.shopData;
        int n = 0;

        foreach(Achievement achievement in AchievementSO)
        {
            int cond = dic.GetValueOrDefault(achievement.id, 0);
            CheckAchievementById(achievement.id, cond);
            if (achievement.unlocked) n++;
        }

        if (n > 0) Debug.Log("Desbloqueados " +  n + " logros de la tienda");
        return n;
    }

    void UnlockAchievement(Achievement achievement)
    {
        achievement.unlocked = true;
        Debug.Log("Achievement unlocked: " + achievement.name);
        loadPanels(); // vuelve a cargar los paneles para cambiar el que se ha desbloqueado
        // Falta por agregar mensaje de desbloqueo
    }

    void CheckAchievementById(int id, int condition)
    {
        foreach(Achievement achievement in AchievementSO)
        {
            if (id == achievement.id)
            {
                if (condition >= achievement.condition) UnlockAchievement(achievement);
                break;
            }
        }
    }

    private List<Achievement> findAllAchievements()
    {
        IEnumerable<Achievement> achievementsEnum = FindObjectsOfType<Achievement>();

        return new List<Achievement>(achievementsEnum);
    }

    //Aqui empieza a editar la cabra (juanma)
    public void loadPanels() // esto carga los paneles:
                             // realmente lo que tengo es una lista de paneles ocultos (que se activan con el primer for del STart())
                             // esta función asigna a cada panel, el nombre y el objeto de los Scripteable Objects que tenemos
    {
        for (int i = 0; i < AchievementSO.Length; i++)
        {
            if (AchievementSO[i].unlocked) // se carga su sprite 
            {
                AchievementPanel[i].spriteImage.sprite = AchievementSO[i].sprite;
            } else // si no está desbloqueado carga el sprite de no desbloqueado
            {
                AchievementPanel[i].spriteImage.sprite = AchievementSO[i].NotUnlockedSprite;
            }
        }
    }
}