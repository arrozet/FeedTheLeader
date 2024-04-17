using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
public class Achievement : ScriptableObject
{
    public string title;
    public int id;
    public string description;
    public int condition;
    public bool unlocked;
}
