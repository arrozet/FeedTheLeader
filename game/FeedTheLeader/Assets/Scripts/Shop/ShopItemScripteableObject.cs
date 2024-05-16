// Autor: Juanma, **Persistencia de datos: Edu**

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scripteable Obects/New Shop Item", order = 1)]
public class ShopItemScripteableObject : ScriptableObject, IDataPersistence { 
    public int id;
    public string title;
    public string description;
    public double price;
    public double basePrice;
    public int amount;
    public double pointsPerSecond;
    public bool unlocked;
    public Sprite sprite;


    public void LoadData(GameData data)
    {
        //Debug.Log("Hola");
        SerializableDictionary<int, int> shopData = data.shopData;
        amount = shopData.GetValueOrDefault(id);

    }

    public void SaveData(ref GameData data)
    {
        if (data.shopData.ContainsKey(id))
        {
            data.shopData.Remove(id);
        }
        data.shopData.Add(id, amount);
    }
}
