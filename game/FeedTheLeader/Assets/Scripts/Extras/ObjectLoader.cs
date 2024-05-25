//Author:ROZ

// AVISO A NAVEGANTES: para que se carguen los archivos del .csv, antes se debe de ejecutar en el editor. Desde la build directamente no funciona
// De ahí los if UNITY_EDITOR

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
    // cuidadito con mostrar esto en el editor, se me bugueó y el valor mostrado era distinto al escrito aquí y no compilaba
    
    //SHOP
    private string filePathGenerator = "Assets/Resources/generators_default-toload.txt";   // ruta del archivo
    ShopItemScripteableObject generatorItem; // tipo de item a cargar
    private string imagePathGenerator = "Images/Generators/toLoadShop/";    // ruta relativa a resources

    //ACHIEVEMENTS
    private string filePathAchievement = "Assets/Resources/achievements_default-toload.txt";   // ruta del archivo
    Achievement achievementItem; // tipo de item a cargar
    private string imagePathAchievements = "Images/Achievements/toLoadAchievements/";    // ruta relativa a resources

    //POWERUPS
    private string filePathPowerup = "Assets/Resources/power-ups_default-toload.txt";   // ruta del archivo
    PowerUpSO powerupItem; // tipo de item a cargar
    private string imagePathPowerup = "Images/PowerUps/toLoadPowerUps/mejora/";    // ruta relativa a resources
    private string marcoPathPowerup = "Images/PowerUps/toLoadPowerUps/marco/";    // ruta relativa a resources

    void Start()
    {
        // DESCOMENTA LO QUE QUIERAS CARGAR PARA CARGAR LOS VALORES DEFAULT DE DICHA COSA
        // COMÉNTALO DESPUÉS. SOLO QUEREMOS QUE SE CARGUE 1 VEZ

        //LoadShop();
        //LoadAchievements();
        //LoadPowerups();
        
    }

    /* El warning que da de 
    The character with Unicode value \u0085 was not found in the [Inter-Regular SDF] font asset or any potential fallbacks. It was replaced by Unicode character \u25A1.
    UnityEngine.GUIUtility:ProcessEvent(int, intptr, bool&)

    Es que Unity no sabe interpretar con ese encoding el caracter de "Next Line", el del intro, y lo reemplaza por un caracter vacío
    */

    void LoadShop()
    {
        
        #if UNITY_EDITOR
            //some code here that uses something from the UnityEditor namespace

            if (File.Exists(filePathGenerator))
            {
                using (StreamReader reader = new StreamReader(filePathGenerator, Encoding.GetEncoding("ISO-8859-1")))
                {
                    reader.ReadLine();  // siempre habrá al menos una línea, la que describe las columnas. para que se la salte y solo lea los datos, se pone esto
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

                        // Cojo el sprite de resources y lo cargo. No se debe poner la extensión
                        string image = imagePathGenerator + generatorItem.name;
                        generatorItem.sprite = Resources.Load<Sprite>(image);

                        // una vez creado el objeto, lo guardo en la ruta de los scriptableobjects para tienda
                        string assetPath = "Assets/Scripts/Shop/ShopSO/";  // donde se va a guardar los items. cuidadin con donde se pone
                        string addItem = generatorItem.name + ".asset";  // para añadir el item

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


    //Author:Eduardo, modificación de ObjectLoader.cs de Rubén. Los comentarios del script original se mantendrán en la mayoría de lo posible
    //Mod:ROZ

    void LoadAchievements()
    {
#if UNITY_EDITOR
        //some code here that uses something from the UnityEditor namespace

        if (File.Exists(filePathAchievement))
        {
            using (StreamReader reader = new StreamReader(filePathAchievement, Encoding.GetEncoding("ISO-8859-1")))
            {
                reader.ReadLine();  // siempre habrá al menos una línea, la que describe las columnas. para que se la salte y solo lea los datos, se pone esto
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(';');   // divido por cada ;

                    // relleno el objeto con los datos pertinentes
                    achievementItem = ScriptableObject.CreateInstance<Achievement>();
                    achievementItem.id = int.Parse(parts[0]);

                    // Tenía problemas con algunos caracteres
                    if (parts[1].Contains("Uy,"))   // '¿', '?' son caracteres inválidos para poner en un nombre de archivo
                    {
                        achievementItem.name = achievementItem.id + "-" + "uy, y esto";
                    }
                    else if (parts[1].Contains("Barba"))// '<' son caracteres inválidos para poner en un nombre de archivo
                    {
                        achievementItem.name = achievementItem.id + "-" + "I love Barba";
                    }
                    else
                    {
                        achievementItem.name = achievementItem.id + "-" + parts[1];
                    }

                    achievementItem.title = parts[1];
                    achievementItem.description = parts[2];
                    achievementItem.type = parts[3];
                    achievementItem.condition = double.Parse(parts[4]);
                    achievementItem.unlocked = bool.Parse(parts[5]);

                    // Cojo el sprite de resources y lo cargo. No se debe poner la 
                    string image = imagePathAchievements + achievementItem.id.ToString();   // esta vez lo hago con id porque es muy tedioso hacerlo con los nombres en este caso
                    achievementItem.sprite = Resources.Load<Sprite>(image);
                    achievementItem.NotUnlockedSprite = Resources.Load<Sprite>(imagePathAchievements + "locked");

                    // una vez creado el objeto, lo guardo en la ruta de los scriptableobjects para tienda
                    string assetPath = "Assets/Scripts/AchievementSystem/Achievements/";  // donde se va a guardar los items. cuidadin con donde se pone
                    string addItem = achievementItem.name + ".asset";  // para añadir el item

                    // por si no existe el directorio
                    if (!Directory.Exists(assetPath))
                    {
                        Directory.CreateDirectory(assetPath);
                    }

                    assetPath += addItem;   // actualizo el directorio para que se cree ok
                    AssetDatabase.CreateAsset(achievementItem, assetPath);
                    AssetDatabase.SaveAssets();
                    //listAchievements.Add(achievementItem);
                }
            }
            Debug.Log("Loaded achievements succesfully");
        }
        else
        {
            Debug.LogError("El archivo no existe en la ruta especificada: " + filePathAchievement);
        }
#endif
        // cuidado, si el return está después del endif, la declaración de la lista debe estar detrás del if
        //return listAchievements;
    }


    void LoadPowerups()
    {
#if UNITY_EDITOR
        //some code here that uses something from the UnityEditor namespace

        if (File.Exists(filePathPowerup))
        {
            Debug.Log("FIle exists");
            using (StreamReader reader = new StreamReader(filePathPowerup, Encoding.GetEncoding("ISO-8859-1")))
            {
                reader.ReadLine();  // siempre habrá al menos una línea, la que describe las columnas. para que se la salte y solo lea los datos, se pone esto
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(';');   // divido por cada ;

                    Debug.Log(line);
                    // relleno el objeto con los datos pertinentes
                    powerupItem = ScriptableObject.CreateInstance<PowerUpSO>();
                    powerupItem.id = int.Parse(parts[0]);
                    powerupItem.level = int.Parse(parts[1]);
                    powerupItem.name = powerupItem.id + "-" + parts[2];
                    powerupItem.title = parts[2];
                    powerupItem.desc = parts[3];
                    powerupItem.effect = double.Parse(parts[4]);
                    powerupItem.price = double.Parse(parts[5]);
                    powerupItem.unlocked = bool.Parse(parts[6]);
                    powerupItem.bought = bool.Parse(parts[7]);
                    powerupItem.type = parts[8];

                    

                    // Cojo el sprite de resources y lo cargo. No se debe poner la extensión
                    string image = imagePathPowerup + parts[9]; // el nombre del sprite está guardado en parts[8]
                    powerupItem.sprite = Resources.Load<Sprite>(image);
                    powerupItem.frame = Resources.Load<Sprite>(marcoPathPowerup + powerupItem.level);

                    powerupItem.generator = int.Parse(parts[10]);

                    // una vez creado el objeto, lo guardo en la ruta de los scriptableobjects para tienda
                    string assetPath = "Assets/Scripts/Shop/PowerUpSO/";  // donde se va a guardar los items. cuidadin con donde se pone
                    string addItem = powerupItem.name + ".asset";  // para añadir el item

                    // por si no existe el directorio
                    if (!Directory.Exists(assetPath))
                    {
                        Directory.CreateDirectory(assetPath);
                    }

                    assetPath += addItem;   // actualizo el directorio para que se cree ok
                    AssetDatabase.CreateAsset(powerupItem, assetPath);
                    AssetDatabase.SaveAssets();

                }
            }
            Debug.Log("Loaded powerups succesfully");
        }
        else
        {
            Debug.LogError("El archivo no existe en la ruta especificada: " + filePathPowerup);
        }
#endif
    }


}
