using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGeneratorManager : MonoBehaviour
{
    public static VisualGeneratorManager Instance;
    public Sprite transparente;
    public GameObject[] generatorPanelsSO;
    public VisualGeneratorTemplate[] generatorPanels;

    void Start()
    {// muestra solamente los paneles que hay en función de los scripteble objects que hemos creado
        for (int i = 0; i < generatorPanelsSO.Length; i++)
        {
            if (ShopManagerScript.Instance.shopItemsSO[i].unlocked == true && ShopManagerScript.Instance.shopItemsSO[i].amount > 0) // si no esta desbloqueado el objeto, no lo muestra
            {
                generatorPanelsSO[i].SetActive(true);
            }
        }
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
        int max;
        int max2 = 0;
        if (generatorPanels[id].generadoresImages.Length <= ShopManagerScript.Instance.shopItemsSO[id].amount)
        {
            max = generatorPanels[id].generadoresImages.Length;
        } else
        {
            max = ShopManagerScript.Instance.shopItemsSO[id].amount;
            max2 = generatorPanels[id].generadoresImages.Length- ShopManagerScript.Instance.shopItemsSO[id].amount;
        }
        if(max != 0)
        {
            for (int i = 0; i < max; i++)
            {
                generatorPanels[id].generadoresImages[i].sprite = ShopManagerScript.Instance.shopItemsSO[id].sprite; // asigna a la cantidad de objetos que haya el sprite del objeto
            }
            if (max2 != 0)
            {
                for (int i = max + 1; i < max2; i++)
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
    public void loadPanels()
    {
        for (int i = 0; i < generatorPanelsSO.Length; i++)
        {
            if (ShopManagerScript.Instance.shopItemsSO[i].unlocked == true && ShopManagerScript.Instance.shopItemsSO[i].amount > 0) // si no esta desbloqueado el objeto, no lo muestra
            {
                generatorPanelsSO[i].SetActive(true);
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
        }
        loadPanels();
    }
}
