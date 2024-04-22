//Author:ROZ

// AVISO A NAVEGANTES: para que se carguen los archivos del .csv, antes se debe de ejecutar en el editor. Desde la build directamente no funciona
// De ah� los if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    // para poder hacer input output con ficheros
using Unity.VisualScripting;


#if UNITY_EDITOR
    using UnityEditor;
#endif


public class ObjectLoader : MonoBehaviour
{
    // cuidadito con mostrar esto en el editor, se me bugue� y el valor mostrado era distinto al escrito aqu� y no compilaba
    
    //SHOP
    private string filePathGenerator = "Assets/Resources/generators.csv";   // ruta del archivo
    ShopItemScripteableObject generatorItem; // tipo de item a cargar

    //ACHIEVEMENTS
    private string filePathAchievement = "Assets/Resources/achievements.csv";   // ruta del archivo
    Achievement achievementItem; // tipo de item a cargar

    void Start()
    {
        /*
        LoadShop();
        LoadAchievements();
        */
    }
    
    void LoadShop()
    {
        
        #if UNITY_EDITOR
            //some code here that uses something from the UnityEditor namespace

            if (File.Exists(filePathGenerator))
            {
                using (StreamReader reader = new StreamReader(filePathGenerator))
                {
                    reader.ReadLine();  // siempre habr� al menos una l�nea, la que describe las columnas. para que se la salte y solo lea los datos, se pone esto
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(';');   // divido por cada ;
                    
                        // relleno el objeto con los datos pertinentes
                        generatorItem = ScriptableObject.CreateInstance<ShopItemScripteableObject>();
                        generatorItem.name = parts[0];
                        generatorItem.title = parts[0];
                        generatorItem.price = int.Parse(parts[1]);

                        // una vez creado el objeto, lo guardo en la ruta de los scriptableobjects para tienda
                        string assetPath = "Assets/Scripts/ShopTrueScript/ScripteableObjetc/";  // donde se va a guardar los items. cuidadin con donde se pone
                        string addItem = generatorItem.name + ".asset";  // para a�adir el item

                        // por si no existe el directorio
                        if (!Directory.Exists(assetPath))
                        {
                            Directory.CreateDirectory(assetPath);
                        }

                        assetPath += addItem;   // actualizo el directorio para que se cree ok
                        AssetDatabase.CreateAsset(generatorItem, assetPath);
                        AssetDatabase.SaveAssets();

                    }
                }
                Debug.Log("Loaded shop succesfully");
            }
            else
            {
                Debug.LogError("El archivo no existe en la ruta especificada: " + filePathGenerator);
            }
    #endif
        
    }
    

    //Author:Eduardo, modificaci�n de ObjectLoader.cs de Rub�n. Los comentarios del script original se mantendr�n en la mayor�a de lo posible
    //Mod:ROZ
    
    public List<Achievement> LoadAchievements()
    {
        List<Achievement> listAchievements = new List<Achievement>();
#if UNITY_EDITOR
        //some code here that uses something from the UnityEditor namespace

        if (File.Exists(filePathAchievement))
        {
            using (StreamReader reader = new StreamReader(filePathAchievement))
            {
                reader.ReadLine();  // siempre habr� al menos una l�nea, la que describe las columnas. para que se la salte y solo lea los datos, se pone esto
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(';');   // divido por cada ;

                    // relleno el objeto con los datos pertinentes
                    achievementItem = ScriptableObject.CreateInstance<Achievement>();
                    achievementItem.name = parts[0];
                    achievementItem.title = parts[0];
                    achievementItem.id = int.Parse(parts[1]);
                    achievementItem.description = parts[2];
                    achievementItem.condition = int.Parse(parts[3]);
                    achievementItem.unlocked = bool.Parse(parts[4]);

                    // una vez creado el objeto, lo guardo en la ruta de los scriptableobjects para tienda
                    string assetPath = "Assets/Scripts/AchievementSystem/Achievements/";  // donde se va a guardar los items. cuidadin con donde se pone
                    string addItem = achievementItem.name + ".asset";  // para a�adir el item

                    // por si no existe el directorio
                    if (!Directory.Exists(assetPath))
                    {
                        Directory.CreateDirectory(assetPath);
                    }

                    assetPath += addItem;   // actualizo el directorio para que se cree ok
                    AssetDatabase.CreateAsset(achievementItem, assetPath);
                    AssetDatabase.SaveAssets();
                    listAchievements.Add(achievementItem);
                }
            }
            Debug.Log("Loaded achievements succesfully");
        }
        else
        {
            Debug.LogError("El archivo no existe en la ruta especificada: " + filePathAchievement);
        }
#endif
        // cuidado, si el return est� despu�s del endif, la declaraci�n de la lista debe estar detr�s del if
        return listAchievements;
    }
    
    
}
