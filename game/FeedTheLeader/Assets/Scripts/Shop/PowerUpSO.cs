// Author: Juanma

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scripteable Obects/New PowerUp", order = 2)]
public class PowerUpSO : ScriptableObject
{
    public int id;
    public string title;
    public string description;
    public double price;
    public bool bought;
    public bool unlocked;
    public Sprite sprite;
}
