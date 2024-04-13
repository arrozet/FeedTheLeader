//Autor: Juanma

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public ShopItemScripteableObject[] shopItemsSO;
    public GameObject[] shopPanelsSO;
    public TemplateShop[] shopPanels;
    public Button[] myPurchaseBtns;
    // Start is called before the first frame update
    void Start()
    {// muestra solamente los paneles que hay en función de los scripteble objects que hemos creado
        for(int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsSO[i].SetActive(true);
        }

        loadPanels();
        CheckPurchaseable();
    }

    // Update is called once per frame
    void Update()
    {
        // esto es super poco eficiente porque lo ejecuta todo el rato, voy a buscar la manera de invocarla desde otro script
        CheckPurchaseable();
        // a ver en verdad, la cosa esque si lo ponemos cada vez que sume o reste puntos, como va a llegar un punto en el que se cosigan muchisimos, diria que da igual dejarlo así
    }
    public void CheckPurchaseable() // esto hace que no se pueda pulsar el boton 
        // si no tienes suficiente dinero para comprar el objeto
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (PointsManager.Instance.getPuntos() >= shopItemsSO[i].price)
            {
                myPurchaseBtns[i].interactable = true;
            }
            else
            {
                myPurchaseBtns[i].interactable = false;
            }
        }
    }
    public void PurchaseItem(int btnNo) // simplemente resta, y comporueba que boton se ha pulsado para poder restarle el precio y dar el efecto a dicho boton
    {
        if (PointsManager.Instance.getPuntos() >= shopItemsSO[btnNo].price)
        {
            PointsManager.Instance.RestarPuntos(shopItemsSO[btnNo].price);
            efecto(btnNo);
        }   
    }


    public void loadPanels() // esto carga los paneles:
        // realmente lo que tengo es una lista de paneles ocultos (que se activan con el primer for del STart())
        // esta función asigna a cada panel, el nombre y el objeto de los Scripteable Objects que tenemos
    {
        for (int i = 0; i <shopItemsSO.Length; i++)
        {
            shopPanels[i].titleText.text = shopItemsSO[i].title;
            shopPanels[i].priceText.text = shopItemsSO[i].price.ToString();
        }
    }
    public void efecto(int btnNo) // cada boton tiene un efecto
    {
        if(btnNo == 0)
        {
            PointsManager.Instance.multiplicarMultiplicador(2);
        } else if(btnNo == 1)
        {
            PointsManager.Instance.multiplicarMultiplicador(3);
        }
    }
}
