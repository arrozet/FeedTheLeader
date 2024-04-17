//Author:Eduardo, modificaci�n de ObjectLoader.cs de Rub�n. Los comentarios del script original se mantendr�n en la mayor�a de lo posible

// AVISO A NAVEGANTES: para que se carguen los archivos del .csv, antes se debe de ejecutar en el editor. Desde la build directamente no funciona
// De ah� los if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    // para poder hacer input output con ficheros
using System.Runtime.InteropServices.WindowsRuntime;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class AchievementLoaderScript : MonoBehaviour
{
    // cuidadito con mostrar esto en el editor, se me bugue� y el valor mostrado era distinto al escrito aqu� y no compilaba
    private string filePath = "Assets/Resources/achievements.csv";   // ruta del archivo
    Achievement item; // tipo de item a cargar



    public List<Achievement> LoadAchievements()
    {
#if UNITY_EDITOR
        //some code here that uses something from the UnityEditor namespace
        List<Achievement> list = new List<Achievement>();
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();  // siempre habr� al menos una l�nea, la que describe las columnas. para que se la salte y solo lea los datos, se pone esto
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(';');   // divido por cada ;

                    // relleno el objeto con los datos pertinentes
                    item = ScriptableObject.CreateInstance<Achievement>();
                    item.name = parts[0];
                    item.title = parts[0];
                    item.id = int.Parse(parts[1]);
                    item.description = parts[2];
                    item.condition = int.Parse(parts[3]);
                    item.unlocked = bool.Parse(parts[4]);

                    // una vez creado el objeto, lo guardo en la ruta de los scriptableobjects para tienda
                    string assetPath = "Assets/Scripts/AchievementSystem/Achievements/";  // donde se va a guardar los items. cuidadin con donde se pone
                    string addItem = item.name + ".asset";  // para a�adir el item

                    // por si no existe el directorio
                    if (!Directory.Exists(assetPath))
                    {
                        Directory.CreateDirectory(assetPath);
                    }

                    assetPath += addItem;   // actualizo el directorio para que se cree ok
                    AssetDatabase.CreateAsset(item, assetPath);
                    AssetDatabase.SaveAssets();
                    list.Add(item);
                }
            }
            Debug.Log("Loaded achievements succesfully");
        }
        else
        {
            Debug.LogError("El archivo no existe en la ruta especificada: " + filePath);
        }
#endif
        return list;
    }
    
}
