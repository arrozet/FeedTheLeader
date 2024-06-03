// CODIGO COMPLETAMENTE ILEGIBLE,NO TRATES DE LEERLO SIMPLEMENTE FUNCIONA ESTA HECHO A LA PRISA
// Author: juanma, obviamente, como el 80% del código
//using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGeneratorManager : MonoBehaviour
{
    public static VisualGeneratorManager Instance;
    public Sprite transparente;
    public GameObject[] generatorPanelsSO;
    public VisualGeneratorTemplate[] generatorPanels;
    public int[] amounts;

    void Start()
    {// muestra solamente los paneles que hay en función de los scripteble objects que hemos creado
        for (int i = 0; i < generatorPanelsSO.Length; i++)
        {
            if (ShopManagerScript.Instance.shopItemsSO[i].unlocked == true && ShopManagerScript.Instance.shopItemsSO[i].amount > 0) // si no esta desbloqueado el objeto, no lo muestra
            {
                generatorPanelsSO[i].SetActive(true);
            }
        }
        amounts = new int[30];
        loadPanels();
    }
    public void Awake()
    {
        // esto es de un tutorial, es para que no destruya el objeto entre escenas
        if (VisualGeneratorManager.Instance == null)
        {
            VisualGeneratorManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void activateImagesID(int id) // esta funcion activa las imagenes que haya y las demas las pone transparentes
    {
       
        if(amounts[id] != 0)
        {
            for (int i = 0; i < amounts[id]; i++)
            {
                generatorPanels[id].generadoresImages[i].sprite = ShopManagerScript.Instance.shopItemsSO[id].spriteList[UnityEngine.Random.Range(0, 4)]; // asigna a la cantidad de objetos que haya el sprite del objeto
            }
            if (amounts[id] < generatorPanels[id].generadoresImages.Length)
            {
                for (int i = amounts[id] + 1; i < generatorPanels[id].generadoresImages.Length; i++)
                {
                    generatorPanels[id].generadoresImages[i].sprite = transparente;
                }
            }
        } else
        {
            for (int i = 0; i < generatorPanels[id].generadoresImages.Length; i++)
            {
                generatorPanels[id].generadoresImages[i].sprite = transparente;
            }
        }
        

        
    }
    public void seHaComprado(int id, int cantidad)
    {
        for (int i = 0; i < generatorPanelsSO.Length; i++)
        {
            if (ShopManagerScript.Instance.shopItemsSO[i].unlocked == true && ShopManagerScript.Instance.shopItemsSO[i].amount > 0) // si no esta desbloqueado el objeto, no lo muestra
            {
                generatorPanelsSO[i].SetActive(true);
            }
        }
        if (amounts[id] < generatorPanels[id].generadoresImages.Length) { 
            if (cantidad == 1)
            {
                generatorPanels[id].generadoresImages[amounts[id]].sprite = ShopManagerScript.Instance.shopItemsSO[id].spriteList[UnityEngine.Random.Range(0, 4)];
                amounts[id]++;
            }
            else if (cantidad == 10)
            {
                for (int i = 0; i < generatorPanels[id].generadoresImages.Length; i++)
                {
                    generatorPanels[id].generadoresImages[i].sprite = ShopManagerScript.Instance.shopItemsSO[id].spriteList[UnityEngine.Random.Range(0, 4)]; // asigna a la cantidad de objetos que haya el sprite del objeto
               }
            }
        }
    }
    public void loadPanels()
    {
        for (int i = 0; i < generatorPanelsSO.Length; i++)
        {
            if (ShopManagerScript.Instance.shopItemsSO[i].unlocked == true && ShopManagerScript.Instance.shopItemsSO[i].amount > 0) // si no esta desbloqueado el objeto, no lo muestra
            {
                generatorPanelsSO[i].SetActive(true);
            }
            if (ShopManagerScript.Instance.shopItemsSO[i].amount < generatorPanels[i].generadoresImages.Length)
            {
                amounts[i] = ShopManagerScript.Instance.shopItemsSO[i].amount; 
            } else
            {
                amounts[i] = generatorPanels[i].generadoresImages.Length;
            }
            
        }
       
        for (int i =0 ; i < generatorPanelsSO.Length; i++)
        {
            if (ShopManagerScript.Instance.shopItemsSO[i].unlocked == true) // si no esta desbloqueado el objeto, no lo muestra
            {
                activateImagesID(i);
            }
        }
    }
    public void resetVisualGenerators()
    {
        for (int i = 0; i < generatorPanels.Length; i++)
        { 
            generatorPanelsSO[i].SetActive(false);
            amounts[i] = 0;
            for (int j = 0; j < generatorPanels[i].generadoresImages.Length; j++)
            {
                generatorPanels[i].generadoresImages[j].sprite = transparente;
            }
        }
        loadPanels();
    }
}
