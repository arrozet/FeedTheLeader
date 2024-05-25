//Autor: Edu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
public class Achievement : ScriptableObject, IDataPersistence
{
    public int id;              // Id empieza en 1000
    public string title;
    public string description;
    public string type;         // Indica el tipo de logro que es
    public double condition;     // Indica un parámetro clave para cumplimentar la condición del logro   //TODO: cambiar a float las dependencias - ROZ
    public bool unlocked;
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
