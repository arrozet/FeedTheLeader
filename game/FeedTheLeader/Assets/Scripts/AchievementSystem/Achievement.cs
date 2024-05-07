//Autor: Edu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
public class Achievement : ScriptableObject, IDataPersistence
{
    public string title;
    public int id;
    public string description;
    public int condition;
    public bool unlocked;
    //public string type; AUN NO IMPLEMENTADO EN LOS LOGROS, SE USARÁ PARA DESBLOQUEAR POR CONDICIÓN
    public Sprite sprite;
    public Sprite NotUnlockedSprite;


    public void LoadData(GameData data)
    {

        Dictionary<int, bool> achievementData = data.achievementData;
        unlocked = achievementData.GetValueOrDefault(id);

    }

    public void SaveData(ref GameData data)
    {
        if (data.achievementData.ContainsKey(id))
        {
            data.achievementData.Remove(id);
        }
        data.achievementData.Add(id, unlocked);
    }
}
