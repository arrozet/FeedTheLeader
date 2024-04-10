// Autor: Juanma

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scripteable Obects/New Shop Item", order = 1)]
public class ShopItemScripteableObject : ScriptableObject
{
    public string title;
    public int price;
}
