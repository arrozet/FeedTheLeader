//Author:ROZ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;    // para poder hacer input output con ficheros

public class ObjectLoader : MonoBehaviour
{
    // cuidadito con mostrar esto en el editor, se me bugueó y el valor mostrado era distinto al escrito aquí y no compilaba
    private string filePath = "Assets/Resources/test.csv";   // ruta del archivo
    ShopItemScripteableObject item; // tipo de item a cargar

    void Start()
    {
        LoadShop();
    }

    void LoadShop()
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(';');   // divido por cada ;
                    
                    // relleno el objeto con los datos pertinentes
                    item = ScriptableObject.CreateInstance<ShopItemScripteableObject>();
                    item.name = parts[0];
                    item.title = parts[0];
                    item.price = int.Parse(parts[1]);

                    // una vez creado el objeto, lo guardo en la ruta de los scriptableobjects para tienda
                    string assetPath = "Assets/Scripts/ShopTrueScript/ScripteableObjetc/" + item.name + ".asset";
                    AssetDatabase.CreateAsset(item, assetPath);
                    AssetDatabase.SaveAssets();

                }
            }
                

                
        }
        else
        {
            Debug.LogError("El archivo no existe en la ruta especificada: " + filePath);
        }
        
    }
    
}
