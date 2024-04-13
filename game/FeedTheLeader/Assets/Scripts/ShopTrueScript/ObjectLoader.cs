//Author:ROZ

// AVISO A NAVEGANTES: para que se carguen los archivos del .csv, antes se debe de ejecutar en el editor. Desde la build directamente no funciona
// De ah� los if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    // para poder hacer input output con ficheros

#if UNITY_EDITOR
    using UnityEditor;
#endif


public class ObjectLoader : MonoBehaviour
{
    // cuidadito con mostrar esto en el editor, se me bugue� y el valor mostrado era distinto al escrito aqu� y no compilaba
    private string filePath = "Assets/Resources/test.csv";   // ruta del archivo
    ShopItemScripteableObject item; // tipo de item a cargar

    void Start()
    {
        LoadShop();
    }

    void LoadShop()
    {
        #if UNITY_EDITOR
            //some code here that uses something from the UnityEditor namespace

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
    #endif
    }

}
