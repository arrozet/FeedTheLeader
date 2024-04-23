// Autor: Juanma

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scripteable Obects/New Shop Item", order = 1)]
public class ShopItemScripteableObject : ScriptableObject { 
    public int id;
    public string title;
    public string description;
    public double price;
    public double basePrice;
    public int amount;
    public double pointsPerSecond;
    public bool unlocked;

    
}
