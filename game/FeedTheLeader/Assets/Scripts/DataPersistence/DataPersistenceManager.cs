//Autor: Edu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.ComponentModel;
public class DataPersistenceManager : MonoBehaviour

{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    //se pueden obtener los datos publicamente, solo se podr�n modificar dentro de la clase
    public static DataPersistenceManager instance {  get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Guarda en un path por defecto
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = findAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //CARGAR DATOS USANDO DATA HANDLER
        this.gameData = dataHandler.Load();

        if(this.gameData == null)
        {
            Debug.Log("No se encontraron datos. Inicializando datos por defecto.");
            NewGame();
        }
        //POR HACER: ENVIAR DATOS A TODOS LOS SCRIPTS QUE LOS USEN
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("Loaded current score: " + gameData.currentScore);
    }
    
    public void SaveGame()
    {
        //POR HACER: ENVIAR DATOS A LOS SCRIPTS PARA QUE ACTUALICEN
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        Debug.Log("Saved current score: " + gameData.currentScore);
        //GUARDAR DATOS EN UN ARCHIVO .JSON CON EL DATA HANDLER
        dataHandler.Save(gameData);

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> findAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}