//Author:ROZ

// AVISO A NAVEGANTES: para que se carguen los archivos del .csv, antes se debe de ejecutar en el editor. Desde la build directamente no funciona
// De ah� los if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    // para poder hacer input output con ficheros
using System.Text;  //
using Unity.VisualScripting;


#if UNITY_EDITOR
    using UnityEditor;
#endif


public class ObjectLoader : MonoBehaviour
{
    // cuidadito con mostrar esto en el editor, se me bugue� y el valor mostrado era distinto al escrito aqu� y no compilaba
    
    //SHOP
    private string filePathGenerator = "Assets/Resources/generators_default-toload.txt";   // ruta del archivo
    ShopItemScripteableObject generatorItem; // tipo de item a cargar

    //ACHIEVEMENTS
    private string filePathAchievement = "Assets/Resources/achievements_default.csv";   // ruta del archivo
    Achievement achievementItem; // tipo de item a cargar

    void Start()
    {
        // DESCOMENTA LO QUE QUIERAS CARGAR PARA CARGAR LOS VALORES DEFAULT DE DICHA COSA
        // COM�NTALO DESPU�S. SOLO QUEREMOS QUE SE CARGUE 1 VEZ

        //LoadShop();
        //LoadAchievements();
        
    }

    /* El warning que da de 
    The character with Unicode value \u0085 was not found in the [Inter-Regular SDF] font asset or any potential fallbacks. It was replaced by Unicode character \u25A1.
    UnityEngine.GUIUtility:ProcessEvent(int, intptr, bool&)

    Es que Unity no sabe interpretar con ese encoding el caracter de "Next Line", el del intro, y lo reemplaza por un caracter vac�o
    */

    void LoadShop()
    {
        
        #if UNITY_EDITOR
            //some code here that uses something from the UnityEditor namespace

            if (File.Exists(filePathGenerator))
            {
                using (StreamReader reader = new StreamReader(filePathGenerator, Encoding.GetEncoding("ISO-8859-1")))
                {
                    reader.ReadLine();  // siempre habr� al menos una l�nea, la que describe las columnas. para que se la salte y solo lea los datos, se pone esto
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(';');   // divido por cada ;
                    
                        // relleno el objeto con los datos pertinentes
                        generatorItem = ScriptableObject.CreateInstance<ShopItemScripteableObject>();
                        generatorItem.id = int.Parse(parts[0]);
                        generatorItem.name = generatorItem.id + "-" + parts[1];
                        generatorItem.title = parts[1];
                        generatorItem.description = parts[2];
                        generatorItem.price = double.Parse(parts[3]);
                        generatorItem.basePrice = double.Parse(parts[3]);
                        generatorItem.amount    = int.Parse(parts[5]);
                        generatorItem.pointsPerSecond = double.Parse(parts[6]);
                        generatorItem.unlocked = bool.Parse(parts[7]);

                        // una vez creado el objeto, lo guardo en la ruta de los scriptableobjects para tienda
                        string assetPath = "Assets/Scripts/Shop/ScripteableObjetc/test/";  // donde se va a guardar los items. cuidadin con donde se pone
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
                    achievementItem.id = int.Parse(parts[0]);
                    achievementItem.name = parts[1];
                    achievementItem.title = parts[1];
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
