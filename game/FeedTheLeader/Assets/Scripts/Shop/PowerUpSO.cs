// Author: Juanma

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scripteable Obects/New PowerUp", order = 2)]
public class PowerUpSO : ScriptableObject
{
    public int id;          // Id empieza en 100
    public int level;       // Determina el tipo de marco que tiene
    public string title;
    public string desc;
    public double effect;   // Determina el multiplicador que aplica
    public double price;    
    public bool unlocked;
    public bool bought;     // Determina si está comprado o no
    public string type;     // Determina el tipo de objeto que es. No es muy importante
    public Sprite sprite;
    public int generator;   // A qué generador se refiere la mejora. Si no se refiere a un generador, será un número negativo (click -> -100, prensa cuantica -> -500, sinergia chef y cafeteria -> -1 )
}
