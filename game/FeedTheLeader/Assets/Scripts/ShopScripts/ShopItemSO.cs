using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//esto sirve para poder crear objetos directamente desde Unity
[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scripteable Obects/New Shop Item", order = 1)] 
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string description;
    public int baseCost;
}
